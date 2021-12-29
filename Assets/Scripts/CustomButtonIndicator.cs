using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class CustomButtonIndicator : MonoBehaviour,
    IPointerEnterHandler, IPointerExitHandler, ISelectHandler , IDeselectHandler//, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] public  GameObject UiSelector, UiSelected, UiPressed;
    
    [SerializeField] public  Color colorDarkText, colorLightText;
    [SerializeField] public  bool hasText = false;
    [SerializeField] public TMP_Text tmpText;

    [SerializeField] public bool disableVisuals = false;

    //private MenuManager menuManager;

    private void Start()
    {
        if(hasText == true)
        {
            if (tmpText == null)
            {
                Debug.LogError("Button '" + this.name + "': TMP obj ref not given in editor");
            }
        }
        //menuManager = MenuManager.Instance;
    }

    private void OnDisable()
    {
        UiSelected.SetActive(false);
    }

    public void OnSelect(BaseEventData eventData)
    {
        if(UiSelected != null && disableVisuals == false)
            UiSelected.SetActive(true);
    }

    public void OnDeselect(BaseEventData eventData)
    {
        if (UiSelected != null && disableVisuals == false)
            UiSelected.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (disableVisuals == false)
            UiSelector.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (disableVisuals == false)
            UiSelector.SetActive(false);
    }

    // public void OnPointerDown(PointerEventData eventData)
    // {
    //     if (disableVisuals == false)
    //     {
    //         UiPressed.SetActive(true);
    //         if (hasText == true)
    //         {
    //             tmpText.color = colorDarkText;
    //         }
    //     }       
    // }

    // public void OnPointerUp(PointerEventData eventData)
    // {
    //     if (disableVisuals == false)
    //     {
    //         UiPressed.SetActive(false);
    //         if (hasText == true)
    //         {
    //             tmpText.color = colorLightText;
    //         }
    //     }      
    // }
}
