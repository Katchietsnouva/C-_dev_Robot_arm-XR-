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

        logFilePath = Application.dataPath + "/u6_slider_ctrl_log.txt"; // Set the path for the log file
        // CheckChildCount(transform);
        string targetChildName = "xArm6(XI1300) xArm6(XI1300) - Copy.STEP-1 Tool head.STEP-1";
        Transform rootTransform = transform;
        CheckChildCount(rootTransform);
        Transform foundChild = FindChildRecursive(transform, targetChildName);

    }

    // public void CheckChildCount(Transform parent)
    // {
    //     int childCount = parent.childCount;

    //     Debug.Log($"Number of children for {parent.name}: {childCount}");
    //     string logMessage = $"Number of children for {parent.name}: {childCount}";
    //     LogToFile(logMessage);

    //     for (int i = 0; i < childCount; i++)
    //     {
    //         Transform child = parent.GetChild(i);
    //         Debug.Log($"Checking child: {child.name}");
    //         string findingchild = $"Checking child: {child.name}";
    //         LogToFile(findingchild);
    //         CheckChildCount(child); // Recursively check children

    //         // Check if the child has the specific name you're looking for
    //         // string J2 = "xArm6(XI1300) xArm6(XI1300) - Copy.STEP-1 J2.STEP-1";
    //         string J2 = "xArm6(XI1300) xArm6(XI1300) - Copy.STEP-1 Tool head.STEP-1";
    //         if (child.name == J2)
    //         {
    //             Debug.Log($"Found and altering rotation for: {child.name}");
    //             string logMessage2 = $"Found and altering rotation for: {child.name}";
    //             LogToFile(logMessage2);
    //             // Do something specific for this child (e.g., alter its rotation)
    //             AlterJ2(J2);
    //             // AlterJointRotation(child, slider1.value);
    //         }
    //     }
    //     LogToFile("--------------------------------------------------------");
    // }
    public void CheckChildCount(Transform parent, string indentation = "")
    {
        int childCount = parent.childCount;

        Debug.Log($"{indentation}Number of children for {parent.name}: {childCount}");
        string logMessage = $"{indentation}Number of children for {parent.name}: {childCount}";
        LogToFile(logMessage);

        for (int i = 0; i < childCount; i++)
        {
            Transform child = parent.GetChild(i);
            Debug.Log($"{indentation}Checking child: {child.name}");
            CheckChildCount(child, $"{indentation}\t"); // Recursively check children with increased indentation

            // Check if the child has the specific name you're looking for
            string J2 = "xArm6(XI1300) xArm6(XI1300) - Copy.STEP-1 Tool head.STEP-1";
            if (child.name == J2)
            {
                Debug.Log($"{indentation}Found and altering rotation for: {child.name}");
                string logMessage2 = $"{indentation}Found and altering rotation for: {child.name}";
                LogToFile(logMessage2);
                // Do something specific for this child (e.g., alter its rotation)
                AlterJ2(J2);
                // AlterJointRotation(child, slider1.value);
            }
            else
            {
                Debug.Log($"No children found under parent: {parent.name}");
                string logMessage3 = $"No children found under parent: {parent.name}";
                LogToFile(logMessage3);
            }

            LogToFile("--------------------------------------------------------");
        }
    }

    private Transform FindChildRecursive(Transform parent, string childName)
    {
        Transform result = null;

        for (int i = 0; i < parent.childCount; i++)
        {
            Transform child = parent.GetChild(i);

            // Check if the child's name matches    
            if (child.name == childName)
            {
                result = child;
                break;
            }

            // Recursively search through the child's descendants
            Transform descendant = FindChildRecursive(child, childName);
            if (descendant != null)
            {
                result = descendant;
                break;
            }
        }

        return result;
    }
    // public void AlterJ2(string J2)
    // {
    //     Transform robotTransform = U6robot3DBox.transform;
    //     float rotationValue1 = slider1.value * 360f;
    //     // Find the child with the specified name
    //     Transform child = robotTransform.Find(J2);
    //     if (child != null)
    //     {
    //         // If the child is found, alter its rotation
    //         child.localRotation = Quaternion.AngleAxis(rotationValue1, Vector3.left);
    //         string LogRotation = $" Quaternion.AngleAxis: {child.name}";
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

    public void AlterJ2(string J2)
    {
        Transform robotTransform = U6robot3DBox.transform;
        float rotationValue1 = slider1.value * 360f;

        // Find the child with the specified name
        Transform child = robotTransform.Find(J2);
        if (child != null)
        {
            // If the child is found, alter its rotation
            child.localRotation = Quaternion.AngleAxis(rotationValue1, Vector3.left);
            string LogRotation = $" Quaternion.AngleAxis: {child.name} under parent: {child.parent.name}";
            LogToFile(LogRotation);
        }
        else
        {
            Debug.LogWarning($"Child with name {J2} not found under parent: {robotTransform.name}");
            string LogWarning = $"Child with name {J2} not found under parent: {robotTransform.name}";
            LogToFile(LogWarning);
        }

        LogToFile("--------------------------------------------------------");
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

