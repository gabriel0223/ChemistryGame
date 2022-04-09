using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;

public class CardDataDisplayer : MonoBehaviour
{
    [SerializeField] private TMP_Text _abbreviation;
    [SerializeField] private TMP_Text _elementName;
    [SerializeField] private TMP_Text _atomicNumber;
    [SerializeField] private TMP_Text _electronegativity;
    [SerializeField] private TMP_Text _atomicRadius;
    [SerializeField] private TMP_Text _elementGroup;

    public void UpdateDataDisplay(ElementsSheetData element)
    {
        _abbreviation.SetText(element.Abbreviation);
        _elementName.SetText(element.Elementname);
        _atomicNumber.SetText(element.Atomicnumber.ToString());
        _electronegativity.SetText(element.Electronegativity.ToString(CultureInfo.InvariantCulture));
        _atomicRadius.SetText(element.Atomicradius.ToString(CultureInfo.InvariantCulture));
        //_elementGroup.SetText(element.GROUP.ToString());
    }
}
