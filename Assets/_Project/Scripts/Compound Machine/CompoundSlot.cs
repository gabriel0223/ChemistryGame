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
    
    public bool Resetting { get; private set; }

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

        OnSendCompound?.Invoke(_generatedCompound);
        StartCoroutine(ResetCompound());

        void sumProperties(ref PropertyQuantity firstQuantity, PropertyQuantity secondQuantity)
        {
            int propertySum = secondQuantity switch
            {
                PropertyQuantity.Minimum => -2,
                PropertyQuantity.Low => -1,
                PropertyQuantity.High => 1,
                PropertyQuantity.Maximum => 2,
                _ => throw new ArgumentOutOfRangeException(nameof(secondQuantity), secondQuantity, null)
            };

            firstQuantity += propertySum;
            
            firstQuantity = (PropertyQuantity)Mathf.Clamp((int)firstQuantity, (int)PropertyQuantity.Minimum, (int)PropertyQuantity.Maximum);
        }
    }

    private IEnumerator ResetCompound()
    {
        Resetting = true;
        Debug.Log($"[{nameof(CompoundSlot)}] - Reset compound");

        yield return new WaitForSeconds(1f);
        
        _elementsOnCompound.Clear();
        OnResetCompound?.Invoke();
        Resetting = false;
    }
}
