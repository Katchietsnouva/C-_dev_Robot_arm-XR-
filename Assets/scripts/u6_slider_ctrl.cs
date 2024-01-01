using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class u6_slider_ctrl : MonoBehaviour
{    
    private Slider slider1;
    private Slider slider2;
    public GameObject U6robot3DBox;
    void Start()
    {
        // slider = GetComponent<Slider>();
        slider1 = GameObject.Find("Slider1").GetComponent<Slider>();
        slider2 = GameObject.Find("Slider2").GetComponent<Slider>();
    }

    public void ApplyAnimation()
    {
        if (U6robot3DBox != null)
        {
            Animator animator = U6robot3DBox.GetComponent<Animator>();
            if (animator != null)
            {
                animator.SetFloat("J2val", slider1.value);
                animator.SetFloat("J3val", slider2.value);
            }

        }

    }
}
