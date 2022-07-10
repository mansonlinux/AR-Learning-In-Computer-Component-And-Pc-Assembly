using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//by Hisyam - Ref - YT - Epi 12,13.

public class SearchingWordsList : MonoBehaviour
{
    public GameData currentGameData;
    public GameObject searchingWordPrefab;
    public float offset = 0.0f;
    public int maxColumns = 5;
    public int maxRows = 4;

    private int _columns = 2;
    private int _rows;
    private int _wordsNumber;

    private List<GameObject> _words = new List<GameObject>();

    private void Start()
    {
        _wordsNumber = currentGameData.selectedBoardData.SearchWords.Count;
        
        if(_wordsNumber < _columns)
            _rows = 1;
        else
            CalculateColumnsAndRowsNumber();

        CreateWordObjects();
        SetWordsPosition();
    }

    //Function to calculate the rows and columns number based on the numbers of the searching words.
    private void CalculateColumnsAndRowsNumber()
    {
        do
        {
            _columns++;
            _rows = _wordsNumber / _columns;
        //Function to prevent from creating more rows than the max rows number which is set into inspector.
        }while(_rows >= maxRows);

        if(_columns > maxColumns)
        {
            _columns = maxColumns;
            _rows = _wordsNumber / _columns;
        }
    }

    private bool TryIncreaseColumnNumber()
    {
        _columns++;
        _rows = _wordsNumber / _columns;

        if(_columns > maxColumns)
        {
            _columns = maxColumns;
            _rows = _wordsNumber / _columns;

            return false;
        }

        //If there is odd number of the rows of the words. It will increase the row number.
        if(_wordsNumber % _columns > 0)
            _rows++;

        return true;
    }

    //Function - Setting the position & scale for seaching word.
    private void CreateWordObjects()
    {
        var squareScale = GetSquareScale(new Vector3( 1f, 1f, 0.1f));

        for(var index = 0; index < _wordsNumber; index++)
        {
            _words.Add(Instantiate(searchingWordPrefab) as GameObject);
            _words[index].transform.SetParent(this.transform);
            _words[index].GetComponent<RectTransform>().localScale = squareScale;
            _words[index].GetComponent<RectTransform>().localPosition = new Vector3 ( 0f, 0f, 0f);
            //Episode 13 - take the word from the current game data list.
            _words[index].GetComponent<SearchingWord>().SetWord(currentGameData.selectedBoardData.SearchWords[index].Word);
        }
    }

    private Vector3 GetSquareScale(Vector3 defaultScale)
    {
        var finalScale = defaultScale;
        var adjustment = 0.01f;

        while(ShouldScaleDown(finalScale))
        {
            finalScale.x -= adjustment;
            finalScale.y -= adjustment;

            //Scale down until the scale is less or equal zero
            if(finalScale.x <= 0 || finalScale.y <= 0)
            {
                finalScale.x = adjustment;
                finalScale.y = adjustment;

                return finalScale;
            }
        }

        return finalScale;
    }

    private bool ShouldScaleDown(Vector3 targetScale)
    {
        var squareRect = searchingWordPrefab.GetComponent<RectTransform>();
        var parentRect = this.GetComponent<RectTransform>();

        var squareSize = new Vector2( 0f, 0f);

        squareSize.x = squareRect.rect.width * targetScale.x + offset;
        squareSize.y = squareRect.rect.height * targetScale.y + offset;

        var totalSquareHeight = squareSize.y * _rows;

        //Make sure all of the square fit in the parent rectangle area.
        if(totalSquareHeight > parentRect.rect.height)
        {
            while(totalSquareHeight > parentRect.rect.height)
            {
                //if cannot increase any columns because it already exceed the limit. it will scale down all the squares
                if(TryIncreaseColumnNumber())
                    totalSquareHeight = squareSize.y * _rows;
                else
                    return true;
            }
        }

        var totalSquareWidth = squareSize.x * _columns;

        if(totalSquareWidth > parentRect.rect.width)
            return true;
        
        return false;
    }

    private void SetWordsPosition()
    {
        var squareRect = _words[0].GetComponent<RectTransform>();
        var wordOffset = new Vector2
        {
            x = squareRect.rect.width * squareRect.transform.localScale.x + offset,
            y = squareRect.rect.height * squareRect.transform.localScale.y + offset
        };

        int columnNumber = 0;
        int rowNumber = 0;
        var startPosition = GetFirstSquarePosition();

        foreach (var word in _words)
        {
            if(columnNumber + 1 > _columns)
            {
                columnNumber = 0;
                rowNumber++;
            }

            var positionX = startPosition.x + wordOffset.x * columnNumber;
            var positionY = startPosition.y - wordOffset.y * rowNumber;

            word.GetComponent<RectTransform>().localPosition = new Vector2(positionX, positionY);
            columnNumber++;
        }
    }

    private Vector2 GetFirstSquarePosition()
    {
        var startPosition = new Vector2( 0f, transform.position.y);
        var squareRect = _words[0].GetComponent<RectTransform>();
        var parentRect = this.GetComponent<RectTransform>();
        var squareSize = new Vector2 ( 0f, 0f);

        squareSize.x = squareRect.rect.width * squareRect.transform.localScale.x + offset;
        squareSize.y = squareRect.rect.height * squareRect.transform.localScale.y + offset; 

        //Make sure they are in the centre
        var shiftBy = (parentRect.rect.width - (squareSize.x * _columns)) / 2;

        startPosition.x = ((parentRect.rect.width - squareSize.x) / 2) * (-1);
        startPosition.x += shiftBy;
        startPosition.y = (parentRect.rect.height - squareSize.y) / 2;

        return startPosition;
    }
}
