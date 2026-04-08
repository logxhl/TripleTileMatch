using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelPage : MonoBehaviour
{
    [SerializeField] public Button[] arrLevelButton;

    private void Start()
    {
        int levelPasses = PlayerPrefs.GetInt("LevelPassed", 1);
        for(int i = 0; i < arrLevelButton.Length; i++)
        {
            arrLevelButton[i].interactable = false;
        }
        for(int i = 0; i < levelPasses; i++)
        {
            arrLevelButton[i].interactable = true;
        }
    }
}
