using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Deck : MonoBehaviour {

    public List<int> cards;
    public float cardOffset=0.2f;
    Vector3 start= new Vector3(200f,200f,0f);
    public Sprite s1;
    public GameObject cardPrefab;
    //public List<GameObject> cards2;
    public List<GameObject> cardCopy;
    public Image cardPrefab2;
    public List<Image> cardCopy2;
    public GameObject PlaceHolderSprite;

    public IEnumerable<int> GetCards()
    {
        foreach (int i in cards)
            yield return i;
    }

    

	// Use this for initialization
	void Start () {
        ShowCards();
        MoveCards();
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public void ShowCards()
    {
        int cardCount = 0;
        foreach(int i in GetCards())
        {
            float co = cardOffset * cardCount;
            Vector3 temp = start + new Vector3(co, 0f);
            cardCopy.Add((GameObject)Instantiate(cardPrefab));
            //cardCopy[cardCount].Rect
            //cards2[cardCount] = cardCopy;
            
            //cardCopy2.Add((Image)Instantiate(cardPrefab2));
            cardCopy[cardCount].transform.position= start + temp;
            Debug.Log(cardCount);
            //
            cardCount++;






        }
        //MoveCards();
    }
    public void MoveCards()
    {
        Debug.Log("In Move Cards");
        iTween.MoveTo(cardCopy[0], iTween.Hash("position", new Vector3(15f, 402f, 0f), "easetype", iTween.EaseType.easeInOutSine, "time", 1f));

    }
    
}
