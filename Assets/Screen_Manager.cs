using UnityEngine;
using System.Collections;
using System.IO;
using UnityEngine.UI;


public class Screen_Manager : MonoBehaviour {
 
    public static short resume=0;                         //Defines the state of game..if it is resumed or started over
    //public static int groupnum=1;                          
    public static string savecardsfile;                   //This file contains remaining cards of the group selected
    public static string savescorefile;                   //This file contains the score saved for a group
    public static string newcardsfile;                    //This file contains all the cards for a group
    public Text noremainingcards;                         //Text area for displaying if there are no remaining cards to play in a group

    public void ClickQuit()                               //If Quit Button is clicked
    {
        Application.Quit();
    }

    public void ClickResume()                             //If Resume Button is clicked
    {
        resume = 1;
        Application.LoadLevel("SelectGroupScene");

    }
    public void ClickStart()                              //If Start Button is clicked
    {
        resume = 0;
        Application.LoadLevel("SelectGroupScene");
    }
    public void ReturnToTitle()                             //If ReturnToTitle button is clicked
    {
        Application.LoadLevel("Title_Screen");
    }
    public void ClickHelp()                                 //If Help Button is clicked
    {
        Application.LoadLevel("Instructions");
    }
    public void AlkaliMetalSelect()                                //If the AlkaliMetals group is selected
    {
        //groupnum = 1;
        savecardsfile= "C:\\Newfolder\\AlkaliMetals_remaining.txt";       //Use the appropriate savecardsfile for remaining cards
        if (resume == 1 && new FileInfo(savecardsfile).Length == 0)       //If the game is resumed and there are no remaining cards in the file
        {
            noremainingcards.text = "No Remaining Cards in this group! Start over or select other group.";            //Display appropriate message
        }
        else
        {
            savescorefile = "C:\\Newfolder\\AlkaliMetals_score.txt";       //Use the appropriate savescorefile                                            
            newcardsfile = "C:\\Newfolder\\AlkaliMetals.txt";              //Use the appropriate newcardsfile to get the element details in the selected group
            Application.LoadLevel("main_game");
        }
    }
    public void NobleGasesSelect()
    {
        //groupnum = 3;
        savecardsfile= "C:\\Newfolder\\NobleGases_remaining.txt";
        if (resume==1 && new FileInfo(savecardsfile).Length == 0)
        {
            noremainingcards.text = "No Remaining Cards in this group! Start over or select other group.";
        }
        else
        {
            savescorefile = "C:\\Newfolder\\NobleGases_score.txt";
            newcardsfile = "C:\\Newfolder\\NobleGases.txt";
            Application.LoadLevel("main_game");
        }

        
    }
    public void AlkaliEarthMetalSelect()
    {
        //groupnum = 2;
        savecardsfile = "C:\\Newfolder\\AlkaliEarthMetals_remaining.txt";
        if (resume==1 && new FileInfo(savecardsfile).Length == 0)
        {
            noremainingcards.text = "No Remaining Cards in this group! Start over or select other group.";
        }
        else
        {

            savescorefile = "C:\\Newfolder\\AlkaliEarthMetals_score.txt";
            newcardsfile = "C:\\Newfolder\\AlkaliEarthMetals.txt";
            Application.LoadLevel("main_game");
        }
    }
    public void HalogensSelect()
    {
        //groupnum = 4;
        savecardsfile = "C:\\Newfolder\\Halogens_remaining.txt";
        if (resume==1 && new FileInfo(savecardsfile).Length == 0)
        {
            noremainingcards.text = "No Remaining Cards in this group! Start over or select other group.";
        }
        else
        {

            savescorefile = "C:\\Newfolder\\Halogens_score.txt";
            newcardsfile = "C:\\Newfolder\\Halogens.txt";
            Application.LoadLevel("main_game");
        }
    }
    public void OxygenSelect()
    {
        //groupnum = 5;
        savecardsfile = "C:\\Newfolder\\Oxygen_remaining.txt";
        if (resume ==1 && new FileInfo(savecardsfile).Length == 0)
        {
            noremainingcards.text = "No Remaining Cards in this group! Start over or select other group.";
        }
        else
        {

            savescorefile = "C:\\Newfolder\\Oxygen_score.txt";
            newcardsfile = "C:\\Newfolder\\Oxygen.txt";
            Application.LoadLevel("main_game");
        }
    }
    public void NitrogenSelect()
    {
        //groupnum = 6;
        savecardsfile = "C:\\Newfolder\\Nitrogen_remaining.txt";
        if (resume==1 && new FileInfo(savecardsfile).Length == 0)
        {
            noremainingcards.text = "No Remaining Cards in this group! Start over or select other group.";
        }
        else
        {
            savescorefile = "C:\\Newfolder\\Nitrogen_score.txt";
            newcardsfile = "C:\\Newfolder\\Nitrogen.txt";
            Application.LoadLevel("main_game");
        }
    }
    public void CarbonSelect()
    {
        //groupnum = 7;
        savecardsfile = "C:\\Newfolder\\Carbon_remaining.txt";
        if (resume==1 && new FileInfo(savecardsfile).Length == 0)
        {
            noremainingcards.text = "No Remaining Cards in this group! Start over or select other group.";
        }
        else
        {
            savescorefile = "C:\\Newfolder\\Carbon_score.txt";
            newcardsfile = "C:\\Newfolder\\Carbon.txt";
            Application.LoadLevel("main_game");
        }
    }
    public void BoronSelect()
    {
        //groupnum = 8;
        savecardsfile = "C:\\Newfolder\\Boron_remaining.txt";
        if (resume==1 && new FileInfo(savecardsfile).Length == 0)
        {
            noremainingcards.text = "No Remaining Cards in this group! Start over or select other group.";
        }
        else
        {
            savescorefile = "C:\\Newfolder\\Boron_score.txt";
            newcardsfile = "C:\\Newfolder\\Boron.txt";
            Application.LoadLevel("main_game");
        }
    }
    public void TMRESelect()
    {
        //groupnum = 9;
        savecardsfile = "C:\\Newfolder\\TMRE_remaining.txt";
        if (resume==1 && new FileInfo(savecardsfile).Length == 0)
        {
            noremainingcards.text = "No Remaining Cards in this group! Start over or select other group.";
        }
        else
        {
            savescorefile = "C:\\Newfolder\\TMRE_score.txt";
            newcardsfile = "C:\\Newfolder\\TMRE.txt";
            Application.LoadLevel("main_game");
        }
    }
    public void IonicSelect()
    {
        //groupnum = 10;
        savecardsfile = "C:\\Newfolder\\Ionic_remaining.txt";
        if (resume==1 && new FileInfo(savecardsfile).Length == 0)
        {
            noremainingcards.text = "No Remaining Cards in this group! Start over or select other group.";
        }
        else
        {
            savescorefile = "C:\\Newfolder\\Ionic_score.txt";
            newcardsfile = "C:\\Newfolder\\Ionic.txt";
            Application.LoadLevel("main_game");
        }
    }
}
