using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GeneralEnumerations;

public class DeckManager : MonoBehaviour
{
    [SerializeField] List<GameObject> cardsInDeck;
    [SerializeField] GameObject mainCamera;
    [SerializeField] List<GameObject> cardsOnHand;
    [SerializeField] float speedOfCard = 5;
    [SerializeField] double spaceBetweenCard = 5; 
    [SerializeField] GameObject[] starterCardPack = new GameObject[3];
    int viewNumber = (int)ViewTypes.CardsOnly; 
    [SerializeField] int numberOfCardsOnHand = 4; //zostawilem na wypadek gdybysmy chcieli to zmienic
    int cursor = -1;
    //Obecnie tworzony jest widok 3 - widok na karty



    // Start is called before the first frame update
    void Start()
    {
        starterPackSelection();
        drawCardsFromDeck();

    }

    // Update is called once per frame
    void Update()
    {
        //wybrac modyfikator w zaleznosci od widoku
        int viewModifierY = 0; 
        int viewModifierZ = 0;
        switch (viewNumber) {
            case (int)ViewTypes.CardsOnly:
                viewModifierY = -18;
                viewModifierZ = 10;
                break;
            case (int)ViewTypes.BoardCards:
                viewModifierY = -18;
                viewModifierZ = 0;
                break;
            case (int)ViewTypes.BoardOnly: //w kontekscie kart sa podobne
            case (int)ViewTypes.Shop:
                viewModifierY = -40;
                viewModifierZ = 0;
                break;
        }

        //Interakcja gracza
        handlePlayerInput();

        //wyliczyczenie pozycji kart z modyfikatorem
        Vector3[] cardPosition = new Vector3[numberOfCardsOnHand];
        double range = spaceBetweenCard * cardsOnHand.Count;
        double spaces = range / (cardsOnHand.Count - 1);
        if (cardsOnHand.Count == 1)
        {
            spaces = 0;
        }

        for (int i = 0; i < cardsOnHand.Count; i++)
        {
            cardPosition[i].x = (float)(mainCamera.transform.position.x + (i * spaces) - range/2);
            cardPosition[i].y = mainCamera.transform.position.y + viewModifierY;
            cardPosition[i].z = mainCamera.transform.position.z + viewModifierZ;

            if(cursor == i + 1)
            {
                cardPosition[i].z += 4;
            }
        }

        //zastosowac ta interpolacje (daje ten efekt ruchu) 
        for(int i = 0; i < cardsOnHand.Count; i++)
        {
            cardsOnHand[i].transform.position = Vector3.Lerp(cardsOnHand[i].transform.position, cardPosition[i], speedOfCard * Time.deltaTime);
        }

    }

    public void changeView(ViewTypes type)
    {
        this.viewNumber = (int)type;
    }

    private void handlePlayerInput()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (cursor > 1 && cursor <= cardsOnHand.Count)
            {
                cursor--;
            }
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (cursor >= 1 && cursor < cardsOnHand.Count)
            {
                cursor++;
            }
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            cursor *= -1;
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (cursor >= 1 && cursor <= cardsOnHand.Count)
            {
                Debug.Log(cardsOnHand[cursor - 1].GetComponent<CardBehaviour>().NameOfCard);
                useCard();
            }
        }
    }

    private void starterPackSelection()
    {
        /*for (int i = 0; i < numberOfCardsOnHand; i++)
        {
            int index = Random.Range(0, starterCardPack.Count);
            cardsInDeck.Add(Instantiate(starterCardPack[index]));
            cardsInDeck[i].SetActive(false);
        }*/

        for (int i = 4; i > 0; i--)
        {
            cardsInDeck.Add(Instantiate(starterCardPack[index]));
            cardsInDeck[i].SetActive(false);
        }
    }

    private void drawCardsFromDeck()
    {
        for(int i = 0; i < numberOfCardsOnHand; i++)
        {
            int index = Random.Range(0, cardsInDeck.Count);
            cardsOnHand.Add(cardsInDeck[index]);
            cardsOnHand[i].SetActive(true);
            cardsInDeck.RemoveAt(index);
        }

    }

    private void useCard()
    {
        if (cursor >= 1 && cursor <= cardsOnHand.Count)
        {
            cardsOnHand[cursor - 1].SetActive(false);
            cardsInDeck.Add(cardsOnHand[cursor - 1]);
            cardsOnHand.RemoveAt(cursor - 1);
        }

        if(cursor < 1 || cursor > cardsOnHand.Count)
        {
            cursor = 1;
        }
    }

}
