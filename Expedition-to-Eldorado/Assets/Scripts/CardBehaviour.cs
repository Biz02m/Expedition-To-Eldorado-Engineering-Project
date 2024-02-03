using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardBehaviour : MonoBehaviour
{
    [SerializeField] public int Power;
    [SerializeField] public string NameOfCard;
    [SerializeField] public string TypeOfCard;
    private bool onHand { get; set; } 


    public void setOnHand(bool isOnHand)
    {
        this.onHand = isOnHand;
    }

    // Start is called before the first frame update
    void Start()
    {
        this.onHand = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
}
