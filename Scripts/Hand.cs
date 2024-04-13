using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// A class to store cards in the associated hand
/// </summary>
public class Hand : MonoBehaviour
{

    public Queue<GameObject> personalHand = new Queue<GameObject>();
}
