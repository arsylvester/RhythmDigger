using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTimelineController : MonoBehaviour
{
    PlayerController playerController;
    void Awake()
    {
        // playerController = PlayerController._instance();
        if(!playerController)
        {
            TryGetComponent<PlayerController>(out playerController);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TryMoveRight()
    {
        Debug.Log("TryMoveRight");
        if(playerController)
        {
            playerController.InputDir = new Vector2(1,0);
        }
        // Player
        // InputDir
        // TryMoveInDir(Vector2.right);
    } // Used in timeline control
    public void TryMoveLeft()
    {
        // TryMoveInDir(Vector2.left);
    }  // Used in timeline control
    public void TryMoveDown()
    {
        // TryMoveInDir(Vector2.down);
    }  // Used in timeline control
    public void TryCharge()
    {
        // playerStateMachine.ChangeState(PlayerStateEnums.Charge);
    }  // Used in timeline control
}
