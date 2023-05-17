using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
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

        if (playerController.state == PlayerController.PlayerState.Jump)
        {
            animator.SetBool("isJumping", true);
        }
        else
        {
            animator.SetBool("isJumping", false);
        }

        if (playerController.state == PlayerController.PlayerState.Slide)
        {
            animator.SetBool("isSlideing", true);
        }
        else
        {
            animator.SetBool("isSlideing", false);
        }

        if (playerController.state == PlayerController.PlayerState.DoubleJump)
        {
            animator.SetBool("isDoubleJumping", true);
        }
        else
        {
            animator.SetBool("isDoubleJumping", false);
        }
        if (playerController.state == PlayerController.PlayerState.Hurt)
        {
            animator.SetBool("isHurt", true);
            //playerController.HurtPlayed();
        }
        else
        {
            animator.SetBool("isHurt", false);
        }

        if (playerController.state == PlayerController.PlayerState.Dead)
        {
            animator.SetBool("isDie", true);
        }
        else
        {
            animator.SetBool("isDie", false);
        }
        if (playerController.state == PlayerController.PlayerState.Ability)
        {
            animator.SetBool("isAbility", true);
        }
        else
        {
            animator.SetBool("isAbility", false);
        }
    }
}
