using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerStateEnums { Idle, Move, Jump, Fall, Land, Attack, AttackMove, Charge, ChargeAttack, ChargeAttackMove, Blocked, Hurt, Dead }; //Idle, Move, Dodge, Jump, Fall, Land, Attack, Hurt, Dead 

public class PlayerStateMachine : MonoBehaviour
{
    public static PlayerStateMachine current;
    public PlayerState currentState;
    public PlayerState previousState;
    public Dictionary<PlayerStateEnums, PlayerState> stateDictionary;
    public GameObject player => this.gameObject;
    public PlayerController playerController => GetComponent<PlayerController>();
    bool busyChange;

    
    public void StartMachine(GameObject playerObject, List<PlayerState> states)
    {
        if(current == null)
		{
            current = this;
		}
		else
		{
            Destroy(this.gameObject);
		}
        stateDictionary = new Dictionary<PlayerStateEnums, PlayerState>();

        for (int i = 0; i<states.Count; i++)
        {
            if (states[i] == null)
            {
                continue;
            }
            states[i] = Instantiate(states[i]) as PlayerState;
            states[i].Init(this);
            stateDictionary.Add((PlayerStateEnums)i, states[i]);
        }

        previousState = stateDictionary[PlayerStateEnums.Idle];
        currentState = stateDictionary[PlayerStateEnums.Idle];

        ChangeState(PlayerStateEnums.Idle);
    }

    private void Start()
    {
        StartMachine(player, playerController.states);
    }

    public void OnUpdate()
    {
        currentState.OnUpdate();
    }

    public void OnFixedUpdate()
    {
        currentState.OnFixedUpdate();
    }

    public void ChangeState(PlayerStateEnums newState) //call this function when changing states
    {
        if (!busyChange)
        {
            StartCoroutine(ChangeStateWait(newState));
        }
    }

    public IEnumerator ChangeStateWait(PlayerStateEnums newState)
    {
        busyChange = true;
        currentState.OnExit();
        currentState = stateDictionary[newState];
        currentState.OnEnter();
        yield return new WaitForEndOfFrame();
        busyChange = false;
    }
}