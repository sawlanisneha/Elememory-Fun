using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.IO;


public class Deck_Script : Element_Script
{
    public List<Element_Script> elemArray = new List<Element_Script>();
    public Element_Script currElement;
    public GameObject manager;
    public List<int> cards;                                                 // used to store the index of cards
    public float cardOffset = 5f;                                         // used for distance between cards in a deck
    Vector3 start = new Vector3(300f, 180f, 0f);                            // Position of the first element of deck
    Vector3 center = new Vector3(-9f, 324f, 0f);                             //Position of the center current card
    Vector3 topleft = new Vector3(-605f, 360f, 0f);                          //Position of top left cards(already played in the game)
    Vector3 topright = new Vector3(605f, 360f, 0f);                          //Position of top right cards(remaining cards in the game)
    public GameObject cardPrefab;                                           //Holds the backside of card(shown in deck)
    public List<GameObject> cardCopy;                                       //contains list of cards in deck    
    public List<GameObject> cardCopy2;         
    public GameObject PlaceHolderSprite;
    private int i = 0;
    int numofcards = 0;



    // Use this for initialization
    void Awake()     //Set the Deck before the game starts
    {
        Debug.Log("Entered Deck");

        if (Screen_Manager.resume == 1)                    //If we are resuming, read the data from the savecardsfile
        {
            StreamReader sr = new StreamReader(Screen_Manager.savecardsfile);
            string Element = sr.ReadLine();

            Debug.Log(Element);
            while (Element != null)
            {
                numofcards++;                                            //This variable is used in Show_Deck_Left function
                string[] members = Element.Split('\t');
                Element_Script test = new Element_Script(members[0], members[1], members[2], members[3]);
                Debug.Log(members[0]);
                elemArray.Add(test);
                Element = sr.ReadLine();
            }
            reshuffle(elemArray);
        }
        else
        {                                                                //if game is started over, read data from newcardsfile
            StreamReader sr;
            sr = new StreamReader(Screen_Manager.newcardsfile);

            string Element = sr.ReadLine();
            while (Element != null)
            {
                numofcards++;
                string[] members = Element.Split('\t');
                Element_Script test = new Element_Script(members[0], members[1], members[2], members[3]);
                elemArray.Add(test);
                Element = sr.ReadLine();
            }
            reshuffle(elemArray);


        }

    }

    // Update is called once per frame
    void Update() {

    }
    public void reshuffle(List<Element_Script> list)  //shuffle the elements
    {
        for (int i = 0; i < elemArray.Count; i++) //HARD CODED FOR 6
        {
            Element_Script temp = list[i];
            int r = Random.Range(i, 6);
            list[i] = list[r];
            list[r] = temp;
        } // http://forum.unity3d.com/threads/randomize-array-in-c.86871/ (10/13/2015)
    }

    /*public Element_Script indexAt(int num)  //Returns the element at index num
    {
        
    }*/

    public Element_Script Get_Card(int num)
    {
        currElement = elemArray[num]; //sets the current element to be shown in the game
        return currElement;   //returns that element
    }
    public void Add_Card()
    {
        elemArray.Add(currElement);  //adds an element to the deck
    }

    public void Remove_Card()
    {
        elemArray.Remove(currElement);  //removes an element from the deck
    }

    public bool isEmpty()  //checks if the deck has gone empty
    {
        if (elemArray.Count == 0)
            return true;
        else
            return false;
    }

    public void Show_Deck()
    {
        int cardCount = 0;
        for(int i=0;i<elemArray.Count;i++)
        {
            float co = cardOffset * cardCount;                  //Set the Offset for each card in deck
            Vector3 temp = start + new Vector3(co, 0f);         
            cardCopy.Add((GameObject)Instantiate(cardPrefab));     //Add the card in the deck
            cardCopy[cardCount].transform.position = start+ temp;   //Set the position of the card in deck
            cardCount++;                                              //Increment cardCount to include the next card in the deck
        }
     }

    public void Show_Deck_Left()
    {
        int cardCount = 0;
        numofcards=(int)Mathf.Abs((float)(numofcards- File.ReadAllLines(Screen_Manager.newcardsfile).Length));
        
        for(int i=0;i<numofcards;i++)
        {
            float co = cardOffset * cardCount;
            Vector3 temp = topleft + new Vector3(co, 0f);
            cardCopy2.Add((GameObject)Instantiate(cardPrefab));
            cardCopy2[cardCount].transform.position = temp;
            cardCount++;

        }

    }

    public void Move_Card()
    {
        Debug.Log("In Move Cards"+i);
        iTween.MoveTo(cardCopy[i], iTween.Hash("position", center, "easetype", iTween.EaseType.easeInOutSine, "time", 1f));

    }

    public void FlipCard()
    {
        PlaceHolderSprite.GetComponent<CardFlipper>().spriteRenderer = cardCopy[i].GetComponent<SpriteRenderer>();
        //PlaceHolderSprite.GetComponent<CardFlipper>().back = cardCopy[i].GetComponent<SpriteRenderer>().sprite;
        //PlaceHolderSprite.GetComponent<CardFlipper>().front = cardCopy[0].GetComponent<SpriteRenderer>().sprite;
        
 
    }
    public void Move_Card_Left()                                                    
    {
        Debug.Log("In Move Cards left"+cardCopy[i]);
        iTween.MoveTo(cardCopy[i], iTween.Hash("position", topleft, "easetype", iTween.EaseType.easeInOutSine, "time", 1f));
        i++;

    }
    public void Move_Card_Right()
    {
        Debug.Log("In Move cards right"+cardCopy[i]);
        iTween.MoveTo(cardCopy[i], iTween.Hash("position", topright, "easetype", iTween.EaseType.easeInOutSine, "time", 1f));
    }
}


