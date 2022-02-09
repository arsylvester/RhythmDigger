using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;

public class TimelineController : Singleton<TimelineController>
{
    [SerializeField] PlayerController playerController;
    [SerializeField] GameObject startMenuChunkPrefab, playerPrefab, playerCameraTarget;
    [SerializeField] Transform startMenuChunkCurrentTransform;
    [SerializeField] PlayableDirector director_StartGame;//, director_Controls1, director_Controls2, director_Controls3;
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
        // GameObject oldPlayer = playerController.gameObject;
        
        // DestroyImmediate(startMenuChunkInitialGO,true);
        Destroy(startMenuChunkCurrentTransform.gameObject);
        GameObject go = Instantiate(startMenuChunkPrefab,Vector3.zero,Quaternion.identity) as GameObject;
        startMenuChunkCurrentTransform = go.transform;
        ChargeRelease();
        QuitRelease();
        // playerCameraTarget.GetComponent<CameraTarget>().enabled = false;
        // Destroy(oldPlayer);
        
        // go = Instantiate(playerPrefab,initialPlayerPosition,Quaternion.identity) as GameObject;
        
        // playerCameraTarget.GetComponent<CameraTarget>().target = go.transform;
        // playerCameraTarget.GetComponent<CameraTarget>().enabled = true;
        
        // playerController = go.GetComponent<PlayerController>();
        playerController.playerMachine.ChangeState(PlayerStateEnums.Reset);
        playerController.gameObject.transform.position = initialPlayerPosition;

        // Destroy(oldPlayer);
        // if(playerController.isDead)
        // {
        //     Destroy(playerController.gameObject);
        //     go = Instantiate(playerPrefab,initialPlayerPosition,Quaternion.identity) as GameObject;
        //     playerController = go.GetComponent<PlayerController>();
        // }
        // else
        // {
        //     playerController.gameObject.transform.position = initialPlayerPosition;
        // }
        
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

    public void StartGame()
    {
        SceneManager.LoadScene("Main Scene");
    }

    public void PlayTimeline_StartGame()
    {
        Debug.Log("PlayTimeline_StartGame");
        director_StartGame.Play();
    }
}
