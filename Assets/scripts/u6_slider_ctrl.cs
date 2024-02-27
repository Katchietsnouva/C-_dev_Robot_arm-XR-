using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using Unity.Mathematics;
//server interactions //server dot net
using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Text;
using System.Net.NetworkInformation;
using Newtonsoft.Json;
using System.IO.Pipes;

    
public class u6_slider_ctrl : MonoBehaviour
{
    // SERVER related interactions
    private const string pipeName = "UnityToResponderPipe";
    private bool isNetworkingEnabled = false;
    private bool isServer = false;
    private int serverPort = 12345;
    private UdpClient udpServer; 
    private NamedPipeServerStream pipeServer;
    private StreamWriter pipeStreamWriter;
    [SerializeField] private Button button_EnableNetworking;
    [SerializeField] private Button button_SetMode;
    // private NamedPipeClientStream pipeClient;
    // private StreamReader pipeStreamReader;


    // private TcpListener tcpListener;
    // private TcpClient tcpClient;
    // private string serverIPAddress = "192.168.0.100"; // Replace with your actual server IP address

    // for android 
    private string rootFolderName = "robot_app";
    private string subFolderName = "user_data";
    private string fileName = "Keyframes.txt";
    // public static u6_slider_ctrl Instance { get; private set; }
    private string keyframesFilePath_to_txt;
    private List<RobotKeyframe> keyframes = new List<RobotKeyframe>();
    private bool isRecording = false;
    private bool isPlaybackPaused = false;
    private Slider slider1;
    private Slider slider2;
    private Slider slider3;
    private Slider slider4;
    private Slider slider5;
    private Slider slider6;

    [SerializeField] private GameObject parentJoint_1_Box;
    [SerializeField] private GameObject childJoint_2_Box;
    [SerializeField] private GameObject childJoint_3_Box;
    [SerializeField] private GameObject childJoint_4_Box;
    [SerializeField] private GameObject childJoint_5_Box;
    [SerializeField] private GameObject endEffJoint_6_Box;
    private float[] initialSliderValues = { 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 0.9f };

    private float childJoint_2_OffsetX = (float)(-0.0754 / 1.0);
    private float childJoint_2_OffsetY = (float)(0.1124 / 1.0);
    private float childJoint_3_OffsetY = (float)(0.2858 / 1.0);
    private float childJoint_3_OffsetZ = (float)(0.053 / 1.0);
    private float childJoint_4_OffsetX = (float)(0.068 / 1.0);
    private float childJoint_4_OffsetY = (float)(-0.171 / 1.0);
    private float childJoint_4_OffsetZ = (float)(0.08 / 1.0);
    private float childJoint_5_OffsetY = (float)(-.172 / 1.0);
    private float endEffJoint_6_OffsetY = (float)(-0.07 / 1.0);
    private float endEffJoint_6_OffsetZ = (float)(0.075 / 1.0);

    [SerializeField] private Button button_1;
    // public Text button_1_Text;
    // public Text button_1_Text_T;// Reference to the Text component of button
    private Image button_1_Image;// Reference to the Image component of button
    [SerializeField] private Button button_2;
    private Image button_2_Image;
    [SerializeField] private Button button_3;
    private Image button_3_Image;
    [SerializeField] private Button button_4;

    private Color initialColor = Color.white;
    private Color recordingColor = Color.green;
    private Color initialColorBG = Color.grey;
    private Color recordingColorBG = Color.black;

    // private Color initialColor = Color.white;  // Set the initial color to white
    private ColorBlock buttonColors;

