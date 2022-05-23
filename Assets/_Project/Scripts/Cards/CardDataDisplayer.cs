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
    [SerializeField] private TMP_Text _electronegativity;

    public void SetCardColor(Sprite color)
    {
        _cardBaseColor.sprite = color;
    }

    public void InitializeDataDisplay(Element element, GameDifficulty difficulty)
    {
        if (difficulty == GameDifficulty.Normal)
        {
            _abbreviation.SetText(element.ElementData.Abbreviation);
            _elementName.SetText(element.ElementData.Elementname);
            _atomicNumber.SetText(element.ElementData.Atomicnumber.ToString());
            _electronegativity.SetText(element.ElementData.Electronegativity.ToString(CultureInfo.InvariantCulture));
        }
        else
        {
            _abbreviation.SetText(element.ElementData.Abbreviation);
            _elementName.SetText(element.ElementData.Elementname);
            _atomicNumber.SetText("");
            _electronegativity.SetText("");
        }
    }
}
