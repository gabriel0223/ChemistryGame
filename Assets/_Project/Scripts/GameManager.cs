using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private ElementsSheet _elementsSheet;
    [SerializeField] private DeckManager _deckManager;
    [SerializeField] private int[] _atomicNumbersUsedInThisMatch;

    private List<Element> _elementsUsedInThisMatch = new List<Element>();

    private void Start()
    {
        GenerateMatchElements();
        _deckManager.BuildDecks(_elementsUsedInThisMatch.ToArray());
    }

    private void GenerateMatchElements()
    {
        foreach (var atomicNumber in _atomicNumbersUsedInThisMatch)
        {
            Element newElement = new Element(_elementsSheet.dataArray[atomicNumber - 1]);

            _elementsUsedInThisMatch.Add(newElement);   
        }

        List<Element> elementsByAtomicNumber = _elementsUsedInThisMatch.OrderBy(e => e.ElementData.Atomicnumber).ToList();
        List<Element> elementsByElectronegativity = _elementsUsedInThisMatch.OrderBy(e => e.ElementData.Electronegativity).ToList();
        List<Element> elementsByAtomicRadius = _elementsUsedInThisMatch.OrderBy(e => e.ElementData.Atomicradius).ToList();
        
        foreach (var element in _elementsUsedInThisMatch)
        {
            CalculatePropertyQuantity(element, PropertyName.AtomicNumber, elementsByAtomicNumber);
            CalculatePropertyQuantity(element, PropertyName.Electronegativity, elementsByElectronegativity);
            CalculatePropertyQuantity(element, PropertyName.AtomicRadius, elementsByAtomicRadius);
        }
    }

    private void CalculatePropertyQuantity(Element element, PropertyName property, List<Element> elementsOrderedByProperty)
    {
        int minimumThreshold = _elementsUsedInThisMatch.Count / 4;
        int maximumThreshold = (int)(_elementsUsedInThisMatch.Count / 2 * 1.5f);
        
        int elementIndex = elementsOrderedByProperty.FindIndex(e => e == element);

        if (elementIndex > _elementsUsedInThisMatch.Count / 2)
        {
            bool isQuantityMaximum = elementIndex >= maximumThreshold;
            element.SetPropertyQuantity(property, isQuantityMaximum ? PropertyQuantity.Maximum : PropertyQuantity.High);
        }
        else
        {
            bool isQuantityMinimum = elementIndex <= minimumThreshold;
            element.SetPropertyQuantity(property, isQuantityMinimum ? PropertyQuantity.Minimum : PropertyQuantity.Low);
        }
    }
}
