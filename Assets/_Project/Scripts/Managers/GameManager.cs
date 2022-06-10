using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private ElementsSheet _elementsSheet;
    [SerializeField] private CardManager _cardManager;
    [SerializeField] private int[] _atomicNumbersUsedInThisMatch;

    private List<Element> _elementsUsedInThisMatch = new List<Element>();

    private void Start()
    {
        GenerateMatchElements();
        _cardManager.GenerateHand(_elementsUsedInThisMatch.ToArray());
    }

    private void GenerateMatchElements()
    {
        foreach (var atomicNumber in _atomicNumbersUsedInThisMatch)
        {
            Element newElement = new Element(_elementsSheet.dataArray[atomicNumber - 1]);

            _elementsUsedInThisMatch.Add(newElement);   
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GoBackToMap()
    {
        SceneManager.LoadScene((int)SceneIndexes.MAP);
    }
}
