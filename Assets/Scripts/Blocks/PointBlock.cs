using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointBlock : Block
{
    protected override void OnBlockDestroy()
    {
        Activate();
        Destroy(gameObject);
    }

    public override void Activate()
    {
        //Play Points vfx and sfx here

        //Give points
    }
}
