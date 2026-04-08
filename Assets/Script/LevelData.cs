using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

public class LevelData : MonoBehaviour
{
    public List<LevelConfig> lsLevel;
    public List<DataTile> lsTile;

    public LevelConfig getLevel(int id)
    {
        foreach (var level in lsLevel)
        {
            if (level.iDLevel == id)
            {
                return level;
            }
        }
        return null;
    }

    public GameObject getTile(int id)
    {
        foreach (var tile in lsTile)
        {
            if (tile.iDTile == id)
            {
                return tile.tile;
            }
        }
        return null;
    }

    public LevelConfig current;
    public void Init()
    {
        
        //var currentLevel = PlayerPrefs.GetInt("CurrentLevel", 1);
        //current = getLevel(currentLevel); // Lay bien luu currentLevel
        //if (current != null)
        //{
        //    foreach (var item in current.lsIDTile)
        //    {
        //        for (int i = 0; i < item.count; i++)
        //        {
        //            var temp = Instantiate(getTile(item.iD));
        //        }
        //    }
        //}
        //var currentLevel = Resources.Load<GameObject>("Level_" + PlayerPrefs.GetInt("CurrentLevel", 1));
        //GameController.Instance.levelData = Instantiate(currentLevel).GetComponent<LevelData>();
    }
}

[System.Serializable]
public class LevelConfig
{
    public int iDLevel;
    public List<CountId> lsIDTile;
}

[System.Serializable]
public class DataTile
{
    public int iDTile;
    public GameObject tile;
}
