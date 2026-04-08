using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Button")]
    public Button nextLevel;
    private void Awake()
    {
        nextLevel.gameObject.SetActive(false);
    }
    public void WinGame()
    {
        nextLevel.gameObject.SetActive(true);
    }
    public void NextLevel()
    {
        GameController.Instance.currentLevel++;
        PlayerPrefs.SetInt("CurrentLevel", GameController.Instance.currentLevel);
        PlayerPrefs.Save();
        GameController.Instance.levelController.GenarateLevel();
    }
}
