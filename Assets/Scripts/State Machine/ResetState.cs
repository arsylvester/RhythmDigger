using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
[CreateAssetMenu(menuName = "PlayerStates/Reset")]
public class ResetState : PlayerState
{
    public float impulseAmount;
	public override void OnEnter()
	{
		base.OnEnter();
		playerStateMachine.playerController.anim.Play("HurtFast");
	}
	public override void Init(PlayerStateMachine playerMachine)
    {
        playerStateMachine = playerMachine;
        playerController = playerMachine.playerController;
    }

	public override void OnUpdate()
	{
       playerController.impulseSource.GenerateImpulse(impulseAmount);
        HandleState();
	}

	public override void OnExit()
	{
        //Camera.main.GetComponent<CinemachineBrain>().ActiveVirtualCamera.VirtualCameraGameObject.GetComponent<CinemachineImpulseListener>().m_ChannelMask = 0;
	}

	public override void HandleState()
	{

		if (!playerController.quitHold)
			playerStateMachine.ChangeState(PlayerStateEnums.Idle);

		if (playerController.currentTime >= maxTime)
		{
			Conductor._instance.gameIsOver = true;
			playerController.TakeDamage(1, 100, ((new Vector3(Random.Range(-1.1f, 1f), 1, 0)) + Vector3.up).normalized, 0, false, false);
		}
	}
}