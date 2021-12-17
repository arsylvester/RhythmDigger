using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "PlayerStates/Move")]
public class MovementState : PlayerState
{
    public AnimationCurve movementCurve;
    public override void Init(PlayerStateMachine playerMachine)
    {
        playerStateMachine = playerMachine;
        playerController = playerMachine.playerController;
    }
    public override void OnEnter()
    {
        base.OnEnter();
        playerController.anim.SetFloat("xDir", playerStateMachine.playerController.facingDir.x * playerController.anim.GetFloat("moveSpeed"));
        playerController.anim.SetFloat("yDir", playerStateMachine.playerController.facingDir.y * playerController.anim.GetFloat("moveSpeed"));
        playerController.anim.Play("Move");
    }


    public override void OnUpdate()
    {
        base.OnUpdate();
    }

    public override void OnFixedUpdate()
    {
        base.OnFixedUpdate();
        playerController.anim.SetFloat("xDir", playerStateMachine.playerController.facingDir.x * playerController.anim.GetFloat("moveSpeed"));
        playerController.anim.SetFloat("yDir", playerStateMachine.playerController.facingDir.y * playerController.anim.GetFloat("moveSpeed"));
        playerController.anim.SetFloat("moveSpeed", playerController.scaledTime);

        if (playerController.InputDir.x != 0f)
        {
            playerController.internalVelocity.x = (playerController.InputDir.x * playerController.movespeed * movementCurve.Evaluate(playerController.currentTime));
        }

    }

    public override void OnExit()
    {

    }

    public override void HandleState()
    {
        base.HandleState();
        if (playerController.InputDir.sqrMagnitude == 0f)
        {
            playerStateMachine.ChangeState(PlayerStateEnums.Idle);
        }
        if (playerStateMachine.playerController.button1Buffer > 0)
        {
            playerStateMachine.ChangeState(PlayerStateEnums.Attack);
        }
        if (!playerStateMachine.playerController.isGrounded)
        {
            playerStateMachine.ChangeState(PlayerStateEnums.Fall);
        }
    }
}
