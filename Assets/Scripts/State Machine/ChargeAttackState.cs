using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PlayerStates/ChargeAttack")]
public class ChargeAttackState : PlayerState
{
    public override void Init(PlayerStateMachine playerMachine)
    {
        playerStateMachine = playerMachine;
        playerController = playerMachine.playerController;
    }

    public override void OnEnter()
    {
        base.OnEnter(); playerStateMachine.playerController.canRotate = false;
        playerStateMachine.playerController.internalVelocity.y = 0;
        playerStateMachine.playerController.anim.SetFloat("xDir", playerStateMachine.playerController.facingDir.x);
        playerStateMachine.playerController.anim.SetFloat("yDir", playerStateMachine.playerController.facingDir.y);
        if (playerController.storedDir == Vector2.down)
            playerController.anim.Play("VChargeAttack");
        else
        {
            playerController.anim.Play("ChargeAttack");
        }

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
        playerStateMachine.playerController.canRotate = true;
    }

    public override void HandleState()
    {
        base.HandleState();


        //if (!playerStateMachine.playerController.isGrounded)
        //{
        //playerStateMachine.ChangeState(PlayerStateEnums.Fall);
        //}
        //if (playerStateMachine.playerController.InputDir.x !=0)
        //{
        //    playerStateMachine.ChangeState(PlayerStateEnums.Move);
        //}
        //{
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

