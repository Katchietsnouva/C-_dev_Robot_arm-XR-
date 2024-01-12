// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.UI;
// using System.IO;


// public class u6_slider_ctrl : MonoBehaviour
// {
//     private Slider slider1;
//     private Slider slider2;
//     private Slider slider3;
//     private Slider slider4;
//     private Slider slider5;
//     private Slider slider6;
//     public GameObject parentObjectBox;
//     public GameObject childTransformBox;
//     private string logFilePath; // Path to the log file

//     public class jointVariables
//     {
//     public string J2 = "xArm6(XI1300) xArm6(XI1300) - Copy.STEP-1 J2.STEP-1";
//     public string J3 = "xArm6(XI1300) xArm6(XI1300) - Copy.STEP-1 J3.STEP-1";
//     }

//     // public JointVariables jointVariables = new JointVariables();

//     void Start()
//     {
//         // slider = GetComponent<Slider>();
//         slider1 = GameObject.Find("Slider1").GetComponent<Slider>();
//         slider2 = GameObject.Find("Slider2").GetComponent<Slider>();
//         slider3 = GameObject.Find("Slider3").GetComponent<Slider>();
//         slider4 = GameObject.Find("Slider4").GetComponent<Slider>();
//         slider5 = GameObject.Find("Slider5").GetComponent<Slider>();
//         slider6 = GameObject.Find("Slider6").GetComponent<Slider>();
//         logFilePath = Application.dataPath + "/u6_slider_ctrl_log.txt";
//     }

//     public void AlterJ2(jointVariables jointVars)
//     {
//         Transform robotTransform = parentObjectBox.transform;
//         float rotationValue1 = slider1.value * 360f;
//         Transform J2Transform = robotTransform.Find(jointVars.J2);
//         // If the desired joint is found, alter its rotation
       
//         J2Transform.localRotation = Quaternion.AngleAxis(rotationValue1, Vector3.up);      
//     }
// }




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
    // public GameObject parentObjectBox;
    // public GameObject childTransformBox;
    // [SerializeField] private string J2_Box = "xArm6(XI1300) xArm6(XI1300) - Copy.STEP-1 J2.STEP-1";
    [SerializeField] private GameObject ParentJointBox;
    [SerializeField] private GameObject childJointBox;
    
    private string logFilePath; // Path to the log file

    void Start()
    {
        slider1 = GameObject.Find("Slider1").GetComponent<Slider>();
        slider2 = GameObject.Find("Slider2").GetComponent<Slider>();
        slider3 = GameObject.Find("Slider3").GetComponent<Slider>();
        slider4 = GameObject.Find("Slider4").GetComponent<Slider>();
        slider5 = GameObject.Find("Slider5").GetComponent<Slider>();
        slider6 = GameObject.Find("Slider6").GetComponent<Slider>();
        logFilePath = Application.dataPath + "/u6_slider_ctrl_log.txt";
    }

    public void AlterJ2()
    {
        float rotationValue1 = slider1.value * 360f;
        AlterJ2WithJointVariables(rotationValue1);  // Pass only rotationValue1
    }

    public void AlterJ2WithJointVariables(float rotationValue1)
    {
        if (ParentJointBox != null)
        {
            Transform robotTransform = ParentJointBox.transform;
            robotTransform.localRotation = Quaternion.AngleAxis(rotationValue1, Vector3.up);
        }
    }
}






// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.UI;
// using System.IO;

// // public class JointVariables
// // {
// //     public string J2 = "xArm6(XI1300) xArm6(XI1300) - Copy.STEP-1 J2.STEP-1";
// //     public string J3 = "xArm6(XI1300) xArm6(XI1300) - Copy.STEP-1 J3.STEP-1";
// // }

// public class u6_slider_ctrl : MonoBehaviour
// {
//     private Slider slider1;
//     private Slider slider2;
//     private Slider slider3;
//     private Slider slider4;
//     private Slider slider5;
//     private Slider slider6;
//     public GameObject parentObjectBox;
//     public GameObject childTransformBox;
//     private string logFilePath; // Path to the log file

//     // public JointVariables jointVariables = new JointVariables();
//     string J2 = "xArm6(XI1300) xArm6(XI1300) - Copy.STEP-1 J2.STEP-1";
//     void Start()
//     {
//         slider1 = GameObject.Find("Slider1").GetComponent<Slider>();
//         slider2 = GameObject.Find("Slider2").GetComponent<Slider>();
//         slider3 = GameObject.Find("Slider3").GetComponent<Slider>();
//         slider4 = GameObject.Find("Slider4").GetComponent<Slider>();
//         slider5 = GameObject.Find("Slider5").GetComponent<Slider>();
//         slider6 = GameObject.Find("Slider6").GetComponent<Slider>();
//         logFilePath = Application.dataPath + "/u6_slider_ctrl_log.txt";
//     }

