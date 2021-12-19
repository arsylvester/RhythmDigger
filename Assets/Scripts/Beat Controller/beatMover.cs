using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BeatMover : MonoBehaviour
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

    public void StartMove(Vector3 endPos, float musicBPM, int beatsOnScreen){
        rectTransform = GetComponent<RectTransform>();
        startPosition = new Vector3(rectTransform.anchoredPosition.x,0,0);
        endPosition = endPos;
        lerpDuration = 60f/musicBPM*beatsOnScreen;
        StartCoroutine(Move());
    }

    public void StopMove()
    {
        StopCoroutine(Move());
    }

    IEnumerator Move()
    {
        while (currentTime <= lerpDuration)
        {
            currentTime += Time.deltaTime;
            normalizedValue = currentTime / lerpDuration; // we normalize our time 

            rectTransform.anchoredPosition = Vector3.Lerp(startPosition, endPosition, normalizedValue);
            yield return null;
        }
        rectTransform.anchoredPosition = endPosition;
        Conductor.Instance.heartbeatAnimator.Play("heartBeat_heartBeat", 0, 0);
        Conductor.Instance.currentBeats.Remove(gameObject);
        yield return new WaitForSeconds(0.05f);

        Destroy(gameObject);
    } 
}
