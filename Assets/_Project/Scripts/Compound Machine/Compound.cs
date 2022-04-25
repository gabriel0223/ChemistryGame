using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Compound
{
    public ElementProperty AtomicNumber { get; private set; }
    public ElementProperty Electronegativity { get; private set; }
    public ElementProperty AtomicRadius { get; private set; }

    public Compound(ElementProperty atomicNumber, ElementProperty electronegativity, ElementProperty atomicRadius)
    {
        AtomicNumber = atomicNumber;
        Electronegativity = electronegativity;
        AtomicRadius = atomicRadius;
    }
}