    void Start()
    {
        // for android 
        keyframesFilePath_to_txt = GetFilePath();
        // for windows 
        // keyframesFilePath_to_txt = Application.dataPath + "/Keyframes.txt";
        // Instance = this;
        slider1 = GameObject.Find("Slider1").GetComponent<Slider>();
        slider2 = GameObject.Find("Slider2").GetComponent<Slider>();
        slider3 = GameObject.Find("Slider3").GetComponent<Slider>();
        slider4 = GameObject.Find("Slider4").GetComponent<Slider>();
        slider5 = GameObject.Find("Slider5").GetComponent<Slider>();
        slider6 = GameObject.Find("Slider6").GetComponent<Slider>();

        for (int i = 0; i < Mathf.Min(initialSliderValues.Length, 6); i++)
        {
            SetSliderValue(GetSliderByIndex(i + 1), initialSliderValues[i]);
        }

        button_1 = GameObject.Find("Button").GetComponent<Button>();
        button_1_Image = button_1.GetComponent<Image>();
        // button_1_Text_T = button_1.GetComponent<Text>();
        // Attach the listener to the button1 click event
        // button.onClick.AddListener(ToggleRecording); 
        button_2 = GameObject.Find("Button2").GetComponent<Button>();
        button_2_Image = button_2.GetComponent<Image>();
        // Attach the listener to the button2 click event
        // button_2.onClick.AddListener(StartPlayback); 
        button_3 = GameObject.Find("Button3").GetComponent<Button>();
        button_3_Image = button_3.GetComponent<Image>();
        button_4 = GameObject.Find("Button4").GetComponent<Button>();

         // button_EnableNetworking.onClick.AddListener(ToggleNetworking);
        // button_SetMode.onClick.AddListener(ToggleClientServerMode);
        // StartNetworking();

        // SERVER realated interactions
        button_EnableNetworking = GameObject.Find("Button_EnableNetworking").GetComponent<Button>();
        button_SetMode = GameObject.Find("Button_SetMode").GetComponent<Button>();
        //  pipe-related objects
        // pipeServer = new NamedPipeServerStream(pipeName, PipeDirection.Out);
        // pipeStreamWriter = new StreamWriter(pipeServer);
        // pipeClient = new NamedPipeClientStream(".", pipeName, PipeDirection.In);
        // pipeStreamReader = new StreamReader(pipeClient);

        StartCoroutine(NetworkingCoroutine());
        // StartCoroutine(ReceiveMessagesCoroutine());
    }
    private void OnDestroy()
    {
        // Clean up resources when the GameObject is destroyed
        if (udpServer != null)
        {
            udpServer.Close();
        }

        if (pipeServer != null)
        {
            if (pipeServer.IsConnected)
            {
                pipeServer.Disconnect();
            }
            pipeServer.Close();
        }

        if (pipeServer != null)
        {
            pipeServer.Disconnect();
            pipeServer.Close();
        }
        // if (pipeClient != null)
        // {
        //     pipeClient.Close();
        // }
    }
    
    
    private IEnumerator NetworkingCoroutine()
    {
        while (true)
        {
            if (IsNetworkingEnabled())
            {
                Debug.Log( "Net status " );
                if (isServerServerEnabled())
                {
                    // StartServer();
                    yield return StartCoroutine(StartServerCoroutine());
                    Debug.Log("Server Mode");
                }
                if (!isServerServerEnabled())
                {
                    // Implement client logic here if needed
                    StopServer();
                    Debug.Log("Client Mode");
                }
                else{
                    Debug.Log("some isServerServerEnabled error ");
                }
            }
            if (!IsNetworkingEnabled())
            {            
                StopNetworking();
            }
            yield return new WaitForSeconds(1f); // Adjust the interval as needed
        }
       
    }
    // Linked to button_EnableNetworking
    public void ToggleNetworking()
    {
        bool networkStateOff = !IsNetworkingEnabled();
        button_EnableNetworking.GetComponent<Image>().color = networkStateOff ? Color.white : Color.green;
        Debug.Log("Network State: " + IsNetworkingEnabled() + ", Server State: " + isServerServerEnabled());
    }
    private bool IsNetworkingEnabled()
    {
        return button_EnableNetworking.GetComponent<Image>().color == Color.green;
    }
    public void ToggleClientServerMode()
    {
        bool isNotServer = !isServerServerEnabled(); // Toggle server mode
        button_SetMode.GetComponent<Image>().color = isNotServer ? Color.blue : Color.red;
        Debug.Log("Network State: " + IsNetworkingEnabled() + ", Server State: " + isServerServerEnabled());
        if (isServerServerEnabled())
        {   Debug.Log("Server Mode toggler");
            // StartServerCoroutine();
        }
        else
        {   Debug.Log("Client Mode toggler");
            // SetClientMode();
        }
    }
    private bool isServerServerEnabled()
    {
        return button_SetMode.GetComponent<Image>().color == Color.red;
    }



