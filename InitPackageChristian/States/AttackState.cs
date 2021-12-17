using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "PlayerStates/Attack")]
public class AttackState : PlayerState
{

    [SerializeField]
    public string attackName;

    public GameObject[] prefab;
    public GameObject hitParticle;
    [SerializeField]
    private GameObject[] particleEffects;

  public Sprite icon;

    [Header("Stats")]
    [SerializeField]
    private Vector2Int iframes;
    [SerializeField]
    private Vector2Int armorFrames;
    [SerializeField]
    private Vector2Int guardFrames;
    [SerializeField]
    private bool unstoppable;
    [SerializeField]
    private bool armorPierce;
    [SerializeField]
    private float attackMovespeed;
    [SerializeField]
    private AnimationCurve forwardMovement;
    [SerializeField]
    private AnimationCurve strafeMovement;


    [Header("VFX")]
    [SerializeField]
    public int[] particleTime;
    [SerializeField]
    private Vector3[] particlePos;
    [SerializeField]
    private Vector3[] particleOffset;
    [HideInInspector]
    public Vector3[] newParticlePos;


    [Header("SFX")]
    [SerializeField]
    public int[] sfxTime;
    [SerializeField]
    private AudioClip[] sfxClip;
    [SerializeField]
    private float[] sfxVolume;
    [SerializeField]
    private AudioClip[] hitSFX;

    [Header("VoiceFX")]
    [SerializeField]
    public int[] voicefxTime;
    [SerializeField]
    private AudioClip[] voicefxClip;
    [SerializeField]
    private float[] voicefxVolume;


    [Header("Prefab")]
    [SerializeField]
    public int[] prefabTime;
    [SerializeField]
    private Vector3[] prefabPos;
    [HideInInspector]
    public Vector3[] newPrefabPos;


    [Header("Time")] //backwards because unity;
    [Space(5)]
    [Header("   Hitbox")]
    [SerializeField]
    public Vector2[] hitboxTime;
    [SerializeField]
    public int[] multihitTime;


    [Header("Range")]
    [SerializeField]
    public Vector3[] hitboxPos;
    [SerializeField]
    public float[] hitboxSize;
    [Space(5)]

    [Header("Damage")]
    [SerializeField]
    private int[] hitboxDamage;
    [SerializeField]
    [Space(5)]

    [Header("Knockback")]
    private float[] hitboxKnockbackDist;
    [SerializeField]
    private Vector2[] hitboxKnockbackDir;

    private int hitID; //ID to prevent overlaps calling stuff twice

    private Vector2 attackDir;
    public Vector2 attackPivot;    //using this
    [HideInInspector]
    public Vector2[] newhitboxKnockbackDir; //temp value for rotated dir
    [HideInInspector]
    public Vector3[] newhitboxPos; //temp value for rotated pos

    [SerializeField]
    private float hitStopTime;

    public override void Init(PlayerStateMachine playerMachine)
    {
        playerStateMachine = playerMachine;
    }

    public override void OnEnter()
    {    
        base.OnEnter();
        GetStats();
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
    }

    public override void OnFixedUpdate()
    {
        if (playerStateMachine.playerController._inputDir != Vector2.zero && playerStateMachine.playerController.currentTime < 4)
        {
            GetStats();
        }
        if(playerStateMachine.playerController.currentTime > 4)
        {
            playerStateMachine.playerController.canRotate = false;
        }
        base.OnFixedUpdate();
        AttackID();
        AttackVelocity();
        CreateDamageFrames();
        CreateHitboxes();
        CreatePrefab();
        CreateVFX();
    }

    public override void OnExit()
    {
        playerStateMachine.playerController.canRotate = true;
    }

    public override void HandleState()
    {
        base.HandleState();
    }

    void AttackID()
    {
        for (int i = 0; i < multihitTime.Length; i++) //changes hitID to allow another hit, based on times in multiHitTime[]
        {
            if (playerStateMachine.playerController.currentTime == multihitTime[i])
            {
                hitID = Random.Range(0, 10000);
            }
        }
    }

