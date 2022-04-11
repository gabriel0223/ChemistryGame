using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using Random = UnityEngine.Random;

public class CardController : MonoBehaviour
{
    public event Action<CardController> OnAddCardToCompound;
    
    [SerializeField] private ElementsSheet _elementsSheet;
    [SerializeField] private int _atomicNumber;
    [SerializeField] private float _destructionTime;
    
    private ElementsSheetData _cardData;
    private CardDataDisplayer _cardDataDisplayer;
    private DeckController _currentDeck;
    private CompoundSlot _compoundSlot;

    public CompoundSlot CompoundSlot => _compoundSlot;

    private void Start()
    {
        _cardDataDisplayer = GetComponent<CardDataDisplayer>();
        _currentDeck = GetComponentInParent<DeckController>();
        _compoundSlot = FindObjectOfType<CompoundSlot>();

        int cardIndex = Random.Range(0, _elementsSheet.dataArray.Length - 1);
        Initialize(cardIndex);
    }

    private void Initialize(int atomicNumber)
    {
        _cardData = _elementsSheet.dataArray[atomicNumber];
        _cardDataDisplayer.UpdateDataDisplay(_cardData);
    }

    public DeckController GetCurrentDeck()
    {
        return _currentDeck;
    }

    public void AddToCompoundSlot()
    {
        _currentDeck = null;
        transform.SetParent(_compoundSlot.transform);
        _compoundSlot.AddCardToCompound(this);
        OnAddCardToCompound?.Invoke(this);
        
        DestroyCard();
    }

    private void DestroyCard()
    {
        transform.DOScale(Vector3.zero, _destructionTime)
            .OnComplete(() => Destroy(gameObject));
    }
}
