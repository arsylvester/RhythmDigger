using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PlayerStates/Idle")]
public class IdleState : PlayerState
{
    public bool requireBeat = true;

    //IF IDLE STATE FOR LONGER THAN 10 FRAMES AND KILLED ENEMY  DO RELOAD //THIS WAY COMBOS AREN'T INTERRUPTED
    public override void Init(PlayerStateMachine playerMachine)
    {
        playerStateMachine = playerMachine;
        playerController = playerMachine.playerController;
    }

    public override void OnEnter()
    {
        base.OnEnter();
        playerStateMachine.playerController.internalVelocity.y = 0;
        playerStateMachine.playerController.anim.SetFloat("xDir", playerStateMachine.playerController.facingDir.x);
        playerStateMachine.playerController.anim.SetFloat("yDir", playerStateMachine.playerController.facingDir.y);
        playerStateMachine.playerController.anim.Play("Idle");
        //playerStateMachine.playerController.airJumpCount = playerStateMachine.playerController.maxAirJumps;
        //playerStateMachine.playerController.airDodgeCount = playerStateMachine.playerController.maxAirDodges;
        //playerStateMachine.playerController.canJump = true;
        //playerStateMachine.playerController.GetComponent<Rigidbody2D>().sharedMaterial.bounciness = -100;
        playerController.canRotate = true;
        playerController.canBounce = false;
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
        if ((playerStateMachine.playerController.InputDir.x !=0 || playerStateMachine.playerController.InputDir.y == -1) && playerController.moveBuffer > 0)
        {
            playerController.moveBuffer = 0;
            if (Conductor.Instance.CheckValidBeat() || !requireBeat)
            {
                if (playerController.InputDir == Vector2.left || playerController.InputDir == Vector2.right || playerController.InputDir == Vector2.down)
                    playerController.storedDir = playerController.InputDir;
                bool canMove = playerController.CheckOpenBlock(playerController.storedDir);
                if (canMove )
                {
                    playerStateMachine.ChangeState(PlayerStateEnums.Move);
                    //Debug.Log("Normal Move");
                }
                else
                {
                    if (!playerController.CheckBlock(playerController.storedDir, 1) && (playerController.InputDir == Vector2.left || playerController.InputDir == Vector2.right))
                    {
                        //Debug.Log("Move Dig");
                        playerStateMachine.ChangeState(PlayerStateEnums.AttackMove);
                    }
                    else
                    {
                        //Debug.Log("Dig");
                        playerStateMachine.ChangeState(PlayerStateEnums.Attack);
                    }
                }
            }
			else if(!Conductor.Instance.CheckValidBeat())
			{
                playerStateMachine.ChangeState(PlayerStateEnums.Hurt);
			}
        }
        if (playerStateMachine.playerController.button1Buffer > 0 && playerStateMachine.playerController.button1Hold)
        {
			if (Conductor.Instance.CheckValidBeat() || !requireBeat)
			{
                    playerStateMachine.ChangeState(PlayerStateEnums.Charge);

            }
            else if (!Conductor.Instance.CheckValidBeat())
            {
                playerStateMachine.ChangeState(PlayerStateEnums.Hurt);
            }

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

