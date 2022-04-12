using System;
using System.Collections;
using System.Collections.Generic;
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
        int atomicNumberAverage = 0;
        float electronegativityAverage = 0;
        float atomicRadiusAverage = 0;
        
        foreach (var atomicNumber in _atomicNumbersUsedInThisMatch)
        {
            Element newElement = new Element(_elementsSheet.dataArray[atomicNumber - 1]);

            atomicNumberAverage += newElement.ElementData.Atomicnumber;
            electronegativityAverage += newElement.ElementData.Electronegativity;
            atomicRadiusAverage += newElement.ElementData.Atomicradius;
            
            _elementsUsedInThisMatch.Add(newElement);   
        }

        atomicNumberAverage /= _elementsUsedInThisMatch.Count;
        electronegativityAverage /= _elementsUsedInThisMatch.Count;
        atomicRadiusAverage /= _elementsUsedInThisMatch.Count;

        foreach (var element in _elementsUsedInThisMatch)
        {
            bool isAtomicNumberAboveAverage = element.ElementData.Atomicnumber > atomicNumberAverage;
            bool isElectronegativityAboveAverage = element.ElementData.Electronegativity > electronegativityAverage;
            bool isAtomicRadiusAboveAverage = element.ElementData.Atomicnumber > atomicRadiusAverage;

            element.SetPropertyQuantity(PropertyName.AtomicNumber,
                isAtomicNumberAboveAverage ? PropertyQuantity.High : PropertyQuantity.Low);
            element.SetPropertyQuantity(PropertyName.Electronegativity,
                isElectronegativityAboveAverage ? PropertyQuantity.High : PropertyQuantity.Low);
            element.SetPropertyQuantity(PropertyName.AtomicRadius,
                isAtomicRadiusAboveAverage ? PropertyQuantity.High : PropertyQuantity.Low);
        }
        
        //TODO 
        //finish average calculations
    }
}
