using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateSprite : MonoBehaviour
{
    public Sprite cardFace;
    public Sprite cardBack;
    private SpriteRenderer spriteRenderer;
    private Selectable selectable;
    private PokerGame pokerGame;


    // Start is called before the first frame update
    void Start()
    {
        List<Card> deck = PokerGame.GenerateDeck();
        pokerGame = FindObjectOfType<PokerGame>();

        int i = 0;
        foreach(Card card in deck)
        {

            if(this.name == pokerGame.CardDecode(card))
            {
                cardFace = pokerGame.cardFaces[i];
                break;
            }
            i++;
        }
        spriteRenderer = GetComponent<SpriteRenderer>();
        selectable = GetComponent<Selectable>();
    }

    // Update is called once per frame
    void Update()
    {
        if(selectable.faceUp == true)
        {
            spriteRenderer.sprite = cardFace;
        }
        else
        {
            spriteRenderer.sprite = cardBack;
        }
    }
}
