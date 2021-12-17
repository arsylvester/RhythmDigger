using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerState : ScriptableObject
{
    public PlayerStateMachine playerStateMachine;
    public PlayerController playerController;
    [SerializeField]
    protected float maxTime;
    [SerializeField]
    protected bool loop;
    [SerializeField]
    private float iasa;

    public virtual void OnEnter()
    {
        playerStateMachine.playerController.currentTime = 0;
        playerStateMachine.playerController.activeTime = 0;
        //Debug.Log(playerStateMachine.currentState.ToString());
    }

    public abstract void Init(PlayerStateMachine playerStateMachine);

    public virtual void OnUpdate()
    {
        HandleState();
    }
    public virtual void OnFixedUpdate()
    {

    }

    public abstract void OnExit();


    public virtual void HandleState()
    {
        if (playerStateMachine.playerController.inputAction.PlayerControls.Button1.triggered && playerStateMachine.playerController.currentTime >= iasa)
        {
            //playerStateMachine.ChangeState(PlayerStateEnums.Attack);
        }

        if (playerStateMachine.playerController.currentTime > maxTime && !playerStateMachine.currentState.loop)
        {
            playerStateMachine.ChangeState(PlayerStateEnums.Idle);
        }
    }
}
