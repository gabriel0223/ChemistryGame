using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;

public class DisplayElementsInfo : MonoBehaviour
{
    [SerializeField] private ElementsSheet _elementsSheet;
    [SerializeField] private int _elementAtomicNumber;

    [SerializeField] private TMP_Text _abbreviation;
    [SerializeField] private TMP_Text _elementName;
    [SerializeField] private TMP_Text _atomicNumber;
    [SerializeField] private TMP_Text _electronegativity;
    [SerializeField] private TMP_Text _atomicRadius;
    [SerializeField] private TMP_Text _elementGroup;

    // Start is called before the first frame update
    void Start()
    {
        int elementIndex = _elementAtomicNumber - 1;
        
        _abbreviation.SetText(_elementsSheet.dataArray[elementIndex].Abbreviation);
        _elementName.SetText(_elementsSheet.dataArray[elementIndex].Elementname);
        _atomicNumber.SetText(_elementsSheet.dataArray[elementIndex].Atomicnumber.ToString());
        _electronegativity.SetText(_elementsSheet.dataArray[elementIndex].Electronegativity.ToString(CultureInfo.InvariantCulture));
        _atomicRadius.SetText(_elementsSheet.dataArray[elementIndex].Atomicradius.ToString(CultureInfo.InvariantCulture));
        _elementGroup.SetText(_elementsSheet.dataArray[elementIndex].GROUP.ToString());
    }
}
