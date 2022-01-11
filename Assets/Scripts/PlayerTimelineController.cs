using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTimelineController : MonoBehaviour
{
    [SerializeField] PlayerController playerController;
    [SerializeField] GameObject startMenuChunkPrefab, playerGO;
    [SerializeField] Transform startMenuChunkCurrentTransform;
    private Vector3 initialPlayerPosition;
    void Awake()
    {
        // playerController = PlayerController._instance();
        if(!playerController)
        {
            TryGetComponent<PlayerController>(out playerController);
        }
        initialPlayerPosition = playerController.gameObject.transform.position;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResetScene()
    {
        // DestroyImmediate(startMenuChunkInitialGO,true);
        Destroy(startMenuChunkCurrentTransform.gameObject);
        GameObject go = Instantiate(startMenuChunkPrefab,Vector3.zero,Quaternion.identity) as GameObject;
        startMenuChunkCurrentTransform = go.transform;
        playerController.gameObject.transform.position = initialPlayerPosition;
    }

    public void TryMove(Vector2 input)
    {
        if(playerController)
        {
            playerController.InputDir = input;
        }
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
        Debug.Log("TryMoveLeft");
        if(playerController)
        {
            playerController.InputDir = new Vector2(-1,0);
        }
    }  // Used in timeline control
    public void TryMoveDown()
    {
        // TryMoveInDir(Vector2.down);
        Debug.Log("TryMoveDown");
        if(playerController)
        {
            playerController.InputDir = new Vector2(0,-1);
        }
    }  // Used in timeline control
    public void ChargeStart()
    {
        // playerStateMachine.ChangeState(PlayerStateEnums.Charge);
        Debug.Log("TryStartCharge");
        if(playerController)
        {
            // playerController.InputDir = new Vector2(1,0);
            // playerController.playerMachine.ChangeState(PlayerStateEnums.Charge);
            playerController.button1Hold = true;
            playerController.button1Buffer = playerController.inputBuffer;
        }
    }  // Used in timeline control

    public void ChargeRelease()
    {
        Debug.Log("TryStartCharge");
        if(playerController)
        {
            // playerController.InputDir = new Vector2(1,0);
            // playerController.playerMachine.ChangeState(PlayerStateEnums.Charge);
            playerController.button1Hold = false;
            playerController.button1Buffer = playerController.inputBuffer;
        }
    }

    public void QuitStart()
    {
        Debug.Log("StartEsc");
        if(playerController)
        {
            playerController.quitHold = true;
        }
    }

    public void QuitRelease()
    {
        Debug.Log("ReleaseEsc");
        if(playerController)
        {
            playerController.quitHold = false;
        }
    }
}
