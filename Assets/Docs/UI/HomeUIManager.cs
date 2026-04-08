using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HomeUIManager : MonoBehaviour
{
    [SerializeField] private GameObject PopupLevel;
    [SerializeField] private GameObject PopupSetting;
    [SerializeField] private Toggle VibrationToggle;

    private void Awake()
    {
        PopupLevel.gameObject.SetActive(false); 
        PopupSetting.gameObject.SetActive(false);
        bool isVibration = PlayerPrefs.GetInt("IsVibration", 1) == 1;
        VibrationToggle.isOn = isVibration;
    }
    public void HandleStartBtn()
    {
        LoadingSceneController.sceneToLoad = "GamePlay";
        SceneManager.LoadScene("Loading");
    }
    public void HandldLevelBtn()
    {
        PopupLevel.gameObject.SetActive(true);
    }
    public void HandleSettingBtn()
    {
        PopupSetting.gameObject.SetActive(true);
    }
    public void HandleLevelSelectBtn(int id)
    {
        PlayerPrefs.SetInt("CurrentLevel", id);
        PlayerPrefs.Save(); 
        LoadingSceneController.sceneToLoad = "GamePlay";
        SceneManager.LoadScene("Loading");
    }
    public void CloseSettingPanel()
    {
        PopupSetting.gameObject.SetActive(false);
    }
    public void CloseLevelPagePanel()
    {
        PopupLevel.gameObject.SetActive(false);
    }
    public void SetVibration(bool isOn)
    {
        isOn = VibrationToggle.isOn;
        PlayerPrefs.SetInt("IsVibration", isOn ? 1 : 0);
        PlayerPrefs.Save();
    }
}
