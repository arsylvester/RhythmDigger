using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomBlock : MonoBehaviour
{
    [SerializeField] Block[] PossibleBlocks;

    private void Awake()
    {
        Instantiate(PossibleBlocks[Random.Range(0, PossibleBlocks.Length)].gameObject, transform.position, transform.rotation, transform.parent);
        Destroy(gameObject);
    }
}
