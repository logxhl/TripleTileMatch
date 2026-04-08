using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.Tilemaps;

public class toolSpawnTile : MonoBehaviour
{
    [Header("Tilemap")]
    public Tilemap tilemapFloor1;
    public Tilemap tilemapFloor2;
    public Tilemap tilemapFloor3;
    public Tilemap tilemapFloor4;
    public Tilemap tilemapFloor5;

    [Header("Tile Positions")]
    public List<Vector3Int> tilePositionsFloor1 = new List<Vector3Int>();
    public List<Vector3Int> tilePositionsFloor2 = new List<Vector3Int>();
    public List<Vector3Int> tilePositionsFloor3 = new List<Vector3Int>();
    public List<Vector3Int> tilePositionsFloor4 = new List<Vector3Int>();
    public List<Vector3Int> tilePositionsFloor5 = new List<Vector3Int>();

    public List<GameObject> lsPrefabs;

    public toolSpawnParentContainer parent;

    [Button]
    private void SpawnParent()
    {
        parent = GetComponent<toolSpawnParentContainer>();
        parent.SpawnContainer();
    }

    [Button]
    private void Btn_GetTilePositions()
    {
        GetTilePositions(tilemapFloor1, tilePositionsFloor1);
        GetTilePositions(tilemapFloor2, tilePositionsFloor2);
        GetTilePositions(tilemapFloor3, tilePositionsFloor3);
        GetTilePositions(tilemapFloor4, tilePositionsFloor4);
        GetTilePositions(tilemapFloor5, tilePositionsFloor5);
    }

    [Button]
    private void Btn_SpawnTile()
    {
        if (parent == null)
        {
            Debug.LogError("Parent container is null. Did you forget to call SpawnParent()?");
            return;
        }

        List<Vector3Int> allTilePositions = new List<Vector3Int>();
        allTilePositions.AddRange(tilePositionsFloor1);
        allTilePositions.AddRange(tilePositionsFloor2);
        allTilePositions.AddRange(tilePositionsFloor3);
        allTilePositions.AddRange(tilePositionsFloor4);
        allTilePositions.AddRange(tilePositionsFloor5);

        int totalSpawnPoints = allTilePositions.Count;
        if (totalSpawnPoints % 3 != 0)
        {
            int excess = totalSpawnPoints % 3;
            allTilePositions.RemoveRange(0, excess);
            totalSpawnPoints -= excess;
        }

        Shuffle(allTilePositions);
        List<GameObject> shuffledPrefabs = CreateShuffledPrefabList(totalSpawnPoints);

        int spawnIndex = 0;
        SpawnObjects(tilePositionsFloor1, "floor1", 4, 0, parent.containerFloor1Instance, shuffledPrefabs, ref spawnIndex, new Vector3(0f, 0f, 0f));
        SpawnObjects(tilePositionsFloor2, "floor2", 3, 1, parent.containerFloor2Instance, shuffledPrefabs, ref spawnIndex, new Vector3(0f, 0f, 0f));
        SpawnObjects(tilePositionsFloor3, "floor3", 2, 2, parent.containerFloor3Instance, shuffledPrefabs, ref spawnIndex, new Vector3(0f, 0f, 0f));
        SpawnObjects(tilePositionsFloor4, "floor4", 1, 3, parent.containerFloor4Instance, shuffledPrefabs, ref spawnIndex, new Vector3(0f, 0f, 0f));
        SpawnObjects(tilePositionsFloor5, "floor5", 0, 4, parent.containerFloor5Instance, shuffledPrefabs, ref spawnIndex, new Vector3(0f, 0f, 0f));
    }

    void GetTilePositions(Tilemap tilemap, List<Vector3Int> tilePositions)
    {
        tilePositions.Clear();
        BoundsInt bounds = tilemap.cellBounds;
        for (int x = bounds.xMin; x < bounds.xMax; x++)
        {
            for (int y = bounds.yMin; y < bounds.yMax; y++)
            {
                Vector3Int tilePos = new Vector3Int(x, y, 0);
                if (tilemap.HasTile(tilePos))
                {
                    tilePositions.Add(tilePos);
                }
            }
        }
    }

    List<GameObject> CreateShuffledPrefabList(int totalSpawnPoints)
    {
        List<GameObject> prefabPool = new List<GameObject>();

        int prefabsPerType = totalSpawnPoints / lsPrefabs.Count;
        foreach (GameObject prefab in lsPrefabs)
        {
            for (int i = 0; i < prefabsPerType; i++)
            {
                prefabPool.Add(prefab);
            }
        }

        int remaining = totalSpawnPoints % lsPrefabs.Count;
        for (int i = 0; i < remaining; i++)
        {
            prefabPool.Add(lsPrefabs[i]);
        }

        Shuffle(prefabPool);
        return prefabPool;
    }

    void SpawnObjects(List<Vector3Int> tilePositions, string layerName, float positionZ, int sortingOrder, GameObject container, List<GameObject> shuffledPrefabs, ref int spawnIndex, Vector3 offset)
    {
        if (container == null)
        {
            Debug.LogError($"Container for {layerName} is null. Check if it was instantiated.");
            return;
        }

        foreach (Vector3Int tilePos in tilePositions)
        {
            if (spawnIndex >= shuffledPrefabs.Count) return;

            Vector3 worldPos = tilemapFloor1.GetCellCenterWorld(tilePos) + offset;
            worldPos.z = positionZ;

            GameObject selectedPrefab = shuffledPrefabs[spawnIndex++];

            GameObject temp = Instantiate(selectedPrefab, worldPos, Quaternion.identity);
            temp.layer = LayerMask.NameToLayer(layerName);
            temp.name = selectedPrefab.name + "_" + layerName + "_" + spawnIndex;
            temp.transform.parent = container.transform;

            SpriteRenderer sr = temp.GetComponent<SpriteRenderer>();
            if (sr != null)
            {
                sr.sortingOrder = sortingOrder;
            }
        }
    }

    void Shuffle<T>(List<T> list)
    {
        for (int i = list.Count - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);
            (list[i], list[randomIndex]) = (list[randomIndex], list[i]);
        }
    }
}
