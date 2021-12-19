using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI goldText;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.OnGoldGained += UpdateGoldCount;
    }

    public void UpdateGoldCount(int gold)
    {
        goldText.text = "Gold: " + gold;
    }
}
