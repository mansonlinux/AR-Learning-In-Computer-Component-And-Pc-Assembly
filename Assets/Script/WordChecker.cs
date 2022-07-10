using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//by Hisyam - Ref - YT - Epi 10,11,13(trigger OnCorrectWord), 17, 26(fixing game bug).

public class WordChecker : MonoBehaviour
{
    public GameData currentGameData;
    public GameLevelData gameLevelData;
    
    private string _word;

    //Followed by Epi 11 - This code is to make 6 rays on each direction selected letter in wordsearch game.
    private int _assignedPoints = 0;
    private int _completedWords = 0;
    private Ray _rayUp, _rayDown;
    private Ray _rayLeft, _rayRight;
    private Ray _rayDiagonalLeftUp, _rayDiagonalLeftDown;
    private Ray _rayDiagonalRightUp, _rayDiagonalRightDown;
    private Ray _currentRay = new Ray();
    private Vector3 _rayStartPosition;
    private List<int> _correctSquareList = new List<int>();
    
    //********************************************************
    private void OnEnable()
    {
        GameEvents.OnCheckSquare += SquareSelected;
        GameEvents.OnClearSelection += ClearSelection;
        GameEvents.OnLoadNextLevel += LoadNextGameLevel;
    }

    private void OnDisable()
    {
        GameEvents.OnCheckSquare -= SquareSelected;
        GameEvents.OnClearSelection -= ClearSelection;
        GameEvents.OnLoadNextLevel -= LoadNextGameLevel;
    }

    private void LoadNextGameLevel()
    {
        SceneManager.LoadScene("GameScene");
    }

    void Start()
    {
        currentGameData.selectedBoardData.ClearData();
        _assignedPoints = 0;
        _completedWords = 0;
    }

    void Update()
    {
        if(_assignedPoints > 0 && Application.isEditor)
        {
            Debug.DrawRay(_rayUp.origin, _rayUp.direction * 4);
            Debug.DrawRay(_rayDown.origin, _rayDown.direction * 4);
            Debug.DrawRay(_rayLeft.origin, _rayLeft.direction * 4);
            Debug.DrawRay(_rayRight.origin, _rayRight.direction * 4);
            Debug.DrawRay(_rayDiagonalLeftUp.origin, _rayDiagonalLeftUp.direction * 4);
            Debug.DrawRay(_rayDiagonalLeftDown.origin, _rayDiagonalLeftDown.direction * 4);
            Debug.DrawRay(_rayDiagonalRightUp.origin, _rayDiagonalRightUp.direction * 4);
            Debug.DrawRay(_rayDiagonalRightDown.origin, _rayDiagonalRightDown.direction * 4);
        }
    }

    //This function subscribing function from GameEvents.
    private void SquareSelected(string letter, Vector3 squarePosition, int SquareIndex)
    {
        if(_assignedPoints == 0)
        {
            _rayStartPosition = squarePosition;
            _correctSquareList.Add(SquareIndex);
            _word += letter;
            
            _rayUp = new Ray(new Vector2(squarePosition.x, squarePosition.y), new Vector2(0f,1));
            _rayDown = new Ray(new Vector2(squarePosition.x, squarePosition.y), new Vector2(0f, -1));
            _rayLeft = new Ray(new Vector2(squarePosition.x, squarePosition.y), new Vector2(-1, 0f));
            _rayRight = new Ray(new Vector2(squarePosition.x, squarePosition.y), new Vector2(1, 0f));
            _rayDiagonalLeftUp = new Ray(new Vector2(squarePosition.x, squarePosition.y), new Vector2( -1, 1));
            _rayDiagonalLeftDown = new Ray(new Vector2(squarePosition.x, squarePosition.y), new Vector2( -1, -1));
            _rayDiagonalRightUp = new Ray(new Vector2(squarePosition.x, squarePosition.y), new Vector2( 1, 1));
            _rayDiagonalRightDown = new Ray(new Vector2(squarePosition.x, squarePosition.y), new Vector2( 1, -1));

        }
        else if(_assignedPoints == 1)
        {
            _correctSquareList.Add(SquareIndex);
            _currentRay = SelectRay(_rayStartPosition, squarePosition);
            GameEvents.SelectSquareMethod(squarePosition);
            _word += letter;
            CheckWord();
        }
        else
        {
            if(IsPointOnTheRay(_currentRay, squarePosition))
            {
                _correctSquareList.Add(SquareIndex);
                GameEvents.SelectSquareMethod(squarePosition);
                _word += letter;
                CheckWord();
            }
        }

        _assignedPoints++;


    }

