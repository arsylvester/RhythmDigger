using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointBlock : Block
{
    //[SerializeField] int pointsToAdd = 1;
    public GameObject goldReward;
    protected override void OnBlockDestroy()
    {
        Activate();
        Destroy(gameObject);
    }

    public override void Activate()
    {
        //Play Points vfx and sfx here
        blockBreakSFX.PlayOneShot(0);
        int r = Random.Range(1, 11);

        for (int i = 0; i < r; i++)
            Instantiate(goldReward, (transform.position + new Vector3(Random.Range(-0.35f, 0.35f), Random.Range(-0.35f, 0.35f), 0)), transform.rotation);

        //GameManager._instance.AddGold(pointsToAdd);
    }
}
