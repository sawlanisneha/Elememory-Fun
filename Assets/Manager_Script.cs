using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.IO;
using UnityEditor;

public class Manager_Script : MonoBehaviour
{

    public Text symbol_display;                        // Text field for symbol display
    public GameObject deck;                           //deck 
    public Element_Script currElement;               // current element being considered in game
    public Text answer;                              //Input Text field for answer
    public Text scoreStr;                                //Text field for score
    public static double score=0;
    public Text hint_display;                         //Text field for hint_display
    public Sprite frontImage;                          //Front image of the card
    public Sprite backImage;                           //Back image of the card
    public SpriteRenderer spriteRenderer;              //SpriteRenderer for placeholder card
    public float duration = 0.9f;                      //Duration for flipping card
    public AnimationCurve scaleCurve;                  //Animation curve for flipping card
    public GameObject placeHolderCard;
    public InputField answer_field;                   //answer input field
    public Sprite hintImage;   
    public SpriteRenderer hintRenderer;
    public Sprite youWin;
    //public AudioClip backgroundSound;
    public AudioSource source1;
    public AudioClip success;
    public AudioClip fail;
    public AudioClip youwin;
    public SpriteRenderer youwinRenderer;
    private bool stable;                                     //This variable is used to ensure proper timing between different cards
    public Text correct_spelling;
    string scorestr;                                         //String that displays the score


    // Use this for initialization
    void Start()
    {
        //Debug.Log("Resume "+Screen_Manager.resume);
        source1 = GetComponent<AudioSource>();
        score = 0;
        if(Screen_Manager.resume==1)                                        //If the game is resumed
        {
            StreamReader sr = new StreamReader(Screen_Manager.savescorefile);            //Read the saved score of the previous session
            score = System.Convert.ToDouble(sr.ReadLine());                              //
            //Debug.Log("score "+score);
            sr.Close();

            Display_Score();
        }
        
        //Debug.Log("Entered Manager");
        hintRenderer.enabled = false;                                   //We want to show hint image only when hint button clicked
        deck.GetComponent<Deck_Script>().Show_Deck();                   //Show the deck of cards on the upper right corner of the screen
        deck.GetComponent<Deck_Script>().Show_Deck_Left();             //Show the deck of cards already played on the top left corner of the screen
        currElement = deck.GetComponent<Deck_Script>().Get_Card(0);    //Get a card from the deck
        inputFocus();                                                  //Input focus on the answer field
        ForEachElement();                                              //Does the job of moving, flipping, and displaying Symbol for each card(or element)


    }

    void ForEachElement()
    {
        deck.GetComponent<Deck_Script>().Move_Card();          //Move the card from the deck to the Placeholder position(moves the actual card)
        deck.GetComponent<Deck_Script>().FlipCard();          //Move the card from the deck to the Placeholder position(adjust the spriteRenderer)

        StartCoroutine(placeHolderCard.GetComponent<CardFlipper>().Wait());  //It will flip the card with appropriate timing
        
        StartCoroutine(Wait());                                //It will display symbol after waiting for the card to flip completely
        stable = true;                                         //With stable true it is allowed to check answer
    }

   

    public void Display_Symbol()                                            //Display symbol over the card
    {
        //Debug.Log("In Symbol Display");
        currElement = deck.GetComponent<Deck_Script>().Get_Card(0);        //Get the first card
        symbol_display.text = currElement.getSymbol();                     //Display the symbol in the symbol area
    }

    public void Remove_Symbol()
    {
        symbol_display.text = " ";                                    //Remove Symbol after the card goes
    }

    public void Display_Score()
    {
        scoreStr.text = "Score: "+score.ToString();
    }

