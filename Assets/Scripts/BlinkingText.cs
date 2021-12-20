using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BlinkingText : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI TMProtext;
    [SerializeField] float onTime = 1f, offTime = 0.5f;
    void Start()
    {
        TMProtext = GetComponent<TextMeshProUGUI>();
        StartBlinking();
    }
    IEnumerator Blink()
    {
        while (true)
        {
            TMProtext.enabled = false;
            yield return new WaitForSeconds(offTime);
            TMProtext.enabled = true;
            yield return new WaitForSeconds(onTime);
        }
    
    }

    public void StartBlinking(){
        StartCoroutine("Blink");
    }
    public void StopBlinking()
    {
        StopCoroutine("Blink");
    }
}
