using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class u6_buttons_ctrl : MonoBehaviour
{
    public GameObject U6robot3DBox;
    private void ExecuteTrigger(string trigger)
    {
        if(U6robot3DBox != null)
        {
            var animator = U6robot3DBox.GetComponent<Animator>();
            if(animator != null)
            {
                animator.SetTrigger(trigger);
            }
        }
    }
    private void OnOpenButtonClick()
    {
        ExecuteTrigger("TrJ2L");
    }

    private void OnCloseButtonClick()
    {
        ExecuteTrigger("TrJ2R");    
    }
}
