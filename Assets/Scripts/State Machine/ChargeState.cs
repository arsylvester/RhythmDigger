using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PlayerStates/Charge")]
public class ChargeState : PlayerState
{
    public override void Init(PlayerStateMachine playerMachine)
    {
        playerStateMachine = playerMachine;
    }

    public override void OnEnter()
    {
        base.OnEnter();
        playerStateMachine.playerController.anim.SetFloat("xDir", playerStateMachine.playerController.internalVelocity.x);
        playerStateMachine.playerController.anim.SetFloat("yDir", playerStateMachine.playerController.internalVelocity.y);
        playerStateMachine.playerController.anim.Play("Charge");
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
    }

    public override void OnFixedUpdate()
    {
        base.OnFixedUpdate();
        playerStateMachine.playerController.internalVelocity *= 0.75f;
    }


    public override void OnExit()
    {


    }

    public override void HandleState()
    {
        base.HandleState();
    }
}