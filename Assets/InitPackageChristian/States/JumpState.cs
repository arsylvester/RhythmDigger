using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(menuName = "PlayerStates/Jump")]
public class JumpState : PlayerState

{
    public float jumpForce;
    public float airJumpForce;
    public float minYVel;
    public AnimationCurve movementCurve;
    public AnimationCurve gravityModCurve;
    public AudioClip jumpSFX;
    public AudioClip airJumpSFX;
    public AudioClip superJumpSFX;

    public override void Init(PlayerStateMachine playerMachine)
    {
        playerStateMachine = playerMachine;
        playerController = playerMachine.playerController;
    }
    public override void OnEnter()
    {
        base.OnEnter();
        playerController.anim.SetFloat("xDir", playerStateMachine.playerController.facingDir.x);
        playerController.anim.SetFloat("yDir", playerStateMachine.playerController.facingDir.y);
        playerController.anim.Play("Dodge", 0, 0);

        if (playerController.coyoteTime <= 0)
        {
            playerStateMachine.playerController.airJumpCount--;
            playerStateMachine.playerController.internalVelocity.y = airJumpForce;
            if (playerStateMachine.playerController.externalVelocity.y > 10)
                playerStateMachine.playerController.audioSource.PlayOneShot(superJumpSFX);
            else
                playerStateMachine.playerController.audioSource.PlayOneShot(airJumpSFX);
        }
		else
		{
            playerStateMachine.playerController.internalVelocity.y = jumpForce;
            if (playerStateMachine.playerController.externalVelocity.y > 10)
                playerStateMachine.playerController.audioSource.PlayOneShot(superJumpSFX);
            else
                playerStateMachine.playerController.audioSource.PlayOneShot(jumpSFX);
        }
    }

    public override void OnExit()
    {
    }

    public override void OnFixedUpdate()
    {
        playerController.gravityMod = gravityModCurve.Evaluate(playerController.currentTime);
        if (playerController.InputDir.x != 0f)
        {
            playerController.internalVelocity.x = (playerController.InputDir.x * playerController.movespeed * movementCurve.Evaluate(playerController.currentTime));
        }
    }

    public override void OnUpdate()
    {
        HandleState();
    }

    public override void HandleState()
    {
        base.HandleState();

        if (playerStateMachine.playerController.internalVelocity.y < minYVel)
        {
            playerStateMachine.ChangeState(PlayerStateEnums.Fall);
        }

        if (playerStateMachine.playerController.isGrounded && playerStateMachine.playerController.currentTime > 10)
        {
            playerStateMachine.ChangeState(PlayerStateEnums.Idle);
        }
        { 
    //    if (playerStateMachine.playerController.button3Buffer > 0
    //          && playerStateMachine.playerController.airDodgeCount > 0)
    //    {
    //        playerStateMachine.ChangeState(PlayerStateEnums.Dodge);
    //    }
    //    if (playerStateMachine.playerController.button2ReleaseBuffer > 0)
    //    {
    //        playerStateMachine.ChangeState(PlayerStateEnums.Fall);
    //    }
        }
    }
}