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
            string J3 = "xArm6(XI1300) xArm6(XI1300) - Copy.STEP-1 J3.STEP-1";
            string J4 = "xArm6(XI1300) xArm6(XI1300) - Copy.STEP-1 J4.STEP-1";
            string J5 = "xArm6(XI1300) xArm6(XI1300) - Copy.STEP-1 J5.STEP-1";
            string J6 = "xArm6(XI1300) xArm6(XI1300) - Copy.STEP-1 J6.STEP-1";
            string J7 = "xArm6(XI1300) xArm6(XI1300) - Copy.STEP-1 Tool head.STEP-1";
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
            if (child.name == J6)
            {
                Debug.Log($"Found and altering rotation for: {child.name}");
                string logMessage2 = $"Found and altering rotation for: {child.name}";
                LogToFile(logMessage2);
                AlterJ6(J6);
            }
            if (child.name == J7)
            {
                Debug.Log($"Found and altering rotation for: {child.name}");
                string logMessage2 = $"Found and altering rotation for: {child.name}";
                LogToFile(logMessage2);
                AlterJ7(J7);
            }
        }
        LogToFile("--------------------------------------------------------");
    }

    public void AlterJ2(string J2)
    {
        Transform robotTransform = U6robot3DBox.transform;
        float rotationValue1 = slider1.value * 360f;
        Transform J2Transform = robotTransform.Find(J2);

        if (J2Transform != null)
        {
            // Get the initial position relative to the parent
            Vector3 initialRelativePosition = J2Transform.localPosition;

            // If the desired joint is found, alter its rotation
            J2Transform.localRotation = Quaternion.AngleAxis(rotationValue1, Vector3.up);

            // Calculate the new position of child of the current joint in world based on the altered rotation
            Vector3 rotatedOffset = J2Transform.localPosition - initialRelativePosition;
            Vector3 newPosition = robotTransform.TransformPoint(rotatedOffset + initialRelativePosition);

            // Set the new position
            J2Transform.position = newPosition;

            // Now, let's assume J3 is a child of J2
            Transform J3Transform = J2Transform.Find("J3"); // Adjust the name accordingly
            if (J3Transform != null)
            {
                // Assuming a simple transformation, adjust the position of J3 based on J2's rotation
                J3Transform.localPosition = new Vector3(0f, 0f, 10f); // Adjust the distance accordingly
            }

            string LogRotation = $" Quaternion.AngleAxis: {J2Transform.name}";
            LogToFile(LogRotation);
            LogToFile("--------------------------------------------------------");
        }
        else
        {
            Debug.LogWarning($"Joint with name {J2} not found.");
            string LogWarning = $"Joint with name {J2} not found. ";
            LogToFile(LogWarning);
            LogToFile("--------------------------------------------------------");
        }
    }

    public void AlterJ3(string J3)
    {
        Transform robotTransform = U6robot3DBox.transform;
        float rotationValue2 = slider2.value * 360f;
        Transform J2Transform = robotTransform.Find("J2"); // Assuming J3 is a child of J2

        if (J2Transform != null)
        {
            // Get the initial position relative to the parent
            Vector3 initialRelativePosition = J2Transform.Find(J3).localPosition;

            // If the desired joint is found, alter its rotation
            J2Transform.localRotation = Quaternion.AngleAxis(rotationValue2, Vector3.right);

            // Calculate the new position of child of the current joint based on the altered rotation
            Vector3 rotatedOffset = J2Transform.Find(J3).localPosition - initialRelativePosition;
            Vector3 newPosition = J2Transform.TransformPoint(rotatedOffset + initialRelativePosition);

            // Set the new position
            J2Transform.Find(J3).position = newPosition;

            string LogRotation = $" Quaternion.AngleAxis: {J2Transform.name}";
            LogToFile(LogRotation);
            LogToFile("--------------------------------------------------------");
        }
        else
        {
            Debug.LogWarning($"Joint with name {J3} not found.");
            string LogWarning = $"Joint with name {J3} not found. ";
            LogToFile(LogWarning);
            LogToFile("--------------------------------------------------------");
        }
    }



    // public void AlterJ2(string J2)
    // {
    //     Transform robotTransform = U6robot3DBox.transform;
    //     float rotationValue1 = slider1.value * 360f;
    //     Transform J2Transform = robotTransform.Find(J2);
    //     if (J2Transform != null)
    //     {
    //         // Get the initial position relative to the parent
    //         Vector3 initialRelativePosition = J2Transform.localPosition;
    //         // If the desired joint is found, alter its rotation
    //         J2Transform.localRotation = Quaternion.AngleAxis(rotationValue1, Vector3.up);

    //         // Calculate the new position of child of the current joint in world based on the altered rotation
    //         // Vector3 rotatedOffset = child.localPosition - initialRelativePosition;
    //         // Vector3 rotatedOffset = child.Position - initialRelativePosition;
    //         Vector3 rotatedOffset = J2Transform.localPosition - initialRelativePosition;
    //         // Vector3 newPosition = robotTransform.TransformPoint(rotatedOffset);
    //         Vector3 newPosition = robotTransform.TransformPoint(rotatedOffset + initialRelativePosition);
    //         // Set the new position
    //         J2Transform.position = newPosition;

    //          // Now, let's assume J3 is a child of J2
    //     Transform J3Transform = J2Transform.Find("J3"); // Adjust the name accordingly
    //     if (J3Transform != null)
    //     {
    //         // Assuming a simple transformation, adjust the position of J3 based on J2's rotation
    //         J3Transform.localPosition = new Vector3(0f, 0f, 1); // Adjust the distance accordingly
    //     }


    //         string LogRotation = $" Quaternion.AngleAxis: {J2Transform.name}";
    //         LogToFile(LogRotation);
    //         LogToFile("--------------------------------------------------------");
    //     }
    //     else
    //     {
    //         Debug.LogWarning($"Child with name {J2} not found.");
    //         string LogWarning = $"Child with name {J2} not found. ";
    //         LogToFile(LogWarning);
    //         LogToFile("--------------------------------------------------------");
    //     }
    //     // robotTransform.Find(J2).localRotation =Quaternion.AngleAxis(rotationValue1,Vector3.forward);
    //     // robotTransform.GetChild(J1).localRotation = Quaternion.AngleAxis(rotationValue1, Vector3.forward);
    // }


    // public void AlterJ3(string J3)
    // {
    //     Transform robotTransform = U6robot3DBox.transform;
    //     float rotationValue2 = slider2.value * 360f;
    //     Transform child = robotTransform.Find(J3);
    //     if (child != null)
    //     {
    //         // Get the initial position relative to the parent
    //         Vector3 initialRelativePosition = child.localPosition;
    //         // If the desired joint is found, alter its rotation
    //         child.localRotation = Quaternion.AngleAxis(rotationValue2, Vector3.right);

    //         // Calculate the new position of child of the current joint based on the altered rotation
    //         Vector3 rotatedOffset = child.localPosition - initialRelativePosition;
    //         Vector3 newPosition = robotTransform.TransformPoint(rotatedOffset);
    //         // Set the new position
    //         child.position = newPosition;

    //         string LogRotation = $" Quaternion.AngleAxis: {child.name}";
    //         LogToFile(LogRotation);
    //         LogToFile("--------------------------------------------------------");
    //     }
    //     else
    //     {
    //         Debug.LogWarning($"Child with name {J3} not found.");
    //         string LogWarning = $"Child with name {J3} not found. ";
    //         LogToFile(LogWarning);
    //         LogToFile("--------------------------------------------------------");
    //     }
    // }

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

    public void AlterJ5(string J5)
    {
        Transform robotTransform = U6robot3DBox.transform;
        float rotationValue4 = slider4.value * 360f;
        // Find the child with the specified name
        Transform child = robotTransform.Find(J5);
        if (child != null)
        {
            // If the child is found, alter its rotation
            // child.localRotation = Quaternion.AngleAxis(rotationValue4, Vector3.up);
            // i corrected the mirror reflection  of the z axis
            child.localRotation = Quaternion.AngleAxis(rotationValue4, Vector3.up) * Quaternion.Euler(180, 0, 0);
            // child.localRotation = Quaternion.AngleAxis(rotationValue4 - 180f, Vector3.up);
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

    public void AlterJ6(string J6)
    {
        Transform robotTransform = U6robot3DBox.transform;
        float rotationValue5 = slider5.value * 360f;
        // Find the child with the specified name
        Transform child = robotTransform.Find(J6);
        if (child != null)
        {
            // If the child is found, alter its rotation
            child.localRotation = Quaternion.AngleAxis(rotationValue5, Vector3.right);
            string LogRotation = $" Quaternion.AngleAxis: {child.name}";
            LogToFile(LogRotation);
            LogToFile("--------------------------------------------------------");
        }
        else
        {
            Debug.LogWarning($"Child with name {J6} not found.");
            string LogWarning = $"Child with name {J6} not found. ";
            LogToFile(LogWarning);
            LogToFile("--------------------------------------------------------");
        }
    }

    public void AlterJ7(string J7)
    {
        Transform robotTransform = U6robot3DBox.transform;
        float rotationValue6 = slider6.value * 360f;
        // Find the child with the specified name
        Transform child = robotTransform.Find(J7);
        if (child != null)
        {
            // If the child is found, alter its rotation  
            // i corrected the mirror reflection  of the z axis
            child.localRotation = Quaternion.AngleAxis(rotationValue6, Vector3.up) * Quaternion.Euler(180, 0, 0);
            // child.localRotation = Quaternion.AngleAxis(rotationValue6 - 180f, Vector3.up);

            string LogRotation = $" Quaternion.AngleAxis: {child.name}";
            LogToFile(LogRotation);
            LogToFile("--------------------------------------------------------");
        }
        else
        {
            Debug.LogWarning($"Child with name {J7} not found.");
            string LogWarning = $"Child with name {J7} not found. ";
            LogToFile(LogWarning);
            LogToFile("--------------------------------------------------------");
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

