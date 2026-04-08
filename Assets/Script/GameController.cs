using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;

public class GameController : MonoBehaviour
{
    private static GameController instance;
    public static GameController Instance { get => instance; }
    [Header("Elements")]
    public LevelController levelController;
    public SortControllerRemake SortControllerRemake;
    public AudioManager audioManager;
    public UIManager UIManager;
    public GameScene gameScene;
    public Transform ContainerTiles;

    [Header("Settings")]
    public List<TilebaseController> lsTilesInCurrentLevel;
    public int numOfCurrentTile;
    public int currentLevel;
    public int totalTile;
    private void Awake()
    {
        instance = this;
        currentLevel = PlayerPrefs.GetInt("CurrentLevel", 1);
        if (currentLevel >= 10) currentLevel = 10;
        gameScene.Init();
    }
    private void Start()
    {
        levelController.GenarateLevel();
        totalTile = numOfCurrentTile;


        // AdsManager.instance.ShowInterstitialAd();
    }
    private void Reset()
    {
        SortControllerRemake = transform.GetComponentInChildren<SortControllerRemake>();
        levelController = FindAnyObjectByType<LevelController>();
        //soundManager = FindAnyObjectByType<SoundManager>();
        //UIManager = FindAnyObjectByType<UIManager>();

    }
}
