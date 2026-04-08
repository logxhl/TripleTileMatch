using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileManager : MonoBehaviour
{
    //public Tilemap gameTilemap;
    //public TileBase[] tiles; 
    //private List<Vector3Int> tilePositions; 
    //private Dictionary<TileBase, int> tileCounts = new Dictionary<TileBase, int>();

    //private void Awake()
    //{
    //    LoadTilePositions();
    //}

    //private void Start()
    //{
    //    GenerateLevel();
    //}

    //void LoadTilePositions()
    //{
    //    string json = PlayerPrefs.GetString("TilePositions", "");
    //    if (!string.IsNullOrEmpty(json))
    //    {
    //        tilePositions = JsonHelper.FromJson<Vector3Int>(json);
    //        Debug.Log("Tải danh sách vị trí từ PlayerPrefs. Số lượng: " + tilePositions.Count);
    //    }
    //    else
    //    {
    //        tilePositions = new List<Vector3Int>();
    //        Debug.LogWarning("Không tìm thấy danh sách tile trong PlayerPrefs!");
    //    }
    //}

    //void GenerateLevel()
    //{
    //    if (tiles.Length == 0 || tilePositions.Count == 0) return;

    //    tileCounts.Clear();

    //    // Khởi tạo số lượng tile bằng 0
    //    foreach (TileBase tile in tiles)
    //    {
    //        tileCounts[tile] = 0;
    //    }

    //    // Gán tile vào các vị trí chỉ định trước
    //    foreach (Vector3Int pos in tilePositions)
    //    {
    //        TileBase selectedTile = GetValidTile();
    //        gameTilemap.SetTile(pos, selectedTile);
    //        tileCounts[selectedTile]++;
    //    }
    //}

    //TileBase GetValidTile()
    //{
    //    // Tạo danh sách tile hợp lệ
    //    List<TileBase> validTiles = new List<TileBase>();

    //    foreach (var tile in tiles)
    //    {
    //        if ((tileCounts[tile] + 1) % 3 == 0)
    //        {
    //            validTiles.Add(tile);
    //        }
    //    }

    //    if (validTiles.Count == 0) // Nếu chưa tile nào đủ 3n
    //    {
    //        validTiles.AddRange(tiles);
    //    }

    //    TileBase chosenTile = validTiles[Random.Range(0, validTiles.Count)];
    //    return chosenTile;
    //}
}
