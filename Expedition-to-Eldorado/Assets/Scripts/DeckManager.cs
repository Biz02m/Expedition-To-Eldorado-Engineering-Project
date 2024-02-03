using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckManager : MonoBehaviour
{
    [SerializeField] GameObject[] cardPositions = new GameObject[4];
    [SerializeField] List<GameObject> cardsInDeck;
    [SerializeField] GameObject[] cardsOnHand = new GameObject[4];

    // Start is called before the first frame update
    void Start()
    {

        drawCards();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void drawCards()
    {
        int numOfCardsInDeck = cardsInDeck.Count;
        for(int i = 0; i <= numOfCardsInDeck; i++)
        {
            int index = Random.Range(0, cardsInDeck.Count);
            cardsOnHand[i] = cardsInDeck[index];
            cardsInDeck[index].GetComponent<CardBehaviour>().setOnHand(true);
            cardsOnHand[i].transform.position = cardPositions[i].transform.position;
            cardsOnHand[i].SetActive(true);
        }

    }
}