    void AttackVelocity()
    {
        playerStateMachine.playerController.internalVelocity = playerStateMachine.playerController.storedDir.normalized * forwardMovement.Evaluate(playerStateMachine.playerController.currentTime) * attackMovespeed;
        playerStateMachine.playerController.internalVelocity += new Vector2(playerStateMachine.playerController.storedDir.x, playerStateMachine.playerController.storedDir.y).normalized.Rotate(-90) * attackMovespeed * strafeMovement.Evaluate(playerStateMachine.playerController.currentTime);
    }

    void CreateDamageFrames()
    {
        if (playerStateMachine.playerController.currentTime >= iframes.x && playerStateMachine.playerController.currentTime <= iframes.y && iframes != Vector2.zero && iframes != Vector2.zero)
        {
            playerStateMachine.playerController.myTang = ObjectTangibility.Invincible;
        }
        else if (playerStateMachine.playerController.currentTime >= armorFrames.x && playerStateMachine.playerController.currentTime <= armorFrames.y && armorFrames != Vector2.zero)
        {
            playerStateMachine.playerController.myTang = ObjectTangibility.Armor;
        }
        else if (playerStateMachine.playerController.currentTime >= guardFrames.x && playerStateMachine.playerController.currentTime <= guardFrames.y && guardFrames != Vector2.zero)
        {
            playerStateMachine.playerController.myTang = ObjectTangibility.Guard;
        }
        else
        {
            playerStateMachine.playerController.myTang = playerStateMachine.playerController.baseTang;
        }
    }

    void CreateHitboxes()
    {
        for (int i = 0; i < hitboxTime.Length; i++) //hitbox placement + prefab instantiation
        {
            if (playerStateMachine.playerController.currentTime >= hitboxTime[i].x && playerStateMachine.playerController.currentTime <= hitboxTime[i].y) 
            {
                Collider2D[] collisions = Physics2D.OverlapCircleAll(newhitboxPos[i] + playerStateMachine.player.transform.position, hitboxSize[i], playerStateMachine.playerController.hittable); //creating hitboxes based on hitBoxPos adjusted by player rotation
                foreach (Collider2D hitObject in collisions)//anything hit
                {
                    if (hitObject.GetComponent<InteractableObject>())
                    {
                        InteractableObject _hitObj = hitObject.GetComponent<InteractableObject>();
                        if (_hitObj.hitID != hitID)
                        {
                            _hitObj.hitID = hitID;
                            if (newhitboxKnockbackDir[i] == Vector2.zero)
                            {
                                newhitboxKnockbackDir[i] = (hitObject.transform.position - playerStateMachine.player.transform.position).normalized;
                            }
                            ObjectTangibility hitTang = _hitObj.TakeDamage(hitboxDamage[i], hitboxKnockbackDist[i], newhitboxKnockbackDir[i], hitStopTime, armorPierce, true);
                            switch (hitTang)
                            {
                                case ObjectTangibility.Normal:
                                    {
                                        //animate card hit
                                        GameObject _prefab = Instantiate(hitParticle, ((_hitObj.transform.position + ((((playerStateMachine.player.transform.position - _hitObj.transform.position)) + (newhitboxPos[i])) * 0.55f))), playerStateMachine.playerController.attackPivot.transform.rotation);
                                        ParticleSystem.MainModule main = _prefab.GetComponent<ParticleSystem>().main;
                                        main.simulationSpeed = playerStateMachine.playerController.scaledTime;
                                        Debug.Log("hit Normal");
                                    }
                                    break;
                                case ObjectTangibility.Armor:
                                    {
                                        Debug.Log("hit Armor");
                                    }
                                    break;
                                case ObjectTangibility.Guard:
                                    {
                                        //animate card miss
                                        //instance blockFX

                                        Debug.Log("hit Guard");
                                        playerStateMachine.ChangeState(PlayerStateEnums.Land);
                                    }
                                    break;
                                case ObjectTangibility.Invincible:
                                    {
                                        //animate card miss
                                        Debug.Log("hit Invincible");
                                    }
                                    break;

                            }
                        }
                    }
                }
            }
        }
    }

