using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompoundSlot : MonoBehaviour
{
    public event Action<Element> OnAddToCompound;
    public event Action OnResetCompound;
    public event Action<Compound> OnSendCompound; 

    private List<Element> _elementsOnCompound = new List<Element>();
    private Compound _generatedCompound;

    public void AddElementToCompound(Element element)
    {
        _elementsOnCompound.Add(element);
        
        OnAddToCompound?.Invoke(element);

        if (_elementsOnCompound.Count == 3)
        {
            ProcessCompound();
        }
    }

    private void ProcessCompound()
    {
        ElementProperty compoundAtomicNumber = new ElementProperty(PropertyName.AtomicNumber, PropertyQuantity.Low);
        ElementProperty compoundElectronegativity = new ElementProperty(PropertyName.Electronegativity, PropertyQuantity.Low);
        ElementProperty compoundAtomicRadius = new ElementProperty(PropertyName.AtomicRadius, PropertyQuantity.Low);

        foreach (var element in _elementsOnCompound)
        {
            if (element.ElementData.Atomicnumber == 1)
            {
                continue;
            }

            sumProperties(ref compoundAtomicNumber.PropertyQuantity, element.AtomicNumber.PropertyQuantity);
            sumProperties(ref compoundElectronegativity.PropertyQuantity, element.Electronegativity.PropertyQuantity);
            sumProperties(ref compoundAtomicRadius.PropertyQuantity, element.AtomicRadius.PropertyQuantity);
        }

        _generatedCompound = new Compound(compoundAtomicNumber, compoundElectronegativity, compoundAtomicRadius);
        Debug.Log($"ATOMIC NUMBER: {_generatedCompound.AtomicNumber.PropertyQuantity}");
        Debug.Log($"ELECTRONEGATIVITY: {_generatedCompound.Electronegativity.PropertyQuantity}");
        Debug.Log($"ATOMIC RADIUS: {_generatedCompound.AtomicRadius.PropertyQuantity}");
        
        OnSendCompound?.Invoke(_generatedCompound);
        Invoke(nameof(ResetCompound), 1f);

        void sumProperties(ref PropertyQuantity firstQuantity, PropertyQuantity secondQuantity)
        {
            int diff = Mathf.Abs((int)firstQuantity - (int)secondQuantity);

            if ((int)secondQuantity > (int)PropertyQuantity.Low)
            {
                diff += diff != 0? 0 : -1;
                firstQuantity += diff;
            }
            else
            {
                diff += diff != 0? 0 : 1;
                firstQuantity -= diff;
            }
            
            firstQuantity = (PropertyQuantity)Mathf.Clamp((int)firstQuantity, (int)PropertyQuantity.Minimum, (int)PropertyQuantity.Maximum);
        }
    }

    private void ResetCompound()
    {
        Debug.Log($"[{nameof(CompoundSlot)}] - Reset compound");
        _elementsOnCompound.Clear();
        
        OnResetCompound?.Invoke();
    }
}
