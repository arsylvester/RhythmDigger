using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using Cinemachine;

public class Block : MonoBehaviour
{
    [SerializeField] int blockHealth = 1;
    [SerializeField] bool falls = false;
    bool falling = false;   
    [SerializeField] bool rotates = false;
    [SerializeField] Sprite[] possibleSprites;
    [SerializeField] protected SoundSystem.SoundEvent blockBreakSFX;
    [SerializeField] protected SoundSystem.SoundEvent blockHitSFX;
    public LayerMask physicsLayers;
    SpriteRenderer spriteRenderer => GetComponentInChildren<SpriteRenderer>();
    public GameObject DestroyFX;
    public bool exploding = false;
    public float destructionIntensity;
    public CinemachineImpulseSource impulseSource => GetComponent<CinemachineImpulseSource>();
    private void Start()
    {
        //Randomize sprite if more then 1 possible sprite;
        if(possibleSprites.Length > 0)
            spriteRenderer.sprite = possibleSprites[Random.Range(0, possibleSprites.Length)];

            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;

        if (rotates)
        {
            spriteRenderer.transform.eulerAngles = new Vector3(0, 0, Random.Range(0, 4) * 90);
            spriteRenderer.flipX = (Random.Range(0,2) == 1);
            spriteRenderer.flipY = (Random.Range(0,2) == 1);
        }
    }

    public bool DamageBlock(int damage)
    {
        //print("Damage block");
        blockHealth -= damage;
        if(blockHealth <= 0)
        {
            //print("Damage block destroyed");
            OnBlockDestroy();
            return true;
        }
        else
        {
            impulseSource.GenerateImpulse(destructionIntensity);
            //print("Damage block but not destroyed");
            blockHitSFX.PlayOneShot(0);
            return false;
        }
    }

    //Override if you want special function on destroying block.
    protected virtual void OnBlockDestroy()
    {
        //Play destroy block vfx and sfx here
        if(!exploding)
            Instantiate(DestroyFX, transform.position, transform.rotation);
        blockBreakSFX.PlayOneShot(0);
        impulseSource.GenerateImpulse(destructionIntensity);
        Destroy(gameObject);
    }

    public virtual void Activate()
    {
        //Do nothing, override if want block to do something on activate.
    }

    private void Update()
    {

        if(falls)
        {
            if (!falling)
            {
                Collider2D[] hitObjects = (Physics2D.OverlapCircleAll(transform.position + Vector3.down, 0.35f, physicsLayers));
                foreach (Collider2D obj in hitObjects)
                    if (obj.gameObject != this)
                    {
                        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
                        falling = true;
                    }
            }
            else 
                GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        }
    }
}
