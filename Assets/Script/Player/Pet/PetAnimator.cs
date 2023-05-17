using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetAnimator : MonoBehaviour
{
    public PlayerController playerController;
    public Animator animator;
    void Start()
    {
        ReadPlayerStateAndAnimate();
    }
    void Update()
    {
        ReadPlayerStateAndAnimate();
    }
    void ReadPlayerStateAndAnimate()
    {
        if (animator == null)
        {
            return;
        }
        if (playerController.state == PlayerController.PlayerState.Idle)
        {
            animator.SetBool("isIdle", true);
        }
        else
        {
            animator.SetBool("isIdle", false);
        }
        if (playerController.state == PlayerController.PlayerState.Hurt)
        {
            animator.SetBool("isHurt", true);
        }
        else
        {
            animator.SetBool("isHurt", false);
        }
        if(playerController.state != PlayerController.PlayerState.Hurt)
        {
            animator.SetBool("isIdle", true);
        }
    }
}
