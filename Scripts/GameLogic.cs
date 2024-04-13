using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// The class that holds the direct functions for the user within the game
/// </summary>
public class GameLogic : MonoBehaviour
{
    public int playerCount;
    List<GameObject> playedCards = new List<GameObject>();
    List<GameObject> recentCards = new List<GameObject>();
    GameObject winningHand;

    public GameObject deck;
    Deck deckScript;

    public GameObject playerHand;
    Hand playerHandScript;

    public GameObject opponent0Hand;
    Hand opponent0HandScript;

    public GameObject opponent1Hand;
    Hand opponent1HandScript;

    public GameObject opponent2Hand;
    Hand opponent2HandScript;

    public GameObject playerCard;
    public GameObject opponent0Card;
    public GameObject opponent1Card;
    public GameObject opponent2Card;

    // Start is called before the first frame update
    void Start()
    {
        deck.GetComponent<Deck>().BuildDeck();

        if (playerCount == 1)
        {
            playerHandScript = playerHand.GetComponent<Hand>();
            opponent0HandScript = opponent0Hand.GetComponent<Hand>();
            deckScript = deck.GetComponent<Deck>();

            Deal2Players();
        }
        else if (playerCount == 2)
        {
            playerHandScript = playerHand.GetComponent<Hand>();
            opponent0HandScript = opponent0Hand.GetComponent<Hand>();
            opponent1HandScript = opponent1Hand.GetComponent<Hand>();
            deckScript = deck.GetComponent<Deck>();

            Deal3Players();
        }
        else
        {
            playerHandScript = playerHand.GetComponent<Hand>();
            opponent0HandScript = opponent0Hand.GetComponent<Hand>();
            opponent1HandScript = opponent1Hand.GetComponent<Hand>();
            opponent2HandScript = opponent2Hand.GetComponent<Hand>();
            deckScript = deck.GetComponent<Deck>();

            Deal4Players();
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (playerCount == 1)
            {
                Play(playerHandScript, playerCard, opponent0HandScript, opponent0Card);
            }
            else if (playerCount == 2)
            {
                Play(playerHandScript, playerCard, opponent0HandScript, opponent0Card, opponent1HandScript, opponent1Card);
            }
            else
            {
                Play(playerHandScript, playerCard, opponent0HandScript, opponent0Card, opponent1HandScript, opponent1Card, opponent2HandScript, opponent2Card);
            }
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            int j = recentCards.Count;

            for (int i = 0; i < j; i++)
            {
                recentCards[0].transform.position = winningHand.transform.position;
                recentCards.Remove(recentCards[0]);
            }
        }
    }

    /// <summary>
    /// Deals 26 cards from the deck to either player
    /// </summary>
    void Deal2Players()
    {
        GameObject nextCard;

        for (int i = 0; i < 26; i++)
        {
            nextCard = deckScript.deck.Pop();
            playerHandScript.personalHand.Enqueue(nextCard);
            nextCard.transform.position += new Vector3(playerHand.transform.position.x, playerHand.transform.position.y);

            nextCard = deckScript.deck.Pop();
            opponent0HandScript.personalHand.Enqueue(nextCard);
            nextCard.transform.position += new Vector3(opponent0Hand.transform.position.x, opponent0Hand.transform.position.y);
        }
    }

    /// <summary>
    /// Deals 17 cards from the deck to the three players
    /// </summary>
    void Deal3Players()
    {
        GameObject nextCard;

        for (int i = 0; i < 17; i++)
        {
            nextCard = deckScript.deck.Pop();
            playerHandScript.personalHand.Enqueue(nextCard);
            nextCard.transform.position += new Vector3(playerHand.transform.position.x, playerHand.transform.position.y);

            nextCard = deckScript.deck.Pop();
            opponent0HandScript.personalHand.Enqueue(nextCard);
            nextCard.transform.position += new Vector3(opponent0Hand.transform.position.x, opponent0Hand.transform.position.y);

            nextCard = deckScript.deck.Pop();
            opponent1HandScript.personalHand.Enqueue(nextCard);
            nextCard.transform.position += new Vector3(opponent1Hand.transform.position.x, opponent1Hand.transform.position.y);
        }
        // One card will be left in the mainDeck
        // It is dismissed and not used in the 3 player gamemode
        deckScript.deck.Pop().GetComponent<SpriteRenderer>().enabled = false;
    }

