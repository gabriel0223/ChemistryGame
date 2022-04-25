using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckManager : MonoBehaviour
{
    [SerializeField] private GameObject _cardPrefab;
    [SerializeField] private DeckController[] _decksToBeBuilt;
    [SerializeField] private Sprite[] _cardColors;

    public void BuildDecks(Element[] elements)
    {
        for (int i = 0; i < _decksToBeBuilt.Length; i++)
        {
            int deckSize = Random.Range(DeckSettings.MIN_DECK_SIZE, DeckSettings.MAX_DECK_SIZE);
            DeckController deck = _decksToBeBuilt[i];
            Sprite deckColor = _cardColors[i];
            float topCardYPos = 0;
            
            for (int j = 0; j < deckSize; j++)
            {
                CardController newCard = Instantiate(_cardPrefab, deck.transform).GetComponent<CardController>();
                Element element = i == 0? elements[0] : elements[Random.Range(1, elements.Length)];
                newCard.Initialize(element);
                
                newCard.gameObject.GetComponent<CardDataDisplayer>().SetCardColor(deckColor);

                deck.AddCardToDeck(newCard);

                topCardYPos += DeckSettings.GetRandomSpacingBetweenCards();
                newCard.transform.localPosition = new Vector3(0, topCardYPos, 0);

                float randomRotation = Random.Range(-DeckSettings.MAX_CARD_ROTATION, DeckSettings.MAX_CARD_ROTATION);
                newCard.transform.GetChild(0).localEulerAngles = Vector3.forward * randomRotation;
            }
        }
    }
}
