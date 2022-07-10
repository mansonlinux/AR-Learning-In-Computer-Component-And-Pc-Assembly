using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//By Hisyam (Ref - YT - 3, 5, 26 (fixing game bug), 27(fixing game bug 2))

[System.Serializable]
[CreateAssetMenu]
public class BoardData : ScriptableObject //Purpose to hold data in WordSearch Game
{
    [System.Serializable]
    public class SearchingWord
    {
        [HideInInspector]
        public bool Found = false;
        public string Word;
    }

    [System.Serializable]
    public class BoardRow
    {
        public int Size;
        public string[] Row;
        
        public BoardRow() {}

        public BoardRow(int size)
        {
           CreateRow(size); 
        }

        public void CreateRow(int size)
        {
            Size = size;
            Row = new string[Size];
            ClearRow();
        }

        public void ClearRow()
        {
            for(int i = 0; i < Size; i++)
            {
                Row[i] = " ";
            }
        }
    }

    public float timeInSeconds; //Time inside the game to solve the puzzle
    public int Columns = 0;
    public int Rows = 0;

    public BoardRow[] Board;
    public List<SearchingWord> SearchWords = new List<SearchingWord>();

    public void ClearData()
    {
        foreach (var word in SearchWords)
        {
            word.Found = false;
        }
    }


    public void ClearWithEmptyString()
    {
        for (int i = 0; i < Columns; i++)
        {
            Board[i].ClearRow();
        }
    }

    public void CreateNewBoard()
    {
        Board = new BoardRow[Columns];
        for(int i = 0; i < Columns; i++)
        {
            Board[i] = new BoardRow(Rows);
        }
    }
}