    /// <summary>
    /// Deals 13 cards from the deck to the four players
    /// </summary>
    void Deal4Players()
    {
        GameObject nextCard;

        for (int i = 0; i < 13; i++)
        {
            nextCard = deckScript.deck.Pop();
            playerHandScript.personalHand.Enqueue(nextCard);
            nextCard.transform.position += new Vector3(playerHand.transform.position.x, playerHand.transform.position.y);

            nextCard = deckScript.deck.Pop();
            opponent0HandScript.personalHand.Enqueue(nextCard);
            nextCard.transform.position += new Vector3(opponent0Hand.transform.position.x, opponent0Hand.transform.position.y);

            nextCard = deckScript.deck.Pop();
            opponent1HandScript.personalHand.Enqueue(nextCard);
            nextCard.transform.position += new Vector3(opponent1Hand.transform.position.x, opponent1Hand.transform.position.y);

            nextCard = deckScript.deck.Pop();
            opponent2HandScript.personalHand.Enqueue(nextCard);
            nextCard.transform.position += new Vector3(opponent2Hand.transform.position.x, opponent2Hand.transform.position.y);
        }
    }

    /// <summary>
    /// Sets the next card for 2 players on the table
    /// </summary>
    /// <param name="player"> User </param>
    /// <param name="playerObject"> The GameObject that holds the position for the user's played card </param>
    /// <param name="opponent"> Opposing computer player </param>
    /// <param name="opponentObject"> The GameObject that holds the position for the opponent's played card </param>
    void Play(Hand player, GameObject playerObject, Hand opponent, GameObject opponentObject)
    {
        Hand winner = player;
        List<Hand> compareHands = new List<Hand>();
        List<Card> compareCards = new List<Card>();
        List<GameObject> compareObjects = new List<GameObject>();

        if (player.personalHand.Count > 0)
        {
            playedCards.Add(player.personalHand.Dequeue());
            playedCards[playedCards.Count - 1].transform.position = playerObject.transform.position;
            compareCards.Add(playedCards[playedCards.Count - 1].GetComponent<Card>());
            compareHands.Add(player);
            compareObjects.Add(playerHand);
            
        }

        if (opponent.personalHand.Count == 52)
        {
            winner = opponent;
        }
        else if (opponent.personalHand.Count > 0)
        {
            playedCards.Add(opponent.personalHand.Dequeue());
            playedCards[playedCards.Count - 1].transform.position = opponentObject.transform.position;
            compareCards.Add(playedCards[playedCards.Count - 1].GetComponent<Card>());
            compareHands.Add(opponent);
            compareObjects.Add(opponent0Hand);
        }


        if (compareCards.Count == 2)
        {
            Compare1(compareObjects[0], compareHands[0], compareCards[0], compareObjects[1], compareHands[1], compareCards[1]);
        }
        else
        {
            foreach (GameObject card in winner.personalHand)
            {
                Destroy(winner.personalHand.Dequeue());
                // Someone won
                SceneManager.LoadScene(0);
            }
        }
    }