//     public void AlterJ2(string J2)
//     {
//         float rotationValue1 = slider1.value * 360f;
//         AlterJ2WithJointVariables(rotationValue1, J2);
//     }

//     public void AlterJ2WithJointVariables(float rotationValue1, string J2)
//     {
//         Transform robotTransform = parentObjectBox.transform;
//         // Transform J2Transform = robotTransform.Find(jointVariables.J2);
//         Transform J2Transform = robotTransform.Find(J2);
//         J2Transform.localRotation = Quaternion.AngleAxis(rotationValue1, Vector3.up);
//     }
// }









// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.UI;
// using System.IO;

// public class JointVariables
// {
//     public string J2 = "xArm6(XI1300) xArm6(XI1300) - Copy.STEP-1 J2.STEP-1";
//     public string J3 = "xArm6(XI1300) xArm6(XI1300) - Copy.STEP-1 J3.STEP-1";
// }

// public class u6_slider_ctrl : MonoBehaviour
// {
//     private Slider slider1;
//     private Slider slider2;
//     private Slider slider3;
//     private Slider slider4;
//     private Slider slider5;
//     private Slider slider6;
//     public GameObject parentObjectBox;
//     public GameObject childTransformBox;
//     private string logFilePath; // Path to the log file

//     public JointVariables jointVariables = new JointVariables();

//     void Start()
//     {
//         slider1 = GameObject.Find("Slider1").GetComponent<Slider>();
//         slider2 = GameObject.Find("Slider2").GetComponent<Slider>();
//         slider3 = GameObject.Find("Slider3").GetComponent<Slider>();
//         slider4 = GameObject.Find("Slider4").GetComponent<Slider>();
//         slider5 = GameObject.Find("Slider5").GetComponent<Slider>();
//         slider6 = GameObject.Find("Slider6").GetComponent<Slider>();
//         logFilePath = Application.dataPath + "/u6_slider_ctrl_log.txt";
//     }

//     public void AlterJ2(JointVariables jointVars)
//     {
//         Transform robotTransform = parentObjectBox.transform;
//         float rotationValue1 = slider1.value * 360f;
//         Transform J2Transform = robotTransform.Find(jointVars.J2);
//         if (J2Transform != null)
//         {
//             J2Transform.localRotation = Quaternion.AngleAxis(rotationValue1, Vector3.up);
//         }
//     }
// }















    // public void AlterJ2(string J2)
    // {
    //     Transform robotTransform = parentObjectBox.transform;
    //     float rotationValue1 = slider1.value * 360f;
    //     Transform J2Transform = robotTransform.Find(J2);

    //     // If the desired joint is found, alter its rotation
    //     J2Transform.localRotation = Quaternion.AngleAxis(rotationValue1, Vector3.up);
    //     // Now, let's assume J3 is a child of J2
    //     // Transform robotTransformchild = childTransformBox.transform;
    //     // Transform J3Transform = robotTransformchild.Find(J3);
    //     // if (J3Transform != null)
    //     // {
    //     //     J3Transform.localRotation = Quaternion.AngleAxis(rotationValue1, Vector3.right);
    //     //     //J3Transform.localPosition = new Vector3(0f, 0f, 10f); // Adjust the distance accordingly
    //     // }
    // }





















// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.UI;
// using System.IO;

// public class u6_slider_ctrl : MonoBehaviour
// {
//     private Slider slider1;
//     private Slider slider2;
//     private Slider slider3;
//     private Slider slider4;
//     private Slider slider5;
//     private Slider slider6;
//     [SerializeField] Slider rotationSlider;
//     public GameObject parentObjectBox;
//     public GameObject childTransformBox;
//     private string logFilePath; // Path to the log file

//     string J2 = "xArm6(XI1300) xArm6(XI1300) - Copy.STEP-1 J2.STEP-1";
//     string J3 = "xArm6(XI1300) xArm6(XI1300) - Copy.STEP-1 J3.STEP-1";

//     void Start()
//     {
//         slider1 = GameObject.Find("Slider1").GetComponent<Slider>();
//         slider2 = GameObject.Find("Slider2").GetComponent<Slider>();
//         slider3 = GameObject.Find("Slider3").GetComponent<Slider>();
//         slider4 = GameObject.Find("Slider4").GetComponent<Slider>();
//         slider5 = GameObject.Find("Slider5").GetComponent<Slider>();
//         slider6 = GameObject.Find("Slider6").GetComponent<Slider>();
//         logFilePath = Application.dataPath + "/u6_slider_ctrl_log.txt"; // Set the path for the log file
//     }

