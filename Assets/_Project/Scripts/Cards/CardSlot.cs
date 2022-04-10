using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSlot : MonoBehaviour
{
    private ElementsSheetData _cardOnSlot;

    private void Start()
    {
        
    }

    public bool IsSlotEmpty()
    {
        return _cardOnSlot == null;
    }

    public ElementsSheetData GetCard()
    {
        return _cardOnSlot;
    }
}
