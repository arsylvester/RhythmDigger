using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "PlayerStates/ChargeAttackMove")]
public class ChargeAttackMoveState : PlayerState
{
    public override void Init(PlayerStateMachine playerMachine)
    {
        playerStateMachine = playerMachine;
    }

    public override void OnEnter()
    {
        base.OnEnter();
        playerStateMachine.playerController.anim.SetFloat("xDir", playerStateMachine.playerController.facingDir.x);
        playerStateMachine.playerController.anim.SetFloat("yDir", playerStateMachine.playerController.facingDir.y);
        playerStateMachine.playerController.anim.Play("ChargeAttackMove");
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
        if (playerStateMachine.playerController.InputDir.x != 0)
        {
            playerStateMachine.ChangeState(PlayerStateEnums.Move);
        }
    }
}
