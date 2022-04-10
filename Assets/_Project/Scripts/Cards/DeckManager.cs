using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckManager : MonoBehaviour
{
    [SerializeField] private GameObject _cardPrefab;
    [SerializeField] private DeckController[] _decksToBeBuilded;
    
    // Start is called before the first frame update
    void Start()
    {
        BuildDecks(_decksToBeBuilded);
    }

    private void BuildDecks(DeckController[] decks)
    {
        foreach (var deck in decks)
        {
            int deckSize = Random.Range(DeckSettings.MIN_DECK_SIZE, DeckSettings.MAX_DECK_SIZE);
            float topCardYPos = 0;
            
            for (int i = 0; i < deckSize; i++)
            {
                CardController newCard = Instantiate(_cardPrefab, deck.transform).GetComponent<CardController>();
                deck.AddCardToDeck(newCard);

                topCardYPos += DeckSettings.GetRandomSpacingBetweenCards();
                newCard.transform.localPosition = new Vector3(0, topCardYPos, 0);

                float randomRotation = Random.Range(-DeckSettings.MAX_CARD_ROTATION, DeckSettings.MAX_CARD_ROTATION);
                newCard.transform.GetChild(0).localEulerAngles = Vector3.forward * randomRotation;
            }
        }
    }
}
