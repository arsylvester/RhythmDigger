using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PlayerStates/Fall")]
public class FallState : PlayerState
{
    [SerializeField]
    private AnimationCurve gravityModCurve;
    [SerializeField]
    private AnimationCurve movementCurve;

    public override void Init(PlayerStateMachine playerMachine)
    {
        playerStateMachine = playerMachine;
        playerController = playerMachine.playerController;
    }

    public override void OnEnter()
    {
        base.OnEnter();
        playerStateMachine.playerController.anim.SetFloat("xDir", playerStateMachine.playerController.facingDir.x);
        playerStateMachine.playerController.anim.SetFloat("yDir", playerStateMachine.playerController.facingDir.y);
		playerController.gravityMod = gravityModCurve.Evaluate(playerController.internalVelocity.y);
        playerController.anim.Play("Fall");

    }

    public override void OnExit()
    {
        playerController.gravityMod = 1;
    }

    public override void OnFixedUpdate()
    {
        if (playerController.InputDir.x != 0f)
        {
            playerController.internalVelocity.x = (playerController.InputDir.x * playerController.movespeed * movementCurve.Evaluate(playerController.currentTime));
        }
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

    }

    public override void HandleState()
    {
        base.HandleState();

        if (playerStateMachine.playerController.isGrounded)
        {
            playerStateMachine.ChangeState(PlayerStateEnums.Land);
        }
        {
            //if (playerStateMachine.playerController.button3Buffer > 0
            //    && playerStateMachine.playerController.airDodgeCount > 0)
            //{
            //    playerStateMachine.ChangeState(PlayerStateEnums.Dodge);
            //}



            //if (playerStateMachine.playerController.button2Buffer > 0
            //    && playerStateMachine.playerController.airJumpCount > 0
            //    && playerStateMachine.playerController.button2ReleaseBuffer == 0)
            //{
            //    playerStateMachine.ChangeState(PlayerStateEnums.Jump);
            //}
        }
    }
}

