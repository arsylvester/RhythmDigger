using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI goldText, goldTextLabel;
    [SerializeField] TextMeshProUGUI goldTextEnd;
    [SerializeField] TextMeshProUGUI chainText, chainTextLabel;
    [SerializeField] TextMeshProUGUI multText;
    [SerializeField] TextMeshProUGUI longestChainTextEnd;
    [SerializeField] TextMeshProUGUI depthCounter;
    [SerializeField] GameObject endUI;
    [SerializeField] GameObject notifTextPrefab;
    [SerializeField] RectTransform notifStartPost, notifEndPos, notifParentUI;
    [SerializeField] float notifDuration = 1f;

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
        NotificationText("+"+gold+" gold!");
        // goldText.text = "Gold:" + gold.ToString().PadLeft(4);
    }

    public void UpdateChainCount(int chain)
    {
        // chainText.text = "Chain: " + chain.ToString().PadLeft(5);
        chainText.text = chain.ToString().PadLeft(5);
        multText.text = "Multiplier x" + Conductor.Instance.goldMultiplier.ToString().PadLeft(5);
    }

    public void QuitButton()
    {
        SceneManager.LoadScene("Start Screen");
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

    public void NotificationText(string notification)
    {
        GameObject textGo = Instantiate(notifTextPrefab, notifStartPost.anchoredPosition, Quaternion.identity, notifParentUI) as GameObject;
        textGo.GetComponent<TextMeshProUGUI>().text = notification;
        textGo.GetComponent<RectTransform>().anchoredPosition = notifStartPost.anchoredPosition;

        textGo.GetComponent<RectTransform>().DOMove(notifEndPos.anchoredPosition,notifDuration);
 
        textGo.GetComponent<TMP_Text>().DOFade(0,notifDuration).OnComplete(() => Destroy(textGo));
        // textGo.GetComponent<TextMeshProUGUI>().DOfade(0,1);
        // textGo.GetComponent<TextMeshProUGUI>().color.DOfade(0,1);
        // DG.dof

    }
}
