using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMenuUIManager : MonoBehaviour
{
    [SerializeField] GameObject go_Title, go_ReturnBT, go_MainLayoutGroup, go_CreditsLayoutGroup;
    [SerializeField] float delayBetweenWords = 0.75f;
    bool started = false;
    private RectTransform titleRectTransform;
    [SerializeField] public Vector3 startPosition, endPosition, creditsPosition;
    [SerializeField] float lerpDuration = 3; 
    float currentTime=0, normalizedValue;
    
    // Start is called before the first frame update
    void Start()
    {
        titleRectTransform = go_Title.GetComponent<RectTransform>();
        LoadMainMenu();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void QuitButton()
    {
        Application.Quit();
    }

    public void StartButton()
    {
        SceneManager.LoadScene("Main Scene");
    }

    public void CreditsButton()
    {
        currentTime=0;
        go_MainLayoutGroup.SetActive(false);

        // StartCoroutine(CreditsLoadSimple());
        go_CreditsLayoutGroup.SetActive(true);
        go_ReturnBT.SetActive(true);
    }

    

    public void ReturnButton()
    {
        // go_MainLayoutGroup.SetActive(true);
        // go_ReturnBT.SetActive(false);
        go_ReturnBT.SetActive(false);
        go_CreditsLayoutGroup.SetActive(false);
        currentTime=0;
        // StartCoroutine(ReturnToMainMenu());
        go_MainLayoutGroup.SetActive(true);
    }

    

    public void SettingsButton()
    {
        Application.Quit();
    }

    
    public void LoadMainMenu()
    {
        // while (currentTime <= lerpDuration)
        // {
        //     currentTime += Time.deltaTime;
        //     normalizedValue = currentTime / lerpDuration; 

        //     titleRectTransform.anchoredPosition = Vector3.Lerp(startPosition, creditsPosition, normalizedValue);
        //     yield return null;
        // }
        // titleRectTransform.anchoredPosition = creditsPosition;
        // StartCoroutine(TurnOnMainButtons());
        // StartCoroutine(TurnOnLayoutText(go_MainLayoutGroup, true));
        TextMeshProUGUI titleText = go_Title.GetComponent<TextMeshProUGUI>();
        titleText.color = new Color(titleText.color.r, titleText.color.g, titleText.color.b, 0);
        foreach(Transform trans in go_MainLayoutGroup.transform)
        {
            GameObject childGo = trans.gameObject;
            TextMeshProUGUI childText = childGo.GetComponentInChildren<TextMeshProUGUI>();
            childText.color = new Color(childText.color.r, childText.color.g, childText.color.b, 0);
        } 
        
        //startPosition = titleRectTransform.anchoredPosition;
        StartCoroutine(LerpObjByRect(go_Title, startPosition, endPosition, lerpDuration));
        StartCoroutine(FadeTextToFullAlpha(lerpDuration*2,go_Title.GetComponent<TextMeshProUGUI>()));
        currentTime=0;
        // StartCoroutine(LoadMainMenu());
        StartCoroutine(TurnOnLayoutText(go_MainLayoutGroup, true));
    }

    IEnumerator TurnOnLayoutText(GameObject layoutGroup, bool inChildren)
    {
        layoutGroup.SetActive(true);
        // if(!inChildren){
        //     StartCoroutine(FadeTextToFullAlpha(lerpDuration,layoutGroup.transform.GetComponentInChildren<TextMeshProUGUI>()));
        // }
        // else
        // {        
        GameObject childGo;
        foreach(Transform trans in layoutGroup.transform)
        {
            childGo = trans.gameObject;
            childGo.SetActive(true);
        } 
        yield return new WaitForSeconds(delayBetweenWords*2);
        foreach(Transform trans in layoutGroup.transform)
        {
            childGo = trans.gameObject;
            if(inChildren)
                StartCoroutine(FadeTextToFullAlpha(lerpDuration,childGo.GetComponentInChildren<TextMeshProUGUI>()));
            else
                StartCoroutine(FadeTextToFullAlpha(lerpDuration,childGo.GetComponent<TextMeshProUGUI>()));
            yield return new WaitForSeconds(delayBetweenWords);
        } 
        // }
           
        
    }
    IEnumerator TurnOffLayoutText(GameObject layoutGroup, bool inChildren)
    {
        if(!inChildren){
            StartCoroutine(FadeTextToZeroAlpha(lerpDuration,layoutGroup.transform.GetComponentInChildren<TextMeshProUGUI>()));
        }
        else{
            GameObject childGo;
            yield return new WaitForSeconds(delayBetweenWords);
            foreach(Transform trans in layoutGroup.transform)
            {
                childGo = trans.gameObject;
                
                if(inChildren)
                    StartCoroutine(FadeTextToZeroAlpha(lerpDuration,childGo.GetComponentInChildren<TextMeshProUGUI>()));
                else
                    StartCoroutine(FadeTextToZeroAlpha(lerpDuration,childGo.GetComponent<TextMeshProUGUI>()));
            }  
            yield return new WaitForSeconds(delayBetweenWords*3);  
            foreach(Transform trans in layoutGroup.transform)
            {
                childGo = trans.gameObject;
                childGo.SetActive(false);
            } 
        }
        
        layoutGroup.SetActive(false);
    }

    public IEnumerator LerpObjByRect(GameObject obj, Vector3 start, Vector3 end, float duration)
    {
        RectTransform rect = obj.GetComponent<RectTransform>();
        currentTime=0;
        while (currentTime <= duration)
        {
            currentTime += Time.deltaTime;
            normalizedValue = currentTime / duration; // we normalize our time 

            rect.anchoredPosition = Vector3.Lerp(start, end, normalizedValue);
            yield return null;
        }
        rect.anchoredPosition = end;
    }
    public IEnumerator FadeTextToFullAlpha(float t, TextMeshProUGUI i)
    {
        i.color = new Color(i.color.r, i.color.g, i.color.b, 0);
        while (i.color.a < 1.0f)
        {
            // titleRectTransform.anchoredPosition = Vector3.Lerp(startPosition, endPosition, normalizedValue);
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a + (Time.deltaTime / t));
            yield return null;
        }
    }

    public IEnumerator FadeTextToZeroAlpha(float t, TextMeshProUGUI i)
    {
        i.color = new Color(i.color.r, i.color.g, i.color.b, 1);
        while (i.color.a > 0.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a - (Time.deltaTime / t));
            yield return null;
        }
    }


    IEnumerator CreditsLoadSimple()
    {
        while (currentTime <= (lerpDuration/2))
        {
            currentTime += Time.deltaTime;
            normalizedValue = currentTime / (lerpDuration/2);  

            titleRectTransform.anchoredPosition = Vector3.Lerp(endPosition, creditsPosition, normalizedValue);
            yield return null;
        }
        yield return new WaitForSeconds(0.1f);
        titleRectTransform.anchoredPosition = creditsPosition;
    }
    IEnumerator CreditsLoad()
    {
        // yield return StartCoroutine(TurnOffMainButtons());   
        while (currentTime <= lerpDuration)
        {
            currentTime += Time.deltaTime;
            normalizedValue = currentTime / lerpDuration; // we normalize our time 

            titleRectTransform.anchoredPosition = Vector3.Lerp(endPosition, creditsPosition, normalizedValue);
            yield return null;
        }
        yield return new WaitForSeconds(0.1f);
        titleRectTransform.anchoredPosition = creditsPosition;
        StartCoroutine(TurnOffLayoutText(go_MainLayoutGroup, true));
        
        go_ReturnBT.SetActive(true);
        StartCoroutine(FadeTextToFullAlpha(lerpDuration,go_ReturnBT.GetComponentInChildren<TextMeshProUGUI>()));
        // go_CreditsLayoutGroup.SetActive(true);
        StartCoroutine(TurnOnLayoutText(go_CreditsLayoutGroup, false));
    }
    IEnumerator ReturnToMainMenuSimple()
    {
        while (currentTime <= lerpDuration)
        {
            currentTime += Time.deltaTime;
            normalizedValue = currentTime / lerpDuration; // we normalize our time 

            titleRectTransform.anchoredPosition = Vector3.Lerp(creditsPosition, endPosition, normalizedValue);
            yield return null;
        }
        titleRectTransform.anchoredPosition = endPosition;
    }

    IEnumerator ReturnToMainMenu()
    {
        StartCoroutine(TurnOffLayoutText(go_CreditsLayoutGroup, false));
        // go_CreditsLayoutGroup.SetActive(false);
        StartCoroutine(FadeTextToZeroAlpha(lerpDuration,go_ReturnBT.GetComponentInChildren<TextMeshProUGUI>()));
        go_ReturnBT.SetActive(false);

        while (currentTime <= lerpDuration)
        {
            currentTime += Time.deltaTime;
            normalizedValue = currentTime / lerpDuration; // we normalize our time 

            titleRectTransform.anchoredPosition = Vector3.Lerp(creditsPosition, endPosition, normalizedValue);
            yield return null;
        }
        titleRectTransform.anchoredPosition = endPosition;
        
        StartCoroutine(TurnOnLayoutText(go_MainLayoutGroup, true));
        
    }
}
