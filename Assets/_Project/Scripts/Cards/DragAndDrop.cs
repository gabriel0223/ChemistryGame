using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragAndDrop : MonoBehaviour, IPointerUpHandler, IPointerDownHandler, IDragHandler
{
    public event Action OnDropOnCompound;

    [SerializeField] private float _dragSpeed;
    [SerializeField] private float _snapSpeed;
    [SerializeField] private float _maxDistanceToSwitchDeck;
    
    private InputManager _inputManager;
    private Animator _animator;
    private RectTransform _rectTransform;
    private CardController _cardController;
    private Canvas _canvasElement;
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
        _canvasElement = GetComponent<Canvas>();
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
        if (!_isDragging)
        {
            return;
        }
        
        Drop();
    }

    private void Drop()
    {
        float distanceToCompoundSlot = (Vector3.Distance(transform.position,_cardController.CompoundSlot.transform.position));
        bool isCompoundResetting = _cardController.CompoundSlot.Resetting;
        
        _animator.SetBool(AnimatorParameters.IS_DRAGGING, false);
        
        if  (distanceToCompoundSlot <_maxDistanceToSwitchDeck && !isCompoundResetting)
        {
            DropInCompoundSlot();
        }
        else
        {
            ReturnToDeck(_cardController.GetCurrentDeck());
        }
        
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
        Transform deck = transform.parent;
        if (transform != deck.GetChild(deck.childCount - 1))
        {
            return;
        }
        
        _canvasElement.overrideSorting = true;
        _isDragging = true;
        _animator.SetBool(AnimatorParameters.IS_DRAGGING, true);
    }

    private void ReturnToDeck(DeckController deck)
    {
        Vector2 deckPosition = new Vector2(0, deck.GetTopCard().GetComponent<RectTransform>().anchoredPosition.y + DeckSettings.GetRandomSpacingBetweenCards());
        
        SnapToTarget(deckPosition);
    }

    private void DropInCompoundSlot()
    {
        _cardController.AddToCompoundSlot();
        SnapToTarget(Vector3.zero);
    }

    private void SnapToTarget(Vector3 target)
    {
        _rectTransform.DOAnchorPos(target, 1 / _snapSpeed).OnComplete(() => _canvasElement.overrideSorting = false);
    }
    
    public void OnPointerDown(PointerEventData eventData)
    {
        
    }
}
