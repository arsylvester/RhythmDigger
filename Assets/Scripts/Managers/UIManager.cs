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
    [SerializeField] GameObject resetUIobj;
    [SerializeField] float notifDuration = 1f;
    [SerializeField] float resetWaitTime = 3f, resetTransitionTime = 1f;
    private float timeOfLastKey = 0f,  activeTime=0;
    private Queue<Sequence> NotificationQueue = new Queue<Sequence>();


    public static UIManager _instance;

    private int prevGoldMult = 1;
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


    private void FixedUpdate()
    {
        activeTime += Time.deltaTime;
        // This will cause the "Press ESC to reset" ui to appear
        if( Input.anyKeyDown)
        {
            timeOfLastKey = activeTime;
            HideResetUI();
        }
        if (Mathf.Abs(activeTime - timeOfLastKey) > resetWaitTime & resetUIobj.activeSelf == false & !Conductor._instance.gameIsOver)
        {
            ShowResetUI();
        }
          
    }
    
    private Vector2 resetUIinitalPos;
    void Start()
    {
        GameManager.OnGoldGained += UpdateGoldCount;
        UpdateGoldCount(0);
        resetUIinitalPos = resetUIobj.GetComponent<RectTransform>().anchoredPosition;
    }

    public void UpdateGoldCount(int gold)
    {
        goldText.text = gold.ToString();//.PadLeft(5);
        // NotificationText("+"+gold+" gold!");
        // goldText.text = "Gold:" + gold.ToString().PadLeft(4);
    }

    public void UpdateChainCount(int chain)
    {
        // chainText.text = "Chain: " + chain.ToString().PadLeft(5);
        chainText.text = chain.ToString();//.PadLeft(5);
        int newGoldMult = GameManager._instance.goldMultiplier;    
        if(newGoldMult > prevGoldMult)
        {
            NotificationText("x"+newGoldMult+" multiplier!");
        }
        multText.text = "Multiplier x" + newGoldMult.ToString();//.PadLeft(5);
        prevGoldMult = newGoldMult;
    }

    public void QuitButton()
    {
        SceneManager.LoadScene("Start Screen");
    }

    public void ShowResetUI()
    {
        resetUIobj.GetComponent<CanvasGroup>().alpha = 0;
        resetUIobj.SetActive(true);
        resetUIobj.GetComponent<CanvasGroup>().DOFade(1f,resetTransitionTime);
        RectTransform rt = resetUIobj.GetComponent<RectTransform>();    
        rt.anchoredPosition = rt.anchoredPosition + new Vector2(0,5);
        rt.DOAnchorPos(resetUIinitalPos,resetTransitionTime*5);
    }

    public void HideResetUI()
    {
        resetUIobj.SetActive(false);
    }

    public void HideUI()
    {
        goldText.gameObject.SetActive(false);
        chainText.gameObject.SetActive(false);
        multText.gameObject.SetActive(false);
        goldTextLabel.gameObject.SetActive(false);
        chainTextLabel.gameObject.SetActive(false);
        resetUIobj.SetActive(false);
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

    public void NotificationText(string notificationText)
    {
        Notification newNotif = new Notification(notificationText);
        
        GameObject textGo = Instantiate(notifTextPrefab, notifStartPost.anchoredPosition, Quaternion.identity, notifParentUI) as GameObject;
        textGo.GetComponent<TextMeshProUGUI>().text = notificationText;
        textGo.GetComponent<RectTransform>().anchoredPosition = notifStartPost.anchoredPosition;

        textGo.GetComponent<RectTransform>().DOAnchorPos(notifEndPos.anchoredPosition,notifDuration).OnComplete(() => Destroy(textGo));
 
        textGo.GetComponent<TMP_Text>().DOFade(0,notifDuration*0.5f);
        // textGo.GetComponent<TextMeshProUGUI>().DOfade(0,1);
        // textGo.GetComponent<TextMeshProUGUI>().color.DOfade(0,1);
        // DG.dof
        // Add sequence to queue
        // Check
    }

    
}
