using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{    public void GenarateLevel()
    {
        var prefabLevel = Resources.Load<GameObject>("Level/Level_" + GameController.Instance.currentLevel);
        var currentLevel = Instantiate(prefabLevel);
        currentLevel.name = "Level" + GameController.Instance.currentLevel;
        foreach (Transform child in currentLevel.transform)
        {
            foreach (Transform child2 in child.transform) 
            {
                GameController.Instance.lsTilesInCurrentLevel.Add(child2.GetComponent<TilebaseController>());
            }
            
        }
        GameController.Instance.numOfCurrentTile = FindObjectsOfType<TilebaseController>().Length;
        if(PlayerPrefs.GetInt("CurrentLevel", 1) > 10)
        {
            SkillManager.instance.ShuffleTile();
        }
    }

}
