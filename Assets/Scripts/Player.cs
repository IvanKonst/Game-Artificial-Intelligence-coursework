using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Player : MonoBehaviour
{
    public List<Card> cards;
    public PokerGame pokergame;
    public bool fold = false;
    public bool playercheck = false;
    public bool playerfold = false;

    public TMP_Text raise_text;

    public double handStrenght;

    public bool finishturn = false;
    public bool privateplayeraction = false;
    public Button button;
    public int playeraction = -2;

    public float player_chips_ammount = 5000;
    public int player_paid;

    public int required_ammount;

    public GameObject playercard;
    public GameObject playercard2;
    public GameObject AIcard1;
    public GameObject AIcard2;
  
 

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    //Player Turn function, that lets the player to decide if they should call, check or fold
    public IEnumerator PlayerTurn()
    {


        if (finishturn == true)
        {
   
        }

        else
        {
            yield return new WaitForSeconds(1f);
        }
    }

    //Reseting Player hand
    public void Reset()
    {
        fold = false;
        cards.Clear();
        handStrenght = 0;
        playercheck = false;
        playerfold = false;
        finishturn = false;
        privateplayeraction = false;
        playeraction = -2;
    }

    //Player action, returning different values, based on the player decision
    public virtual void Playeraction()
    {
      //  Debug.Log("Player turn");
        if (finishturn == true)
        {
            if (playercheck == true)
            {
                playercheck = false;
                finishturn = false;
                playeraction = 0;
                Debug.Log(" player check");
                //Check

            }
            else if (playerfold == true)
            {

                playeraction = -1;
                playerfold = false;
                finishturn = false;
                //Fold
            }
            else if (privateplayeraction == true)
            {
                playeraction = 3;
                finishturn = false;
                privateplayeraction = false;
                //Raise
            }
            //else
            //{
            //    playeraction = -2;
            //    finishturn = false;

            //}
        }
        else
        {
            StartCoroutine(PlayerTurn());
        }
    }

    //A function that is called once the raise slider is moved, changing the raise ammount
    public void Raiseslider(float sliderValue)
    {
        pokergame.raise_ammount = sliderValue;
        raise_text.text = pokergame.raise_ammount.ToString();
        // Debug.Log(pokergame.raise_ammount);
       // Debug.Log(sliderValue);
    }

    //Dealing cards to the player and the AI based on the player order that is given
    public void DealHand(int player)
    {
        if (player == 1)
        {
            playercard = Instantiate(pokergame.cardPrefab, new Vector3(pokergame.playercardposition1.transform.position.x, pokergame.playercardposition1.transform.position.y, pokergame.playercardposition1.transform.position.z), Quaternion.identity);
            playercard.name = pokergame.CardDecode(pokergame.deck[0]);
            pokergame.player.cards.Add(pokergame.deck[0]);
            pokergame.deck.RemoveAt(0);
            playercard.GetComponent<Selectable>().faceUp = true;
            playercard.gameObject.tag = "card";

            playercard2 = Instantiate(pokergame.cardPrefab, new Vector3(pokergame.playercardposition2.transform.position.x, pokergame.playercardposition2.transform.position.y, pokergame.playercardposition2.transform.position.z), Quaternion.identity);
            playercard2.name = pokergame.CardDecode(pokergame.deck[0]);
            pokergame.player.cards.Add(pokergame.deck[0]);
            pokergame.deck.RemoveAt(0);
            playercard2.GetComponent<Selectable>().faceUp = true;
            playercard2.gameObject.tag = "card";
        }
        //AI1 cards
        if (player == 2)
        {
            AIcard1 = Instantiate(pokergame.cardPrefab, new Vector3(pokergame.ai1cardposition1.transform.position.x, pokergame.ai1cardposition1.transform.position.y, pokergame.ai1cardposition1.transform.position.z), Quaternion.identity);
            AIcard1.name = pokergame.CardDecode(pokergame.deck[0]);
            pokergame.ai1.cards.Add(pokergame.deck[0]);
            pokergame.deck.RemoveAt(0);
            AIcard1.GetComponent<Selectable>().faceUp = false;
            AIcard1.gameObject.tag = "card";

            AIcard2 = Instantiate(pokergame.cardPrefab, new Vector3(pokergame.ai1cardposition2.transform.position.x, pokergame.ai1cardposition2.transform.position.y, pokergame.ai1cardposition2.transform.position.z), Quaternion.identity);
            AIcard2.name = pokergame.CardDecode(pokergame.deck[0]);
            pokergame.ai1.cards.Add(pokergame.deck[0]);
            pokergame.deck.RemoveAt(0);
            AIcard2.GetComponent<Selectable>().faceUp = false;
            AIcard2.gameObject.tag = "card";
        }
        //AI2 cards
        if (player == 3)
        {
            AIcard1 = Instantiate(pokergame.cardPrefab, new Vector3(pokergame.ai2cardposition1.transform.position.x, pokergame.ai2cardposition1.transform.position.y, pokergame.ai2cardposition1.transform.position.z), Quaternion.identity);
            AIcard1.name = pokergame.CardDecode(pokergame.deck[0]);
            pokergame.ai2.cards.Add(pokergame.deck[0]);
            pokergame.deck.RemoveAt(0);
            AIcard1.GetComponent<Selectable>().faceUp = false;
            AIcard1.gameObject.tag = "card";

            AIcard2 = Instantiate(pokergame.cardPrefab, new Vector3(pokergame.ai2cardposition2.transform.position.x, pokergame.ai2cardposition2.transform.position.y, pokergame.ai2cardposition2.transform.position.z), Quaternion.identity);
            AIcard2.name = pokergame.CardDecode(pokergame.deck[0]);
            pokergame.ai3.cards.Add(pokergame.deck[0]);
            pokergame.deck.RemoveAt(0);
            AIcard2.GetComponent<Selectable>().faceUp = false;
            AIcard2.gameObject.tag = "card";
        }
        if (player == 4)
        {
            AIcard1 = Instantiate(pokergame.cardPrefab, new Vector3(pokergame.ai3cardposition1.transform.position.x, pokergame.ai3cardposition1.transform.position.y, pokergame.ai3cardposition1.transform.position.z), Quaternion.identity);
            AIcard1.name = pokergame.CardDecode(pokergame.deck[0]);
            pokergame.ai2.cards.Add(pokergame.deck[0]);
            pokergame.deck.RemoveAt(0);
            AIcard1.GetComponent<Selectable>().faceUp = false;
            AIcard1.gameObject.tag = "card";

            AIcard2 = Instantiate(pokergame.cardPrefab, new Vector3(pokergame.ai3cardposition2.transform.position.x, pokergame.ai3cardposition2.transform.position.y, pokergame.ai3cardposition2.transform.position.z), Quaternion.identity);
            AIcard2.name = pokergame.CardDecode(pokergame.deck[0]);
            pokergame.ai3.cards.Add(pokergame.deck[0]);
            pokergame.deck.RemoveAt(0);
            AIcard2.GetComponent<Selectable>().faceUp = false;
            AIcard2.gameObject.tag = "card";
        }

    }

    //Changing booleans and destroying player cards in his/her hand once they fold
    public void PlayerFold()
    {
        fold = true;
        Destroy(playercard);
        Destroy(playercard2);
        pokergame.player_left--;
        finishturn = true;
    }

    //A function that is called once the playercheck button is pressed.
    public void PlayerCheckButton()
    {
        playercheck = true;
        finishturn = true;
        //   Debug.Log("check");
    }

    //A function that is called once the playerfold button is pressed.
    public void PlayerFoldButton()
    {
        playerfold = true;
        PlayerFold();
     //   Debug.Log("fold");
    }

    //A function that is called once the raise button is pressed and raise ammount is selected using the slider
    public void Raise()
    {
        //pokergame.allchecked = 0;
        
        pokergame.pot += pokergame.raise_ammount;
        player_chips_ammount -= pokergame.raise_ammount;
      //  Debug.Log(pokergame.raise_ammount);
        privateplayeraction = true;
        pokergame.raise = true;
        finishturn = true;
       
        //   Debug.Log("raise");
    }

    //The main function that calculates the handstrenght of the player and the AI, by checking every possible combination that they have 
    public double CalculateHand()
    {
        List<Card> sortbyvalue = new List<Card>(cards.Count);
        List<Card> sortbysuit = new List<Card>(cards.Count);
        cards.ForEach((item) =>
        {
            sortbyvalue.Add(new Card(item));
            sortbysuit.Add(new Card(item));
        });

        SortCards(sortbyvalue);
        SortCardsbySuit(sortbysuit);
        handStrenght = sortbyvalue[cards.Count - 1].cardValue;
        FlushCheck(sortbysuit);
        StraightCheck(sortbyvalue);
        PairCheck(sortbyvalue);
        ThreeofaKind(sortbyvalue);
        FourofaKind(sortbyvalue);
        TwoPairs(sortbyvalue);
        FullHouse(sortbyvalue);
        // Debug.Log(handStrenght);
        return handStrenght;

    }

    //Sorting the cards by value
    public void SortCards(List<Card> data)
    {
        int i, j;
        int N = data.Count;
        for (j = N - 1; j > 0; j--)
        {
            for (i = 0; i < j; i++)
            {
                if (data[i].cardValue > data[i + 1].cardValue)
                {
                    (data[i], data[i + 1]) = (data[i + 1], data[i]);
                }

            }
        }
    }

    //Sorting the cards by suits.
    public void SortCardsbySuit(List<Card> data)
    {

        int n = data.Count;
        for (int i = 0; i < n - 1; i++)
        {
            int min_indx = i;
            for (int j = i + 1; j < n; j++)
            {
                if (data[j].cardSuit < data[min_indx].cardSuit)
                {
                    min_indx = j;
                }
                if (data[j].cardSuit == data[min_indx].cardSuit)
                {
                    if (data[j].cardValue < data[min_indx].cardValue)
                    {
                        min_indx = j;
                    }
                }

            }
            (data[i], data[min_indx]) = (data[min_indx], data[i]);
        }
    }

    //Checking for flush combination
    public void FlushCheck(List<Card> sortbysuit)
    {
        int flush = 1;
        bool straightflush = true;
        for (int i = 1; i < cards.Count; i++)
        {
            if (sortbysuit[i].cardSuit == sortbysuit[i - 1].cardSuit)
            {
                if (sortbysuit[i].cardValue != sortbysuit[i - 1].cardValue + 1)
                {
                    //Debug.Log(sortbysuit[i].cardValue);
                    //Debug.Log(sortbysuit[i-1].cardValue);
                    straightflush = false;
                }
                flush++;
                if (flush >= 5)
                {
                    double temp;
                    temp = 75 + (double)sortbysuit[i].cardValue + (double)sortbysuit[i - 1].cardValue / 100 + (double)sortbysuit[i - 2].cardValue / 10000
                    + (double)sortbysuit[i - 3].cardValue / 1000000 + (double)sortbysuit[i - 4].cardValue / 100000000;
                    if (straightflush)
                    {
                        temp += 45;
                    }
                    //Debug.Log("Flush");
                    if (temp > handStrenght)
                    {
                        handStrenght = temp;
                    }
                }
            }
            else
            {
                flush = 1;
                straightflush = true;
            }
        }
    }

    //Checking for straight combination
    public void StraightCheck(List<Card> sortbyvalue)
    {
        int straight = 1;
        for (int i = 1; i < cards.Count; i++)
        {
            if (sortbyvalue[i].cardValue == sortbyvalue[i - 1].cardValue + 1)
            {
                double temp;
                straight++;

                if (straight >= 5)
                {

                    temp = 60 + (double)sortbyvalue[i].cardValue + (double)sortbyvalue[i - 1].cardValue / 100 + (double)sortbyvalue[i - 2].cardValue / 10000
                        + (double)sortbyvalue[i - 3].cardValue / 1000000 + (double)sortbyvalue[i - 4].cardValue / 100000000;
                    if (temp > handStrenght)
                    {
                        handStrenght = temp;
                    }
                }

            }
            else if (sortbyvalue[i].cardValue != sortbyvalue[i - 1].cardValue)
            {
                straight = 1;
            }
        }
    }

    //Checking for Pair combination
    public void PairCheck(List<Card> sortbyvalue)
    {
        double HighCard = sortbyvalue[sortbyvalue.Count - 1].cardValue;
        for (int i = 1; i < cards.Count; i++)
        {
            if (sortbyvalue[i].cardValue == sortbyvalue[i - 1].cardValue)
            {
                // double HighCard = (double)(sortbyvalue[cards.Count - 1].cardValue) / 100;
                double temp = 15 + sortbyvalue[i].cardValue + HighCard / 100;
                //+ HighCard;
                //Debug.Log("Double");
                if (temp > handStrenght)
                {
                    handStrenght = temp;
                }
            }
        }
    }

    //Checking for ThreeofaKind combination
    public void ThreeofaKind(List<Card> sortbyvalue)
    {
        double HighCard = sortbyvalue[sortbyvalue.Count - 1].cardValue; ;
        for (int i = 2; i < cards.Count; i++)
        {
            if (sortbyvalue[i].cardValue == sortbyvalue[i - 1].cardValue && sortbyvalue[i].cardValue == sortbyvalue[i - 2].cardValue)
            {
                // double HighCard = (double)(sortbyvalue[cards.Count - 1].cardValue) / 100;
                double temp = 45 + sortbyvalue[i].cardValue + HighCard / 100;
                //+ HighCard;
                //Debug.Log("Double");
                if (temp > handStrenght)
                {
                    handStrenght = temp;
                }
            }
        }
    }

    //Checking for FourofaKind combination
    public void FourofaKind(List<Card> sortbyvalue)
    {
        double HighCard = sortbyvalue[sortbyvalue.Count - 1].cardValue; ;
        for (int i = 3; i < cards.Count; i++)
        {
            if (sortbyvalue[i].cardValue == sortbyvalue[i - 1].cardValue && sortbyvalue[i].cardValue == sortbyvalue[i - 2].cardValue && sortbyvalue[i].cardValue == sortbyvalue[i - 3].cardValue)
            {
                // double HighCard = (double)(sortbyvalue[cards.Count - 1].cardValue) / 100;
                double temp = 105 + sortbyvalue[i].cardValue + HighCard / 100;
                //+ HighCard;
                //Debug.Log("Double");
                if (temp > handStrenght)
                {
                    handStrenght = temp;
                }
            }
        }
    }

    //Checking for TwoPairs combination
    public void TwoPairs(List<Card> sortbyvalue)
    {
        int pairCount = 0;
        int index = 0;
        for (int i = 1; i < sortbyvalue.Count; i++)
        {
            if (sortbyvalue[i].cardValue == sortbyvalue[i - 1].cardValue)
            {
                pairCount++;
                index = i;
                i++;
                // double HighCard = (double)(sortbyvalue[cards.Count - 1].cardValue) / 100;                
            }
        }
        if (pairCount >= 2)
        {
            double temp = 30 + sortbyvalue[index].cardValue;
            //+ HighCard;
            //Debug.Log("Double");
            if (temp > handStrenght)
            {
                handStrenght = temp;
            }
        }
    }

    //It should check for Full House combination, but it is currently not working, because it has conflict with player having threeofakind
    public void FullHouse(List<Card> sortbyvalue)
    {
        //    bool threeofakind = false;
        //    bool twoofakind = false;
        //    int index = 0;
        //    int threeindex = 0;
        //    for (int i = 2; i < cards.Count; i++)
        //    {
        //        if (sortbyvalue[i - 1].cardValue == sortbyvalue[i - 2].cardValue)
        //        {

        //            // double HighCard = (double)(sortbyvalue[cards.Count - 1].cardValue) / 100;       
        //            if (sortbyvalue[i - 1].cardValue == sortbyvalue[i].cardValue)
        //            {
        //                threeindex = i;
        //                threeofakind = true;
        //            }
        //            else
        //            {
        //                index = i;
        //                i++;
        //                twoofakind = true;
        //            }
        //        }
        //    }
        //    if (threeofakind == true && twoofakind == true)
        //    {
        //        //Full House
        //        double temp = 90 + sortbyvalue[threeindex].cardValue + sortbyvalue[index].cardValue / 100;

        //        if (temp > handStrenght)
        //        {
        //            handStrenght = temp;
        //        }
        //    }
    }
}
//Table of all possible combinations in Texas hold' em and the points they give
//1.High Card = 0 + value of the card
//2.Pair = 15 + value of the card in the pair
//3.Two Pairs = 30 + value of the card in the higher pair
//4.Three of a Kind = 45 + value of the card from which is the three of a kind
//4.Straight = 60 + value of the highest card in the straight
//5.Flush = 75 + value of the highest card in the flush
//6.Full House = 90 = value of the card in the three of a kind
//7.Four of a Kind = 105 + value of the card in the four of a kind
//8.Straight Flush = 120 + value of the highest card in the straight 
//9.Royal Flush = 135 It is the highest straight flush
