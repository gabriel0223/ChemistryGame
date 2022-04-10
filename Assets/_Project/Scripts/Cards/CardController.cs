using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardController : MonoBehaviour
{
    [SerializeField] private ElementsSheet _elementsSheet;
    [SerializeField] private int _atomicNumber;
    
    private ElementsSheetData _cardData;
    private CardDataDisplayer _cardDataDisplayer;
    private CardSlot _currentSlot;

    private void Start()
    {
        _cardDataDisplayer = GetComponent<CardDataDisplayer>();
        _currentSlot = GetComponentInParent<CardSlot>();

        Initialize(_atomicNumber);
    }

    private void Initialize(int atomicNumber)
    {
        int cardIndex = atomicNumber - 1;

        _cardData = _elementsSheet.dataArray[cardIndex];
        _cardDataDisplayer.UpdateDataDisplay(_cardData);
    }

    public CardSlot GetCurrentSlot()
    {
        return _currentSlot;
    }

    public void SetCurrentSlot(CardSlot newSlot)
    {
        _currentSlot = newSlot;
    }
}
