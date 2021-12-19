using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI goldText;
    [SerializeField] TextMeshProUGUI goldTextEnd;
    [SerializeField] TextMeshProUGUI depthCounter;
    [SerializeField] GameObject endUI;

    public static UIManager _instance;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        GameManager.OnGoldGained += UpdateGoldCount;
    }

    public void UpdateGoldCount(int gold)
    {
        goldText.text = "Gold: " + gold;
    }

    public void ShowEndUI(int gold, int depth)
    {
        goldText.gameObject.SetActive(false);
        goldTextEnd.text = "Gold found: " + gold;
        depthCounter.text = "Depth Reached: " + depth;
        endUI.SetActive(true);
    }

    public void ResetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
