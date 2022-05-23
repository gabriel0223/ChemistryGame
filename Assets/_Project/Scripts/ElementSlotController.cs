using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class ElementSlotController : MonoBehaviour
{
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private TMP_Text _elementText;

    public void ActivateSlot(string elementInitials)
    {
        _elementText.SetText(elementInitials);
        _canvasGroup.DOFade(1f, 0.5f);
    }

    public void DeactivateSlot()
    {
        _canvasGroup.DOFade(0f, 0.5f);
    }
}
