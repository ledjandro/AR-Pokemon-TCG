using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class OnTriggerInteraction : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private Animator remoAnimator;
    private void OnTriggerEnter(Collider other)
    {
        remoAnimator.SetBool("IsInteracting",true);
    }

    private void OnTriggerExit(Collider other)
    {
        remoAnimator.SetBool("IsInteracting",false);
    }
}
