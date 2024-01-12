using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class u6_slider_ctrl : MonoBehaviour
{
    private Slider slider1;
    [SerializeField] private GameObject parentJointBox;
    [SerializeField] private GameObject childJointBox;
    private float childJointOffset = (float)(0.155 / 1000.0); // Adjust this value based on your scene

    void Start()
    {
        slider1 = GameObject.Find("Slider1").GetComponent<Slider>();
    }

    public void AlterJoints()
    {
        float rotationValue1 = slider1.value * 360f;
        AlterJointWithVariables(rotationValue1, parentJointBox, Vector3.zero);

        // Calculate offset position for the child joint
        Vector3 childJointOffsetPosition = Quaternion.Euler(0, rotationValue1, 0) * new Vector3(0, 0, childJointOffset);
        AlterJointWithVariables(rotationValue1, childJointBox, childJointOffsetPosition);
    }

    public void AlterJointWithVariables(float rotationValue, GameObject jointBox, Vector3 offset)
    {
        if (jointBox != null)
        {
            Transform jointTransform = jointBox.transform;
            jointTransform.localRotation = Quaternion.Euler(0, rotationValue, 0);
            jointTransform.localPosition = offset;
        }
        else
        {
            Debug.LogError("Joint box is null. Please assign it in the Unity Editor.");
        }
    }
}
