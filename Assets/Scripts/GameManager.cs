using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private int gold;

    public delegate void GoldGained();
    public static event GoldGained OnGoldGained;

    public static GameManager _instance;

    private void Awake()
    {
        if(_instance == null)
        {
            _instance = this;
        }
        else if(_instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void AddGold(int goldAdded)
    {
        gold += goldAdded;
        OnGoldGained();
    }
}
