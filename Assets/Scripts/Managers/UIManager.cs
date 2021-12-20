using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI goldText, goldTextLabel;
    [SerializeField] TextMeshProUGUI goldTextEnd;
    [SerializeField] TextMeshProUGUI chainText, chainTextLabel;
    [SerializeField] TextMeshProUGUI multText;
    [SerializeField] TextMeshProUGUI longestChainTextEnd;
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
        UpdateGoldCount(0);
    }

    public void UpdateGoldCount(int gold)
    {
        goldText.text = gold.ToString().PadLeft(5);
        // goldText.text = "Gold:" + gold.ToString().PadLeft(4);
    }

    public void UpdateChainCount(int chain)
    {
        // chainText.text = "Chain: " + chain.ToString().PadLeft(5);
        chainText.text = chain.ToString().PadLeft(5);
        multText.text = "Multiplier x" + Conductor.Instance.goldMultiplier.ToString().PadLeft(5);
    }

    public void HideUI()
    {
        goldText.gameObject.SetActive(false);
        chainText.gameObject.SetActive(false);
        multText.gameObject.SetActive(false);
        goldTextLabel.gameObject.SetActive(false);
        chainTextLabel.gameObject.SetActive(false);
    }

    public void ShowEndUI(int gold, int depth, int highestChain)
    {   
        goldTextEnd.text = "Gold found: " + gold;
        longestChainTextEnd.text = "Longest chain: " + highestChain; 
        depthCounter.text = "Depth Reached: " + depth;
        endUI.SetActive(true);
    }

    public void ResetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
