using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CardsStructure : MonoBehaviour
{
    #region Singleton
    //Uso del patrón singleton para tener una instancia estática de la clase de estructuración del juego
    public static CardsStructure instance;

    private void Awake()
    {
        instance = instance ? instance : this;
    }
    #endregion

    //Clase encargada de asignar  con un numero limitado de ellas un array (la baraja completa) una serie de cartas (de una lista con cada tipo de carta)
    public List<CardCreatorManager> cardsType;

    public int nCards;

    public GameObject cardPrefab;

    public GameObject hand1Parent, hand2parent;

    //Arrays de las cartas totales de ambos jugadores
    [SerializeField]
    private CardCreatorManager[] deck1, deck2;

    //Lista de las cartas de la mano actual de ambos jugadores
    private List<GameObject> hand1, hand2;

    private int nCardUse = 0;

    private void Start()
    {
        //para probar el funcionamiento de la creacion de cartas no se desarrollara la parte de seleccionar las cartas de la mano, sino que ser� aleatorio
        deck1 = CreateHand();
        deck2 = CreateHand();

        //Pool de 5 cartas maximas por mano
        hand1 = Pooler.GetPoolObjects(cardPrefab, 5, hand1Parent);
        //Asigno la mano de cada pool de cartas
        foreach (var c in hand1) c.GetComponent<Dragable>().typeOfHand = Dragable.Hands.Hand1;
        hand2 = Pooler.GetPoolObjects(cardPrefab, 5, hand2parent);
        foreach (var c in hand2) c.GetComponent<Dragable>().typeOfHand = Dragable.Hands.Hand2;

        //Resetear las transformaciones del Rect Transform
        ResetTransforms(hand1);
        ResetTransforms(hand2);

        //Seteo los tiops de cartas a los valores de la mano generada
        CardSetter(hand1, deck1);
        CardSetter(hand2, deck2);
    }

    //Crea la baraja
    private CardCreatorManager[] CreateHand()
    {
        CardCreatorManager[] cardsSelected = new CardCreatorManager[nCards];
        
        for (int i = 0; i < nCards; i++)
        {
            cardsSelected[i] = cardsType[Random.Range(0, cardsType.Count)];
        }

        return cardsSelected;
    }

    //Al meterlos en un canvas el rect transform se desconfigura, y aqui lo reseteo a los valores requeridos
    private void ResetTransforms(List<GameObject> hand)
    {
        foreach(var card in hand)
        {
            RectTransform cardTransform = card.GetComponent<RectTransform>();
            cardTransform.localPosition = Vector3.zero;
            cardTransform.localRotation = Quaternion.Euler(Vector3.zero);
            cardTransform.localScale = new Vector3(0.5f, 1f, 0f);
        }
    }
    
    //Configura la mano en base a la baraja previamente creada
    private void CardSetter(List<GameObject> hand, CardCreatorManager[] deck)
    {
        foreach (var card in hand)
        {
            if (card.GetComponent<CardUImanagement>().cardUse)
            {
                if (nCardUse < deck.Length )
                {
                    Debug.Log("refresh" + deck[(deck.Length - 1) - nCardUse]);
                    card.GetComponent<CardUImanagement>().RefreshCard(deck[(deck.Length - 1) - nCardUse]);
                    card.GetComponent<CardUImanagement>().cardUse = false;

                    nCardUse++;
                }
                else
                {
                    Debug.Log("Not enough cards");
                    break;
                }
            }

            

            if (!card.gameObject.activeInHierarchy)
            {
                
                card.gameObject.SetActive(true);
            }
        }
    }
}
