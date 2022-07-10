using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//by Hisyam - Ref - YT - Epi 15,16.

public class GameDataSelector : MonoBehaviour
{
    public GameData currentGameData;
    public GameLevelData levelData;

    void Awake()
    {
        SelectSequentalBoardData();
    }

    //Function which have ability to select 1st puzzle, second, third and etc.
    //Complicated make me crazy erkkkkk
    private void SelectSequentalBoardData()
    {
        foreach(var data in levelData.data)
        {
            if(data.categoryName == currentGameData.selectedCategoryName)
            {
                var boardIndex = DataSaver.ReadCategoryCurrentIndexValues(currentGameData.selectedCategoryName);

                if(boardIndex < data.boardData.Count)
                {
                    currentGameData.selectedBoardData = data.boardData[boardIndex];
                }
                else
                {
                    var randomIndex = Random.Range(0, data.boardData.Count);
                    currentGameData.selectedBoardData = data.boardData[randomIndex];
                }
            }
        }
    }
}