    //This function will be called from square selected.
    //Triggering OnCorrectWord event from episode 13 YT.

    //possibily can make score through this code! - 28 mac 2022
    private void CheckWord()
    {
        foreach (var searchingWord in currentGameData.selectedBoardData.SearchWords)
        {
            if(_word == searchingWord.Word && searchingWord.Found == false)
            {
                searchingWord.Found = true;
                GameEvents.CorrectWordMethod(_word,_correctSquareList);
                _completedWords++; //added after fixed some issue in epi 17.
                _word = string.Empty;
                _correctSquareList.Clear();
                CheckBoardCompleted();
                return;
            }
        }
    }

    //function to check if selected square is on the array
    private bool IsPointOnTheRay(Ray currentRay, Vector3 point)
    {
        var hits = Physics.RaycastAll(currentRay, 100.0f);

        for(int i = 0; i < hits.Length; i++)
        {
            if(hits[i].transform.position == point)
            return true;
        }

        return false;
    }

    //this code is to select first square which first position then second square based on square selection of the correct array.
    private Ray SelectRay(Vector2 firstPosition, Vector2 secondPosition)
    {
        var direction = (secondPosition - firstPosition).normalized;
        float tolerance = 0.01f;

        if(Mathf.Abs(direction.x) < tolerance && Mathf.Abs(direction.y - 1f) < tolerance)
        {
            return _rayUp;
        }

        if(Mathf.Abs(direction.x) < tolerance && Mathf.Abs(direction.y - (-1f)) < tolerance)
        {
            return _rayDown;
        }

        if(Mathf.Abs(direction.x - (-1f)) < tolerance && Mathf.Abs(direction.y) < tolerance)
        {
            return _rayLeft;
        }

        if(Mathf.Abs(direction.x - 1f) < tolerance && Mathf.Abs(direction.y) < tolerance)
        {
            return _rayRight;
        }

        if(direction.x < 0f && direction.y > 0f)
        {
            return _rayDiagonalLeftUp;
        }

        if(direction.x < 0f && direction.y < 0f)
        {
            return _rayDiagonalLeftDown;
        }

        if(direction.x > 0f && direction.y > 0f)
        {
            return _rayDiagonalRightUp;
        }

        if(direction.x > 0f && direction.y < 0f)
        {
            return _rayDiagonalRightDown;
        }

        return _rayDown;

    }

    private void ClearSelection()
    {
        _assignedPoints = 0;
        _correctSquareList.Clear();
        _word = string.Empty;
    }

    private void CheckBoardCompleted()
    {
        bool loadNextCategory = false;

        if(currentGameData.selectedBoardData.SearchWords.Count == _completedWords)
        {
            //Save current level progress
            var categoryName = currentGameData.selectedCategoryName;
            var currentBoardIndex = DataSaver.ReadCategoryCurrentIndexValues(categoryName);
            var nextBoardIndex = -1;
            var currentCategoryIndex = 0;
            bool readNextLevelName = false;

            for(int index = 0; index < gameLevelData.data.Count; index++)
            {
                if(readNextLevelName)
                {
                    nextBoardIndex = DataSaver.ReadCategoryCurrentIndexValues(gameLevelData.data[index].categoryName);
                    readNextLevelName = false;
                }

                if(gameLevelData.data[index].categoryName == categoryName)
                {
                    readNextLevelName = true;
                    currentCategoryIndex = index;
                }
            }

            var currentLevelSize = gameLevelData.data[currentCategoryIndex].boardData.Count;
            if(currentBoardIndex < currentLevelSize)
            currentBoardIndex += 1;

            DataSaver.SaveCategoryData(categoryName, currentBoardIndex);

            //Unlock Next category
            if(currentBoardIndex >= currentLevelSize)
            {
                currentCategoryIndex++;
                //If this is not the last category
                if(currentCategoryIndex < gameLevelData.data.Count)
                {
                    categoryName = gameLevelData.data[currentCategoryIndex].categoryName;
                    currentBoardIndex = 0;
                    loadNextCategory = true;

                    if(nextBoardIndex <= 0)
                    {
                        DataSaver.SaveCategoryData(categoryName, currentBoardIndex);
                    }
                }else
                {
                    SceneManager.LoadScene("WordSearch_SelectCategory");
                }
            }
            else
            {
                GameEvents.BoardCompletedMethod();
            }

            if(loadNextCategory)
            GameEvents.UnlockNextCategoryMethod();

        }
    }
}
