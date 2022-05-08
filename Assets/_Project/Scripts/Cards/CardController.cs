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
    
    private Element _element;
    private CardDataDisplayer _cardDataDisplayer;
    private DeckController _currentDeck;
    private CompoundSlot _compoundSlot;

    public CompoundSlot CompoundSlot => _compoundSlot;
    public Element Element => _element;

    public void Initialize(Element element)
    {
        _cardDataDisplayer = GetComponent<CardDataDisplayer>();
        _currentDeck = GetComponentInParent<DeckController>();
        _compoundSlot = FindObjectOfType<CompoundSlot>();
        
        _element = element;
        _cardDataDisplayer.UpdateDataDisplay(element);
    }
}
