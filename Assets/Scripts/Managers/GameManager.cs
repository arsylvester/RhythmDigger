using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private int gold;
    public int goldMultiplier = 1;

    public delegate void GoldGained(int num);
    public static event GoldGained OnGoldGained = delegate {};

    public delegate void GameOver();
    public static event GameOver OnGameOver;

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
        gold += goldAdded * goldMultiplier;
        OnGoldGained(gold);
    }

    public int GetGold()
    {
        return gold;
    }

    public void SetGameOver()
    {
        OnGameOver();
    }
}
