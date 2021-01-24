using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class Dragable : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Vector3 offset;

    internal Transform previousParent;

    public enum Hands { Hand1, Hand2 };
    public Hands typeOfHand;

    [Space]
    [Range(100,200)]
    public int moveStep;

    public static Action<int, Hands> moveEvent; 

    public void OnBeginDrag(PointerEventData eventData)
    {
        //Declare the offset
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        Physics.Raycast(ray, out hit, Mathf.Infinity);
        offset = transform.position - hit.point;

        //Update the canvas layout
        previousParent = this.transform.parent;
        if (!transform.parent.gameObject.CompareTag("Slot"))
        {
            this.transform.SetParent(this.transform.parent.parent);
        }

        //Block Raycast off
        GetComponent<CanvasGroup>().blocksRaycasts = false;

        //asignar la carta que se esta moviendo
        Dropable.d = this;
    }

    //Clase encargada del movimiento de las cartas sobre el tablero
    public void OnDrag(PointerEventData eventData)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
    
        if (Physics.Raycast(ray,out hit, Mathf.Infinity, 1 << LayerMask.NameToLayer("Tablero")))
        {
            transform.position = hit.point + offset;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        this.transform.SetParent(previousParent);
        GetComponent<RectTransform>().localPosition = Vector3.zero;

        GetComponent<CanvasGroup>().blocksRaycasts = true;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //Subir carta para ver sus informacion
        if (!GetComponent<CardUImanagement>().cardUse)
        {
            moveEvent?.Invoke(moveStep, typeOfHand);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //Volver la carta a su posicion 
        if (!GetComponent<CardUImanagement>().cardUse)
        {
            moveEvent?.Invoke(-moveStep, typeOfHand);
        }
    }
}
