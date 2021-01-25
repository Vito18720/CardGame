using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ShowCardsHand : MonoBehaviour
{
    public Dragable.Hands typeOfHand;

    // Start is called before the first frame update
    void Start()
    {
        Dragable.moveEvent += MoveCards;
    }

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
