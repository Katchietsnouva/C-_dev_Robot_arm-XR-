

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class u6_slider_ctrl : MonoBehaviour
{
    private Slider slider1;
    [SerializeField] private GameObject parentJointBox;
    [SerializeField] private GameObject childJointBox;
    private float initialChildJointOffset = (float)(0.0754 / 100.0);
    private float parentJointRadius = (float)(0.0754 / 100.0);

    void Start()
    {
        slider1 = GameObject.Find("Slider1").GetComponent<Slider>();
    }


public void AlterJoints()
{
    float rotationValue1 = slider1.value * 360f;

    // Set the parent's rotation
    AlterJointWithVariables(rotationValue1, parentJointBox, Vector3.zero);

    // Set the childJointBox's parent to parentJointBox
    SetParentAndAlterJointWithVariables(childJointBox, parentJointBox, parentJointRadius);
}

private void SetParentAndAlterJointWithVariables(GameObject child, GameObject newParent, float parentRadius)
{
    if (child != null && newParent != null)
    {
        // Set the parent of the childJointBox
        child.transform.SetParent(newParent.transform, false);

        // Alter the local position of the childJointBox in the y-axis
        child.transform.localPosition = new Vector3(child.transform.localPosition.x, initialChildJointOffset, child.transform.localPosition.z);

        // Alter joint rotation
        AlterJointWithVariables(0f, child, new Vector3(parentRadius, 0, 0));
    }
    else
    {
        Debug.LogError("Child or newParent is null. Please assign them in the Unity Editor.");
    }
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
























// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.UI;

// public class u6_slider_ctrl : MonoBehaviour
// {
//     private Slider slider1;
//     [SerializeField] private GameObject parentJointBox;
//     [SerializeField] private GameObject childJointBox;
//     private float initialChildJointOffset = (float)(0.0754 / 100.0);
//     private float parentJointRadius = (float)(0.0754 / 100.0);

//     void Start()
//     {
//         slider1 = GameObject.Find("Slider1").GetComponent<Slider>();
//     }

//     public void AlterJoints()
//     {
//         float rotationValue1 = slider1.value * 360f;

//         // Set the parent's rotation
//         AlterJointWithVariables(rotationValue1, parentJointBox, Vector3.zero);

//         // Calculate offset position for the child joint
//         // Vector3 childJointOffsetPosition = Quaternion.Euler(0, rotationValue1, 0) * new Vector3(-initialChildJointOffset, 0, 0);
//         // Set the childJointBox's parent to parentJointBox
//         // SetParentAndAlterJointWithVariables(childJointBox, parentJointBox, childJointOffsetPosition, parentJointRadius);
//         SetParentAndAlterJointWithVariables(childJointBox, parentJointBox, parentJointRadius);
//     }

//     // Method to set parent and alter joint with variables
//     // private void SetParentAndAlterJointWithVariables(GameObject child, GameObject newParent, Vector3 offset, float parentRadius)
//     private void SetParentAndAlterJointWithVariables(GameObject child, GameObject newParent,  float parentRadius)
//     {
//         if (child != null && newParent != null)
//         {
//             // Set the parent of the childJointBox
//             child.transform.SetParent(newParent.transform, false);

//             // Alter joint positions
//             // AlterJointWithVariables(0f, child, offset + new Vector3(parentRadius, 0, 0));
//             AlterJointWithVariables(0f, child, new Vector3(parentRadius, 0, 0));
//         }
//         else
//         {
//             Debug.LogError("Child or newParent is null. Please assign them in the Unity Editor.");
//         }
//     }

//     public void AlterJointWithVariables(float rotationValue, GameObject jointBox, Vector3 offset)
//     {
//         if (jointBox != null)
//         {
//             Transform jointTransform = jointBox.transform;
//             jointTransform.localRotation = Quaternion.Euler(0, rotationValue, 0);
//             jointTransform.localPosition = offset;
//         }
//         else
//         {
//             Debug.LogError("Joint box is null. Please assign it in the Unity Editor.");
//         }
//     }
// }
















// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.UI;

// public class u6_slider_ctrl : MonoBehaviour
// {
//     private Slider slider1;
//     [SerializeField] private GameObject parentJointBox;
//     [SerializeField] private GameObject childJointBox;
//     // Initial offset of the child joint in the x-axis
//     private float initialChildJointOffset = (float)(0.0754 / 100.0);
//     // Adjust this value based on your scene
//     private float parentJointRadius = (float)(0.0754 / 100.0);

//     void Start()
//     {
//         slider1 = GameObject.Find("Slider1").GetComponent<Slider>();
//     }


//     public void AlterJoints()
//     {
//         float rotationValue1 = slider1.value * 360f;
//         // Set the parent's rotation
//         AlterJointWithVariables(rotationValue1, parentJointBox, Vector3.zero);

//         // Calculate offset position for the child joint
//         Vector3 childJointOffsetPosition = Quaternion.Euler(0, rotationValue1, 0) * new Vector3(-initialChildJointOffset, 0, 0) + new Vector3(parentJointRadius, 0, 0);
//         // Set the childJointBox's parent to parentJointBox
//         SetParentAndAlterJointWithVariables(childJointBox, parentJointBox, childJointOffsetPosition);
//     }

//     // Method to set parent and alter joint with variables
//     private void SetParentAndAlterJointWithVariables(GameObject child, GameObject newParent, Vector3 offset)
//     {
//         if (child != null && newParent != null)
//         {
//             child.transform.SetParent(newParent.transform);
//             AlterJointWithVariables(0f, child, offset);  // Set initial rotation to 0 for the child
//         }
//         else
//         {
//             Debug.LogError("Child or newParent is null. Please assign them in the Unity Editor.");
//         }
//     }




//     public void AlterJointWithVariables(float rotationValue, GameObject jointBox, Vector3 offset)
//     {
//         if (jointBox != null)
//         {
//             Transform jointTransform = jointBox.transform;
//             jointTransform.localRotation = Quaternion.Euler(0, rotationValue, 0);
//             jointTransform.localPosition = offset;
//         }
//         else
//         {
//             Debug.LogError("Joint box is null. Please assign it in the Unity Editor.");
//         }
//     }
// }


