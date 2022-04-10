using System;

public static class DeckSettings
{
    private const int MAX_SPACING_BETWEEN_CARDS = 12;
    private const int MIN_SPACING_BETWEEN_CARDS = 6;
    public const int MIN_DECK_SIZE = 5;
    public const int MAX_DECK_SIZE = 12;
    public const float MAX_CARD_ROTATION = 2f;

    public static float GetRandomSpacingBetweenCards()
    {
        Random random = new Random();

        return random.Next(MIN_SPACING_BETWEEN_CARDS, MAX_SPACING_BETWEEN_CARDS);
    }
}