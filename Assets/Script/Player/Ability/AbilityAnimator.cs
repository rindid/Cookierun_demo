using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityAnimator : MonoBehaviour
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

        if (playerController.state == PlayerController.PlayerState.Ability)
        {
            animator.SetBool("isAbility", true);
            animator.SetFloat("AbilityDurationTime", GameManager.Instance.GetAbilityDurationTime() - playerController.abilityTimeElipsed);
        }
        else
        {
            animator.SetBool("isAbility", false);
        }
        if (playerController.state != PlayerController.PlayerState.Ability)
        {
            animator.SetBool("isIdle", true);
        }
    }
}
