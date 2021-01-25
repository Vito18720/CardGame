using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Dropable : MonoBehaviour, IPointerEnterHandler, IDropHandler, IPointerExitHandler
{
    private Color startColor;

    public Color dropable;
    public Color notDropable;

    public Dragable.Hands typeOfHand;

    public static Dragable d;

    private void Awake()
    {
        startColor = dropable;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(d != null)
        {
            if (typeOfHand == d.typeOfHand)
            {
                startColor = dropable;
            }
            else
            {
                startColor = notDropable;
            }
        }

        startColor.a = 0.5f;
        GetComponent<Image>().color = startColor;

    }

    public void OnDrop(PointerEventData eventData)
    {
        if(eventData.pointerDrag.GetComponent<Dragable>() != null)
        {
            d = eventData.pointerDrag.GetComponent<Dragable>();

            int i = transform.childCount;
            if(typeOfHand == d.typeOfHand && i == 0)
            {

                d.previousParent = this.transform;
                d.GetComponent<CardUImanagement>().cardUse = true;
            }
        }

        //cuando sueltas la carta automaticamente el componente que determina la carta desaparece
        d = null;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        startColor.a = 0;
        GetComponent<Image>().color = startColor;
    }
}
