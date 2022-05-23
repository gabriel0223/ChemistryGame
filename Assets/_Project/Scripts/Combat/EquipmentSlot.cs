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
    [SerializeField] private ElementSlotController[] _elementSlots;
    [SerializeField] private Equipment _equipment;
    [SerializeField] private GameObject _normalPowerPreview;
    [SerializeField] private GameObject _hardPowerPreview;
    [SerializeField] private TMP_Text _previewText;

    [Header("SETTINGS")] 
    [SerializeField] private int _maxElementsFused = 3;

    private List<Element> _elementsFused = new List<Element>();
    private Element _elementBeingPreviewed;
    private bool _isSlotLocked;
    private GameDifficulty _gameDifficulty = GameDifficulty.Hard;

    // Start is called before the first frame update
    void Start()
    {
        UpdateEquipmentUI();
    }

    private void OnEnable()
    {
        _equipment.OnChangeDurability += _durabilityBar.AnimateBar;
        _equipment.OnEquipmentBreak += ResetEquipment;

        _battleController.OnStartPlayerTurn += UnlockSlot;
    }

    private void OnDisable()
    {
        _equipment.OnChangeDurability -= _durabilityBar.AnimateBar;
        _equipment.OnEquipmentBreak -= ResetEquipment;
        
        _battleController.OnStartPlayerTurn -= UnlockSlot;
    }

    private void ResetEquipment()
    {
        foreach (var elementSlot in _elementSlots)
        {
            elementSlot.DeactivateSlot();  
        }
        
        _elementsFused.Clear();
        UpdateEquipmentUI();
    }

    private void UpdateEquipmentUI()
    {
        _powerText.SetText(_equipment.Power.ToString());
    }

    private void EnablePreviewCardEffect()
    {
        if (_gameDifficulty == GameDifficulty.Normal)
        {
            _normalPowerPreview.SetActive(true);
            _previewText.SetText(_elementBeingPreviewed.ElementData.Atomicnumber.ToString());
        }
        else
        {
            _hardPowerPreview.SetActive(true);
        }
    }

    private void DisablePreviewCardEffect()
    {
        if (_gameDifficulty == GameDifficulty.Normal)
        {
            _normalPowerPreview.SetActive(false);
        }
        else
        {
            _hardPowerPreview.SetActive(false);
        }
    }

    private void FuseElement(Element element)
    {
        _elementsFused.Add(element);

        _equipment.AddPower(element.ElementData.Atomicnumber);
        
        //ELECTRONEGATIVITY MULTIPLICATION
        // if (_elementsFused.Count == _maxElementsFused)
        // {
        //     _equipment.MultiplyPower(element.ElementData.Electronegativity);
        // }
        // else
        // {
        //     _equipment.AddPower(element.ElementData.Atomicnumber);
        // }

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
        _elementSlots[_elementsFused.Count - 1].ActivateSlot(elementInitials);
    }

    public void ActivateSlot()
    {
        _equipmentImage.transform.DOPunchScale(Vector3.one * 0.15f, 0.75f, 5, 0.5f);
        _equipmentImage.transform.DOShakeRotation(0.75f, 10f);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (eventData.pointerDrag == null || !eventData.pointerDrag.TryGetComponent(out CardController card))
        {
            return;
        }

        if (_isSlotLocked || _elementsFused.Count == _maxElementsFused)
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
