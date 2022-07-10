using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//by Hisyam - Ref - YT - Epi 13.

public class SearchingWord : MonoBehaviour
{
    public Text displayedText;
    public Image crossLine;

    private string _word;

    void Start()
    {
        
    }

    //Assigning correct word function to the newly created event
    private void OnEnable()
    {
        GameEvents.OnCorrectWord += CorrectWord;
    }

    //Unsubscribe from correct word event
    private void OnDisable() 
    {
        GameEvents.OnCorrectWord -= CorrectWord;   
    }

    public void SetWord(string word)
    {
        _word = word;
        displayedText.text = word;
    }

    //To pass the list of the all of the squares which select 
    private void CorrectWord(string word, List<int> squareIndexes)
    {
        //if word is correct then it will cross the words
        if(word == _word)
        {
            crossLine.gameObject.SetActive(true);
        }
    }
}
