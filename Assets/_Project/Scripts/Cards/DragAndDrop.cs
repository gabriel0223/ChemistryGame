using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragAndDrop : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public static event Action<Element> OnDestroyCard;

    [SerializeField] private float _dragSpeed;
    [SerializeField] private float _snapSpeed;

    private InputManager _inputManager;
    private Animator _animator;
    private RectTransform _rectTransform;
    private Canvas _canvasElement;
    private CanvasGroup _canvasGroup;
    private bool _isDragging;
    private bool _isActive = true;
    private Vector3 _targetPosition;

    public bool IsDragging => _isDragging;

    // Start is called before the first frame update
    void Start()
    {
        _inputManager = InputManager.Instance;
        _animator = GetComponent<Animator>();
        _rectTransform = GetComponent<RectTransform>();
        _canvasElement = GetComponent<Canvas>();
        _canvasGroup = GetComponent<CanvasGroup>();
        
        SnapToParent();
    }

    private void Update()
    {
        if (!_isDragging || !_isActive)
        {
            return;
        }
        
        _targetPosition = _inputManager.GetTouchPosition();
        transform.position = Vector3.Lerp(transform.position, _targetPosition, _dragSpeed * Time.deltaTime);
    }

    private void Drop()
    {
        _animator.SetBool(AnimatorParameters.IS_DRAGGING, false);
        
        SnapToParent();
        
        _isDragging = false;
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
    
    private void SnapToTarget(Vector3 target)
    {
        _rectTransform.DOAnchorPos(target, 1 / _snapSpeed).OnComplete(() => _canvasElement.overrideSorting = false);
    }
    
    public void SnapToTarget(Vector3 target, float duration)
    {
        _rectTransform.DOAnchorPos(target, duration).OnComplete(() => _canvasElement.overrideSorting = false);
    }

    private void SnapToParent()
    {
        SnapToTarget(Vector3.zero);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _canvasGroup.blocksRaycasts = false;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _canvasGroup.blocksRaycasts = true;
        
        if (!_isDragging || !_isActive)
        {
            return;
        }
        
        Drop();
    }
    
    public void DestroyCard()
    {
        _isActive = false;
        OnDestroyCard?.Invoke(GetComponent<CardController>().Element);
        
        transform.DOScale(Vector3.zero, 0.5f)
            .OnComplete(() => Destroy(gameObject));
    }
}
