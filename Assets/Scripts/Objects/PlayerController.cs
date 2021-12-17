using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class PlayerController : InteractableObject
{
    [Header("   Tangibility")]
    [Space(10)]
    public ObjectTangibility baseTang;
    public LayerMask hittable;
    public LayerMask oneWayGroundLayers;

    public PlayerStateMachine playerMachine => GetComponent<PlayerStateMachine>();
    private Rigidbody2D rbody => GetComponent<Rigidbody2D>();
    public Animator anim => GetComponentInChildren<Animator>();
    public SpriteRenderer spriteRenderer => GetComponentInChildren<SpriteRenderer>();
    public GameObject attackPivot => gameObject.transform.GetChild(1).gameObject;
    public AudioSource audioSource => GetComponent<AudioSource>();

    [Header("   Movement")]
    [Space(10)]
    public float movespeed;

    [Header("   Jump")]
    [Space(10)]
    public int maxAirJumps;
    public int airJumpCount;
    public int maxAirDodges;
    public int airDodgeCount;
    public int coyoteTime;
    public bool canJump = true;

    [Header("   States")]
    [Space(10)]
    public List<PlayerState> states;

    [Header("   Input")]
    [Space(10)]
    public bool canRotate = true;
    public Vector2 _inputDir;
    public Vector2 facingDir = new Vector2(1, 0);
    public Vector2 storedDir;

    public int inputBuffer;
    public int moveBuffer;
    public int button1Buffer;
    public int button1ReleaseBuffer;

    public int fallBuffer;
    public bool button1Hold;


    public PlayerInputActions inputAction;
    public Vector2 InputDir => _inputDir;

    [Header("   MovementVFX")]
    [Space(10)]
    [SerializeField]
    private int dustTime;
    public GameObject dustParticle;
    [HideInInspector]
    public bool drawHitboxes;

    public bool isDead;
    public Vector3 desiredPosition;

    void Start()
    {
        HP = maxHP;

    }

    private void Awake()
    {
        Application.targetFrameRate = 60;
        inputAction = new PlayerInputActions();
        inputAction.PlayerControls.Move.performed += ctx => _inputDir = ctx.ReadValue<Vector2>();
        inputAction.Enable();
        isDead = false;
    }

    private void OnEnable()
    {
        inputAction?.Enable();
        canRotate = true;
        facingDir = new Vector2(1, 0);
        attackPivot.transform.rotation = Quaternion.Euler(0, 0, 90);
    }

    private void OnDisable()
    {
        inputAction.Disable();
    }

    public override void OnPause(bool paused)
    {
        base.OnPause(paused);
        _inputDir = Vector2.zero;
        rbody.velocity *= 0;
    }


    public override void OnCollisionEnter2D(Collision2D collision)
    {
        if (((1 << collision.gameObject.layer) & bounceLayers) != 0)
        {
            Bounce(collision);
        }
        if (Stomped() || Bonked() || isGrounded)
            internalVelocity.y = -gravity;
    }
    public override void OnCollisionStay2D(Collision2D collision)
    {
    }

    void Update()
    {
        if(!isDead)
        BufferInputs();
        playerMachine.OnUpdate();
        anim.SetFloat("scaledTime", scaledTime);

        if (InputDir.sqrMagnitude != 0)
        {
            if (canRotate)
            {
                attackPivot.transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(InputDir.y, InputDir.x) * Mathf.Rad2Deg - 90f); //set InputDir to 0 to lock a direction,
                storedDir = InputDir;
                SetFacingDir();
            }
        }
        if (Mathf.Round(externalVelocity.sqrMagnitude * 100) * 0.01f == 0)
        {
            externalVelocity = Vector2.zero;
        }

        if (inputAction.PlayerControls.Quit.triggered)
        {
            StartCoroutine(WaitRestart());
        }
    }

    private void FixedUpdate()
    {
        activeTime += scaledTime;
        if (activeTime - currentTime >= 1)
        {
            ApplyFriction();
            if(!isDead)
                playerMachine.OnFixedUpdate();
            if (isGrounded)
                targetVelocity = groundVelocity;
            else
                targetVelocity.x = 0;
            targetVelocity += internalVelocity + externalVelocity;
            rbody.velocity = targetVelocity * scaledTime * Time.deltaTime;
            currentTime = (int)activeTime;
        }
        DecrementBuffer();
    }


    public override ObjectTangibility TakeDamage(int damage, float distance,Vector2 knockback, float hitstopTime, bool armorPierce, bool fromPlayer)
    {
        HP -= damage;
        externalVelocity += (knockback * distance);
        //DO A CAMERASHAKE
        playerMachine.ChangeState(PlayerStateEnums.Hurt);
        if (HP < 0)
        {
            //DO A GAME OVER
        }
        SetHealthbar();
        return myTang;
    }

    public void SetFacingDir()
    {
        if (InputDir.x !=0)
        {

            attackPivot.transform.rotation = Quaternion.Euler(0, 0, (Mathf.Atan2(0, InputDir.x) * Mathf.Rad2Deg - 90f)); //set InputDir to 0 to lock a direction,
            facingDir = (Quaternion.Euler(0, 0, attackPivot.transform.rotation.eulerAngles.z) * Vector2.up).normalized;
            facingDir.x = Mathf.RoundToInt(facingDir.x);
            facingDir.y = Mathf.RoundToInt(facingDir.y);
            if (facingDir.x != 0)
                spriteRenderer.flipX = storedDir.x == -1;
        }
    }

    public void SetHealthbar()
	{
        //GameEventManager.current.PlayerDamage(HP);
    }
    public void SetHealthbarMax()
    {
        //GameEventManager.current.PlayerHeal(maxHP);
    }

    public override void ApplyFriction()
    {
        if (fallBuffer > 0 || targetVelocity.y > 0)
            isGrounded = Grounded(oneWayGroundLayers);
        else
            isGrounded = Grounded(groundLayers);

        if (!isGrounded)
        {
            internalVelocity.y += -gravity * gravityMod;
        }
        else
            coyoteTime = 10;

        if (isGrounded && internalVelocity.y <= 0)
        {
            internalVelocity.y = -gravity;
        }

        internalVelocity.x *= friction;

        if (isGrounded)
        {
            externalVelocity.x *= friction;
            externalVelocity.y *= friction * 0.9f;
            if (storeGroundVelocityTime == 0)
            {
                groundVelocity.x *= friction;
                groundVelocity.y *= friction * 0.9f;
            }
        }
        else
        {
            externalVelocity.x *= airFriction;
            externalVelocity.y *= airFriction * 0.9f;
            if (storeGroundVelocityTime == 0)
            {
                groundVelocity.x *= airFriction;
                groundVelocity.y *= airFriction * 0.9f;
            }
        }

        if (internalVelocity.y < terminalYVelocity)
        {
            internalVelocity.y = terminalYVelocity;
        }

        if (Mathf.Round(externalVelocity.sqrMagnitude * 100f) / 100f == 0)
        {
            externalVelocity *= 0;
        }
        if (Mathf.Round(groundVelocity.sqrMagnitude * 100f) / 100f == 0)
        {
            groundVelocity *= 0;
        }
        if (storeGroundVelocityTime > 0)
        {
            storeGroundVelocityTime -= 1;
        }

        if (bounceTime > 0)
        {
            bounceTime--;
        }
    }



    void BufferInputs()
    {
        if (inputAction.PlayerControls.Button1.triggered)
        {
            button1Buffer = inputBuffer;
            button1Hold = true;
        }

        if (inputAction.PlayerControls.Button1Release.triggered)
        {
            button1ReleaseBuffer = inputBuffer;
            button1Hold = false;
        }

        if (inputAction.PlayerControls.MovePressUp.triggered ||
            inputAction.PlayerControls.MovePressDown.triggered ||
            inputAction.PlayerControls.MovePressLeft.triggered ||
            inputAction.PlayerControls.MovePressRight.triggered)
            moveBuffer = inputBuffer;

        if (InputDir.y == -1)
            fallBuffer = 15;
    }

    void DecrementBuffer()
    {
        if (button1Buffer > 0)
            button1Buffer--;

        if (button1ReleaseBuffer > 0)
            button1ReleaseBuffer--;

        if (coyoteTime > 0)
            coyoteTime--;

        if (moveBuffer > 0)
            moveBuffer--;

        if (fallBuffer > 0 || targetVelocity.y > 0)
        {
            fallBuffer--;
            gameObject.layer = 30; //LAYER FOR FALLING THROUGH PASS THROUGH OBJECTS
            
        }
		else
		{
            gameObject.layer = 8; //STANDARD LAYER
		}
    }

    IEnumerator WaitRestart()
	{
        //Time.timeScale = 0.5f;
        yield return new WaitForSecondsRealtime(1f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}
}