    /// <summary>
    /// Sets the next card for 3 players on the table
    /// </summary>
    /// <param name="player"> User </param>
    /// <param name="playerObject"> The GameObject that holds the position for the user's played card </param>
    /// <param name="opponent0"> First opposing computer player </param>
    /// <param name="opponent0Object"> The GameObject that holds the position for the first opponent's played card </param>
    /// <param name="opponent1"> Second opposing computer player </param>
    /// <param name="opponent1Object"> The GameObject that holds the position for the second opponent's played card </param>
    void Play(Hand player, GameObject playerObject, Hand opponent0, GameObject opponent0Object, Hand opponent1, GameObject opponent1Object)
    {
        Hand winner = player;
        List<Hand> compareHands = new List<Hand>();
        List<Card> compareCards = new List<Card>();
        List<GameObject> compareObjects = new List<GameObject>();

        if (player.personalHand.Count > 0)
        {
            playedCards.Add(player.personalHand.Dequeue());
            playedCards[playedCards.Count - 1].transform.position = playerObject.transform.position;
            compareCards.Add(playedCards[playedCards.Count - 1].GetComponent<Card>());
            compareHands.Add(player);
            compareObjects.Add(playerHand);
        }

        if (opponent0.personalHand.Count == 51)
        {
            winner = opponent0;
        }
        else if (opponent0.personalHand.Count > 0)
        {
            playedCards.Add(opponent0.personalHand.Dequeue());
            playedCards[playedCards.Count - 1].transform.position = opponent0Object.transform.position;
            compareCards.Add(playedCards[playedCards.Count - 1].GetComponent<Card>());
            compareHands.Add(opponent0);
            compareObjects.Add(opponent0Hand);
        }

        if (opponent1.personalHand.Count == 51)
        {
            winner = opponent1;
        }
        else if (opponent1.personalHand.Count > 0)
        {
            playedCards.Add(opponent1.personalHand.Dequeue());
            playedCards[playedCards.Count - 1].transform.position = opponent1Object.transform.position;
            compareCards.Add(playedCards[playedCards.Count - 1].GetComponent<Card>());
            compareHands.Add(opponent1);
            compareObjects.Add(opponent1Hand);
        }


        if (compareCards.Count == 3)
        {
            Compare2(compareObjects[0], compareHands[0], compareCards[0], compareObjects[1], compareHands[1], compareCards[1], compareObjects[2], compareHands[2], compareCards[2]);
        }
        else if (compareCards.Count == 2)
        {
            Compare1(compareObjects[0], compareHands[0], compareCards[0], compareObjects[1], compareHands[1], compareCards[1]);
        }
        else
        {
            foreach (GameObject card in winner.personalHand)
            {
                Destroy(winner.personalHand.Dequeue());
                // Someone won
                SceneManager.LoadScene(0);
            }
        }
    }

