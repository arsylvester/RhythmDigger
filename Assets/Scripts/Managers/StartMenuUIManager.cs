using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class StartMenuUIManager : MonoBehaviour
{
    [SerializeField] GameObject titleGO;
    [SerializeField] GameObject mainLayoutGroupGO; 
    [SerializeField] GameObject creditsPanelGO;
    [SerializeField] GameObject controlsParentGO;
    [SerializeField] GameObject btnCtrlNextGO;
    [SerializeField] GameObject btnCtrlPrevGO;
    public int currentControlsPage;
    [SerializeField] GameObject[] controlsPages;
    [SerializeField] float delayBetweenWords = 0.75f;
    private RectTransform titleRectTransform;
    [SerializeField] float fadeDuration = 6f, swoopDuration = 3f;
    private Vector2 titleIntialAnchoredPos;
    [SerializeField] public Vector2 titleSwoopInOffset;
    [SerializeField] public TimelineController timelineController;
    
    // Start is called before the first frame update
    void Start()
    {
        titleRectTransform = titleGO.GetComponent<RectTransform>();
        titleIntialAnchoredPos = titleRectTransform.anchoredPosition;
        LoadMainMenu();
        try{
            timelineController = TimelineController._instance;
        } catch {}
    }

    void Awake()
    {
        currentControlsPage = 0;
        
    }

    public void BtnQuit()
    {
        Application.Quit();
    }

    public void BtnStart()
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

    public void BtnControlsNext()
    {
        if(currentControlsPage < controlsPages.Length)
        {
            controlsPages[currentControlsPage].SetActive(false);
            currentControlsPage++;
            timelineController.ResetScene();
            controlsPages[currentControlsPage].SetActive(true);
            if(currentControlsPage == controlsPages.Length-1)
            {
                btnCtrlNextGO.gameObject.SetActive(false);
            }
            if(currentControlsPage > 0)
            {
                btnCtrlPrevGO.gameObject.SetActive(true);
            }
        }
    }

    public void BtnControlsPrev()
    {
        if(currentControlsPage > 0)
        {
            controlsPages[currentControlsPage].SetActive(false);
            currentControlsPage--;
            timelineController.ResetScene();
            controlsPages[currentControlsPage].SetActive(true);
            if(currentControlsPage == 0)
            {
                btnCtrlPrevGO.gameObject.SetActive(false);
            }
            if(controlsPages.Length > currentControlsPage)
            {
                btnCtrlNextGO.gameObject.SetActive(true);
            }
        }
    }

    public void BtnOpenControls()
    {
        mainLayoutGroupGO.SetActive(false);
        controlsParentGO.SetActive(true);
        currentControlsPage = 0;
        foreach (GameObject obj  in controlsPages)
        {
            obj.SetActive(false);
        }
        controlsPages[0].SetActive(true);
        btnCtrlPrevGO.gameObject.SetActive(false);
        if(controlsPages.Length > 1){
            btnCtrlNextGO.gameObject.SetActive(true);
        }
        else{
            btnCtrlNextGO.gameObject.SetActive(false);
        }
    }  

    public void BtnOpenCredits()
    {
        mainLayoutGroupGO.SetActive(false);
        creditsPanelGO.SetActive(true);
    }   

    // public void BtnControlsReturn()
    // {
    //     TimelineController._instance.ResetScene();
    //     creditsPanelGO.SetActive(false);
    //     controlsParentGO.SetActive(false);
    //     mainLayoutGroupGO.SetActive(true);

    //     // StartCoroutine(TurnOnLayoutText(go_MainLayoutGroup, true));
    // }
    public void BtnReturn()
    {
        TimelineController._instance.ResetScene();
        creditsPanelGO.SetActive(false);
        controlsParentGO.SetActive(false);
        mainLayoutGroupGO.SetActive(true);
        titleGO.SetActive(true);
        // StartCoroutine(TurnOnLayoutText(go_MainLayoutGroup, true));
    }

    public void SettingsButton()
    {
        Debug.Log("Settings button pressed!");
    }

    
    public void LoadMainMenu()
    {
        TextMeshProUGUI titleText = titleGO.GetComponent<TextMeshProUGUI>();
        //titleText.color = new Color(titleText.color.r, titleText.color.g, titleText.color.b, 0);
        //titleText.DOFade(1f,fadeDuration);
        titleText.rectTransform.anchoredPosition += titleSwoopInOffset;
        titleText.rectTransform.DOAnchorPos(titleIntialAnchoredPos, swoopDuration);

        StartCoroutine(TurnOnLayoutText(mainLayoutGroupGO, true));
    }

    IEnumerator TurnOnLayoutText(GameObject layoutGroup, bool inChildren)
    {
        layoutGroup.SetActive(true);      

        foreach(RectTransform rt in layoutGroup.transform)
        {
            rt.gameObject.SetActive(true);
            //rt.GetComponent<CanvasGroup>().DOFade(1,fadeDuration);
            yield return new WaitForSeconds(delayBetweenWords);
        }                
    }
}