    void CreatePrefab()
    {
        for (int i = 0; i < prefab.Length; i++)
        {
            if (prefab[i])
            {
                if (playerStateMachine.playerController.currentTime == prefabTime[i])
                {
                    GameObject obj = Instantiate(prefab[i], playerStateMachine.player.transform.position + newPrefabPos[i], Quaternion.identity);
                    if (obj.GetComponent<ProjectileSpawner>())
                    {
                        obj.transform.parent = playerStateMachine.player.transform;
                        obj.GetComponent<ProjectileSpawner>().hittable = playerStateMachine.playerController.hittable;
                        obj.GetComponent<ProjectileSpawner>().dir = attackDir;
                        obj.GetComponent<ProjectileSpawner>().SetTimeScale(playerStateMachine.playerController.scaledTime);
                    }
                }
            }
        }
    }

    void CreateVFX()
    {
        for (int i = 0; i < particleEffects.Length; i++)
        {
            if (playerStateMachine.playerController.currentTime == particleTime[i])
            {
                GameObject VFX = Instantiate(particleEffects[i], playerStateMachine.player.transform.position + new Vector3(newParticlePos[i].x, newParticlePos[i].y, 0) + particleOffset[i], playerStateMachine.playerController.attackPivot.transform.rotation, playerStateMachine.player.transform);
                if (VFX.GetComponent<CustomVFX>())
                {
                    VFX.GetComponent<CustomVFX>().scaledTime = playerStateMachine.playerController.scaledTime;
                    VFX.GetComponent<CustomVFX>().CreateVFX(attackDir, playerStateMachine.playerController.storedDir);
                }
            }
        }
    }//creates prefabs at specified frame and location

    void CreateSFX()
    {
        for (int i = 0; i < sfxTime.Length; i++)
        {
            if (playerStateMachine.playerController.currentTime == sfxTime[i])
            {
                playerStateMachine.playerController.audioSource.PlayOneShot(sfxClip[i], sfxVolume[i]);
            }
        }
    }

    void CreateVoiceFX()
    {
        for (int i = 0; i < voicefxTime.Length; i++)
        {
            if (playerStateMachine.playerController.currentTime == voicefxTime[i])
            {
                int voiceClip = Random.Range(0, voicefxClip.Length);
                playerStateMachine.playerController.audioSource.PlayOneShot(voicefxClip[voiceClip], voicefxVolume[voiceClip]);
            }
        }
    }

    void GetStats()
    {
        attackDir = playerStateMachine.playerController.facingDir;
        playerStateMachine.playerController.attackPivot.transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(attackDir.y, attackDir.x) * Mathf.Rad2Deg - 90f);

        newhitboxPos = (Vector3[])hitboxPos.Clone();
        newPrefabPos = (Vector3[])prefabPos.Clone();
        newhitboxKnockbackDir = (Vector2[])hitboxKnockbackDir.Clone();
        newParticlePos = (Vector3[])particlePos.Clone();

        hitID = Random.Range(0, 10000);

        playerStateMachine.playerController.drawHitboxes = true;

        for (int i = 0; i < hitboxPos.Length; i++)
        {
            newhitboxPos[i] = new Vector2(hitboxPos[i].x, hitboxPos[i].y).Rotate(playerStateMachine.playerController.attackPivot.transform.rotation.eulerAngles.z);
        }

        for (int i = 0; i < prefabPos.Length; i++)
        {
            newPrefabPos[i] = new Vector2(prefabPos[i].x, prefabPos[i].y).Rotate(playerStateMachine.playerController.attackPivot.transform.rotation.eulerAngles.z);
        }

        for (int i = 0; i < particlePos.Length; i++)
        {
            newParticlePos[i] = new Vector2(particlePos[i].x, particlePos[i].y).Rotate(playerStateMachine.playerController.attackPivot.transform.rotation.eulerAngles.z);
        }

        Quaternion storedDir = Quaternion.Euler(0, 0, Mathf.Atan2(playerStateMachine.playerController.storedDir.y, playerStateMachine.playerController.storedDir.x) * Mathf.Rad2Deg - 90f);
        for (int i = 0; i < hitboxKnockbackDir.Length; i++)
        {
            newhitboxKnockbackDir[i] = new Vector2(hitboxKnockbackDir[i].x, hitboxKnockbackDir[i].y).Rotate(storedDir.eulerAngles.z);
        }

        playerStateMachine.playerController.anim.SetFloat("xDir", attackDir.x);
        playerStateMachine.playerController.anim.SetFloat("yDir", attackDir.y);
        playerStateMachine.playerController.anim.Play(attackName, 0,0);
    }
}