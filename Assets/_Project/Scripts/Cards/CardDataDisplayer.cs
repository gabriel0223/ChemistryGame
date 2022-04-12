using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardDataDisplayer : MonoBehaviour
{
    [SerializeField] private TMP_Text _abbreviation;
    [SerializeField] private TMP_Text _elementName;
    [SerializeField] private TMP_Text _atomicNumber;
    [SerializeField] private TMP_Text _electronegativity;
    [SerializeField] private TMP_Text _atomicRadius;
    [SerializeField] private TMP_Text _elementGroup;

    public void UpdateDataDisplay(Element element)
    {
        _abbreviation.SetText(element.ElementData.Abbreviation);
        _elementName.SetText(element.ElementData.Elementname);
        _atomicNumber.SetText(element.ElementData.Atomicnumber.ToString());
        _electronegativity.SetText(element.ElementData.Electronegativity.ToString(CultureInfo.InvariantCulture));
        _atomicRadius.SetText(element.ElementData.Atomicradius.ToString(CultureInfo.InvariantCulture));
        //_elementGroup.SetText(element.GROUP.ToString());
    }
}
