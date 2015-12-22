using UnityEngine;
using System.Collections;

public class Element_Script : MonoBehaviour {

    public string elemName;
    public string atomNo;
    public string symbol;
    public string hint;
    public int pointValue = 5;

    // Use this for initialization
    void Start () {
        Debug.Log("Entered ELement");
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public Element_Script()
    {
        //Debug.Log ("BASIC DEFAULT of element constructor");
    }

    public Element_Script(string name, string atomNum, string rep, string clue) // This is the constructor that initializes values for an element
    {
        elemName = name;
        atomNo = atomNum;
        symbol = rep;
        hint = clue;

    }
    // Below are the getter and setter functions for each element field

    public string getName()
    {
        return elemName;
    }

    public string getAtomNo()
    {
        return atomNo;
    }

    public string getSymbol()
    {
        return symbol;
    }

    public string getHint()
    {
        Debug.Log(hint);
        return hint;
    }


    public void setPointValue(int newVal)
    {
        pointValue = newVal;
    }

    public int getPointValue()
    {
        return pointValue;
    }
}
