using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

//Clase para los slot del tablero que se comunican entre el script dragable de las cartas para determinar si una carta se puede o no posicionar en el
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

    //Compruebo si la carta puede o no soltarse sobre el slot dependiendo del la mano a la que pertenezcan ambos
    //(puedo porque el bloqueo de raycast de la carta esta desactivado)
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

    //Cuando suelto la carta y si esta sobre un slot valido la emparento a el 
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

    //Al dejar de apuntar con el raton al slot vualve a su estado normal 
    public void OnPointerExit(PointerEventData eventData)
    {
        startColor.a = 0;
        GetComponent<Image>().color = startColor;
    }
}
