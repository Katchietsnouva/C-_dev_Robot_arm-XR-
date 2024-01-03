using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class u6_slider_ctrl : MonoBehaviour
{
    private Slider slider1;
    private Slider slider2;
    private Slider slider3;
    private Slider slider4;
    private Slider slider5;
    private Slider slider6;
    public GameObject U6robot3DBox;
    private const string J2_PARAMETER_NAME = "J2val";
    private const string J3_PARAMETER_NAME = "J3val";
    private const string J4_PARAMETER_NAME = "J3val";
    private string logFilePath; // Path to the log file
    void Start()
    {
        // slider = GetComponent<Slider>();
        slider1 = GameObject.Find("Slider1").GetComponent<Slider>();
        slider2 = GameObject.Find("Slider2").GetComponent<Slider>();
        slider3 = GameObject.Find("Slider3").GetComponent<Slider>();
        slider4 = GameObject.Find("Slider4").GetComponent<Slider>();
        slider5 = GameObject.Find("Slider5").GetComponent<Slider>();
        slider6 = GameObject.Find("Slider6").GetComponent<Slider>();

        logFilePath = Application.dataPath + "/u6_slider_ctrl_log.txt"; // Set the path for the log file
        // CheckChildCount(transform);
        // string targetChildName = "xArm6(XI1300) xArm6(XI1300) - Copy.STEP-1 Tool head.STEP-1";
        Transform rootTransform = transform;
        CheckChildCount(rootTransform);
        // Transform foundChild = FindChildRecursive(transform, targetChildName);

    }

    public void CheckChildCount(Transform parent)
    {
        int childCount = parent.childCount;

        Debug.Log($"Number of children for {parent.name}: {childCount}");
        string logMessage = $"Number of children for {parent.name}: {childCount}";
        LogToFile(logMessage);

        for (int i = 0; i < childCount; i++)
        {
            Transform child = parent.GetChild(i);
            Debug.Log($"Checking child: {child.name}");
            string findingchild = $"Checking child: {child.name}";
            LogToFile(findingchild);
            CheckChildCount(child); // Recursively check children

            // Check if the child has the specific name you're looking for
            string J2 = "xArm6(XI1300) xArm6(XI1300) - Copy.STEP-1 J2.STEP-1";
            // string J2 = "xArm6(XI1300) xArm6(XI1300) - Copy.STEP-1 Tool head.STEP-1";
            string J3 = "xArm6(XI1300) xArm6(XI1300) - Copy.STEP-1 J3.STEP-1";
            string J4 = "xArm6(XI1300) xArm6(XI1300) - Copy.STEP-1 J4.STEP-1";
            string J5 = "xArm6(XI1300) xArm6(XI1300) - Copy.STEP-1 J5.STEP-1";
            if (child.name == J2)
            {
                Debug.Log($"Found and altering rotation for: {child.name}");
                string logMessage2 = $"Found and altering rotation for: {child.name}";
                LogToFile(logMessage2);
                AlterJ2(J2);
            }
            if (child.name == J3)
            {
                Debug.Log($"Found and altering rotation for: {child.name}");
                string logMessage2 = $"Found and altering rotation for: {child.name}";
                LogToFile(logMessage2);
                AlterJ3(J3);
            }
            if (child.name == J4)
            {
                Debug.Log($"Found and altering rotation for: {child.name}");
                string logMessage2 = $"Found and altering rotation for: {child.name}";
                LogToFile(logMessage2);
                AlterJ4(J4);
            }
            if (child.name == J5)
            {
                Debug.Log($"Found and altering rotation for: {child.name}");
                string logMessage2 = $"Found and altering rotation for: {child.name}";
                LogToFile(logMessage2);
                AlterJ5(J5);
            }
        }
        LogToFile("--------------------------------------------------------");
    }

    public void AlterJ2(string J2)
    {
        Transform robotTransform = U6robot3DBox.transform;
        float rotationValue1 = slider1.value * 360f;
        // Find the child with the specified name
        Transform child = robotTransform.Find(J2);
        if (child != null)
        {
            // If the child is found, alter its rotation
            // child.localRotation = Quaternion.AngleAxis(rotationValue1, Vector3.right);
            child.localRotation = Quaternion.AngleAxis(rotationValue1, Vector3.up);
            // child.localRotation = Quaternion.AngleAxis(rotationValue1, Vector3.forward);
            //forward worked almost fine with the 1st 2 arms(J1,J2),
            //up worked perfextly sine with the 3rd one (J1, J2)
            string LogRotation = $" Quaternion.AngleAxis: {child.name}";
            LogToFile(LogRotation);
            LogToFile("--------------------------------------------------------");
        }
        else
        {
            Debug.LogWarning($"Child with name {J2} not found.");
            string LogWarning = $"Child with name {J2} not found. ";
            LogToFile(LogWarning);
            LogToFile("--------------------------------------------------------");
        }
        // robotTransform.Find(J2).localRotation =Quaternion.AngleAxis(rotationValue1,Vector3.forward);
        // robotTransform.GetChild(J1).localRotation = Quaternion.AngleAxis(rotationValue1, Vector3.forward);
    }


    public void AlterJ3(string J3)
    {
        Transform robotTransform = U6robot3DBox.transform;
        float rotationValue2 = slider2.value * 360f;
        // Find the child with the specified name
        Transform child = robotTransform.Find(J3);
        if (child != null)
        {
            // If the child is found, alter its rotation
            child.localRotation = Quaternion.AngleAxis(rotationValue2, Vector3.right);
            // child.localRotation = Quaternion.AngleAxis(rotationValue1, Vector3.up);
            // child.localRotation = Quaternion.AngleAxis(rotationValue1, Vector3.forward);
            //forward worked almost fine with the 1st 2 arms(J1,J2),
            //up worked perfextly fine with the 3rd one (J1, J2)
            //rignt worked perfextly fine with the 3rd one (J3)
            string LogRotation = $" Quaternion.AngleAxis: {child.name}";
            LogToFile(LogRotation);
            LogToFile("--------------------------------------------------------");
        }
        else
        {
            Debug.LogWarning($"Child with name {J3} not found.");
            string LogWarning = $"Child with name {J3} not found. ";
            LogToFile(LogWarning);
            LogToFile("--------------------------------------------------------");
        }
    }

    public void AlterJ4(string J4)
    {
        Transform robotTransform = U6robot3DBox.transform;
        float rotationValue3 = slider3.value * 360f;
        // Find the child with the specified name
        Transform child = robotTransform.Find(J4);
        if (child != null)
        {
            // If the child is found, alter its rotation
            child.localRotation = Quaternion.AngleAxis(rotationValue3, Vector3.right);
            string LogRotation = $" Quaternion.AngleAxis: {child.name}";
            LogToFile(LogRotation);
            LogToFile("--------------------------------------------------------");
        }
        else
        {
            Debug.LogWarning($"Child with name {J4} not found.");
            string LogWarning = $"Child with name {J4} not found. ";
            LogToFile(LogWarning);
            LogToFile("--------------------------------------------------------");
        }
    }

    public void AlterJ5(string J5){
        Transform robotTransform = U6robot3DBox.transform;
        float rotationValue4 = slider4.value * 360f;
        // Find the child with the specified name
        Transform child = robotTransform.Find(J5);
        if (child != null)
        {
            // If the child is found, alter its rotation
            child.localRotation = Quaternion.AngleAxis(rotationValue4, Vector3.up);
            string LogRotation = $" Quaternion.AngleAxis: {child.name}";
            LogToFile(LogRotation);
            LogToFile("--------------------------------------------------------");
        }
        else
        {
            Debug.LogWarning($"Child with name {J5} not found.");
            string LogWarning = $"Child with name {J5} not found. ";
            LogToFile(LogWarning);
            LogToFile("--------------------------------------------------------");
        }
    }








    private void AlterJointRotation(Transform joint, float sliderValue)
    {
        // Rotate joint around its local up axis
        float rotationValue = sliderValue * 360f;
        joint.localRotation = Quaternion.AngleAxis(rotationValue, Vector3.forward);
    }

    public void ApplyAnimation()
    {
        if (U6robot3DBox != null)
        {
            Animator animator = U6robot3DBox.GetComponent<Animator>();
            if (animator != null)
            {
                // Retrieve the current state of the animator
                AnimatorStateInfo currentState = animator.GetCurrentAnimatorStateInfo(0);
                // Calculate the normalized time to smoothly blend between the current and new state
                float blendTime = Mathf.Clamp01(currentState.normalizedTime);
                // Set the blend time for each parameter based on the slider values
                // animator.SetFloat("J2val", slider1.value,blendTime);
                // animator.CrossFade("J2val", slider1.value, 0, currentState.normalizedTime);

                // Transform robotTransform = U6robot3DBox.transform;
                // Rotate joint 1 around its local up axis

                // float rotationValue1 = slider1.value * 360f;
                // robotTransform.GetChild(1).localRotation = Quaternion.AngleAxis(rotationValue1, Vector3.forward);

                // animator.CrossFade(J2_PARAMETER_NAME, slider1.value, 0, currentState.normalizedTime);
                // animator.SetFloat(J2_PARAMETER_NAME, slider1.value);
                animator.CrossFade(J3_PARAMETER_NAME, slider2.value, 0, currentState.normalizedTime);
                animator.CrossFade(J4_PARAMETER_NAME, slider3.value, 0, currentState.normalizedTime);
            }

        }

    }
    private void LogToFile(string logMessage)
    {
        using (StreamWriter sw = File.AppendText(logFilePath))
        {
            sw.WriteLine(logMessage);
        }
    }




}

