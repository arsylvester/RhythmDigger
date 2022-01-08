using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PlayerStates/Dead")]
public class DeadState : PlayerState
{
    public PhysicsMaterial2D bounceMat;

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
        playerStateMachine.playerController.anim.Play("Dead");       
        //playerStateMachine.playerController.canFall = false;
        //playerStateMachine.playerController.stack.gameObject.SetActive(false);
        playerStateMachine.playerController.audioSource.PlayOneShot(deadSFX);
        playerStateMachine.playerController.canBounce = true;
        //playerStateMachine.playerController.isDead = true;
        playerStateMachine.playerController.GetComponent<Rigidbody2D>().sharedMaterial = bounceMat;
    }

    public override void OnExit()
    {
        playerStateMachine.playerController.GetComponent<Rigidbody2D>().sharedMaterial = null;
    }

    public override void OnFixedUpdate()
    {
        playerStateMachine.playerController.internalVelocity *= playerStateMachine.playerController.friction;
        if (playerStateMachine.playerController.currentTime > maxTime) 
        if ((Mathf.RoundToInt(playerStateMachine.playerController.externalVelocity.sqrMagnitude * 100) / 100) <= 0.1f && !playerStateMachine.playerController.isDead && playerStateMachine.playerController.isGrounded)
            playerStateMachine.playerController.PlayerDeath();
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

    }

    public override void HandleState()
    {
    }
}