    public void Check_Answer()
    {
        if (stable)                                                  //if its the right time to check the answer
        {
            string name = currElement.getName();                     //gets the name of current element
            name = name.ToLower();
            if (Input.GetButtonDown("Submit"))
            {
                double percentageSimilar = ComputePercentageSimilarity(answer.text.ToLower(), name);        //Check how correct is the spelling
                if (percentageSimilar >= 0.6)                   // checks if the spelling is atleast 60% accurate
                {
                    source1.PlayOneShot(success, 1f);
                    Change_Score(percentageSimilar*5);                                              //Increment score by percent correct of 5
                    //Save_Score();                                                                   //Saves the score to a file
                    if (percentageSimilar != 1)
                        correct_spelling.text = "Close enough: Correct Spelling is " + name;
                    else
                        correct_spelling.text = "Perfect!!!";
                    deck.GetComponent<Deck_Script>().Remove_Card();              // Remove the card from the deck of original cards

                    if (deck.GetComponent<Deck_Script>().isEmpty())             //If the deck is empty, the user has won the game
                    {
                        //Debug.Log("You win");
                        source1.Play();
                        youwinRenderer.sprite = youWin;                          //Display you win image
                        StartCoroutine(WaitBeforeTitleScreen());
                    }
                    else
                    {
                        Remove_Answer();                                           //Empty the input field
                        Remove_Hint();                                            //Removes the Hint for that element from the screen

                        //Remove_Symbol();
                        StartCoroutine(placeHolderCard.GetComponent<CardFlipper>().Wait());
                        StartCoroutine(WaitBeforeMovingLeft(1.5f));
                        //deck.GetComponent<Deck_Script>().Move_Card_Left();       //Move the card to the top left corner
                        //StartCoroutine(WaitBeforeNextCard(2f));                 //ForEachElement() will be called after waiting for 2 seconds
                    }
                }
                else
                {
                    source1.PlayOneShot(fail, 1f);
                    Change_Score(-2);                                       //Decrement score by 2 if the answer is wrong
                    //Save_Score();                                           //Saves the score to a file
                    correct_spelling.text = "Wrong: Correct Answer is " + name;
                    deck.GetComponent<Deck_Script>().Remove_Card();        //Removes the card from the front of the deck
                    deck.GetComponent<Deck_Script>().Add_Card();           //Places that card at the end of the deck
                    Remove_Answer();                                          //empty input field
                    Remove_Hint();                                            //Removes the Hint for that element from the screen
                    StartCoroutine(placeHolderCard.GetComponent<CardFlipper>().Wait());
                    StartCoroutine(WaitBeforeMovingRight(1.5f));

                }
                

                currElement = deck.GetComponent<Deck_Script>().Get_Card(0);
                stable = false;                                                    //will not check answer until next card is loaded


            }
        }
        inputFocus();
    }

    public void Change_Score(double num)
    {
        score += num;                                           // Modifies score
        score = Mathf.Round((float)score);
        Display_Score();
    }

    void Save_Score()
    {
        try {

            StreamWriter sw = new StreamWriter(Screen_Manager.savescorefile); //Opening the file for saving score
            sw.WriteLine(score);        //Write the score in that file
            sw.Close();                 //Close the file
        }
        catch(System.Exception e)
        {
            Debug.Log("Exception: " + e.Message);
        }

    }

    public void Save_Cards()
    {
        Save_Score();
        StreamWriter cards_data = new StreamWriter(Screen_Manager.savecardsfile);            //Open the file for saving the remaining cards
        List<Element_Script> remaining_cards = deck.GetComponent<Deck_Script>().elemArray;   
        foreach (Element_Script element in remaining_cards)
        {
            cards_data.Write(element.elemName);
            cards_data.Write('\t');
            cards_data.Write(element.atomNo);
            cards_data.Write('\t');
            cards_data.Write(element.symbol);
            cards_data.Write('\t');
            cards_data.Write(element.hint);

            cards_data.WriteLine();

        }
        cards_data.Close();
        
    }

    public void Show_Hint()
    {
        
            hintRenderer.enabled = true;
            hint_display.text = currElement.getHint();                //Show the hint in the hint text_field
    }

