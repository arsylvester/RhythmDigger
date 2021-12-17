using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "PlayerStates/Move")]
public class MovementState : PlayerState
{
    public AnimationCurve movementCurve;
    public float distance;
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
        float Xpos = playerController.InputDir.x < 0 ? Mathf.CeilToInt(playerController.transform.position.x) : Mathf.FloorToInt(playerController.transform.position.x);
        playerController.desiredPosition = new Vector3(Xpos + distance * (1.5f * playerController.InputDir.x) , playerController.transform.position.y, playerController.transform.position.z);
        playerController.moveBuffer = 0;
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

        //if (playerController.InputDir.x != 0f)
        {
            playerController.internalVelocity.x = (playerController.storedDir.x * playerController.movespeed * movementCurve.Evaluate(playerController.currentTime));
        }

    }

    public override void OnExit()
    {
        playerController.transform.position = new Vector3(playerController.desiredPosition.x, playerController.transform.position.y, playerController.transform.position.z);
        playerController.internalVelocity.x *= 0;
    }

    public override void HandleState()
    {
        base.HandleState();
        if (!playerStateMachine.playerController.isGrounded)
        {
            playerStateMachine.ChangeState(PlayerStateEnums.Fall);
        }
    }
}
