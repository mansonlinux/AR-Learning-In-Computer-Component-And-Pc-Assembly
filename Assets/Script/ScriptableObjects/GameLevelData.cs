using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//by Hisyam - Ref - YT - Epi 15.
//Important part to insert category puzzles > BoardData into GameLevelData.

[System.Serializable]
[CreateAssetMenu]
public class GameLevelData : ScriptableObject
{
    [System.Serializable]
    public struct CategoryRecord
    {
        public string categoryName;
        public List<BoardData> boardData;
    }

    public List<CategoryRecord> data;
}
