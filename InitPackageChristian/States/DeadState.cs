using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PlayerStates/Dead")]
public class DeadState : PlayerState
{

    public AudioClip deadSFX;

    public override void Init(PlayerStateMachine playerMachine)
    {
        playerStateMachine = playerMachine;
    }

    public override void OnEnter()
    {
        base.OnEnter();
        playerStateMachine.playerController.anim.SetFloat("xDir", playerStateMachine.playerController.facingDir.x);
        playerStateMachine.playerController.anim.SetFloat("yDir", playerStateMachine.playerController.facingDir.y);
        //playerStateMachine.playerController.anim.Play("Dead");       
        //playerStateMachine.playerController.canFall = false;
        //playerStateMachine.playerController.stack.gameObject.SetActive(false);
        playerStateMachine.playerController.audioSource.PlayOneShot(deadSFX);

        playerStateMachine.playerController.internalVelocity.x *= 0.8f;
        playerStateMachine.playerController.isDead = true;
    }

    public override void OnExit()
    {
    }

    public override void OnFixedUpdate()
    {

    }

    public override void OnUpdate()
    {
        base.OnUpdate();

    }

    public override void HandleState()
    {
    }
}