using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementProperty
{
    public PropertyName PropertyName;
    public PropertyQuantity PropertyQuantity;

    public ElementProperty(PropertyName propertyName, PropertyQuantity propertyQuantity)
    {
        PropertyName = propertyName;
        PropertyQuantity = propertyQuantity;
    }
}
