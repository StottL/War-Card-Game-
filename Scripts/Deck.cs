using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The class that builds a deck
/// </summary>
public class Deck : MonoBehaviour
{
    public GameObject[] cardPrefabs;

    public Stack<GameObject> deck = new Stack<GameObject>();

    /// <summary>
    /// Creates a set of 52 random GameObjects as cards
    /// based on the prefabs stored in cardPrefabs
    /// </summary>
    public void BuildDeck()
    {
        int a = 0;
        int tempRandomNum;
        GameObject thing;
        for (int i = 0; i < 52; i++)
        {
            tempRandomNum = Random.Range(0, cardPrefabs.Length);
            thing = Instantiate(cardPrefabs[tempRandomNum]);
            
            a++;

            thing.name = $"Card {a}";

            deck.Push(thing);
        }
    }
}