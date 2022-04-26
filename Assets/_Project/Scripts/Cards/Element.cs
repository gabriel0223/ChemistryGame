using System;
using System.Collections;
using System.Collections.Generic;

public class Element
{
    public ElementsSheetData ElementData { get; private set; }
    public ElementProperty AtomicNumber { get; private set; }
    public ElementProperty Electronegativity { get; private set; }
    public ElementProperty AtomicRadius { get; private set; }

    public Element(ElementsSheetData elementData)
    {
        ElementData = elementData;
        AtomicNumber = new ElementProperty(PropertyName.AtomicNumber, PropertyQuantity.Low);
        Electronegativity = new ElementProperty(PropertyName.Electronegativity, PropertyQuantity.Low);
        AtomicRadius = new ElementProperty(PropertyName.AtomicRadius, PropertyQuantity.Low);
    }

    public void SetPropertyQuantity(PropertyName name, PropertyQuantity quantity)
    {
        switch (name)
        {
            case PropertyName.AtomicNumber:
                AtomicNumber.PropertyQuantity = quantity;
                break;
            case PropertyName.Electronegativity:
                Electronegativity.PropertyQuantity = quantity;
                break;
            case PropertyName.AtomicRadius:
                AtomicRadius.PropertyQuantity = quantity;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(name), name, null);
        }
    }
}