    /// <summary>
    /// Sets the next card for 4 players on the table
    /// </summary>
    /// <param name="player"> User </param>
    /// <param name="playerObject"> The GameObject that holds the position for the user's played card </param>
    /// <param name="opponent0"> First opposing computer player </param>
    /// <param name="opponent0Object"> The GameObject that holds the position for the first opponent's played card </param>
    /// <param name="opponent1"> Second opposing computer player </param>
    /// <param name="opponent1Object"> The GameObject that holds the position for the second opponent's played card </param>
    /// <param name="opponent2"> Third opposing computer player </param>
    /// <param name="opponent2Object"> The GameObject that holds the position for the third opponent's played card </param>
    void Play(Hand player, GameObject playerObject, Hand opponent0, GameObject opponent0Object, Hand opponent1, GameObject opponent1Object, Hand opponent2, GameObject opponent2Object)
    {
        Hand winner = player;
        List<Hand> compareHands = new List<Hand>();
        List<Card> compareCards = new List<Card>();
        List<GameObject> compareObjects = new List<GameObject>();

        if (player.personalHand.Count > 0)
        {
            playedCards.Add(player.personalHand.Dequeue());
            playedCards[playedCards.Count - 1].transform.position = playerObject.transform.position;
            compareCards.Add(playedCards[playedCards.Count - 1].GetComponent<Card>());
            compareHands.Add(player);
            compareObjects.Add(playerHand);
        }

        if (opponent0.personalHand.Count == 52)
        {
            winner = opponent0;
        }
        else if (opponent0.personalHand.Count > 0)
        {
            playedCards.Add(opponent0.personalHand.Dequeue());
            playedCards[playedCards.Count - 1].transform.position = opponent0Object.transform.position;
            compareCards.Add(playedCards[playedCards.Count - 1].GetComponent<Card>());
            compareHands.Add(opponent0);
            compareObjects.Add(opponent0Hand);
        }

        if (opponent1.personalHand.Count == 52)
        {
            winner = opponent1;
        }
        else if (opponent1.personalHand.Count > 0)
        {
            playedCards.Add(opponent1.personalHand.Dequeue());
            playedCards[playedCards.Count - 1].transform.position = opponent1Object.transform.position;
            compareCards.Add(playedCards[playedCards.Count - 1].GetComponent<Card>());
            compareHands.Add(opponent1);
            compareObjects.Add(opponent1Hand);
        }

        if (opponent2.personalHand.Count == 52)
        {
            winner = opponent2;
        }
        else if (opponent2.personalHand.Count > 0)
        {
            playedCards.Add(opponent2.personalHand.Dequeue());
            playedCards[playedCards.Count - 1].transform.position = opponent2Object.transform.position;
            compareCards.Add(playedCards[playedCards.Count - 1].GetComponent<Card>());
            compareHands.Add(opponent2);
            compareObjects.Add(opponent2Hand);
        }


        if (compareCards.Count == 4)
        {
            Compare3(compareObjects[0], compareHands[0], compareCards[0], compareObjects[1], compareHands[1], compareCards[1], compareObjects[2], compareHands[2], compareCards[2], compareObjects[3], compareHands[3], compareCards[3]);
        }
        else if (compareCards.Count == 3)
        {
            Compare2(compareObjects[0], compareHands[0], compareCards[0], compareObjects[1], compareHands[1], compareCards[1], compareObjects[2], compareHands[2], compareCards[2]);
        }
        else if (compareCards.Count == 2)
        {
            Compare1(compareObjects[0], compareHands[0], compareCards[0], compareObjects[1], compareHands[1], compareCards[1]);
        }
        else
        {
            foreach (GameObject card in winner.personalHand)
            {
                Destroy(winner.personalHand.Dequeue());
                // Someone won
                SceneManager.LoadScene(0);
            }
        }
    }

    /// <summary>
    /// Compares 2 cards by their rank
    /// </summary>
    /// <param name="object0"> The GameObject that holds the deck position of the first player </param>
    /// <param name="hand0"> First player </param>
    /// <param name="card0"> Played card from first player </param>
    /// <param name="object1"> The GameObject that holds the deck position of the second player </param>
    /// <param name="hand1"> Second player </param>
    /// <param name="card1"> Played card from the second player </param>
    void Compare1(GameObject object0, Hand hand0, Card card0, GameObject object1, Hand hand1, Card card1)
    {
        int j = playedCards.Count;
        if (card0.rank == card1.rank)
        {
            Play(hand0, object0, hand1, object1);
        }
        else if (card0.rank < card1.rank)
        {
            for (int i = 0; i < j; i++)
            {
                hand1.personalHand.Enqueue(playedCards[0]);
                recentCards.Add(playedCards[0]);
                playedCards.Remove(playedCards[0]);
                winningHand = object1;
            }
        }
        else
        {
            for (int i = 0; i < j; i++)
            {
                hand0.personalHand.Enqueue(playedCards[0]);
                recentCards.Add(playedCards[0]);
                playedCards.Remove(playedCards[0]);
                winningHand = object0;
            }
        }
    }

