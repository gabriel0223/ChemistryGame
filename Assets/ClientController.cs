using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ClientController : MonoBehaviour
{
    [SerializeField] private Image _clientImage;
    [SerializeField] private TMP_Text _firstPropertyText;
    [SerializeField] private TMP_Text _secondPropertyText;
    [SerializeField] private Image _firstArrow;
    [SerializeField] private Image _firstExtraArrow;
    [SerializeField] private Image _secondArrow;
    [SerializeField] private Image _secondExtraArrow;

    [SerializeField] private Color _largeQuantityColor;
    [SerializeField] private Color _smallQuantityColor;
    
    private Order _firstOrder;
    private Order _secondOrder;
    private CompoundSlot _compoundSlot;

    public void InitializeOrders(Order firstOrder, Order secondOrder)
    {
        _firstOrder = firstOrder;
        _secondOrder = secondOrder;
        
        DisplayOrdersInfo();
    }

    private void DisplayOrdersInfo()
    {
        DisplayPropertyLetter(_firstOrder, _firstPropertyText);
        DisplayPropertyLetter(_secondOrder, _secondPropertyText);

        DisplayOrderArrows(_firstOrder, _firstArrow, _firstExtraArrow);
        DisplayOrderArrows(_secondOrder, _secondArrow, _secondExtraArrow);
    }

    private void DisplayPropertyLetter(Order order, TMP_Text balloonText)
    {
        switch (order.ElementProperty.PropertyName)
        {
            case PropertyName.AtomicNumber:
                balloonText.SetText("z");
                break;
            case PropertyName.Electronegativity:
                balloonText.SetText("e");
                break;
            case PropertyName.AtomicRadius:
                balloonText.SetText("r");
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void DisplayOrderArrows(Order order, Image arrow, Image extraArrow)
    {
        switch (order.ElementProperty.PropertyQuantity)
        {
            case PropertyQuantity.Minimum:
                PaintArrows(_smallQuantityColor);
                SetArrowsUpsideDown(true);
                break;
            case PropertyQuantity.Low:
                extraArrow.gameObject.SetActive(false);
                PaintArrows(_smallQuantityColor);
                SetArrowsUpsideDown(true);
                break;
            case PropertyQuantity.High:
                extraArrow.gameObject.SetActive(false);
                PaintArrows(_largeQuantityColor);
                SetArrowsUpsideDown(false);
                break;
            case PropertyQuantity.Maximum:
                PaintArrows(_largeQuantityColor);
                SetArrowsUpsideDown(false);
                break;
            default:
                Debug.Log(order.ElementProperty.PropertyQuantity);
                throw new ArgumentOutOfRangeException();
        }

        void SetArrowsUpsideDown(bool value)
        {
            Vector3 arrowRotation = value == true ? Vector3.forward * -90 : Vector3.forward * 90;

            arrow.transform.rotation = Quaternion.Euler(arrowRotation);
            extraArrow.transform.rotation = Quaternion.Euler(arrowRotation);
        }

        void PaintArrows(Color color)
        {
            arrow.color = color;
            extraArrow.color = color;
        }
    }

    public void SetClientImage(Sprite clientImage)
    {
        _clientImage.sprite = clientImage;
    }
    
    public void SetCompoundSlot(CompoundSlot compoundSlot)
    {
        _compoundSlot = compoundSlot;
        _compoundSlot.OnSendCompound += ReceiveOrder;
    }

    private void ReceiveOrder(List<CardController> compound)
    {
        
    }
}
