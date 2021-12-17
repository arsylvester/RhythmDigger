using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PlayerStates/Dodge")]
public class DodgeState : PlayerState
{
    public float jumpHeight;
    [SerializeField]
    private float dodgeSpeed;
    private Vector2 dodgeDir;
    [SerializeField]
    private AnimationCurve dodgeCurve;
    [SerializeField]
    private Vector2 iframes;
    [SerializeField]
    private int afterImageFrames;
    public GameObject afterImage;
    public AudioClip dodgeSFX;

    public override void Init(PlayerStateMachine playerMachine)
    {
        playerStateMachine = playerMachine;
        playerController = playerMachine.playerController;
    }
    public override void OnEnter()
    {
        base.OnEnter();
        dodgeDir = new Vector2(playerController.facingDir.x, 0);
        playerController.anim.SetFloat("xDir", playerStateMachine.playerController.facingDir.x);
        playerController.anim.Play("Dodge", 0, 0);
        playerStateMachine.playerController.canRotate = false;
        //playerStateMachine.playerController.sprintMod = 1.35f;
        if (!playerController.isGrounded)
        {
            playerController.internalVelocity.y = jumpHeight;
            playerController.airDodgeCount--;
        }
        playerStateMachine.playerController.audioSource.PlayOneShot(dodgeSFX);
        playerStateMachine.playerController.fallBuffer = (int)maxTime;
    }

    public override void OnExit()
    {
        playerStateMachine.playerController.canRotate = true;
        playerController.myTang = ObjectTangibility.Normal;
    }

    public override void OnFixedUpdate()
    {
        base.OnFixedUpdate();
        if(playerController.currentTime <= 6 && playerController.storedDir.x != 0)
		{
            dodgeDir = playerController.storedDir;

        }
        playerController.internalVelocity.x = (dodgeDir.x * dodgeSpeed * dodgeCurve.Evaluate(playerController.currentTime));
        if (playerStateMachine.playerController.currentTime >= iframes.x && playerStateMachine.playerController.currentTime <= iframes.y)
        {
            if(playerStateMachine.playerController.currentTime % afterImageFrames == 0)
			{
                AfterImageVFX obj = Instantiate(afterImage, playerController.transform.position, Quaternion.identity).GetComponent<AfterImageVFX>();
                obj.spriteRenderer.sprite = playerController.spriteRenderer.sprite;
			}
            playerController.myTang = ObjectTangibility.Invincible;
        }
        else
        {
            playerController.myTang = ObjectTangibility.Normal;
        }
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
		//if (playerStateMachine.playerController.inputAction.PlayerControls.Button2Release.triggered)
		//{
        //    playerStateMachine.playerController.runMod = 1;
		//}
    }

    public override void HandleState()
    {
        if (playerStateMachine.playerController.currentTime > maxTime && !loop)
        {
            if(playerController.isGrounded)
                playerStateMachine.ChangeState(PlayerStateEnums.Idle);
            else
                playerStateMachine.ChangeState(PlayerStateEnums.Fall);
        }
    }
}
