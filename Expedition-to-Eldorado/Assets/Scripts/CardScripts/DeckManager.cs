using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GeneralEnumerations;

public class DeckManager : MonoBehaviour
{
    [SerializeField] List<GameObject> cardsInDeck;
    [SerializeField] GameObject mainCamera;
    [SerializeField]List<GameObject> cardsOnHand;
    [SerializeField] float speedOfCard = 5;
    [SerializeField] double spaceBetweenCard = 5; //chyba zle ale cosz
    int viewNumber = (int)ViewTypes.CardsOnly; //TODO - uzaleznic widok kamery od pozycji kart
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
                viewModifierY = -18;
                viewModifierZ = 10;
                break;
            case (int)ViewTypes.BoardCards:
                break;
            case (int)ViewTypes.BoardOnly: //w kontekscie kart sa podobne
            case (int)ViewTypes.Shop:
                break;
        }

        //TODO wyliczyc pozycje kart z modyfikatorem
        Vector3[] cardPosition = new Vector3[4];
        double range = spaceBetweenCard * cardsOnHand.Count;
        double spaces = range / (cardsOnHand.Count - 1);

        for (int i = 0; i < cardsOnHand.Count; i++)
        {
            cardPosition[i].x = (float)(mainCamera.transform.position.x + (i * spaces) - range/2);
            cardPosition[i].y = mainCamera.transform.position.y + viewModifierY;
            cardPosition[i].z = mainCamera.transform.position.z + viewModifierZ;
        }

        //TODO zastosowac ta interpolacje 
        for(int i = 0; i < cardsOnHand.Count; i++)
        {
            cardsOnHand[i].transform.position = Vector3.Lerp(cardsOnHand[i].transform.position, cardPosition[i], speedOfCard * Time.deltaTime);
        }

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
