using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PlayerStates/Charge")]
public class ChargeState : PlayerState
{
    public int minTime;
    public bool requireBeat;
    public GameObject chargeFX;
    public override void Init(PlayerStateMachine playerMachine)
    {
        playerStateMachine = playerMachine;
        playerController = playerMachine.playerController;
    }

    public override void OnEnter()
    {
        base.OnEnter();

        playerStateMachine.playerController.anim.Play("Charge");

    }

    public override void OnUpdate()
    {
        base.OnUpdate();
    }

    public override void OnFixedUpdate()
    {
        base.OnFixedUpdate();
        playerStateMachine.playerController.anim.SetFloat("yDir", Mathf.Abs(playerStateMachine.playerController.storedDir.y));
    }

    public override void OnExit()
    {

    }

    public override void HandleState()
    {
        if ((!playerStateMachine.playerController.button1Hold || playerStateMachine.playerController.button1ReleaseBuffer > 0) )
        {
            if (playerController.currentTime > minTime)
            {
                playerController.moveBuffer = 0;
                if (playerController.animatePlayer || Conductor._instance.CheckValidBeat() || !requireBeat)
                {
                    if (playerController.InputDir == Vector2.left || playerController.InputDir == Vector2.right || playerController.InputDir == Vector2.down)
                        playerController.storedDir = playerController.InputDir;

                    Instantiate(chargeFX, playerController.transform.position + new Vector3(playerController.storedDir.x, 0.5f + playerController.storedDir.y, 0), Quaternion.identity);
                    Instantiate(chargeFX, playerController.transform.position + new Vector3(playerController.storedDir.x * 2, 0.5f + playerController.storedDir.y *2, 0), Quaternion.identity);
                    bool canMove = playerController.CheckOpenBlock(playerController.storedDir);
                    if (canMove)
                    {
                        playerController.CheckBlock(playerController.storedDir * 2, 3);

                        playerStateMachine.ChangeState(PlayerStateEnums.ChargeAttackMove);
                        //Debug.Log("Normal Move");
                    }
                    else
                    {
                        if (!playerController.CheckBlock(playerController.storedDir, 3) && (playerController.storedDir == Vector2.left || playerController.storedDir == Vector2.right))
                        {
                            // Debug.Log("Move Dig");
                            playerController.CheckBlock(playerController.storedDir * 2, 3);
                            playerStateMachine.ChangeState(PlayerStateEnums.ChargeAttackMove);
                        }
                        else
                        {
                            // Debug.Log("Dig");
                            playerStateMachine.ChangeState(PlayerStateEnums.ChargeAttack);
                            playerController.CheckBlock(playerController.storedDir * 2, 3);

                        }
                    }
                }
                else //if (!Conductor._instance.CheckValidBeat())
                {
                    playerStateMachine.ChangeState(PlayerStateEnums.Hurt);
                }
            }
            else
            {
                playerStateMachine.ChangeState(PlayerStateEnums.Hurt);
            }
        }

        if  (playerController.currentTime > maxTime)
		{
            playerStateMachine.ChangeState(PlayerStateEnums.Hurt);
        }
    }
}
