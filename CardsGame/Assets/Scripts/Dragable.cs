using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

//Clase que permite a una carta moverse por el tablero siendo arrastrada por el raton
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

    //Determino un offset para posicionar luego la carta justo en la posicion en la que ha sido recogida
    public void OnBeginDrag(PointerEventData eventData)
    {
        //Declaro el offset
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        Physics.Raycast(ray, out hit, Mathf.Infinity);
        offset = transform.position - hit.point;

        //Actualizo el layout del canvas desenparentado la carta de la mano
        previousParent = this.transform.parent;
        if (!transform.parent.gameObject.CompareTag("Slot"))
        {
            this.transform.SetParent(this.transform.parent.parent);
        }

        //Bloqueo los raycast de la carta para poder comprobar si se puede soltar
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

    //Cuando suelto la carta emparento de nuevo la carta a la variable previous parent que puede ser la mano si no hay slot o el slot si la suletas sobre el
    public void OnEndDrag(PointerEventData eventData)
    {
        this.transform.SetParent(previousParent);
        GetComponent<RectTransform>().localPosition = Vector3.zero;

        GetComponent<CanvasGroup>().blocksRaycasts = true;
    }

    //Cuando paso el raton por las cartas que estan en la mano (si la carta no ha sido usada) elevo la mano para ver la informacion
    public void OnPointerEnter(PointerEventData eventData)
    {
        //Subir carta para ver sus informacion
        if (!GetComponent<CardUImanagement>().cardUse)
        {
            moveEvent?.Invoke(moveStep, typeOfHand);
        }
    }

    //Bajo la mano cuando el raton deja de apuntar a las cartas
    public void OnPointerExit(PointerEventData eventData)
    {
        //Volver la carta a su posicion 
        if (!GetComponent<CardUImanagement>().cardUse)
        {
            moveEvent?.Invoke(-moveStep, typeOfHand);
        }
    }
}
