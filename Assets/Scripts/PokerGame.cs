using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class PokerGame : MonoBehaviour
{
    public Sprite[] cardFaces;
    public GameObject cardPrefab;
    public GameObject dealerButton;

    public GameObject playercardposition1;
    public GameObject playercardposition2;
    public GameObject ai1cardposition1;
    public GameObject ai1cardposition2;
    public GameObject ai2cardposition1;
    public GameObject ai2cardposition2;
    public GameObject ai3cardposition1;
    public GameObject ai3cardposition2;

    public GameObject[] cardsPositions;
    public GameObject[] Dealer_buttonposition;
    public GameObject[] topPos;
    public Card card;
    public AI ai1;
    public AI ai2;
    public AI ai3;
    public Player player;
    public TMP_Text player_chips;
    public TMP_Text AI1_chips;
    public TMP_Text AI2_chips;
    public TMP_Text AI3_chips;
    public TMP_Text potText;
    public TMP_Text endText;
    public int allchecked = 0;

    public static string[] suits = new string[] { "Clubs", "Diamonds", "Hearths", "Spades" };
    public static string[] values = new string[] {  "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K", "A" };


    public List<string>[] Player1cardsposition;
    public List<string>[] Player2cardsposition;
    public List<string>[] Player3cardsposition;

    public bool raise = false;
    public float raise_ammount;
    public int player_left = 4;
    public float pot = 0;
    public Slider slidera;

    public List<Card> deck;

    public int smallblind_pos = 0;
    int smallblind_ammount = 20;

    public bool gameover = false;

    public int gameStage;

    Queue whoisnext = new Queue();

    //Start function
    void Start()
    {
        player.player_chips_ammount = 5000;
        ai1.player_chips_ammount = 5000;
        ai2.player_chips_ammount = 5000;
        ai3.player_chips_ammount = 5000;

        SetupGame();      
    }

    //Game Loop
    IEnumerator GameLoop()
    {
       
        gameStage = 0;
        allchecked = 0;
        while (gameStage < 4)
        {

            Player p = (Player)whoisnext.Dequeue();           
            StartCoroutine(p.PlayerTurn());
            p.CalculateHand();
            p.Playeraction();
            
            yield return new WaitForSeconds(0.5f);
            //    Debug.Log(p.playeraction);
          //  Debug.Log("Array count " + whoisnext.Count);
            if (p.playeraction == -2)
            {
                whoisnext.Enqueue(p);
                for (int i = 0; i < whoisnext.Count - 1; i++)
                {
                    Player temp = (Player)whoisnext.Dequeue();
                    whoisnext.Enqueue(temp);
                }
            }
            else
            {
                if (p.playeraction == 0)
                {
                    allchecked++;
                    p.playeraction = -2;
                    whoisnext.Enqueue(p);
                }
                if (p.playeraction == -1)
                {
                  //  whoisnext.Enqueue(p);
                    allchecked++;
                    
                }
                if(p.playeraction == 3)
                {
                    allchecked = 1;
                    whoisnext.Enqueue(p);
                }
            }
                if (allchecked == player_left)
                {
                    allchecked = 0;
                    Phase(gameStage);
                    gameStage++;
                // Debug.Log("gs" + gameStage + "players left : " + player_left);
              //  Debug.Log("I got here");
                }

            if (player_left <= 1)
            {
                gameover = true;
            }

        }
    }

    // Update is called once per frame
    void Update()
    {
        //Responsible for displaying player and AI chips
        player_chips.text = player.player_chips_ammount.ToString() + " £";
        AI1_chips.text = ai1.player_chips_ammount.ToString() + " £";
        AI2_chips.text = ai2.player_chips_ammount.ToString() + " £";
        AI3_chips.text = ai3.player_chips_ammount.ToString() + " £";
        potText.text = "Current pot: " + pot.ToString() + "£";


    }

    //Deals cards to the player and the AI
    public void DealCardsToPlayers()
    {
        //Generates the deck
        deck = GenerateDeck();
        //Shuffles the deck
        Shuffle(deck);
        // OrderedDeck(deck);
        //Deals cards to the Player and the AI       
        player.DealHand(1);
        ai1.DealHand(2);
        ai2.DealHand(3);
        ai3.DealHand(4);
    }    

    //Shuffles the deck with cards randomly
    void Shuffle<T>(List<T> list)
    {
        //Reference required.
        System.Random random = new System.Random();
        int n = list.Count;
        while(n>1)
        {
            int k = random.Next(n);
            n--;
            T temp = list[k];
            list[k] = list[n];
            list[n] = temp;
        }
    }

    //Generates ordered Deck of cards / testing purposes only
    void OrderedDeck(List <Card> list)
    {
        list[0] = new Card(0, 5);
        list[1] = new Card(0, 9);
        list[2] = new Card(1, 2);
        list[3] = new Card(2, 13);
        list[4] = new Card(2, 6);
        list[5] = new Card(0, 6);
        list[6] = new Card(0, 11);
        list[7] = new Card(2, 7);
        list[8] = new Card(1, 6);
        list[9] = new Card(0, 7);
        list[10] = new Card(1, 3);
        list[11] = new Card(0, 8);
        list[12] = new Card(3, 6);
    }

    //Generates Deck of cards
    public static List<Card> GenerateDeck()
    {
        List<Card> newDeck = new List<Card>();
        for (int i = 0; i<4; i++)
        {
            for (int j = 2; j<15; j++)
            {
                
                newDeck.Add(new Card(i,j));
            }
        }
        return newDeck;
    }

    //Deals 3 cards for the Flop
    void FlopDeal()
    {
        for (int i = 0; i < 3; i++)
        {
            GameObject newCard = Instantiate(cardPrefab, new Vector3(cardsPositions[i].transform.position.x, cardsPositions[i].transform.position.y, cardsPositions[i].transform.position.z), Quaternion.identity);
            newCard.name = CardDecode(deck[0]);
            player.cards.Add(deck[0]);
            ai1.cards.Add(deck[0]);
            ai2.cards.Add(deck[0]);
            ai3.cards.Add(deck[0]);
            deck.RemoveAt(0);
            newCard.GetComponent<Selectable>().faceUp = true;
            newCard.gameObject.tag = "card";
        }
    } 

    //Deals 1 card for the Turn
    void TurnDeal()
    {
        GameObject turn = Instantiate(cardPrefab, new Vector3(cardsPositions[3].transform.position.x, cardsPositions[3].transform.position.y, cardsPositions[3].transform.position.z), Quaternion.identity);
        turn.name = CardDecode(deck[0]);
        player.cards.Add(deck[0]);
        ai1.cards.Add(deck[0]);
        ai2.cards.Add(deck[0]);
        ai3.cards.Add(deck[0]);
        deck.RemoveAt(0);
        turn.GetComponent<Selectable>().faceUp = true;
        turn.gameObject.tag = "card";
    }

    ////Deals 1 card for the River
    void RiverDeal()
    {
        GameObject river = Instantiate(cardPrefab, new Vector3(cardsPositions[4].transform.position.x, cardsPositions[4].transform.position.y, cardsPositions[4].transform.position.z), Quaternion.identity);
        river.name = CardDecode(deck[0]);
        player.cards.Add(deck[0]);
        ai1.cards.Add(deck[0]);
        ai2.cards.Add(deck[0]);
        ai3.cards.Add(deck[0]);
        deck.RemoveAt(0);
        river.GetComponent<Selectable>().faceUp = true;
        river.gameObject.tag = "card";

        //  CalculateWinner();
    }   

    //Decoding the cards so 
    public string CardDecode(Card c)
    {
        string name = "";
        switch(c.cardSuit)
        {
            case 0:
                name = "CLUBS";
                break;
            case 1:
                name = "DIAMONDS";
                break;
            case 2:
                name = "HEARTS";
                break;
            case 3:
                name = "SPADES";
                break;               
        }
           name += c.cardValue.ToString();
        return name;
    }
    //Calculates who is the winner after the game is over.
    void CalculateWinner()
    {
        if(player.handStrenght > ai1.handStrenght && player.handStrenght > ai2.handStrenght && player.handStrenght > ai3.handStrenght && player.fold != true)
        {
            EndText("Player WINS ! ");
            player.player_chips_ammount += pot;         
        }
        if (ai1.handStrenght > ai2.handStrenght && ai1.handStrenght > ai3.handStrenght && ai1.handStrenght > player.handStrenght && ai1.fold !=true)
        {
            ai1.AIcard1.GetComponent<Selectable>().faceUp = true;
            ai1.AIcard2.GetComponent<Selectable>().faceUp = true;
            EndText("AI1 WINS ! ");           
            ai1.player_chips_ammount += pot;
        }
        if(ai2.handStrenght > ai1.handStrenght && ai2.handStrenght > ai3.handStrenght && ai2.handStrenght > player.handStrenght && ai2.fold != true)
        {
            ai2.AIcard1.GetComponent<Selectable>().faceUp = true;
            ai2.AIcard2.GetComponent<Selectable>().faceUp = true;
            EndText("AI2 WINS ! " );
            ai2.player_chips_ammount += pot;
        }
        if(ai3.handStrenght > ai2.handStrenght && ai3.handStrenght > ai1.handStrenght && ai3.handStrenght > player.handStrenght && ai3.fold != true)
        {
            ai3.AIcard1.GetComponent<Selectable>().faceUp = true;
            ai3.AIcard2.GetComponent<Selectable>().faceUp = true;
            EndText("AI3 WINS ! ");
            ai3.player_chips_ammount += pot;
        }      
        if(player.fold == false && ai1.fold == true && ai2.fold == true && ai3.fold ==true)
        {
            EndText("Player WINS ! ");
            player.player_chips_ammount += pot;
        }
        if (player.fold == true && ai1.fold == false && ai2.fold == true && ai3.fold == true)
        {
            EndText("AI1 WINS! ");
            ai1.player_chips_ammount += pot;
        }
        if (player.fold == true && ai1.fold == true && ai2.fold == false && ai3.fold == true)
        {
            EndText("AI2 WINS! ");
            ai2.player_chips_ammount += pot;
        }
        if (player.fold == true && ai1.fold == true && ai2.fold == true && ai3.fold == false)
        {
            ai3.player_chips_ammount += pot;
            EndText("AI3 WINS! ");
        }
    }

    //Changes between the phases in one round
    void Phase(int phase)
    {
        if(phase == 0)
        {
            raise = false;
            FlopDeal();
          //  Debug.Log("Flop");
            
        }
        if(phase == 1 && gameover == false)
        {
            allchecked = 0;
            raise = false;
            slidera.value = 0;
            raise_ammount = 0;
            TurnDeal();
         //   Debug.Log("Turn");
        }
        if(phase == 2 && gameover == false)
        {
            allchecked = 0;
            slidera.value = 0;
            raise_ammount = 0;
            raise = false;
            RiverDeal();
           // Debug.Log("River");
        }
        if (phase == 3)
        {
            allchecked = 0;
            slidera.value = 0;
            raise_ammount = 0;
            raise = false;
           // Debug.Log("End of round");
            player.CalculateHand();
            ai1.CalculateHand();
            ai2.CalculateHand();
            ai3.CalculateHand();
            CalculateWinner();
           
            gameStage = 4;
        }
        
    }

    //Setting up the game and reseting everything once a new rounds beggins
    public void SetupGame()
    {
        slidera.value = 0;
        raise = false;
        raise_ammount = 0;
        gameStage = 0;
        gameover = false;
        pot = 0;
        player_left = 4;
        player.Reset();
        ai1.Reset();
        ai2.Reset();
        ai3.Reset();
        DestroyCards();
        DealCardsToPlayers();
        smallblind_pos++;

        if(smallblind_pos == 4)
        {
            smallblind_pos = 0;
        }
        Dealerbutton();
        whoisnext.Clear();
        if(player.player_chips_ammount > 0)
        whoisnext.Enqueue(player);
        if (ai3.player_chips_ammount > 0)
            whoisnext.Enqueue(ai3);
        if (ai1.player_chips_ammount > 0)
            whoisnext.Enqueue(ai1);
        if (ai2.player_chips_ammount > 0)
            whoisnext.Enqueue(ai2);
        Blinds();
        StartCoroutine(GameLoop());       
    }

    //Destroying all the cards before the start of new round
    public void DestroyCards()
    {
        GameObject[] cards = GameObject.FindGameObjectsWithTag("card");
        foreach (GameObject card in cards)
            GameObject.Destroy(card);
    }

    //Changing big and small blinds, in the different rounds
    public void Blinds()
    {
        for (int i = 0; i < smallblind_pos; i++)
        {
            Player temp = (Player)whoisnext.Dequeue();
            whoisnext.Enqueue(temp);
        }
        Player sb = (Player)whoisnext.Dequeue();
        sb.player_chips_ammount -= smallblind_ammount;
        pot += smallblind_ammount;
        whoisnext.Enqueue(sb);

        Player bb = (Player)whoisnext.Dequeue();
        bb.player_chips_ammount -= smallblind_ammount * 2;
        pot += smallblind_ammount * 2;
        whoisnext.Enqueue(bb);

    }

    //Changing the dealer button every round
    public void Dealerbutton()
    {
        switch (smallblind_pos)
        {
            case 0:
                dealerButton.transform.position = Dealer_buttonposition[3].transform.position;
                break;
            case 1:
                dealerButton.transform.position = Dealer_buttonposition[0].transform.position;
                break;
            case 2:
                dealerButton.transform.position = Dealer_buttonposition[1].transform.position;
                break;
            case 3:
                dealerButton.transform.position = Dealer_buttonposition[2].transform.position;
                break;
        }
    }
    public void EndText(string endingtext)
    {

        float Destroytime = 10f;
        TMP_Text wintext = Instantiate(endText, transform.position, Quaternion.identity, transform);
        wintext.GetComponent<TMP_Text>().text = endingtext;
        Destroy(wintext, Destroytime);
    }
}
