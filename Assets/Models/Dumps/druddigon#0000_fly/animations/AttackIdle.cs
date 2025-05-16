using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class AttackIdle : MonoBehaviour
{
    private Animator mAnimator;

    void Start(){
        mAnimator = GetComponent<Animator>();

    }
    void Update(){
        if(mAnimator != null){
            if(Input.GetKeyDown(KeyCode.O)){
                mAnimator.SetTrigger("TrAttack");
            }
            if(Input.GetKeyDown(KeyCode.C)){
                mAnimator.SetTrigger("TrIdle");
            }
        }
    }
}
