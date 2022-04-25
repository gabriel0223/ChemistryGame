using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardDataDisplayer : MonoBehaviour
{
    [SerializeField] private Image _cardBaseColor;
    [SerializeField] private TMP_Text _abbreviation;
    [SerializeField] private TMP_Text _elementName;
    [SerializeField] private TMP_Text _atomicNumber;
    [SerializeField] private Color _smallQuantityColor;
    [SerializeField] private Color _largeQuantityColor;
    [SerializeField] private TMP_Text _atomicNumberSymbol;
    [SerializeField] private Image _atomicNumberArrow;
    [SerializeField] private Image _atomicNumberExtraArrow;
    [SerializeField] private TMP_Text _electronegativitySymbol;
    [SerializeField] private Image _electronegativityArrow;
    [SerializeField] private Image _electronegativityExtraArrow;
    [SerializeField] private TMP_Text _atomicRadiusSymbol;
    [SerializeField] private Image _atomicRadiusArrow;
    [SerializeField] private Image _atomicRadiusExtraArrow;

    public void SetCardColor(Sprite color)
    {
        _cardBaseColor.sprite = color;
    }
    
    public void UpdateDataDisplay(Element element)
    {
        _abbreviation.SetText(element.ElementData.Abbreviation);
        _elementName.SetText(element.ElementData.Elementname);
        _atomicNumber.SetText(element.ElementData.Atomicnumber.ToString());

        if (element.ElementData.Atomicnumber == 1)
        {
            DisplayHidrogenInfo();
            return;
        }
        
        UpdateArrowDisplay(element.AtomicNumber.PropertyQuantity, _atomicNumberSymbol, _atomicNumberArrow, _atomicNumberExtraArrow);
        UpdateArrowDisplay(element.Electronegativity.PropertyQuantity, _electronegativitySymbol, _electronegativityArrow, _electronegativityExtraArrow);
        UpdateArrowDisplay(element.AtomicRadius.PropertyQuantity, _atomicRadiusSymbol, _atomicRadiusArrow, _atomicRadiusExtraArrow);
    }

    private void UpdateArrowDisplay(PropertyQuantity quantity, TMP_Text symbol, Image arrow, Image extraArrow)
    {
        switch (quantity)
        {
            case PropertyQuantity.Minimum:
                setArrowsUpsideDown(true);
                paintArrows(_smallQuantityColor);
                setNumberOfArrowsActive(2);
                
                break;
            case PropertyQuantity.Low:
                setArrowsUpsideDown(true);
                paintArrows(_smallQuantityColor);
                setNumberOfArrowsActive(1);
                
                break;
            case PropertyQuantity.High:
                setArrowsUpsideDown(false);
                paintArrows(_largeQuantityColor);
                setNumberOfArrowsActive(1);
                
                break;
            case PropertyQuantity.Maximum:
                setArrowsUpsideDown(false);
                paintArrows(_largeQuantityColor);
                setNumberOfArrowsActive(2);
                
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        
        void setArrowsUpsideDown(bool value)
        {
            Vector3 arrowRotation = value == true ? Vector3.forward * -180 : Vector3.zero;

            arrow.transform.rotation = Quaternion.Euler(arrowRotation);
            extraArrow.transform.rotation = Quaternion.Euler(arrowRotation);
        }

        void paintArrows(Color color)
        {
            arrow.color = color;
            extraArrow.color = color;
            symbol.color = color;
        }

        void setNumberOfArrowsActive(int number)
        {
            switch (number)
            {
                case 1:
                    arrow.gameObject.SetActive(true);
                    extraArrow.gameObject.SetActive(false);
                    break;
                case 2:
                    arrow.gameObject.SetActive(true);
                    extraArrow.gameObject.SetActive(true);
                    break;
            }
        }
    }

    private void DisplayHidrogenInfo()
    {
        _atomicNumber.gameObject.SetActive(false);
        _atomicNumberSymbol.gameObject.SetActive(false);
        _electronegativitySymbol.gameObject.SetActive(false);
        _atomicRadiusSymbol.gameObject.SetActive(false);
    }
}
