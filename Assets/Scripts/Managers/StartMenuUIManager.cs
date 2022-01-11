using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class StartMenuUIManager : MonoBehaviour
{
    [SerializeField] GameObject go_Title, go_MainLayoutGroup, go_CreditsPanel;
    [SerializeField] float delayBetweenWords = 0.75f;
    private RectTransform titleRectTransform;
    [SerializeField] float fadeDuration = 6f, swoopDuration = 3f;
    private Vector2 titleIntialAnchoredPos;
    [SerializeField] public Vector2 titleSwoopInOffset;
    [SerializeField] public TimelineController timelineController;
    
    // Start is called before the first frame update
    void Start()
    {
        titleRectTransform = go_Title.GetComponent<RectTransform>();
        titleIntialAnchoredPos = titleRectTransform.anchoredPosition;
        LoadMainMenu();
        try{
            timelineController = TimelineController._instance;
        } catch {}
    }

    public void QuitButton()
    {
        Application.Quit();
    }

    public void StartButton()
    {
        Debug.Log("Starting game");
        if(timelineController)
        {
            timelineController.PlayTimeline_StartGame();
        }
        else
        {
            SceneManager.LoadScene("Main Scene");
        }
        
    }

    public void CreditsButton()
    {
        go_MainLayoutGroup.SetActive(false);
        go_CreditsPanel.SetActive(true);
    }   

    public void ReturnButton()
    {
        go_CreditsPanel.SetActive(false);
        go_MainLayoutGroup.SetActive(true);
        // StartCoroutine(TurnOnLayoutText(go_MainLayoutGroup, true));
    }

    public void SettingsButton()
    {
        Debug.Log("Settings button pressed!");
    }

    
    public void LoadMainMenu()
    {
        TextMeshProUGUI titleText = go_Title.GetComponent<TextMeshProUGUI>();
        titleText.color = new Color(titleText.color.r, titleText.color.g, titleText.color.b, 0);
        titleText.DOFade(1f,fadeDuration);
        titleText.rectTransform.anchoredPosition += titleSwoopInOffset;
        titleText.rectTransform.DOAnchorPos(titleIntialAnchoredPos, swoopDuration);

        StartCoroutine(TurnOnLayoutText(go_MainLayoutGroup, true));
    }

    IEnumerator TurnOnLayoutText(GameObject layoutGroup, bool inChildren)
    {
        layoutGroup.SetActive(true);      
        foreach(RectTransform rt in layoutGroup.transform)
        {
            rt.gameObject.SetActive(true);
            rt.GetComponent<CanvasGroup>().alpha = 0;
        } 
        yield return new WaitForSeconds(delayBetweenWords);
        foreach(RectTransform rt in layoutGroup.transform)
        {
            rt.GetComponent<CanvasGroup>().DOFade(1,fadeDuration);
            yield return new WaitForSeconds(delayBetweenWords);
        }                
    }
}
