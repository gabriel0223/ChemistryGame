using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class CardManager : MonoBehaviour
{
    [SerializeField] private BattleController _battleController;
    [SerializeField] private GameObject _cardPrefab;
    [SerializeField] private Transform[] _cardSlots;
    [SerializeField] private Sprite[] _easyCardColors;
    [SerializeField] private Sprite[] _hardCardColors;
    [SerializeField] private Transform _cardGenerationPosition;
    [SerializeField] private Transform _cardDiscardPosition;

    [Space(10)] 
    [SerializeField] private GameDifficulty _gameDifficulty;

    private Element[] _elementsInTheDeck;
    private List<Element> _cardsInTheDeck = new List<Element>();

    private void OnEnable()
    {
        _battleController.OnStartPlayerTurn += GenerateHand;
        _battleController.OnEndPlayerTurn += DiscardHand;
        DragAndDrop.OnDestroyCard += ReturnCardToDeck;
    }
    
    private void OnDisable()
    {
        _battleController.OnStartPlayerTurn -= GenerateHand;
        _battleController.OnEndPlayerTurn -= DiscardHand;
        DragAndDrop.OnDestroyCard -= ReturnCardToDeck;
    }

    public void GenerateHand(Element[] elements)
    {
        _elementsInTheDeck = elements;
        
        StartCoroutine(GenerateHandCoroutine(_elementsInTheDeck));
    }

    private void GenerateHand()
    {
        StartCoroutine(GenerateHandCoroutine(_elementsInTheDeck));
    }

    private void DiscardHand()
    {
        StartCoroutine(DiscardHandCoroutine());
    }

    private IEnumerator GenerateHandCoroutine(Element[] elements)
    {
        if (_cardsInTheDeck.Count == 0)
        {
            _cardsInTheDeck = elements.ToList();
        }

        for (int i = 0; i < _cardSlots.Length; i++)
        {
            if (_cardSlots[i].childCount > 0)
            {
                continue;
            }
            
            yield return new WaitForSeconds(0.25f);

            Element chosenElement = _cardsInTheDeck[Random.Range(0, _cardsInTheDeck.Count)];
            _cardsInTheDeck.Remove(chosenElement);

            Sprite cardColor = _hardCardColors[(int)chosenElement.ElementData.GROUP];
            CardController newCard = Instantiate(_cardPrefab, _cardGenerationPosition.position, Quaternion.identity, _cardSlots[i]).GetComponent<CardController>();

            newCard.Initialize(chosenElement, _gameDifficulty);
            newCard.gameObject.GetComponent<CardDataDisplayer>().SetCardColor(cardColor);
        }
    }
    
    private IEnumerator DiscardHandCoroutine()
    {
        yield return new WaitForSeconds(0.5f);
        
        for (int i = 0; i < _cardSlots.Length; i++)
        {
            if (_cardSlots[i].childCount == 0)
            {
                continue;
            }

            CardController card = _cardSlots[i].GetComponentInChildren<CardController>();
            card.transform.SetParent(_cardDiscardPosition);
            
            card.GetComponent<DragAndDrop>().SnapToTarget(Vector3.zero, 0.5f);
        }
    }

    private void ReturnCardToDeck(Element element)
    {
        _cardsInTheDeck.Add(element);
    }
}
