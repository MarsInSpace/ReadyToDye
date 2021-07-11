using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class FontColorChange : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public TMP_Text Text;

    void Start()
    {
        Text.GetComponent<TMP_Text>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Text.color = FontColorMaster.Instance.Beige;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Text.color = FontColorMaster.Instance.Orange;
    }
}

