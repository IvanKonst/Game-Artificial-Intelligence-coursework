using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : Player
{
   // public PokerGame poker;
    public int[] Outcomes;
    public bool haspaid = false;
    public GameObject FloatingText;
    public GameObject floatingFoldText;
    // Update is called once per frame
    void Update()
    {

    }

    //Responsibble for the AI Fold decision, which makes the AI leave the game, and destroy its current cards in the hand
    public void AIFold()
    {
        Debug.Log("I fold");
        Debug.Log(pokergame.raise_ammount);
        ShowFloatingFoldText();
        Destroy(AI1card);
        Destroy(AI1card2);     
        fold = true;
    //    finishturn = true;
        pokergame.player_left--;
        playeraction = -1;
      
    }

    //Override of the Player function, which is responsible for calculating the possibility of the bot to improve its hand in the next 2 cards and calculating the current hand strengh
    //of the bot and base on that desiding if it should call, check or fold.
    public override void Playeraction()
    {
        //  Debug.Log("I am in");
        CalculateHand();
        if (fold == false)
        {
            Outcomes = new int[135];
            int outs = 0;
            double winprobability = 0;
            finishturn = false;
            double handStrenghtbefore = handStrenght;
            Calculatepossibility();
            for (int i = (int)handStrenghtbefore + 1; i < 135; i++)
            {
                if (Outcomes[i] >= 1)
                {

                }
                if (i > 25)
                {
                    outs += Outcomes[i];
                }

            }

            winprobability = (double)outs * 2 / (47 * 46) * 100;
            
            if (winprobability >= 20)
            {
                if (pokergame.raise == true )
                {
                    if (pokergame.raise_ammount >= player_chips_ammount / 5 && (handStrenghtbefore <= 28 || winprobability <= 45))
                    {
                        AIFold();
                        
                    }
                    if (pokergame.raise_ammount >= player_chips_ammount / 5 && (handStrenghtbefore > 28 || winprobability > 45))
                    {
                        AIChecksorCalls();

                    }
                    if (pokergame.raise_ammount < player_chips_ammount / 5 && (handStrenghtbefore < 21 || winprobability <= 20))
                    {
                        AIFold();
                    }
                    if (pokergame.raise_ammount < player_chips_ammount / 5 && (handStrenghtbefore >= 21 && winprobability >= 20))
                    {
                        AIChecksorCalls();
                    }
                    if (pokergame.raise_ammount < player_chips_ammount / 20 && (handStrenghtbefore < 21 || winprobability >= 20))
                    {
                        AIChecksorCalls();
                    }             

                }
                else
                {
                    if (handStrenghtbefore <= 21)
                    {
                        AIChecksorCalls();
                        playeraction = 0;
                    }
                    else
                    {
                        AIChecksorCalls();
                        playeraction = 0;
                        //Raise
                    }
                }
                
            }
            if(winprobability < 20)
            {
                if (pokergame.raise == false)
                {
                    AIChecksorCalls();
                    playeraction = 0;
                }
                else
                {
                    AIFold();
                }
            }
           
            Debug.Log("hand strenght " + handStrenghtbefore + "win probability " + winprobability + "player action: " + playeraction);
        }
        //else 
        //{
        //    AIFold();
        //}

        
    }

    //Responsibble for the AI Check or Call decision
    public void AIChecksorCalls()
    {
        if (pokergame.raise_ammount > 0)
        {
            if ( pokergame.raise_ammount > 0 && haspaid == false)
            {
                
                //haspaid = true;
                pokergame.pot += pokergame.raise_ammount;
                player_chips_ammount -= pokergame.raise_ammount;
               
                // Debug.Log("I pay");
                //finishturn = true;
                Debug.Log(pokergame.raise_ammount);
                ShowFloatingText();
                playeraction = 0;
                haspaid = true;
            }
            
        }
        if(pokergame.raise_ammount == 0)
        {
            Debug.Log(pokergame.raise_ammount);
            ShowFloatingCheckText();

          //  finishturn = true;
        }
    }

    //Calculating the possibility of the AI to improve their hand until the end of the round
    public void Calculatepossibility()
    {

        List<Card> cardsleft = PokerGame.GenerateDeck();
        for (int i = 0; i < cards.Count; i++)
        {
            for (int j = 0; j < cardsleft.Count; j++)
            {
                if (cardsleft[j].cardSuit == cards[i].cardSuit && cardsleft[j].cardValue == cards[i].cardValue)
                {
                    cardsleft.RemoveAt(j);

                }
            }

        }

        List<Card> checkhand = new List<Card>();
        for (int i = 0; i < cardsleft.Count; i++)
        {
            for (int j = i + 1; j < cardsleft.Count; j++)
            {
                checkhand.Clear();
                handStrenght = 0;
                cards.ForEach((item) =>
                {
                    checkhand.Add(new Card(item));

                });
                checkhand.Add(cardsleft[i]);
                checkhand.Add(cardsleft[j]);
                CheckProbability(checkhand);
                Outcomes[(int)(handStrenght)]++;

            }
        }
    }
    public void ShowFloatingText()
    {
        float Destroytime = 2f;
       GameObject floattext = Instantiate(FloatingText, transform.position, Quaternion.identity, transform);
        Destroy(floattext, Destroytime);
    }
    public void ShowFloatingCheckText()
    {
        float Destroytime = 2f;
        GameObject floattext = Instantiate(FloatingText, transform.position, Quaternion.identity, transform);
        floattext.GetComponent<TextMesh>().text = "Check";
        Destroy(floattext, Destroytime);
    }
    public void ShowFloatingFoldText()
    {
        float Destroytime = 2f;
       GameObject foldtext = Instantiate(floatingFoldText, transform.position, Quaternion.identity, transform);
        Destroy(foldtext, Destroytime);

    }

    //Part of the function that calculates the AI chance to improve their hand and their hand strenght at the moment
    public void CheckProbability(List<Card> checkhand)
    {
        List<Card> sortbyvalue = new List<Card>(checkhand.Count);
        List<Card> sortbysuit = new List<Card>(checkhand.Count);

        checkhand.ForEach((item) =>
        {
            sortbyvalue.Add(new Card(item));
            sortbysuit.Add(new Card(item));
        });

        SortCards(sortbyvalue);
        SortCardsbySuit(sortbysuit);
        handStrenght = sortbyvalue[checkhand.Count - 1].cardValue;
        FlushCheck(sortbysuit);
        StraightCheck(sortbyvalue);
        PairCheck(sortbyvalue);
        ThreeofaKind(sortbyvalue);
        FourofaKind(sortbyvalue);
        TwoPairs(sortbyvalue);
        // FullHouse(sortbyvalue);
        //Debug.Log(handStrenght);
        //return handStrenght;
    }
}
