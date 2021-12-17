using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField] int blockHealth = 1;
    [SerializeField] bool falls = false;
    [SerializeField] Sprite[] possibleSprites;

    private void Start()
    {
        //Randomize sprite if more then 1 possible sprite;
        if(possibleSprites.Length > 0)
            GetComponent<SpriteRenderer>().sprite = possibleSprites[Random.Range(0, possibleSprites.Length)];   
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
