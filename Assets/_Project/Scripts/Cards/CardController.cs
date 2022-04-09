using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardController : MonoBehaviour
{
    [SerializeField] private ElementsSheet _elementsSheet;
    [SerializeField] private int _atomicNumber;
    
    private ElementsSheetData _cardData;
    private CardDataDisplayer _cardDataDisplayer;

    private void Start()
    {
        _cardDataDisplayer = GetComponent<CardDataDisplayer>();
        
        Initialize(_atomicNumber);
    }

    private void Initialize(int atomicNumber)
    {
        int cardIndex = atomicNumber - 1;

        _cardData = _elementsSheet.dataArray[cardIndex];
        _cardDataDisplayer.UpdateDataDisplay(_cardData);
    }
}
