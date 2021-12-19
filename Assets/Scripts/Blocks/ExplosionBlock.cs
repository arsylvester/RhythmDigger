using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class ExplosionBlock : Block
{
    [SerializeField] Vector2[] blocksToDestroy;
    [SerializeField] int damageDealt = 50;

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

        exploding = true;
        Vector2 currentPos = transform.position;
        ContactFilter2D filter = new ContactFilter2D();
        Collider2D[] results = new Collider2D[5];
        Instantiate(explosionFX, transform.position, transform.rotation);
        //Loop through all circle casts and damage block if needed
        for (int i = 0; i < blocksToDestroy.Length; i++)
        {
            if (Physics2D.OverlapCircle(currentPos + blocksToDestroy[i], .1f, filter, results) > 0)
            {
                foreach(Collider2D result in results)
                {
                    
                    if (result == null) break;
                    Block block = result?.GetComponent<Block>();
                    if(block && block != this)
                    {
                        block.exploding = true;
                        block.DamageBlock(damageDealt);
                        Instantiate(explosionFX, block.transform.position, block.transform.rotation);
                    }
                    else
                    {
                        PlayerController player = result.GetComponent<PlayerController>();
                        if(player)
                        {
                            player.TakeDamage(1, 200, (((player.transform.position - transform.position).normalized) + Vector3.up).normalized, 0, false, false);
                        }
                    }
                }
            }
        }

        Destroy(gameObject);
    }
}
