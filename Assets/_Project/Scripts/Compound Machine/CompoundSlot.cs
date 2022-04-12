using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompoundSlot : MonoBehaviour
{
    public event Action<Element> OnAddToCompound;
    
    private List<CardController> _cardsOnCompound = new List<CardController>();

    public void AddCardToCompound(CardController card)
    {
        _cardsOnCompound.Add(card);
        
        OnAddToCompound?.Invoke(card.Element);
    }
}
