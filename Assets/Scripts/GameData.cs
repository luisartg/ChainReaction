using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{
    public static GameData Instance;

    private List<LevelData> levelDataList;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        InitializeData();
    }

    private void InitializeData()
    {
        levelDataList = new List<LevelData>();
        levelDataList.Add(item: new LevelData() { levelButtonName = "01", levelSceneName = "Level01", completed = false }) ;
        levelDataList.Add(item: new LevelData() { levelButtonName = "02", levelSceneName = "Level02", completed = false });
        levelDataList.Add(item: new LevelData() { levelButtonName = "03", levelSceneName = "Level03", completed = false });
        levelDataList.Add(item: new LevelData() { levelButtonName = "04", levelSceneName = "Level04", completed = false });
        levelDataList.Add(item: new LevelData() { levelButtonName = "05", levelSceneName = "Level05", completed = false });
        levelDataList.Add(item: new LevelData() { levelButtonName = "06", levelSceneName = "Level06", completed = false });
        levelDataList.Add(item: new LevelData() { levelButtonName = "07", levelSceneName = "Level07", completed = false });
        levelDataList.Add(item: new LevelData() { levelButtonName = "08", levelSceneName = "Level08", completed = false });
        levelDataList.Add(item: new LevelData() { levelButtonName = "09", levelSceneName = "Level09", completed = false });
        levelDataList.Add(item: new LevelData() { levelButtonName = "10", levelSceneName = "Level10", completed = false });
        levelDataList.Add(item: new LevelData() { levelButtonName = "11", levelSceneName = "Level11", completed = false });
        levelDataList.Add(item: new LevelData() { levelButtonName = "12", levelSceneName = "Level12", completed = false });
    }

    //public void CompleteLevel(int index)
    //{
    //    levelDataList[index].completed = true;
    //}

    public void CompleteLevel(string levelName)
    {
        foreach (var levelData in levelDataList)
        {
            if (levelData.levelSceneName == levelName)
            {
                levelData.completed = true; 
                break;
            }
        }
    }

    public bool IsThisLevelComplete(string levelName)
    {
        foreach (var levelData in levelDataList)
        {
            if (levelData.levelSceneName == levelName)
            {
                return levelData.completed;
            }
        }
        return false;
    }
}

public class LevelData
{
    public string levelButtonName;
    public string levelSceneName;
    public bool completed = false;

}
