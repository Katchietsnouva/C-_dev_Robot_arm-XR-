using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class u6_ctrl_ : MonoBehaviour
{
    private Animator mAnimator;
    // Start is called before the first frame update
    void Start()
    {
        mAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(mAnimator != null)
        {
            if(Input.GetKeyDown(KeyCode.Q))
            {
                mAnimator.SetTrigger("TrJ2L");
            }
            if(Input.GetKeyDown(KeyCode.W))
            {
                mAnimator.SetTrigger("TrJ2R");
            }
            if(Input.GetKeyDown(KeyCode.E))
            {
                mAnimator.SetTrigger("TrJ3L");
            }
            if(Input.GetKeyDown(KeyCode.R))
            {
                mAnimator.SetTrigger("TrJ3R");
            }
        }
    }
}

