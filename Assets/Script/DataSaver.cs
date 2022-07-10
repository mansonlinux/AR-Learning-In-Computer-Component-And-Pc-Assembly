using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//by Hisyam - Ref - YT - Epi 16.

public class DataSaver : MonoBehaviour
{
    //Function - To pass the name of category
    //If the category has been unlock that's mean name of the category will be present in the player Prefab
    public static int ReadCategoryCurrentIndexValues(string name)
    {
        var value = -1;
        if(PlayerPrefs.HasKey(name))
            //Check how many puzzles has been solve from category to display value
            value = PlayerPrefs.GetInt(name);

            return value;
    }

    public static void SaveCategoryData(string categoryName, int currentIndex)
    {
        PlayerPrefs.SetInt(categoryName, currentIndex);
        PlayerPrefs.Save();
    }

    public static void ClearGameData(GameLevelData levelData)
    {
        foreach (var data in levelData.data)
        {
            PlayerPrefs.SetInt(data.categoryName, -1);
        }

        //Unlock first level
        PlayerPrefs.SetInt(levelData.data[0].categoryName, 0);
        PlayerPrefs.Save();
    }
}
