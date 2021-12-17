using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PlayerStates/Idle")]
public class IdleState : PlayerState
{


    //IF IDLE STATE FOR LONGER THAN 10 FRAMES AND KILLED ENEMY  DO RELOAD //THIS WAY COMBOS AREN'T INTERRUPTED
    public override void Init(PlayerStateMachine playerMachine)
    {
        playerStateMachine = playerMachine;
    }

    public override void OnEnter()
    {
        base.OnEnter();
        playerStateMachine.playerController.internalVelocity.y = 0;
        playerStateMachine.playerController.anim.SetFloat("xDir", playerStateMachine.playerController.facingDir.x);
        playerStateMachine.playerController.anim.SetFloat("yDir", playerStateMachine.playerController.facingDir.y);
        playerStateMachine.playerController.anim.Play("Idle");
        playerStateMachine.playerController.airJumpCount = playerStateMachine.playerController.maxAirJumps;
        playerStateMachine.playerController.airDodgeCount = playerStateMachine.playerController.maxAirDodges;
        playerStateMachine.playerController.canJump = true;
    }


    public override void OnUpdate()
    {
        base.OnUpdate();
    }

    public override void OnFixedUpdate()
    {
        base.OnFixedUpdate();
    }

    public override void OnExit()
    {

    }

    public override void HandleState()
    {
        base.HandleState();


        if (!playerStateMachine.playerController.isGrounded)
        {
            playerStateMachine.ChangeState(PlayerStateEnums.Fall);
        }
        if (playerStateMachine.playerController.InputDir.x !=0)
        {
            playerStateMachine.ChangeState(PlayerStateEnums.Move);
        }
        {
        //    if (playerStateMachine.playerController.button2Buffer > 0)
        //    {
        //        playerStateMachine.ChangeState(PlayerStateEnums.Jump);
        //    }
        //    if (playerStateMachine.playerController.currentTime > 6)
        //    {
        //        playerStateMachine.playerController.sprintMod = 1;
        //        playerStateMachine.playerController.anim.SetFloat("moveSpeed", 1);
        //    }
        //    if (playerStateMachine.playerController.button3Buffer > 0
        //        && playerStateMachine.playerController.airDodgeCount > 0)
        //    {
        //        playerStateMachine.ChangeState(PlayerStateEnums.Dodge);
        //    }
        }
    }
}

