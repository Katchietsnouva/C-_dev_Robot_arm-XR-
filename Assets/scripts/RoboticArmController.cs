using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoboticArmController : MonoBehaviour
{
    public class RoboticJoint
    {
        public GameObject JointBox { get; }
        public float OffsetX { get; }
        public float OffsetY { get; }
        public float OffsetZ { get; }

        public RoboticJoint(GameObject jointBox, float offsetX, float offsetY, float offsetZ)
        {
            JointBox = jointBox;
            OffsetX = offsetX;
            OffsetY = offsetY;
            OffsetZ = offsetZ;
        }
    }

    private Slider slider1;
    private Slider slider2;
    [SerializeField] private GameObject parentJoint_1_Box;
    [SerializeField] private RoboticJoint parentJoint;
    [SerializeField] private RoboticJoint childJoint2;
    [SerializeField] private RoboticJoint childJoint3;
    [SerializeField] private RoboticJoint childJoint4;
    [SerializeField] private RoboticJoint childJoint5;
    [SerializeField] private RoboticJoint endEffectorJoint;

    void Start()
    {
        slider1 = GameObject.Find("Slider1").GetComponent<Slider>();
        slider2 = GameObject.Find("Slider2").GetComponent<Slider>();
    }

    public void AlterJoints()
    {
        float rotationValue1 = slider1.value * 360f;
        float rotationValue2 = slider2.value * 360f;

        AlterJointWithVariables(rotationValue1, parentJoint);
        SetParentAndAlterJointWithVariables(childJoint2, parentJoint, rotationValue2);
        SetParentAndAlterJointWithVariables(childJoint3, childJoint2);
        SetParentAndAlterJointWithVariables(childJoint4, childJoint3);
        SetParentAndAlterJointWithVariables(childJoint5, childJoint4);
        SetParentAndAlterJointWithVariables(endEffectorJoint, childJoint5);
    }

    private void SetParentAndAlterJointWithVariables(RoboticJoint child, RoboticJoint newParent, float rotationValue = 0f)
    {
        if (child != null && newParent != null)
        {
            child.JointBox.transform.SetParent(newParent.JointBox.transform, false);
            child.JointBox.transform.localScale = Vector3.one;

            Vector3 offset = new Vector3(newParent.OffsetX, newParent.OffsetY, newParent.OffsetZ);
            child.JointBox.transform.localPosition = offset;

            if (rotationValue != 0f)
            {
                AlterJointWithVariables(rotationValue, child);
            }
        }
        else
        {
            Debug.LogError("Child or newParent is null. Please assign them in the Unity Editor.");
        }
    }

    public void AlterJointWithVariables(float rotationValue, RoboticJoint joint)
    {
        if (joint != null && joint.JointBox != null)
        {
            Transform jointTransform = joint.JointBox.transform;
            jointTransform.localRotation = Quaternion.Euler(0, rotationValue, 0);
            jointTransform.localPosition = new Vector3(joint.OffsetX, joint.OffsetY, joint.OffsetZ);
        }
        else
        {
            Debug.LogError("Joint or JointBox is null. Please assign them in the Unity Editor.");
        }
    }
}
