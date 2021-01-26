using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

//Clase para mostrar la mano dependiendo de que mano señale el raton
public class ShowCardsHand : MonoBehaviour
{
    public Dragable.Hands typeOfHand;
    
    void Start()
    {
        Dragable.moveEvent += MoveCards;
    }

    //Dependiendo del valor se moverá hacia arriba o hacia abajo
    void MoveCards(int moveStp, Dragable.Hands type)
    {
       if(typeOfHand == type)
        {
            if(typeOfHand == Dragable.Hands.Hand1)
            {
                Vector3 pos = GetComponent<RectTransform>().localPosition;
                pos.y += moveStp;
                GetComponent<RectTransform>().localPosition = pos;
            }
            else
            {
                Vector3 pos = GetComponent<RectTransform>().localPosition;
                pos.y -= moveStp;
                GetComponent<RectTransform>().localPosition = pos;
            }
        }
    }
}
