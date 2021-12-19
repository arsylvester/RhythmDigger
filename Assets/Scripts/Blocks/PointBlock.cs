using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointBlock : Block
{
    [SerializeField] int pointsToAdd = 1;
    protected override void OnBlockDestroy()
    {
        Activate();
        Destroy(gameObject);
    }

    public override void Activate()
    {
        //Play Points vfx and sfx here
        blockBreakSFX.PlayOneShot(0);

        GameManager._instance.AddGold(pointsToAdd);
    }
}
