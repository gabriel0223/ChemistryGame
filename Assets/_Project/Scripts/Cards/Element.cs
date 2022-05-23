using System;
using System.Collections;
using System.Collections.Generic;

public class Element
{
    public ElementsSheetData ElementData { get; private set; }
    public ElementProperty AtomicNumber { get; private set; }
    public ElementProperty Electronegativity { get; private set; }

    public Element(ElementsSheetData elementData)
    {
        ElementData = elementData;
        AtomicNumber = new ElementProperty(PropertyName.AtomicNumber, PropertyQuantity.Low);
        Electronegativity = new ElementProperty(PropertyName.Electronegativity, PropertyQuantity.Low);
    }
}
