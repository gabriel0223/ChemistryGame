using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DeckController : MonoBehaviour
{
    private List<CardController> _cardsOnDeck = new List<CardController>();

    private void Start()
    {
        
    }

    public void Initialize()
    {
        
    }

    public bool IsDeckEmpty()
    {
        return _cardsOnDeck == null;
    }

    public void AddCardToDeck(CardController card)
    {
        _cardsOnDeck.Add(card);
    }

    public CardController GetCard(int index)
    {
        return _cardsOnDeck[index];
    }

    public CardController GetTopCard()
    {
        CardController topCard = _cardsOnDeck[_cardsOnDeck.Count - 1];
        
        _cardsOnDeck.Reverse();
        
        foreach (var card in _cardsOnDeck)
        {
            if (card.GetComponent<DragAndDrop>().IsDragging) continue;

            topCard = card;
            break;
        }

        _cardsOnDeck.Reverse();

        return topCard;
    }
}
