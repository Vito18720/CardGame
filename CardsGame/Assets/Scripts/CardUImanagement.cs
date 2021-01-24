using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardUImanagement : MonoBehaviour
{
    //Clase encargada del actualizado de la información de la carta
    //public CardCreatorManager cardValues;
    public bool cardUse;
    [Space]
    public Image champImage;
    public Text nameText, infoText;
    public Text manaCostText, damageText, resistenceText;

    void Awake()
    {
        cardUse = true;
    }

    public void RefreshCard(CardCreatorManager cardV)
    {
        Debug.Log(cardV.name);
        champImage.sprite = cardV.image;
        nameText.text = cardV.name;
        infoText.text = cardV.description;
        manaCostText.text = cardV.manaCost.ToString();
        damageText.text = cardV.damage.ToString();
        resistenceText.text = cardV.resistence.ToString();
    }
}
