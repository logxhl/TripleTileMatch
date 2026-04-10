using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.Tilemaps;

public class toolSpawnTile : MonoBehaviour
{
    [Header("Tilemaps")]
    [ListDrawerSettings(ShowIndexLabels = true)]
    public List<Tilemap> tilemaps = new List<Tilemap>();

    [Header("Tile Positions (Auto-filled)")]
    [ReadOnly]
    public List<List<Vector3Int>> tilePositionsPerFloor = new List<List<Vector3Int>>();

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
        tilePositionsPerFloor.Clear();
        foreach (Tilemap tilemap in tilemaps)
        {
            List<Vector3Int> positions = new List<Vector3Int>();
            GetTilePositions(tilemap, positions);
            tilePositionsPerFloor.Add(positions);
        }
    }

    [Button]
    private void Btn_SpawnTile()
    {
        if (parent == null)
        {
            Debug.LogError("Parent container is null. Did you forget to call SpawnParent()?");
            return;
        }

        if (tilemaps.Count != parent.containerPrefabs.Count)
        {
            Debug.LogError($"Số tilemaps ({tilemaps.Count}) phải bằng số container prefabs ({parent.containerPrefabs.Count}).");
            return;
        }

        // Đảm bảo tilePositionsPerFloor đã được fill
        if (tilePositionsPerFloor.Count != tilemaps.Count)
        {
            Debug.LogWarning("tilePositionsPerFloor chưa khớp, đang tự động gọi Btn_GetTilePositions...");
            Btn_GetTilePositions();
        }

        // Gom tất cả tile positions
        List<Vector3Int> allTilePositions = new List<Vector3Int>();
        foreach (var positions in tilePositionsPerFloor)
            allTilePositions.AddRange(positions);

        int totalSpawnPoints = allTilePositions.Count;
        if (totalSpawnPoints % 3 != 0)
        {
            int excess = totalSpawnPoints % 3;
            allTilePositions.RemoveRange(0, excess);
            totalSpawnPoints -= excess;
        }

        Shuffle(allTilePositions);
        List<GameObject> shuffledPrefabs = CreateSolvablePrefabList(totalSpawnPoints);

        int floorCount = tilemaps.Count;
        int spawnIndex = 0;

        for (int i = 0; i < floorCount; i++)
        {
            Shuffle(tilePositionsPerFloor[i]);

            string layerName = "floor" + (i + 1);
            float positionZ = (floorCount - 1 - i);   // floor1 = Z cao nhất, giống logic cũ (4,3,2,1,0)
            int sortingOrder = i;  // floor1 = sorting cao nhất

            SpawnObjects(tilemaps[i],
                tilePositionsPerFloor[i],
                layerName,
                positionZ,
                sortingOrder,
                parent.containerInstances[i],
                shuffledPrefabs,
                ref spawnIndex,
                Vector3.zero
            );
        }
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
                    tilePositions.Add(tilePos);
            }
        }
    }

    List<GameObject> CreateSolvablePrefabList(int totalSpawnPoints)
    {
        List<GameObject> prefabPool = new List<GameObject>();

        int tripleCount = totalSpawnPoints / 3;

        for (int i = 0; i < tripleCount; i++)
        {
            GameObject prefab = lsPrefabs[Random.Range(0, lsPrefabs.Count)];

            prefabPool.Add(prefab);
            prefabPool.Add(prefab);
            prefabPool.Add(prefab);
        }

        Shuffle(prefabPool);

        return prefabPool;
    }

    void SpawnObjects(Tilemap tilemap, List<Vector3Int> tilePositions, string layerName, float positionZ, int sortingOrder,
        GameObject container, List<GameObject> shuffledPrefabs, ref int spawnIndex, Vector3 offset)
    {
        if (container == null)
        {
            Debug.LogError($"Container for {layerName} is null. Check if it was instantiated.");
            return;
        }

        foreach (Vector3Int tilePos in tilePositions)
        {
            if (spawnIndex >= shuffledPrefabs.Count) return;

            Vector3 worldPos = tilemap.GetCellCenterWorld(tilePos) + offset;
            worldPos.z = positionZ;

            GameObject selectedPrefab = shuffledPrefabs[spawnIndex++];

            GameObject temp = Instantiate(selectedPrefab, worldPos, Quaternion.identity);
            temp.layer = LayerMask.NameToLayer(layerName);
            temp.name = selectedPrefab.name + "_" + layerName + "_" + spawnIndex;
            temp.transform.parent = container.transform;

            SpriteRenderer sr = temp.GetComponent<SpriteRenderer>();
            if (sr != null)
                sr.sortingOrder = sortingOrder;
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