using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk : MonoBehaviour
{
    public int height = 0;
    [SerializeField] int distanceToDespawn = 30;
    private PlayerController player;

    public void SetPlayer(PlayerController newPlayer)
    {
        player = newPlayer;
    }

    private void Update()
    {
        if (player.transform.position.y < transform.position.y - distanceToDespawn)
            Destroy(gameObject);
    }
}
