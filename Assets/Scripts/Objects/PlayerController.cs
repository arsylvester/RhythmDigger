using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Cinemachine;
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
    public Vector2 _inputDir = Vector2.down;
    public Vector2 facingDir = new Vector2(1, 0);
    public Vector2 storedDir = Vector2.down;

    public int inputBuffer;
    public int moveBuffer;
    public int button1Buffer;
    public int button1ReleaseBuffer;

    public int fallBuffer;
    public bool button1Hold;
    public bool quitHold;


    public PlayerInputActions inputAction;
    public Vector2 InputDir;

    [Header("   MovementVFX")]
    [Space(10)]
    [SerializeField]
    private int dustTime;
    public GameObject dustParticle;
    [HideInInspector]
    public bool drawHitboxes;

    public bool isDead;
    public Vector3 desiredPosition;

    [Header("   SFX")]
    [SerializeField]
    private SoundSystem.SoundEvent hurtSFX;
    [SerializeField]
    private SoundSystem.SoundEvent deathSFX;
    [SerializeField]
    private GameObject deathVFX;

    public int floatTime;
    public CinemachineImpulseSource impulseSource => GetComponent<CinemachineImpulseSource>();

    public bool animatePlayer;

    void Start()
    {
        HP = maxHP;
    }

    private void Awake()
    {
        inputAction = new PlayerInputActions();
        inputAction.PlayerControls.Move.performed += ctx => _inputDir = ctx.ReadValue<Vector2>();
        //inputAction.PlayerControls.Reset.performed += ctx => PlayerDeath(); // This is for when the player gets stuck
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
        if (Crushed())
        {
            PlayerDeath();
            Debug.Log($"CRUSH ENTER {collision.gameObject.name}");
        }

        if (((1 << collision.gameObject.layer) & bounceLayers) != 0 && canBounce)
        {
            impulseSource.GenerateImpulse(0.75f);
            Bounce(collision);
        }
        if (Stomped() || Bonked() || isGrounded)
            internalVelocity.y = -gravity;
    }
    public override void OnCollisionStay2D(Collision2D collision)
    {
        //just a safety when unity sucks ya know
        if (Crushed() && !isDead)
        {
            PlayerDeath();
        }
    }

    void Update()
    {
        if (!isDead)
            BufferInputs();
        playerMachine.OnUpdate();
        anim.SetFloat("scaledTime", scaledTime);

        if (!animatePlayer)
            InputDir = _inputDir;
        if (InputDir.sqrMagnitude != 0)
        {
            if (canRotate)
            {
                attackPivot.transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(InputDir.y, InputDir.x) * Mathf.Rad2Deg - 90f); //set InputDir to 0 to lock a direction,
                if (InputDir == Vector2.left || InputDir == Vector2.right || InputDir == Vector2.down)
                    storedDir = InputDir;

            }
        }
        SetFacingDir();
        if (Mathf.Round(externalVelocity.sqrMagnitude * 100) * 0.01f == 0)
        {
            externalVelocity = Vector2.zero;
        }
            
        // // if (inputAction.PlayerControls.Quit.triggered)
        // {
        //     // StartCoroutine(WaitRestart());
        //     PlayerDeath();
        // }
        
        // This is to prevent infinite movement when animating the player
        if(animatePlayer)
            InputDir = Vector2.zero;
    }

  

    private void FixedUpdate()
    {
        activeTime += scaledTime;
        if (activeTime - currentTime >= 1)
        {
            ApplyFriction();
            if (!isDead)
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


    public override ObjectTangibility TakeDamage(int damage, float distance, Vector2 knockback, float hitstopTime, bool armorPierce, bool fromPlayer)
    {
        HP -= damage;
        externalVelocity += (knockback * distance);
        //DO A CAMERASHAKE

        hurtSFX.PlayOneShot(0);
        if (HP <= 0)
        {
            playerMachine.ChangeState(PlayerStateEnums.Dead);
        }
        else
            playerMachine.ChangeState(PlayerStateEnums.Hurt);
        SetHealthbar();
        return myTang;
    }
    public void PlayerDeath()
    {
        isDead = true;
        //Player Death Sound and vfx
        deathSFX.PlayOneShot(0);
        
        Instantiate(deathVFX, transform.position, deathVFX.transform.rotation);

        groundCollider.enabled = false;
        headCollider.enabled = false;
        leftCollider.enabled = false;
        rightCollider.enabled = false;

        spriteRenderer.enabled = (false);
        GetComponent<CapsuleCollider2D>().enabled = false;

        StartCoroutine(WaitRestart());
    }

    public void SetFacingDir()
    {
        if (InputDir.x != 0)
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

    public bool CheckOpenBlock(Vector2 direction)
	{
        Collider2D[] collisions = Physics2D.OverlapCircleAll(direction + new Vector2(transform.position.x, transform.position.y + 0.4f), 0.35f, hittable);
        foreach (Collider2D hitObject in collisions)//anything hit
        {
            if (hitObject.GetComponent<Block>())
            {
                //Debug.Log($"Checking {hitObject.gameObject.name}");
                return !hitObject.TryGetComponent(out Block block);
            }
        }
        return true;
    }

    public bool CheckBlock(Vector2 direction, int damage)
    {
        Collider2D[] collisions = Physics2D.OverlapCircleAll(direction + new Vector2(transform.position.x, transform.position.y+0.4f), 0.35f, hittable);
        foreach (Collider2D hitObject in collisions)//anything hit
        {
            if (hitObject.GetComponent<Block>())
            {
                //Debug.Log($"Mining {hitObject.gameObject.name}");
                return !hitObject.GetComponent<Block>().DamageBlock(damage);
            }
        }
        return false;
    }


    public override void ApplyFriction()
    {
        if (fallBuffer > 0 || targetVelocity.y > 0)
            isGrounded = Grounded(oneWayGroundLayers);
        else
        {
            isGrounded = Grounded(groundLayers);
        }

        if (!isGrounded && currentTime > floatTime)
        {
            internalVelocity.y += -gravity;
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
        if(!animatePlayer) // To prevent the player from using charged attacks on the start screens
        {
            if (inputAction.PlayerControls.Quit.triggered)
            quitHold = true;

            if (inputAction.PlayerControls.QuitRelease.triggered)
                quitHold = false;

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
        }    

        if (inputAction.PlayerControls.MovePressUp.triggered ||
            inputAction.PlayerControls.MovePressDown.triggered ||
            inputAction.PlayerControls.MovePressLeft.triggered ||
            inputAction.PlayerControls.MovePressRight.triggered || animatePlayer)
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
            //gameObject.layer = 30; //LAYER FOR FALLING THROUGH PASS THROUGH OBJECTS

        }
        else
        {
            //gameObject.layer = 8; //STANDARD LAYER
        }
    }


    IEnumerator WaitRestart()
    {
        // Time.timeScale = 0.5f;
        UIManager._instance.HideUI();
        CameraTarget.instance.ResetToTop();
        yield return new WaitUntil(() => CameraTarget.instance.waiting == true);
        UIManager._instance.ShowEndUI(GameManager._instance.GetGold(), (int)transform.position.y, Conductor._instance.highestChain);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector2 direction = storedDir;
        Gizmos.DrawSphere(direction + new Vector2(transform.position.x, transform.position.y+0.4f), 0.35f);
    }
}
