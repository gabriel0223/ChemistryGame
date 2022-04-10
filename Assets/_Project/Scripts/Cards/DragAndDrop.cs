using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragAndDrop : MonoBehaviour, IPointerUpHandler, IPointerDownHandler, IDragHandler
{
    [SerializeField] private float _dragSpeed;
    [SerializeField] private float _snapSpeed;
    [SerializeField] private float _maxDistanceToSwitchDeck;
    
    private InputManager _inputManager;
    private Animator _animator;
    private RectTransform _rectTransform;
    private CardController _cardController;
    private bool _isDragging;
    private Vector3 _targetPosition;

    public bool IsDragging => _isDragging;

    // Start is called before the first frame update
    void Start()
    {
        _inputManager = InputManager.Instance;
        _animator = GetComponent<Animator>();
        _rectTransform = GetComponent<RectTransform>();
        _cardController = GetComponent<CardController>();
    }

    private void Update()
    {
        if (!_isDragging)
        {
            return;
        }
        
        _targetPosition = _inputManager.GetTouchPosition();
        transform.position = Vector3.Lerp(transform.position, _targetPosition, _dragSpeed * Time.deltaTime);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Drop();
    }

    private void Drop()
    {
        _animator.SetBool(AnimatorParameters.IS_DRAGGING, false);
        //SnapToTarget(GetNearestDeck().transform.position);
        SnapToDeck(_cardController.GetCurrentDeck());
        _isDragging = false;
    }

    private DeckController GetNearestDeck()
    {
        DeckController[] cardDecks = FindObjectsOfType<DeckController>();
        DeckController nearestDeckController = _cardController.GetCurrentDeck();

        foreach (var deck in cardDecks)
        {
            float distanceToDeck = Vector3.Distance(transform.position, deck.transform.position);
            float distanceToNearestDeck = Vector3.Distance(transform.position, nearestDeckController.transform.position);
            
            if (distanceToDeck < distanceToNearestDeck && distanceToDeck < _maxDistanceToSwitchDeck)
            {
                nearestDeckController = deck;
            }
        }
        
        return nearestDeckController;
    }

    public void OnDrag(PointerEventData eventData)
    {
        _isDragging = true;
        _animator.SetBool(AnimatorParameters.IS_DRAGGING, true);
    }

    private void SnapToDeck(DeckController deck)
    {
        Vector2 deckPosition = new Vector2(0, deck.GetTopCard().GetComponent<RectTransform>().anchoredPosition.y + DeckSettings.GetRandomSpacingBetweenCards());
        
        _rectTransform.DOAnchorPos(deckPosition, 1 / _snapSpeed);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        
    }
}
