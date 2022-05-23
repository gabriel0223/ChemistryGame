using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckManager : MonoBehaviour
{
    [SerializeField] private GameObject _cardPrefab;
    [SerializeField] private DeckController[] _decksToBeBuilt;
    [SerializeField] private Sprite[] _cardColors;
}
