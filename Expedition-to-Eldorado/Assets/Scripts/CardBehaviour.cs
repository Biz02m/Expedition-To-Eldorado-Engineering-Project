using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardBehaviour : MonoBehaviour
{
    [SerializeField] public int Power;
    [SerializeField] public string NameOfCard;
    [SerializeField] public int TypeOfCard;


    private void OnEnable()
    {
        MouseController.instance.OnLeftMouseClick += OnLeftMouseClick;
        MouseController.instance.OnRightMouseClick += OnRightMouseClick;
    }

    private void OnDisable() 
    {
        MouseController.instance.OnLeftMouseClick -= OnLeftMouseClick;
        MouseController.instance.OnRightMouseClick -= OnRightMouseClick;
    }

    private void OnRightMouseClick(RaycastHit hit)
    {
        throw new NotImplementedException();
    }

    private void OnLeftMouseClick(RaycastHit hit)
    {
        Debug.Log(this.Power);
        Debug.Log(this.NameOfCard);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
}
