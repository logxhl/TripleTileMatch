using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public enum SKillType
{
    Undo,
    Hint,
    Shuffle
}
public class SkillManager : MonoBehaviour
{
    public static SkillManager instance;

    [Header("Elements")]
    GameObject currentLevel;

    [Header("Settings")]
    [Header("Undo")]
    public Dictionary<string, Vector3> tilePosTracking = new Dictionary<string, Vector3>();

    [Header("Hint")]
    protected int count;
    protected TilebaseController fallbackTile = null;

    [Header("Shuffle")] 
    protected List<Vector3> tilePosFloor1, tilePosFloor2, tilePosFloor3, tilePosFloor4, tilePosFloor5;
    [SerializeField] protected List<List<Vector3>> tilePosFloors;

    private void Awake()
    {
        if(instance != null) Destroy(gameObject);
        else instance = this;

        tilePosFloor1 = new List<Vector3>();
        tilePosFloor2 = new List<Vector3>();
        tilePosFloor3 = new List<Vector3>();
        tilePosFloor4 = new List<Vector3>();
        tilePosFloor5 = new List<Vector3>();

        tilePosFloors = new List<List<Vector3>>()
        {
            tilePosFloor1,
            tilePosFloor2,
            tilePosFloor3,
            tilePosFloor4,
            tilePosFloor5
        };
    }
    public void UndoTile()
    {
        if (GameController.Instance.SortControllerRemake.lsTilebaseSelected.Count == 0) return;
        var tile = GameController.Instance.SortControllerRemake.lsTilebaseSelected.Last();
        Vector3 previosPos = GetPreviousPosOfTile(tile);
        if (previosPos == Vector3.zero) return;
        GameController.Instance.SortControllerRemake.lsTilebaseSelected.Remove(tile);
        GameController.Instance.SortControllerRemake.lsTilebaseClicked.Remove(tile);
        GameController.Instance.SortControllerRemake.MoveTileToTarget(tile, previosPos, () => { tile.polygonCollider.enabled = true; });
        tilePosTracking.Remove(tile.name);
        GameController.Instance.numOfCurrentTile++;
        SkillButtonUI.Instance._undoSKillAmount--;
    }
    private Vector3 GetPreviousPosOfTile(TilebaseController tile)
    {
        foreach (var temp in tilePosTracking)
        {
            if (temp.Key == tile.name)
            {
                return temp.Value;
            }
        }
        return Vector3.zero;
    }
    public void HintTile()
    {
        if (GameController.Instance.SortControllerRemake.lsTilebaseClicked.Count == 0) return;
        int id = GetIdOfTheMostPopTile(GameController.Instance.SortControllerRemake.lsTilebaseClicked);
        TilebaseController tile1 = GetHintTile(id);
        if (count == 0)
        {
            GameController.Instance.SortControllerRemake.HandleOnMouseDown(tile1);
            TilebaseController tile2 = GetHintTile(id);
            GameController.Instance.SortControllerRemake.HandleOnMouseDown(tile2);
        }
        else if (count == 1)
        {
            GameController.Instance.SortControllerRemake.HandleOnMouseDown(tile1);
        }
        count = 0;
        SkillButtonUI.Instance._hintSkillAmount--;
    }
    private int GetIdOfTheMostPopTile(List<TilebaseController> lsTiles)
    {
        int maxCount = 0;
        for (int i = 0; i < lsTiles.Count - 1; i++)
        {
            count = 0;
            if (lsTiles[i].id == lsTiles[i + 1].id)
            {
                count++;
            }
            if (count > maxCount)
            {
                maxCount = count;
                return lsTiles[i].id;
            }
        }
        if (count == 0)
        {
            return lsTiles[0].id;
        }
        return 0;
    }
    private TilebaseController GetHintTile(int id)
    {
        for (int i = GameController.Instance.lsTilesInCurrentLevel.Count - 1; i >= 0; i--)
        {
            var tile = GameController.Instance.lsTilesInCurrentLevel[i];
            if (tile.id == id)
            {
                if (tile.lsTileHigher.Count > 0)
                {
                    GameController.Instance.lsTilesInCurrentLevel.Remove(tile);
                    return tile;
                }
                else
                {
                    fallbackTile = tile;
                }
            }
        }
        GameController.Instance.lsTilesInCurrentLevel.Remove(fallbackTile);
        return fallbackTile;
    }
    public void ShuffleTile()
    {
        currentLevel = GameObject.Find("Level" + GameController.Instance.currentLevel);
        
        int index = 0;
        foreach(Transform child in currentLevel.transform)
        {
            tilePosFloors[index].Clear();
            foreach (Transform child2 in child.transform)
            {
                tilePosFloors[index].Add(child2.position);
            }
            index++;
        }
        List<TilebaseController> listShuffleTiles = new List<TilebaseController>(GameController.Instance.lsTilesInCurrentLevel);

        foreach (var tile in listShuffleTiles)
        {
            tile.spriteRenderer.sortingOrder = 0;
            tile.transform.position = new Vector3(tile.transform.position.x, tile.transform.position.y, 0);
            tile.gameObject.layer = LayerMask.NameToLayer("floor1");
            tile.polygonCollider.enabled = false;
        }
        listShuffleTiles = listShuffleTiles.OrderBy(x => Random.value).ToList();
        for (int i = 0; i < index; i++)
        {
            PlaceTileInPos(listShuffleTiles, tilePosFloors[i], i);
        }
    }
    private void PlaceTileInPos(List<TilebaseController> lsTile, List<Vector3> lsPos,int floorIndex)
    {
        foreach(var pos in lsPos)
        {
            if (lsTile.Count == 0) return;
            var tile = lsTile[0];
            lsTile.RemoveAt(0);
            Vector3 newPos = pos;
            newPos.z = -floorIndex;

            tile.spriteRenderer.sortingOrder = floorIndex;
            string layerName = "floor" + (floorIndex + 1);
            int layerIndex = LayerMask.NameToLayer(layerName);

            if (layerIndex == -1)
            {
                Debug.LogWarning("Layer " + layerName + " not found.");
                return;
            }
            else
            {
                tile.gameObject.layer = layerIndex;
            }

            tile.transform.DOMove(newPos, 0.5f).SetEase(Ease.InOutQuad);
            Transform container = currentLevel.transform.GetChild(floorIndex);
            tile.transform.SetParent(null);
            tile.transform.SetParent(container);
            tile.polygonCollider.enabled = true;
        }
    }
}
