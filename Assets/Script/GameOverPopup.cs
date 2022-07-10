using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//by Hisyam - Ref - YT - Epi 19(excluding gameAds).

public class GameOverPopup : MonoBehaviour
{
    public GameObject gameOverPopup;
    //public GameObject continueGameAfterAdsButton; //Not necessary ads lol 

    void Start()
    {
        //continueGameAfterAdsButton.GetComponent<Button>().interactable = false;
        gameOverPopup.SetActive(false);

        GameEvents.OnGameOver += ShowGameOverPopup;
    }

    private void OnDisable()
    {
        GameEvents.OnGameOver -= ShowGameOverPopup;
    }

    private void ShowGameOverPopup()
    {
        gameOverPopup.SetActive(true);
        //continueGameAfterAdsButton.GetComponent<Button>().interactable = false;
    }

}
