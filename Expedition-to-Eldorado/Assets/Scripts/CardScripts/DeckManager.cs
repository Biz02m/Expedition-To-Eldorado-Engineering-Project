using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GeneralEnumerations;

public class DeckManager : MonoBehaviour
{
    [SerializeField] List<GameObject> cardsInDeck;
    [SerializeField] GameObject mainCamera;
    [SerializeField]List<GameObject> cardsOnHand;
    int viewNumber = 0; //TODO - uzaleznic widok kamery od pozycji kart
    int numberOfCardsOnHand = 4; //zostawilem na wypadek gdybysmy chcieli to zmienic
    //Obecnie tworzony jest widok 3 - widok na karty

    

    // Start is called before the first frame update
    void Start()
    {

        drawCards();

    }

    // Update is called once per frame
    void Update()
    {
        //TODO wybrac modyfikator w zaleznosci od widoku
        int viewModifierY = 0; 
        int viewModifierZ = 0;
        switch (viewNumber) {
            case (int)ViewTypes.CardsOnly:
                viewModifierY = -26;
                viewModifierZ = 20;
                break;
            case (int)ViewTypes.BoardCards:
                break;
            case (int)ViewTypes.BoardOnly: //w kontekscie kart sa podobne
            case (int)ViewTypes.Shop:
                break;
        }


        //TODO wyliczyc pozycje kart z modyfikatorem

        //TODO zastosowac tego larpa
    }

    public void drawCards()
    {
        for(int i = 0; i < numberOfCardsOnHand; i++)
        {
            int index = Random.Range(0, cardsInDeck.Count);
            cardsOnHand.Add(cardsInDeck[index]);
            cardsOnHand[i].SetActive(true);
            cardsInDeck.RemoveAt(index);
        }

    }

}
