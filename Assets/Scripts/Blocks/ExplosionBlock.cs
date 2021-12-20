using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class ExplosionBlock : Block
{
    [SerializeField] Vector2[] blocksToDestroy;
    [SerializeField] int damageDealt = 50;
    public LayerMask hittable;
    public GameObject explosionFX;
    [Button]
    protected override void OnBlockDestroy()
    {
        if (!exploding)
        {
            Activate();
        }
    }

    public override void Activate()
    {
        //Play explosion vfx and sfx here
        impulseSource.GenerateImpulse(destructionIntensity);
        blockBreakSFX.PlayOneShot(0);
        GetComponent<Collider2D>().enabled = false;
        exploding = true;
        Vector2 currentPos = transform.position;
        //ContactFilter2D filter = new ContactFilter2D();
        Instantiate(explosionFX, transform.position, transform.rotation);
        //Loop through all circle casts and damage block if needed
        for (int i = 0; i < blocksToDestroy.Length; i++)
        {
            Instantiate(explosionFX, currentPos + blocksToDestroy[i], transform.rotation);
            Collider2D[] results = Physics2D.OverlapCircleAll(currentPos + blocksToDestroy[i], .1f, hittable);
            {
                foreach(Collider2D result in results)
                {

                    if (result == null) { Debug.LogError("bad block"); break; }
                    Block block = result?.GetComponent<Block>();
                    if(block && block != this)
                    {
                        if(block as ExplosionBlock == null)
						{
                            block.exploding = true;
                            //Debug.Log("good to set to explode");
						}
    
                        block.DamageBlock(damageDealt);
                        //Instantiate(explosionFX, block.transform.position, block.transform.rotation);
                    }
                    else
                    {
                        PlayerController player = result.GetComponent<PlayerController>();
                        if (player || result.GetComponentInParent<PlayerController>())
                        {
                            player.TakeDamage(1, 200, ((((player.transform.position - transform.position).normalized) + new Vector3(Random.Range(-1.1f, 1f), 0,  0))+ Vector3.up).normalized, 0, false, false);
                        }
                    }
                }
            }
        }

        Destroy(gameObject, 0.05f);
    }
}
