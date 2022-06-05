using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ElementSlotController : MonoBehaviour
{
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private TMP_Text _elementText;

    private Image _image;
    private Image _slotImage;

    private void Awake()
    {
        _image = GetComponent<Image>();
        _slotImage = transform.GetChild(0).GetComponent<Image>();
    }

    public void ActivateSlot(string elementInitials)
    {
        _elementText.SetText(elementInitials);
        _canvasGroup.DOFade(1f, 0.5f);
        AudioManager.instance.PlayRandomBetweenSounds(new[] { "powerUp01", "powerUp02" });
    }

    public void DeactivateSlot()
    {
        _canvasGroup.DOFade(0f, 0.5f);
    }

    public void ChangeSlotColor(Color color)
    {
        _image.DOColor(color, 0.5f);
        _slotImage.DOColor(color, 0.5f);
    }
}
