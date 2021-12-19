using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PlayerStates/Hurt")]
public class HurtState : PlayerState
{

    [SerializeField]
    private Vector2 iframes;
    [SerializeField]
    private AnimationCurve hurtFriction;
    public AudioClip hurtSFX;

    public override void Init(PlayerStateMachine playerMachine)
    {
        playerStateMachine = playerMachine;
    }
    public override void OnEnter()
    {
        base.OnEnter();
        playerStateMachine.playerController.anim.SetFloat("xDir", playerStateMachine.playerController.externalVelocity.x);
        playerStateMachine.playerController.anim.SetFloat("xDir", playerStateMachine.playerController.externalVelocity.y);
       // playerStateMachine.playerController.canBounce = true;
        //playerStateMachine.playerController.canJump = false;
        //playerStateMachine.playerController.audioSource.PlayOneShot(hurtSFX);
        playerStateMachine.playerController.anim.Play("Hurt");
    }

    public override void OnExit()
    {
        playerStateMachine.playerController.myTang = ObjectTangibility.Normal;
       // playerStateMachine.playerController.canBounce = false;
        //playerStateMachine.playerController.canJump = true;
    }

    public override void OnFixedUpdate()
    {
        playerStateMachine.playerController.externalVelocity *= hurtFriction.Evaluate(playerStateMachine.playerController.currentTime);
        if (playerStateMachine.playerController.currentTime >= iframes.x && playerStateMachine.playerController.currentTime <= iframes.y)
        {
            playerStateMachine.playerController.myTang = ObjectTangibility.Invincible;
        }
        else
        {
            playerStateMachine.playerController.myTang = ObjectTangibility.Normal;
        }
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

    }

    public override void HandleState()
    {
        base.HandleState();
    }
}