    private IEnumerator StartServerCoroutine()
    {
        try{
            if (udpServer == null)
            {
                udpServer = new UdpClient(serverPort);
                udpServer.EnableBroadcast = true;
            }
            if (pipeServer ==null)
            {
                pipeServer = new NamedPipeServerStream(pipeName, PipeDirection.Out);
                pipeStreamWriter = new StreamWriter(pipeServer);
            }

            // Broadcast address and port
            IPAddress networkBroadcastAddress = IPAddress.Parse("192.168.0.255"); // Replace with your network's broadcast address
            int networkBroadcastPort = 12345;

            // Send a broadcast message
            string broadcastMessage = "DISCOVER";
            byte[] bytes = Encoding.ASCII.GetBytes(broadcastMessage);
            udpServer.Send(bytes, bytes.Length, new IPEndPoint(networkBroadcastAddress, networkBroadcastPort));
            
            if (pipeServer != null && pipeServer.IsConnected)
            {
                pipeStreamWriter.WriteLine(broadcastMessage);
                pipeStreamWriter.Flush(); 
            }

            Debug.Log($"Broadcast messages '{broadcastMessage}' sent to {networkBroadcastAddress}:{networkBroadcastPort}");
        }
        catch (Exception ex)
        {
            Debug.LogError($"Error starting server: {ex.Message}");
        }
        yield return null; 
    }


    private void ListenForResponses()
    {
        try
        {
            // UdpClient udpServer = new UdpClient(serverPort);
            IPEndPoint remoteEndPoint = new IPEndPoint(IPAddress.Any, 0);

            while (udpServer.Available > 0)
            {
                byte[] responseBytes = udpServer.Receive(ref remoteEndPoint);
                string responseMessage = Encoding.ASCII.GetString(responseBytes);

                // Handle the received response (e.g., print to console)
                Debug.Log($"Received response from {remoteEndPoint.Address}: {responseMessage}");
            }
        }
        catch (Exception ex)
        {
            Debug.LogError($"Error listening for responses: {ex.Message}");
        }
    }
    
    // SERVER realated interactions
    private void StopNetworking()
    {
        StopServer();
    }

    private void StopServer()
    {
        if (udpServer != null)
        {
            udpServer.Close();
            udpServer = null;
        }

        if (pipeServer != null)
        {
            if (pipeServer.IsConnected)
            {
                pipeServer.Disconnect();
            }
            pipeServer.Close();
            pipeServer = null;
        }
    }

    
    private void SetClientMode()
    {
    //     tcpClient = new TcpClient();
    //     tcpClient.Connect(serverIPAddress, port);
    //     // Optionally, start a thread to listen for data from the server
    //     new Thread(() => { ListenForServerData(tcpClient); }).Start();
    }


    private void ListenForClients()
    {
        while (true)
        {
            // TcpClient client = tcpListener.AcceptTcpClient();
            // new Thread(() => { HandleClient(client); }).Start();
            // Debug.Log("Client connected: " + ((IPEndPoint)client.Client.RemoteEndPoint).Address.ToString());
            // Example: Update a UI Text element with server information
            // serverStatusText.text = "Server Status: Connected";
        }
    }


    private void HandleClient(object obj)
    {
        TcpClient client = (TcpClient)obj;
        NetworkStream clientStream = client.GetStream();
        // Implement server logic for receiving data from the client
        // Example: Read data from clientStream and update sliders
        // You can call a method like UpdateSlidersFromNetworkData(data) to handle the received data
    }

