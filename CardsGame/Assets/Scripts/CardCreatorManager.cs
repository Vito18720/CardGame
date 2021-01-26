using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/CardCreatorManager", order = 1)]
public class CardCreatorManager : ScriptableObject
{
    //Una clase que hereda de scriptable object para crear los tipos de cartas según una serie de informacion y reutilizar así memoria (flyweight pattern)
    public int manaCost;
    public int damage;
    public int resistence;

    public Sprite image;

    public string name;
    [TextArea]
    public string description;
}
