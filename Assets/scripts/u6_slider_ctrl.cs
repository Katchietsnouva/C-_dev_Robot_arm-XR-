// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.UI;

// public class u6_slider_ctrl : MonoBehaviour
// {    
//     private Slider slider1;
//     private Slider slider2;
//     private Slider slider3;
//     public GameObject U6robot3DBox;
//     void Start()
//     {
//         // slider = GetComponent<Slider>();
//         slider1 = GameObject.Find("Slider1").GetComponent<Slider>();
//         slider2 = GameObject.Find("Slider2").GetComponent<Slider>();
//         slider3 = GameObject.Find("Slider3").GetComponent<Slider>();
//     }

//     public void ApplyAnimation()
//     {
//         if (U6robot3DBox != null)
//         {
//             Animator animator = U6robot3DBox.GetComponent<Animator>();
//             if (animator != null)
//             {
//                 // Retrieve the current state of the animator
//                 AnimatorStateInfo currentState = animator.GetCurrentAnimatorStateInfo(0);
//                 // Calculate the normalized time to smoothly blend between the current and new state
//                 float blendTime = Mathf.Clamp01(currentState.normalizedTime);
//                 // Set the blend time for each parameter based on the slider values
//                 animator.SetFloat("J2val", slider1.value,blendTime);
//                 animator.SetFloat("J3val", slider2.value);
//                 animator.SetFloat("J4val", slider3.value);
//             }

//         }

//     }
// }



using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RobotArmController : MonoBehaviour
{
    public Slider slider1;
    public Slider slider2;
    public GameObject U6robot3DBox;
    private Animator animator;

    private const string J2_PARAMETER_NAME = "J2val";
    private const string J3_PARAMETER_NAME = "J3val";

    void Start()
    {
        animator = U6robot3DBox.GetComponent<Animator>();
    }

    void Update()
    {
        if (U6robot3DBox != null && animator != null)
        {
            // Get the current state information
            AnimatorStateInfo currentState = animator.GetCurrentAnimatorStateInfo(0);

            // Crossfade to the new state based on slider values
            animator.CrossFade(J2_PARAMETER_NAME, slider1.value, 0, currentState.normalizedTime);
            animator.CrossFade(J3_PARAMETER_NAME, slider2.value, 0, currentState.normalizedTime);
        }
    }
}
