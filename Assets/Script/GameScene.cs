using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameScene : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private Button btnPause;
    [SerializeField] private Slider progressBar;
    [Header("-----------PopUpPause-------------")]
    public GameObject popUpPause;
    public Button btnClose;
    public Button btnHome;
    public Button btnRestart;
    public Button btnSpeaker;
    public Image imgbtnSpeaker;
    public Sprite spriteOnSpeaker;
    public Sprite spriteOffSpeaker;
    public bool isSpeakerOn = true;

    [Header("-------------PopUpWin-----------")]
    public GameObject popUpWin;
    public Button btnHomeWin;
    public Button btnResetWin;
    public Button btnNextLevel;

    [Header("-------------PopUpLose-----------")]
    public GameObject popUpLose;
    public Button btnHomeLose;
    public Button btnResetLose;

    public float checkLoseTime;
    
    private void Awake()
    {
        progressBar.value = 0;
        levelText.text = "LEVEL " + PlayerPrefs.GetInt("CurrentLevel", 1);
    }
    public void Init()
    {
        // PopUp Pause
        btnPause.onClick.AddListener(HandleClickBtnPause);
        btnClose.onClick.AddListener(HandleClickBtnClose);
        btnHome.onClick.AddListener(HandleClickHome);
        btnRestart.onClick.AddListener(HandleClickRestart);
        // popUp Win
        btnNextLevel.onClick.AddListener(HandleClickBtnNextLevel);
        btnHomeWin.onClick.AddListener(HandleClickHome);
        btnResetWin.onClick.AddListener(HandleClickRestart);
        // pop Up Lose
        btnHomeLose.onClick.AddListener(HandleClickHome);
        btnResetLose.onClick.AddListener(HandleClickRestart);
        GameController.Instance.audioManager.Init();
    }
    private void Update()
    {
        if (CheckWinCondition())
        {
            //popUpWin.SetActive(true);int levelPassed = PlayerPrefs.GetInt("LevelPassed", 1);
          
            StartCoroutine(WaitForWin());
        }
        if(CheckLose())
        {
            popUpLose.SetActive(true);
        }
       // StartCoroutine(WaitForCheckLose());
        UpdateProgressBar();
    }
    #region Handle Pause Game
    public void HandleClickBtnPause()
    {
        popUpPause.SetActive(true);
        TilebaseController.isPause = true;
        
    }

    public void HandleClickBtnClose()
    {
        popUpPause.SetActive(false);

        TilebaseController.isPause = false;
        
    }
    public void HandleClickHome()
    {
        LoadingSceneController.sceneToLoad = "Home";
        SceneManager.LoadScene("Loading");

    }

    public void HandleClickRestart()
    {
        LoadingSceneController.sceneToLoad = "GamePlay";
        SceneManager.LoadScene("Loading");

        //SceneManager.LoadScene("GamePlay");
        TilebaseController.isPause = false;
        
    }
    #endregion

    #region Handle Win Game
    public bool CheckWinCondition()
    {
        return GameController.Instance.numOfCurrentTile <= 0 && GameController.Instance.SortControllerRemake.lsTilebaseClicked.Count == 0;
    }
    
    public void CheckSameTile(int id)
    {
        foreach (var item in GameController.Instance.SortControllerRemake.lsTilebaseClicked)
        {
            
        }    
    }    
    public IEnumerator WaitForWin()
    {
        yield return new WaitForSeconds(0.5f);
        popUpWin.SetActive(true);
    }    

    public void HandleClickBtnNextLevel()
    {
        LoadingSceneController.sceneToLoad = "GamePlay";
        SceneManager.LoadScene("Loading");
        GameController.Instance.currentLevel = PlayerPrefs.GetInt("CurrentLevel",1);
        Debug.Log(GameController.Instance.currentLevel);
        GameController.Instance.currentLevel++;
        PlayerPrefs.SetInt("CurrentLevel",GameController.Instance.currentLevel);
        PlayerPrefs.Save();
        int levelPassed = PlayerPrefs.GetInt("LevelPassed", 1);
        levelPassed++;
        if (levelPassed >= 10) levelPassed = 10;
        PlayerPrefs.SetInt("LevelPassed", levelPassed);
        PlayerPrefs.Save();
    }
    #endregion

    private float checkLoseTimer = 0f;

    public bool CheckLose()
    {
        if (GameController.Instance.SortControllerRemake.lsTilebaseClicked.Count >= 8 && !GameController.Instance.SortControllerRemake.CanMatchTriple())
        {
            checkLoseTimer += Time.deltaTime;

            if (checkLoseTimer >= 0.5f)
            {
                checkLoseTimer = 0f;
                return true;
            }
        }
        else
        {
            checkLoseTimer = 0f;
        }

        return false;
    }
    public void UpdateProgressBar()
    {
        if (CheckLose() || CheckWinCondition()) return;

        float progress = 1f - ((float)GameController.Instance.numOfCurrentTile / GameController.Instance.totalTile);

        progressBar.value = progress;
    }
}
