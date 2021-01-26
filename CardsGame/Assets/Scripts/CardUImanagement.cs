using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardUImanagement : MonoBehaviour
{
    //Clase encargada del actualizado de la información de la carta
    //public CardCreatorManager cardValues; ya no es necesario porque se lo paso directamente a la funcion
    public bool cardUse;
    [Space]
    public Image champImage;
    public Text nameText, infoText;
    public Text manaCostText, damageText, resistenceText;

    void Awake()
    {
        cardUse = true;
    }

    //Funcion para actualizar la información de la carta en base a unos valores que se le pasan
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
