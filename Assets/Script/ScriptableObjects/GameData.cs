using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//by Hisyam - Ref - YT - Epi-7

[System.Serializable]
[CreateAssetMenu]
public class GameData : ScriptableObject
{
    public string selectedCategoryName;
    public BoardData selectedBoardData;
}
