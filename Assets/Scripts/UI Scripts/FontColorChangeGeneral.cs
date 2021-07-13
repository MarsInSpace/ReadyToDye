using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class FontColorChangeGeneral : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] Color Highlight;
    Color DefaultColor;
    TMP_Text Text;

    void Start()
    {
        Text = GetComponent<TMP_Text>();
        DefaultColor = Text.color;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Text.color = Highlight;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Text.color = DefaultColor;
    }
}

