using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EquipmentSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IDropHandler
{
    [SerializeField] private BattleController _battleController;
    [SerializeField] private TMP_Text _powerText;
    [SerializeField] private Image _equipmentImage;
    [SerializeField] private DurabilityBar _durabilityBar;
    [SerializeField] private TMP_Text[] _elementSlots;
    [SerializeField] private Equipment _equipment;
    [SerializeField] private GameObject _powerPreview;
    [SerializeField] private TMP_Text _previewText;

    [Header("SETTINGS")] 
    [SerializeField] private int _maxElementsFused = 3;

    private List<Element> _elementsFused = new List<Element>();
    private Element _elementBeingPreviewed;
    private bool _isSlotLocked;

    // Start is called before the first frame update
    void Start()
    {
        UpdateEquipmentUI();
    }

    private void OnEnable()
    {
        _equipment.OnChangeDurability += _durabilityBar.AnimateBar;
        _equipment.OnEquipmentBreak += ResetEquipment;

        _battleController.OnChangeTurn += UnlockSlot;
    }

    private void OnDisable()
    {
        _equipment.OnChangeDurability -= _durabilityBar.AnimateBar;
        _equipment.OnEquipmentBreak -= ResetEquipment;
        
        _battleController.OnChangeTurn -= UnlockSlot;
    }

    private void ResetEquipment()
    {
        foreach (var elementText in _elementSlots)
        {
            elementText.SetText("");    
        }
        
        _equipmentImage.DOColor(Color.black, 0.5f);
        _elementsFused.Clear();
        UpdateEquipmentUI();
    }

    private void UpdateEquipmentUI()
    {
        if (_elementsFused.Count == 1)
        {
            _equipmentImage.DOColor(Color.white, 0.5f);
        }
        
        _powerText.SetText(_equipment.Power.ToString());
    }

    private void EnablePreviewCardEffect()
    {
        _previewText.SetText(_elementBeingPreviewed.ElementData.Atomicnumber.ToString());
        _powerPreview.SetActive(true);
    }

    private void DisablePreviewCardEffect()
    {
        _powerPreview.SetActive(false);
    }

    private void FuseElement(Element element)
    {
        _elementsFused.Add(element);
        _equipment.AddPower(element.ElementData.Atomicnumber);
        UpdateEquipmentUI();
        DisablePreviewCardEffect();
        AddElementInitialsToSlot(element.ElementData.Abbreviation);
        LockSlot();
    }

    private void LockSlot()
    {
        _isSlotLocked = true;
    }

    private void UnlockSlot()
    {
        _isSlotLocked = false;
    }

    private void AddElementInitialsToSlot(string elementInitials)
    {
        TMP_Text elementSlot = _elementSlots[_elementsFused.Count - 1];
        elementSlot.SetText(elementInitials);
        elementSlot.gameObject.SetActive(true);
    }

    public void ActivateSlot()
    {
        // Sequence activationTween = DOTween.Sequence();
        //
        // activationTween.Append(_equipmentImage.transform.DOScale(Vector3.one * , 1));
        // activationTween.AppendInterval(1f);
        // activationTween.Append(_equipmentImage.transform.DOScale(Vector3.one, 1));
        
        _equipmentImage.transform.DOPunchScale(Vector3.one * 0.15f, 0.75f, 5, 0.5f);
        _equipmentImage.transform.DOShakeRotation(0.75f, 10f);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (eventData.pointerDrag == null || !eventData.pointerDrag.TryGetComponent(out CardController card))
        {
            return;
        }

        if (_isSlotLocked)
        {
            return;
        }

        _elementBeingPreviewed = card.Element;
        EnablePreviewCardEffect();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (eventData.pointerDrag == null || !eventData.pointerDrag.TryGetComponent(out CardController card))
        {
            return;
        }
        
        DisablePreviewCardEffect();
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag == null || !eventData.pointerDrag.TryGetComponent(out CardController card))
        {
            return;
        }

        if (_isSlotLocked || _elementsFused.Count == _maxElementsFused)
        {
            return;
        }

        FuseElement(card.Element);
        card.GetComponent<DragAndDrop>().DestroyCard();
    }
}
