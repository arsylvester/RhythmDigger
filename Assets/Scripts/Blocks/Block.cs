using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class Block : MonoBehaviour
{
    [SerializeField] int blockHealth = 1;
    [SerializeField] bool falls = false;
    [SerializeField] bool rotates = false;
    [SerializeField] Sprite[] possibleSprites;
    SpriteRenderer spriteRenderer => GetComponentInChildren<SpriteRenderer>();

    private void Start()
    {
        //Randomize sprite if more then 1 possible sprite;
        if(possibleSprites.Length > 0)
            spriteRenderer.sprite = possibleSprites[Random.Range(0, possibleSprites.Length)];

        if (falls)
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        else
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;

        if (rotates)
        {
            spriteRenderer.transform.eulerAngles = new Vector3(0, 0, Random.Range(0, 4) * 90);
            spriteRenderer.flipX = (Random.Range(0,2) == 1);
            spriteRenderer.flipY = (Random.Range(0,2) == 1);
        }
    }

    public bool DamageBlock(int damage)
    {
        blockHealth -= damage;
        if(blockHealth <= 0)
        {
            OnBlockDestroy();
            return true;
        }
        else
        {
            return false;
        }
    }

    //Override if you want special function on destroying block.
    protected virtual void OnBlockDestroy()
    {
        //Play destroy block vfx and sfx here
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
            /*if(Physics2D.OverlapCircle(transform.position + Vector3.down, 1))
            {

            }*/
        }
    }
}
