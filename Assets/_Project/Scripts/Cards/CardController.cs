using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Random = UnityEngine.Random;

public class CardController : MonoBehaviour
{
    [SerializeField] private ElementsSheet _elementsSheet;
    [SerializeField] private int _atomicNumber;
    
    private ElementsSheetData _cardData;
    private CardDataDisplayer _cardDataDisplayer;
    private DeckController _currentDeckController;

    private void Start()
    {
        _cardDataDisplayer = GetComponent<CardDataDisplayer>();
        _currentDeckController = GetComponentInParent<DeckController>();

        Initialize();
    }

    private void Initialize(int atomicNumber)
    {
        int cardIndex = atomicNumber - 1;

        _cardData = _elementsSheet.dataArray[cardIndex];
        _cardDataDisplayer.UpdateDataDisplay(_cardData);
    }
    
    private void Initialize()
    {
        int cardIndex = Random.Range(0, _elementsSheet.dataArray.Length);

        _cardData = _elementsSheet.dataArray[cardIndex];
        _cardDataDisplayer.UpdateDataDisplay(_cardData);
    }

    public DeckController GetCurrentDeck()
    {
        return _currentDeckController;
    }

    public void SwitchDeck(DeckController newDeckController)
    {
        transform.SetParent(newDeckController.transform);
        _currentDeckController = newDeckController;
    }
}
