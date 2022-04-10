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
    
    private InputManager _inputManager;
    private Animator _animator;
    private CardController _cardController;
    private bool _isDragging;
    private Vector3 _targetPosition;

    // Start is called before the first frame update
    void Start()
    {
        _inputManager = InputManager.Instance;
        _animator = GetComponent<Animator>();
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
        _isDragging = false;
        _animator.SetBool(AnimatorParameters.IS_DRAGGING, false);
        SnapToTarget(GetNearestEmptySlot().transform.position);
    }

    private CardSlot GetNearestEmptySlot()
    {
        CardSlot[] cardSlots = FindObjectsOfType<CardSlot>();
        CardSlot nearestSlot = _cardController.GetCurrentSlot();

        foreach (var slot in cardSlots)
        {
            if (!slot.IsSlotEmpty()) continue;

            float distanceToSlot = Vector3.Distance(transform.position, slot.transform.position);
            float distanceToNearestSlot = Vector3.Distance(transform.position, nearestSlot.transform.position);
            
            if (distanceToSlot < distanceToNearestSlot)
            {
                nearestSlot = slot;
            }
        }

        return nearestSlot;
    }

    public void OnDrag(PointerEventData eventData)
    {
        _isDragging = true;
        _animator.SetBool(AnimatorParameters.IS_DRAGGING, true);
    }

    private void SnapToTarget(Vector3 target)
    {
        transform.DOMove(target, 1 / _snapSpeed);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        
    }
}
