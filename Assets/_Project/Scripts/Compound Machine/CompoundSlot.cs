using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompoundSlot : MonoBehaviour
{
    public event Action<Element> OnAddToCompound;
    public event Action OnResetCompound;
    public event Action<List<CardController>> OnSendCompound; 

    private List<CardController> _cardsOnCompound = new List<CardController>();

    public void AddCardToCompound(CardController card)
    {
        _cardsOnCompound.Add(card);
        
        OnAddToCompound?.Invoke(card.Element);

        if (_cardsOnCompound.Count == 3)
        {
            OnSendCompound?.Invoke(_cardsOnCompound);
            Invoke(nameof(ResetCompound), 1f);
        }
    }

    private void ResetCompound()
    {
        Debug.Log($"[{nameof(CompoundSlot)}] - Reset compound");
        _cardsOnCompound.Clear();
        
        OnResetCompound?.Invoke();
    }
}
