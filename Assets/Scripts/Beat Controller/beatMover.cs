using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class beatMover : MonoBehaviour
{
    private Vector3 startLocation;
    [SerializeField]
    float lerpDuration = 3; 
    [SerializeField]
    float startValue = 0; 
    [SerializeField]
    float endValue = 10; 
    [SerializeField]
    float valueToLerp;

    [SerializeField]
    float speedModifier = 5f;

    float timeOfTravel=5; //time after object reach a target place 
    float currentTime=0; // actual floting time 
    float normalizedValue;
    public Vector3 startPosition, endPosition;
    private RectTransform rectTransform;
    public Image beatImage;
    // Start is called before the first frame update
    void Start()
    {
        // rectTransform = GetComponent<RectTransform>();
        beatImage.SetNativeSize();
        
    }

    public void StartMove(Vector3 endPos, float musicBPM){
        rectTransform = GetComponent<RectTransform>();
        // startPosition = rectTransform.position;
        startPosition = new Vector3(rectTransform.anchoredPosition.x,0,0);
        endPosition = endPos;
        lerpDuration = 60f/musicBPM*4;
        // endPosition = startPosition + new Vector3(100,0,0);
        StartCoroutine(Move());
    }

    public void StopMove()
    {
        StopCoroutine(Move());
    }

    IEnumerator Move()
    // {
    //     float timeElapsed = 0;

    //     while (timeElapsed < lerpDuration)
    //     {
    //         valueToLerp = Mathf.Lerp(startValue, endValue, timeElapsed / lerpDuration);
    //         timeElapsed += Time.deltaTime;

    //         yield return null;
    //     }

    //     valueToLerp = endValue;
    // }
    { 
        while (currentTime <= lerpDuration) { 
            currentTime += Time.deltaTime; 
            normalizedValue=currentTime/lerpDuration; // we normalize our time 
        
            rectTransform.anchoredPosition=Vector3.Lerp(startPosition, endPosition, normalizedValue); 
            // rectTransform.localPosition=Vector3.Lerp(startPosition, endPosition, normalizedValue); 
            yield return null; 
            }
        rectTransform.anchoredPosition = endPosition;
        yield return new WaitForSeconds(0.05f);
        Conductor.instance.currentBeats.Remove(gameObject);
        // Conductor.instance.currentBeats.RemoveAt(0);
        Destroy(gameObject);
        Conductor.instance.validBuffer = 6;
    }
    
}
