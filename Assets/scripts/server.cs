using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using UnityEngine;

public class serverCode : MonoBehaviour
{
    // Existing code for sliders and other functionalities

    // SERVER related interactions
    private TcpListener tcpListener;
    private TcpClient tcpClient;
    private bool isServer = false;
    private string serverIPAddress = "192.168.0.100"; // Replace with your actual server IP address
    private int port = 12345;

    void Start()
    {
        // Initialize sliders and other components

        // SERVER related interactions
        StartNetworking();
    }

    // SERVER related interactions
    private void StartNetworking()
    {
        isServer = CheckIfServer();

        if (isServer)
        {
            // Start server
            tcpListener = new TcpListener(IPAddress.Any, port);
            tcpListener.Start();
            new Thread(ListenForClients).Start();
        }
        else
        {
            // Start client
            tcpClient = new TcpClient();
            tcpClient.Connect(serverIPAddress, port);

            // Optionally, you can start a thread to listen for data from the server
            new Thread(() => { ListenForServerData(tcpClient); }).Start();
        }
    }

    // SERVER related interactions
    private void ListenForClients()
    {
        while (true)
        {
            TcpClient client = tcpListener.AcceptTcpClient();
            new Thread(() => { HandleClient(client); }).Start();
            Debug.Log("Client connected: " + ((IPEndPoint)client.Client.RemoteEndPoint).Address.ToString());
        }
    }

    // SERVER related interactions
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

    // Existing code for sliders, AlterJoints, and other functionalities
}
