using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;

public class LoftController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{   

    private TMP_Text loft_text; 
    private ThrowBall script;

    public bool isTouching = false;


    void Start(){
        loft_text = transform.GetChild(3).GetComponent<TMP_Text>();
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        isTouching = true;
    }
 
    public void OnPointerExit(PointerEventData eventData)
    {
        isTouching = false;
        
    }

    public void changeLoftText(Slider slider) 
    {
        loft_text.text = slider.value + "°";
    }

}