    // Optional: Listen for data from the server if this instance is a client
    private void ListenForServerData(TcpClient client)
    {
        NetworkStream clientStream = client.GetStream();
        StreamReader reader = new StreamReader(clientStream);

        while (true)
        {
            try
            {
                string data = reader.ReadLine();
                if (data != null)
                {
                    Debug.Log("Received data from server: " + data);
                    // Handle the received data, e.g., UpdateSlidersFromNetworkData(data)
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions
                Debug.LogError("Error while reading data from server: " + ex.Message);
                break;
            }
        }
    }

    // private IEnumerator ReceiveMessagesCoroutine()
    // {
    //     while (true)
    //     {
    //         if (isNetworkingEnabled)
    //         {
    //             if (!isServer)
    //             {
    //                 // Implement client logic here if needed
    //                 if (pipeClient.IsConnected)
    //                 {
    //                     string receivedMessage = pipeStreamReader.ReadLine();
    //                     Debug.Log($"Received message from Unity: {receivedMessage}");
    //                 }
    //             }
    //         }
    //         yield return null; // Adjust the interval as needed
    //     }
    // }

    // Optional: Implement a method to update sliders based on received network data
    private void UpdateSlidersFromNetworkData(string data)
    {
        // Parse the data and update sliders accordingly
        // Example: Split data into slider values and update sliders
        string[] sliderValues = data.Split(' ');
        if (sliderValues.Length == 6)
        {
            for (int i = 0; i < sliderValues.Length; i++)
            {
                float sliderValue = float.Parse(sliderValues[i]);
                SetSliderValue(GetSliderByIndex(i + 1), sliderValue);
            }

            // Call AlterJoints or any other method to update the robot arm
            AlterJoints();
        }
    }



    private void SetSliderValue(Slider slider, float value)
    {
        slider.value = value;
        AlterJoints(); // Or any other necessary actions when altering a slider
    }
    private Slider GetSliderByIndex(int index)
    {
        switch (index)
        {
            case 1: return slider1;
            case 2: return slider2;
            case 3: return slider3;
            case 4: return slider4;
            case 5: return slider5;
            case 6: return slider6;
            default:
                Debug.LogError("Invalid slider index");
                return null;
        }
    }
    public void ResetToInitialValues()
    {
        Start(); // Call the Start function to reset to initial values
    }

    void Update()
    {
        if (isRecording)
        {
            RecordKeyframe();
        }
        if (!isRecording && keyframes.Count > 0)
        {
            //for windows
            SaveKeyframesTo_txt_File();
            //for android
            SaveKeyframesToFile();
        }
        // server related code
        if (!CheckIfServer())
        {
            // Implement client logic here
            // Read slider changes and send them to the server
            // Example: Send slider values to the server
        }
    }
    // server related code
    private bool CheckIfServer()
    {
        // Implement logic to check if the device should act as a server
        // Return true for server, false for client
        // Example: Use a tickle button or any other input method
        return false; // Change this based on your logic
    }


    public void AlterJoints()
    {
        float rotationValue1 = slider1.value * 360f;
        float rotationValue2 = slider2.value * 360f;
        float rotationValue3 = slider3.value * 360f;
        float rotationValue4 = slider4.value * 360f;
        float rotationValue5 = slider5.value * 360f;
        float rotationValue6 = slider6.value * 360f;
        // Set the parent's rotation
        if (slider1.value > 0f)
        {
            AlterJointWithVariablesfrom_Joint_1(rotationValue1, parentJoint_1_Box, Vector3.zero);
            // Set the childJoint_2_Box's parent to parentJoint_1_Box
            SetParentAndAlterJointWithVariables(childJoint_2_Box, parentJoint_1_Box, childJoint_2_OffsetX, childJoint_2_OffsetY, 0f);
            // Set the childJoint_3_Box's parent to childJoint_2_Box
            SetParentAndAlterJointWithVariables(childJoint_3_Box, childJoint_2_Box, 0f, childJoint_3_OffsetY, childJoint_3_OffsetZ);
            // Set the childJoint_4_Box's parent to childJoint_3_Box
            SetParentAndAlterJointWithVariables(childJoint_4_Box, childJoint_3_Box, childJoint_4_OffsetX, childJoint_4_OffsetY, childJoint_4_OffsetZ);
            // Set the childJoint_5_Box's parent to childJoint_4_Box
            SetParentAndAlterJointWithVariables(childJoint_5_Box, childJoint_4_Box, 0f, childJoint_5_OffsetY, 0f);
            // Set the endEffJoint_6_Box's parent to childJoint_5_Box
            SetParentAndAlterJointWithVariables(endEffJoint_6_Box, childJoint_5_Box, 0f, endEffJoint_6_OffsetY, endEffJoint_6_OffsetZ);
        }
        if (slider2.value > 0f)
        {
            // AlterJointWithVariablesfromJoint2(rotationValue2, childJoint_2_Box, Vector3.zero);
            // AlterJointWithVariablesfromJoint2(rotationValue2, childJoint_2_Box, new Vector3(0f, 0f, 0f));
            AlterJointWithVariablesfrom_Joint_2_AND_3_AND_5(rotationValue2, childJoint_2_Box, new Vector3(childJoint_2_OffsetX, childJoint_2_OffsetY, 0f));
            // Set the childJoint_3_Box's parent to childJoint_2_Box
            SetParentAndAlterJointWithVariables(childJoint_3_Box, childJoint_2_Box, 0f, childJoint_3_OffsetY, childJoint_3_OffsetZ);
            // Set the childJoint_4_Box's parent to childJoint_3_Box
            SetParentAndAlterJointWithVariables(childJoint_4_Box, childJoint_3_Box, childJoint_4_OffsetX, childJoint_4_OffsetY, childJoint_4_OffsetZ);
            // Set the childJoint_5_Box's parent to childJoint_4_Box
            SetParentAndAlterJointWithVariables(childJoint_5_Box, childJoint_4_Box, 0f, childJoint_5_OffsetY, 0f);
            // Set the endEffJoint_6_Box's parent to childJoint_5_Box
            SetParentAndAlterJointWithVariables(endEffJoint_6_Box, childJoint_5_Box, 0f, endEffJoint_6_OffsetY, endEffJoint_6_OffsetZ);
        }
        if (slider3.value > 0f)
        {
            // AlterJointWithVariablesfromJoint2(rotationValue2, childJoint_2_Box, Vector3.zero);
            // AlterJointWithVariablesfromJoint2(rotationValue2, childJoint_2_Box, new Vector3(0f, 0f, 0f));
            AlterJointWithVariablesfrom_Joint_2_AND_3_AND_5(rotationValue3, childJoint_3_Box, new Vector3(0f, childJoint_3_OffsetY, childJoint_3_OffsetZ));
            // Set the childJoint_4_Box's parent to childJoint_3_Box
            SetParentAndAlterJointWithVariables(childJoint_4_Box, childJoint_3_Box, childJoint_4_OffsetX, childJoint_4_OffsetY, childJoint_4_OffsetZ);
            // Set the childJoint_5_Box's parent to childJoint_4_Box
            SetParentAndAlterJointWithVariables(childJoint_5_Box, childJoint_4_Box, 0f, childJoint_5_OffsetY, 0f);
            // Set the endEffJoint_6_Box's parent to childJoint_5_Box
            SetParentAndAlterJointWithVariables(endEffJoint_6_Box, childJoint_5_Box, 0f, endEffJoint_6_OffsetY, endEffJoint_6_OffsetZ);
        }
        if (slider4.value > 0f)
        {
            // AlterJointWithVariablesfromJoint2(rotationValue2, childJoint_2_Box, Vector3.zero);
            // AlterJointWithVariablesfromJoint2(rotationValue2, childJoint_2_Box, new Vector3(0f, 0f, 0f));
            AlterJointWithVariablesfrom_Joint_4_AND_6(rotationValue4, childJoint_4_Box, new Vector3(childJoint_4_OffsetX, childJoint_4_OffsetY, childJoint_4_OffsetZ));
            // Set the childJoint_5_Box's parent to childJoint_4_Box
            SetParentAndAlterJointWithVariables(childJoint_5_Box, childJoint_4_Box, 0f, childJoint_5_OffsetY, 0f);
            // Set the endEffJoint_6_Box's parent to childJoint_5_Box
            SetParentAndAlterJointWithVariables(endEffJoint_6_Box, childJoint_5_Box, 0f, endEffJoint_6_OffsetY, endEffJoint_6_OffsetZ);
        }
        if (slider5.value > 0f)
        {
            // AlterJointWithVariablesfromJoint2(rotationValue2, childJoint_2_Box, Vector3.zero);
            // AlterJointWithVariablesfromJoint2(rotationValue2, childJoint_2_Box, new Vector3(0f, 0f, 0f));
            AlterJointWithVariablesfrom_Joint_2_AND_3_AND_5(rotationValue5, childJoint_5_Box, new Vector3(0f, childJoint_5_OffsetY, 0f));
            // Set the endEffJoint_6_Box's parent to childJoint_5_Box
            SetParentAndAlterJointWithVariables(endEffJoint_6_Box, childJoint_5_Box, 0f, endEffJoint_6_OffsetY, endEffJoint_6_OffsetZ);
        }
        if (slider6.value > 0f)
        {
            // AlterJointWithVariablesfromJoint2(rotationValue2, childJoint_2_Box, Vector3.zero);
            // AlterJointWithVariablesfromJoint2(rotationValue2, childJoint_2_Box, new Vector3(0f, 0f, 0f));
            AlterJointWithVariablesfrom_Joint_4_AND_6(rotationValue6, endEffJoint_6_Box, new Vector3(0f, endEffJoint_6_OffsetY, endEffJoint_6_OffsetZ));
        }

    }

    private void SetParentAndAlterJointWithVariables(GameObject child, GameObject newParent, float X, float Y, float Z)
    {
        if (child != null && newParent != null)
        {
            // Set the parent of the child
            child.transform.SetParent(newParent.transform, false);
            // Adjust local scale to prevent unexpected scaling
            child.transform.localScale = Vector3.one;
            // Alter the local position of the child
            child.transform.localPosition = new Vector3(X, Y, Z);
            // Alter joint rotation
            AlterJointWithVariablesfrom_Joint_1(0f, child, new Vector3(X, Y, Z));
        }
        else
        {
            Debug.LogError("Child or newParent is null. Please assign them in the Unity Editor.");
        }
    }

    public void AlterJointWithVariablesfrom_Joint_1(float rotationValue_1, GameObject jointBox, Vector3 offset)
    {
        if (jointBox != null)
        {
            Transform jointTransform = jointBox.transform;
            jointTransform.localRotation = Quaternion.Euler(0, rotationValue_1, 0);
            jointTransform.localPosition = offset;
        }
        else
        {
            Transform jointTransform = jointBox.transform;
            jointTransform.localRotation = Quaternion.Euler(0, rotationValue_1, 0);
            jointTransform.localPosition = offset;
            Debug.LogError("Joint box is null. Please assign it in the Unity Editor.");
        }
    }

    public void AlterJointWithVariablesfrom_Joint_2_AND_3_AND_5(float rotationValue_2_3_5, GameObject jointBox, Vector3 offset)
    {
        if (jointBox != null)
        {
            Transform jointTransform = jointBox.transform;
            jointTransform.localRotation = Quaternion.Euler(rotationValue_2_3_5, 0, 0);
            jointTransform.localPosition = offset;
        }
        else
        {
            Debug.LogError("Joint box is null. Please assign it in the Unity Editor.");
        }
    }
    public void AlterJointWithVariablesfrom_Joint_4_AND_6(float rotationValue_4_6, GameObject jointBox, Vector3 offset)
    {
        if (jointBox != null)
        {
            Transform jointTransform = jointBox.transform;
            jointTransform.localRotation = Quaternion.Euler(0, rotationValue_4_6, 0);
            jointTransform.localPosition = offset;
        }
        else
        {
            Debug.LogError("Joint box is null. Please assign it in the Unity Editor.");
        }
    }


    public void ToggleRecording()
    {
        isRecording = !isRecording;
        button_1_Image.color = isRecording ? recordingColor : initialColor;
        buttonColors.normalColor = isRecording ? recordingColor : initialColor;
        if (isRecording)
        {
            StartRecording();
        }
        else
        {
            StopRecording();
            // SaveKeyframesTo_txt_File();
            // SaveKeyframes_jsonFormat();
        }
    }
    void UpdateButtonUI()
    {
        // Update button UI
        // button_1_Text_T.text = isRecording ? "Recording" : "Start Recording";
        // button_1_Text_T.color = isRecording ? recordingColor : initialColor;
        button_1_Image.color = isRecording ? recordingColorBG : initialColorBG;
        // buttonColors.normalColor = isRecording ? recordingColor : initialColor;
        // button.colors = buttonColors;
        buttonColors.normalColor = isRecording ? recordingColor : initialColor;
    }
    public void StartRecording()
    {
        isRecording = true;
        keyframes.Clear(); // Clear existing keyframes when starting a new recording
    }
    private void RecordKeyframe()
    {
        RobotKeyframe keyframe = new RobotKeyframe(
            slider1.value, slider2.value, slider3.value,
            slider4.value, slider5.value, slider6.value);

        keyframes.Add(keyframe);
        Debug.Log("Recorded keyframe: " + keyframe.Slider1 + " " + keyframe.Slider2 + " " + keyframe.Slider3 + " " + keyframe.Slider4 + " " + keyframe.Slider5 + " " + keyframe.Slider6);
    }

    public void StopRecording()
    {
        isRecording = false;
    }

    public void StartPlayback()
    {
        StartCoroutine(Playback());
        Debug.Log("Starting Playback");
        button_2_Image.color = recordingColor;
    }


    private IEnumerator Playback()
    {
        // Read keyframes from the file
        List<RobotKeyframe> playbackKeyframes = LoadKeyframesFrom_txt_File();
        Debug.Log(playbackKeyframes);


        foreach (var keyframe in playbackKeyframes)
        {
            Debug.Log($"Joint 1 rotation: {keyframe.Slider1}, Joint 2 rotation: {keyframe.Slider2}, Joint 3 rotation: {keyframe.Slider3}, Joint 4 rotation: {keyframe.Slider4}, Joint 5 rotation: {keyframe.Slider5}, End Effector rotation: {keyframe.Slider6}");
            // yield return null; // Wait for the next frame
            // Interpolate between keyframes and set robot positions
            float duration = 0.0001f; // Adjust the duration of each keyframe
            float startTime = Time.time;
            float elapsedTime = 0f;

            while (elapsedTime < duration)
            {
                float t = elapsedTime / duration;

                // Interpolate between the current keyframe and the next keyframe
                RobotKeyframe interpolatedKeyframe = InterpolateKeyframes(keyframe, playbackKeyframes[math.min(playbackKeyframes.Count - 1, playbackKeyframes.IndexOf(keyframe) + 1)], t);

                // Set robot positions based on interpolated keyframe values
                SetRobotPositions(interpolatedKeyframe);

                elapsedTime = Time.time - startTime;

                yield return null; // Wait for the next frame
            }
        }
    }
    private RobotKeyframe InterpolateKeyframes(RobotKeyframe start, RobotKeyframe end, float t)
    {
        // Linear interpolation between two keyframes
        return new RobotKeyframe(
            Mathf.Lerp(start.Slider1, end.Slider1, t),
            Mathf.Lerp(start.Slider2, end.Slider2, t),
            Mathf.Lerp(start.Slider3, end.Slider3, t),
            Mathf.Lerp(start.Slider4, end.Slider4, t),
            Mathf.Lerp(start.Slider5, end.Slider5, t),
            Mathf.Lerp(start.Slider6, end.Slider6, t)
        );
    }

    private List<RobotKeyframe> LoadKeyframesFrom_txt_File()
    {
        List<RobotKeyframe> loadedKeyframes = new List<RobotKeyframe>();
        using (StreamReader reader = new StreamReader(keyframesFilePath_to_txt))
        {
            while (!reader.EndOfStream)
            {
                string[] values = reader.ReadLine().Split(' ');
                float s1 = float.Parse(values[0]);
                float s2 = float.Parse(values[1]);
                float s3 = float.Parse(values[2]);
                float s4 = float.Parse(values[3]);
                float s5 = float.Parse(values[4]);
                float s6 = float.Parse(values[5]);

                loadedKeyframes.Add(new RobotKeyframe(s1, s2, s3, s4, s5, s6));
            }
        }
        return loadedKeyframes;
    }

    private void SetRobotPositions(RobotKeyframe keyframe)
    { // Update sliders based on keyframe values
        // Assuming you have a method like AlterJoints, update the sliders based on keyframe values
        slider1.value = keyframe.Slider1;
        slider2.value = keyframe.Slider2;
        slider3.value = keyframe.Slider3;
        slider4.value = keyframe.Slider4;
        slider5.value = keyframe.Slider5;
        slider6.value = keyframe.Slider6;

        // Call the method that updates the robot's joint positions
        AlterJoints();
    }


    public void TogglePlayback()
    {
        if (isPlaybackPaused)
        {
            StartCoroutine(ResumePlayback());
            Debug.Log("Resuming Playback");
            button_3_Image.color = initialColor;
        }
        else
        {
            StartCoroutine(PausePlayback());
            Debug.Log("Pausing Playback");
            button_3_Image.color = recordingColor;
        }

        isPlaybackPaused = !isPlaybackPaused;
    }

    private IEnumerator PausePlayback()
    {
        while (true)
        {
            yield return null; // Wait for the next frame

            // Add any additional logic you may need when playback is paused
            // For example, you might want to freeze the robot's animation.
        }
    }

    private IEnumerator ResumePlayback()
    {
        List<RobotKeyframe> playbackKeyframes = LoadKeyframesFrom_txt_File();

        float startTime = Time.time;
        float elapsedTime = 0f;

        for (int i = 0; i < playbackKeyframes.Count - 1; i++)
        {
            while (elapsedTime < 1.0f)
            {
                if (!isPlaybackPaused)
                {
                    float t = elapsedTime;

                    // Interpolate between the current keyframe and the next keyframe
                    RobotKeyframe interpolatedKeyframe = InterpolateKeyframes(playbackKeyframes[i], playbackKeyframes[i + 1], t);

                    // Set robot positions based on interpolated keyframe values
                    SetRobotPositions(interpolatedKeyframe);

                    elapsedTime = (Time.time - startTime) / 1.0f; // Normalize to 0-1 range

                    yield return null; // Wait for the next frame
                }
                else
                {
                    yield return null; // Wait for the next frame while paused
                }
            }

            elapsedTime = 0f; // Reset elapsed time for the next keyframe
            startTime = Time.time; // Reset start time for the next keyframe
        }

        isPlaybackPaused = false; // Reset the paused state after playback completes
    }

    
    
    //android
    private string GetFilePath()
    {
        string rootPath = Path.Combine(Application.persistentDataPath, rootFolderName);
        string subFolderPath = Path.Combine(rootPath, subFolderName);
        string filePath = Path.Combine(subFolderPath, fileName);

        return filePath;
    }
    //adroid
    private void CreateFoldersAndFile()
    {
        string rootPath = Path.Combine(Application.persistentDataPath, rootFolderName);
        string subFolderPath = Path.Combine(rootPath, subFolderName);

        if (!Directory.Exists(rootPath))
        {
            Directory.CreateDirectory(rootPath);
        }

        if (!Directory.Exists(subFolderPath))
        {
            Directory.CreateDirectory(subFolderPath);
        }

        // You can create the file here if needed
        // string filePath = Path.Combine(subFolderPath, fileName);
        // File.WriteAllText(filePath, "Initial content");
    }
    // for adroid
    private void SaveKeyframesToFile()
    {
        CreateFoldersAndFile(); // Ensure folders and file exist
        using (StreamWriter writer = new StreamWriter(keyframesFilePath_to_txt))
        {
            foreach (var keyframe in keyframes)
            {
                // Write each keyframe data to the file
                writer.WriteLine($"{keyframe.Slider1} {keyframe.Slider2} {keyframe.Slider3} {keyframe.Slider4} {keyframe.Slider5} {keyframe.Slider6}");
            }
        }

        Debug.Log("Keyframes saved to file: " + keyframesFilePath_to_txt);
    }
    
    //for windows
    private void SaveKeyframesTo_txt_File()
    {
        using (StreamWriter writer = new StreamWriter(keyframesFilePath_to_txt))
        {
            foreach (var keyframe in keyframes)
            {
                // Write each keyframe data to the file
                writer.WriteLine($"{keyframe.Slider1} {keyframe.Slider2} {keyframe.Slider3} {keyframe.Slider4} {keyframe.Slider5} {keyframe.Slider6}");
            }
        }

        Debug.Log("Keyframes saved to file: " + keyframesFilePath_to_txt);
    }


}







public struct RobotKeyframe
{
    public float Slider1;
    public float Slider2;
    public float Slider3;
    public float Slider4;
    public float Slider5;
    public float Slider6;

    public RobotKeyframe(float s1, float s2, float s3, float s4, float s5, float s6)
    {
        Slider1 = s1;
        Slider2 = s2;
        Slider3 = s3;
        Slider4 = s4;
        Slider5 = s5;
        Slider6 = s6;
    }
}