    /// <summary>
    /// Compares 3 cards by their rank
    /// </summary>
    /// <param name="object0"> The GameObject that holds the deck position of the first player </param>
    /// <param name="hand0"> First player </param>
    /// <param name="card0"> Played card from first player </param>
    /// <param name="object1"> The GameObject that holds the deck position of the second player </param>
    /// <param name="hand1"> Second player </param>
    /// <param name="card1"> Played card from the second player </param>
    /// <param name="object2">  The GameObject that holds the deck position of the third player </param>
    /// <param name="hand2"> Third player </param>
    /// <param name="card2"> Played card from the third player </param>
    void Compare2(GameObject object0, Hand hand0, Card card0, GameObject object1, Hand hand1, Card card1, GameObject object2, Hand hand2, Card card2)
    {
        int j = playedCards.Count;
        if (card0.rank == card1.rank ||
            card0.rank == card2.rank ||
            card1.rank == card2.rank)
        {
            Play(hand0, object0, hand1, object1, hand2, object2);
        }
        else if (card1.rank > card0.rank &&
                 card1.rank > card2.rank)
        {
            for (int i = 0; i < j; i++)
            {
                hand1.personalHand.Enqueue(playedCards[0]);
                recentCards.Add(playedCards[0]);
                playedCards.Remove(playedCards[0]);
                winningHand = object1;
            }
        }
        else if (card2.rank > card1.rank &&
                 card2.rank > card0.rank)
        {
            for (int i = 0; i < j; i++)
            {
                hand2.personalHand.Enqueue(playedCards[0]);
                recentCards.Add(playedCards[0]);
                playedCards.Remove(playedCards[0]);
                winningHand = object2;
            }
        }
        else
        {
            for (int i = 0; i < j; i++)
            {
                hand0.personalHand.Enqueue(playedCards[0]);
                recentCards.Add(playedCards[0]);
                playedCards.Remove(playedCards[0]);
                winningHand = object0;
            }
        }
    }

    /// <summary>
    /// Compares 4 cards by their rank
    /// </summary>
    /// <param name="object0"> The GameObject that holds the deck position of the first player </param>
    /// <param name="hand0"> First player </param>
    /// <param name="card0"> Played card from first player </param>
    /// <param name="object1"> The GameObject that holds the deck position of the second player </param>
    /// <param name="hand1"> Second player </param>
    /// <param name="card1"> Played card from the second player </param>
    /// <param name="object2">  The GameObject that holds the deck position of the third player </param>
    /// <param name="hand2"> Third player </param>
    /// <param name="card2"> Played card from the third player </param>
    /// <param name="object3"> The GameObject that holds the deck position of the fourth player </param>
    /// <param name="hand3"> Fourth player </param>
    /// <param name="card3"> Played cards from the fourth player </param>
    void Compare3(GameObject object0, Hand hand0, Card card0, GameObject object1, Hand hand1, Card card1, GameObject object2, Hand hand2, Card card2,
                  GameObject object3, Hand hand3, Card card3)
    {
        int j = playedCards.Count;
        if (card0.rank == card1.rank ||
            card0.rank == card2.rank ||
            card0.rank == card3.rank ||
            card1.rank == card2.rank ||
            card1.rank == card3.rank ||
            card2.rank == card3.rank)
        {
            Play(hand0, object0, hand1, object1, hand2, object2, hand3, object3);
        }
        else if (card1.rank > card0.rank &&
                 card1.rank > card2.rank &&
                 card1.rank > card3.rank)
        {
            for (int i = 0; i < j; i++)
            {
                hand1.personalHand.Enqueue(playedCards[0]);
                recentCards.Add(playedCards[0]);
                playedCards.Remove(playedCards[0]);
                winningHand = object1;
            }
        }
        else if (card2.rank > card0.rank &&
                 card2.rank > card1.rank &&
                 card2.rank > card3.rank)
        {
            for (int i = 0; i < j; i++)
            {
                hand2.personalHand.Enqueue(playedCards[0]);
                recentCards.Add(playedCards[0]);
                playedCards.Remove(playedCards[0]);
                winningHand = object2;
            }
        }
        else if (card3.rank > card0.rank &&
                 card3.rank > card1.rank &&
                 card3.rank > card2.rank)
        {
            for (int i = 0; i < j; i++)
            {
                hand3.personalHand.Enqueue(playedCards[0]);
                recentCards.Add(playedCards[0]);
                playedCards.Remove(playedCards[0]);
                winningHand = object3;
            }
        }
        else
        {
            for (int i = 0; i < j; i++)
            {
                hand0.personalHand.Enqueue(playedCards[0]);
                recentCards.Add(playedCards[0]);
                playedCards.Remove(playedCards[0]);
                winningHand = object0;
            }
        }
    }
}