    public void Remove_Hint()
    {
        hintRenderer.enabled = false;
        hint_display.text = " ";                                //Remove the hint from the hint text field
    }

    public void Remove_Answer()
    {
        //Debug.Log("in remove answer");
        answer_field.text = "";
       
    }
    public void inputFocus()                                       //This function will put the focus on the inputfield
    {
        answer_field.Select();
        answer_field.ActivateInputField();                          
    }
    public void Quit()
    {
        //Save_Cards();
        bool quit=EditorUtility.DisplayDialog("Quit Application", "Are you sure you want to quit?", "Yes", "No");
        if(quit)
          Application.Quit();
    }


   
   

    IEnumerator Wait()
    {
        Debug.Log("In Wait");
        yield return new WaitForSeconds(3.5f);
        stable = true;
        Display_Symbol();
    }
    IEnumerator WaitBeforeNextCard(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        deck.GetComponent<Deck_Script>().Move_Card();          //Move card from the deck to the center of the screen(moves actual card)
        deck.GetComponent<Deck_Script>().FlipCard();          //Move the card from the deck to the Placeholder position(adjust the spriteRenderer)

        StartCoroutine(placeHolderCard.GetComponent<CardFlipper>().Wait());             //Flips card with appropriate timing
        StartCoroutine(Wait());                                                         //Displays symbol after the card has been completely flipped                                                            //With stable true it is allowed to check answer
    }
    IEnumerator WaitBeforeMovingLeft(float seconds)
    {
        yield return new WaitForSeconds(1.5f);
        Remove_Symbol();
        yield return new WaitForSeconds(seconds);
        //Remove_Symbol();
        deck.GetComponent<Deck_Script>().Move_Card_Left();       //Move the card to the top left corner
        StartCoroutine(WaitBeforeNextCard(2f));      
    }

    IEnumerator WaitBeforeMovingRight(float seconds)
    {
        yield return new WaitForSeconds(1.5f);
        Remove_Symbol();
        yield return new WaitForSeconds(seconds);
        //Remove_Symbol();
        deck.GetComponent<Deck_Script>().Move_Card_Right();       //Move the card to the top left corner
        StartCoroutine(WaitBeforeNextCard(2f));
    }
    IEnumerator WaitBeforeTitleScreen()
    {
        yield return new WaitForSeconds(2f);
        Application.LoadLevel("Title_screen");
    }
    double ComputePercentageSimilarity(string source, string target)
    {
        if (source == null || target == null) return 0.0;                    //if any of the string is empty return 0
        if (source.Length == 0.0 || target.Length == 0.0) return 0.0;
        if (source == target) return 1.0;
        //Debug.Log("hi");
        int stepstosame = ComputeMatch(source, target);
        return (1.0 - ((double)stepstosame / (double)Mathf.Max(source.Length, target.Length)));
    }

    int ComputeMatch(string source, string target)
    {                                                           //implementing Levenshtein algorithm for string comparison
        int cost = 0;

        int sourcewordcount = source.Length;
        int targetwordcount = target.Length;

        if (sourcewordcount == 0)
            return targetwordcount;
        if (targetwordcount == 0)
            return sourcewordcount;

        int[,] distance = new int[sourcewordcount + 1, targetwordcount + 1];

        for (int i = 0; i <= sourcewordcount; distance[i, 0] = i++) ;
        for (int j = 0; j <= targetwordcount; distance[0, j] = j++) ;

        for (int i = 1; i <= sourcewordcount; i++)
        {
            for (int j = 1; j <= targetwordcount; j++)
            {
                cost = (target[j - 1] == source[i - 1]) ? 0 : 1;
                // Debug.Log(cost);
                distance[i, j] = Mathf.Min(Mathf.Min(distance[i - 1, j] + 1, distance[i, j - 1] + 1), distance[i - 1, j - 1] + cost);
            }
        }
       // Debug.Log(distance[sourcewordcount, targetwordcount]);
        return distance[sourcewordcount, targetwordcount];

    }
}



