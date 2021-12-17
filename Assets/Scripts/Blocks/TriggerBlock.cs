using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class TriggerBlock : Block
{
    [SerializeField] Vector2[] blocksToTrigger;

    protected override void OnBlockDestroy()
    {
        Activate();
        Destroy(gameObject);
    }

    [Button]
    public override void Activate()
    {
        //Play Trigger vfx and sfx here

        Vector2 currentPos = transform.position;
        ContactFilter2D filter = new ContactFilter2D();
        Collider2D[] results = new Collider2D[5];

        //Loop through all circle casts and damage block if needed
        for (int i = 0; i < blocksToTrigger.Length; i++)
        {
            if (Physics2D.OverlapCircle(currentPos + blocksToTrigger[i], .1f, filter, results) > 0)
            {
                foreach (Collider2D result in results)
                {
                    if (result == null) break;
                    Block block = result.GetComponent<Block>();
                    if (block && block != this)
                    {
                        block.Activate();
                    }
                }
            }
        }
    }
}