//     public void ChangeRotation()
//     {
//         slider1.value = rotationSlider.value;
//         // Assuming J2 and J3 are the strings you want to pass
//         string J2 = "xArm6(XI1300) xArm6(XI1300) - Copy.STEP-1 J2.STEP-1";
//         string J3 = "xArm6(XI1300) xArm6(XI1300) - Copy.STEP-1 J3.STEP-1";
//         AlterJ2(J2);
//     }


//     public void AlterJ2(string J2)
//     {
//         slider1.value = rotationSlider.value;
//         Transform robotTransform = parentObjectBox.transform;
//         float rotationValue1 = slider1.value * 360f;
//         // float rotationValue1 = RotationListener.value * 360f;
//         Transform J2Transform = robotTransform.Find(J2);

//         // If the desired joint is found, alter its rotation
//         J2Transform.localRotation = Quaternion.AngleAxis(rotationValue1, Vector3.up);
//     }

//     public void AlterJ3(string J3)
//     {
//         Transform robotTransformchild = childTransformBox.transform;
//         float rotationValue2 = slider2.value * 360f;
//         Transform J2Transform = robotTransformchild.Find(J3);
//         // If the desired joint is found, alter its rotation
//         J2Transform.localRotation = Quaternion.AngleAxis(rotationValue2, Vector3.up);
//     }
// }



























// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.UI;
// using System.IO;

// public class u6_slider_ctrl : MonoBehaviour
// {
//     private Slider slider1;
//     private Slider slider2;
//     private Slider slider3;
//     private Slider slider4;
//     private Slider slider5;
//     private Slider slider6;
//     [SerializeField] Slider rotationSlider;
//     public GameObject parentObjectBox;
//     public GameObject childTransformBox;
//     private string logFilePath; // Path to the log file

//     string J2 = "xArm6(XI1300) xArm6(XI1300) - Copy.STEP-1 J2.STEP-1";
//     string J3 = "xArm6(XI1300) xArm6(XI1300) - Copy.STEP-1 J3.STEP-1";

//     void Start()
//     {
//         slider1 = GameObject.Find("Slider1").GetComponent<Slider>();
//         slider2 = GameObject.Find("Slider2").GetComponent<Slider>();
//         slider3 = GameObject.Find("Slider3").GetComponent<Slider>();
//         slider4 = GameObject.Find("Slider4").GetComponent<Slider>();
//         slider5 = GameObject.Find("Slider5").GetComponent<Slider>();
//         slider6 = GameObject.Find("Slider6").GetComponent<Slider>();
//         logFilePath = Application.dataPath + "/u6_slider_ctrl_log.txt"; // Set the path for the log file
//     }

//     public void ChangeRotation()
//     {
//         RotationListener.value = rotationSlider.value;

//     }

//      public void AlterJ2(string J2, string J3)
//     {
//         Transform robotTransform = parentObjectBox.transform;
//         // float rotationValue1 = slider1.value * 360f;
//         float rotationValue1 = RotationListener.value * 360f;
//         Transform J2Transform = robotTransform.Find(J2);

//         // If the desired joint is found, alter its rotation
//         J2Transform.localRotation = Quaternion.AngleAxis(rotationValue1, Vector3.up);
//         // Now, let's assume J3 is a child of J2
//         Transform robotTransformchild = childTransformBox.transform;
//         Transform J3Transform = robotTransformchild.Find(J3);
//         if (J3Transform != null)
//         {
//             J3Transform.localRotation = Quaternion.AngleAxis(rotationValue1, Vector3.right);
//             //J3Transform.localPosition = new Vector3(0f, 0f, 10f); // Adjust the distance accordingly
//         }
//     }

//     public void AlterJ3(string J3)
//     {
//         Transform robotTransformchild  = childTransformBox.transform;
//         float rotationValue2 = slider2.value * 360f;
//         Transform J2Transform = robotTransformchild .Find(J3);
//         // If the desired joint is found, alter its rotation
//         J2Transform.localRotation = Quaternion.AngleAxis(rotationValue2, Vector3.up);
//     }

//     private void LogToFile(string logMessage)
//     {
//         using (StreamWriter sw = File.AppendText(logFilePath))
//         {
//             sw.WriteLine(logMessage);
//         }
//     }
// }
