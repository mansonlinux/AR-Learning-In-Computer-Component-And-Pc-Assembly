using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


//by Hisyam - Ref - YT - Epi 17.

public class WinPopup : MonoBehaviour
{
    public GameObject winPopup;

    
    void Start()
    {
        winPopup.SetActive(false);        
    }

    private void OnEnable()
    {
        GameEvents.OnBoardCompleted += ShowWinPopup;
    }

    private void OnDisable()
    {
        GameEvents.OnBoardCompleted -= ShowWinPopup;

    }

    private void ShowWinPopup()
    {
        winPopup.SetActive(true);
    }

    public void LoadNextLevel()
    {
        GameEvents.LoadNextLevelMethod();
    }
}
