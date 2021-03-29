using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public int cardSuit;
    public int cardValue;
     public Card(int suit ,int value)
    {
       cardSuit = suit;
       cardValue = value;
    }
    public Card(Card card)
    {
        cardSuit = card.cardSuit;
        cardValue = card.cardValue;
    }

}
