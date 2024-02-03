using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GeneralEnumerations 
{
    //zdecydujcie czy wolicie po kolorze czy po nazwie
    public enum CardTypes
    {
        blue,
        green,
        yellow
    }

    public enum ViewTypes
    {
        BoardOnly,
        CardsOnly,
        BoardCards,
        Shop
    }
}
