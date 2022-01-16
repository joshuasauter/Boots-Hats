using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]

public class AgentAnimation : MonoBehaviour
{
    protected Animator agentAnimator;

    private void Awake()
    {
        agentAnimator = GetComponent<Animator>();
    }

    public void SetDirectionAnimation(Vector2 val)
    {
        Vector3 directionVector = (Vector3)val - transform.position;
        float directionAngle = Vector3.Angle(directionVector, transform.right);
        if (val.y > transform.position.y)
        {
            agentAnimator.SetBool("Above", true);
        }
        else
        {
            agentAnimator.SetBool("Above", false);
        }
        agentAnimator.SetFloat("Direction", directionAngle);
    }

    public void SetWalkAnimation(bool val)
    {
        agentAnimator.SetBool("Walk", val);
    }

    public void AnimatePlayer(float velocity)
    {
       SetWalkAnimation(velocity > 0);
    }

    public void PlayDeathAnimation()
    {
        agentAnimator.SetTrigger("Death");
    }

    public void SetDodgeRollDirection(bool val)
    {
        agentAnimator.SetBool("Backwards", val);
    }

    public void SetWalkSpeed(int val)
    {
        agentAnimator.SetFloat("WalkMultiplier", val);
    }

    public void SetDodgeRollAnimation(bool val)
    {
        agentAnimator.SetBool("DodgeRoll", val);
    }
}
