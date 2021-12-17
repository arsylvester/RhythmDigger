using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class ExplosionBlock : Block
{
    [SerializeField] Vector2[] blocksToDestroy;
    [SerializeField] int damageDealt = 50;
    bool exploding = false;

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

        exploding = true;
        Vector2 currentPos = transform.position;
        ContactFilter2D filter = new ContactFilter2D();
        Collider2D[] results = new Collider2D[5];

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
                        block.DamageBlock(damageDealt);
                    }
                    else
                    {
                        PlayerController player = result.GetComponent<PlayerController>();
                        if(player)
                        {
                            player.TakeDamage(1, 0, Vector2.zero, 0, false, false);
                        }
                    }
                }
            }
        }

        Destroy(gameObject);
    }
}
