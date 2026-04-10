using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class toolSpawnParentContainer : MonoBehaviour
{
    [ListDrawerSettings(ShowIndexLabels = true, ListElementLabelName = "name")]
    public List<GameObject> containerPrefabs = new List<GameObject>();

    [ReadOnly]
    public List<GameObject> containerInstances = new List<GameObject>();

    public void SpawnContainer()
    {
        // Đảm bảo list instances có đủ slot
        while (containerInstances.Count < containerPrefabs.Count)
            containerInstances.Add(null);

        for (int i = 0; i < containerPrefabs.Count; i++)
        {
            if (containerPrefabs[i] == null) continue;

            if (containerInstances[i] == null || !containerInstances[i])
            {
                containerInstances[i] = Instantiate(containerPrefabs[i]);
                containerInstances[i].name = containerPrefabs[i].name;
            }
        }
    }

    public void DespawnAll()
    {
        for (int i = 0; i < containerInstances.Count; i++)
        {
            if (containerInstances[i] != null)
            {
                DestroyImmediate(containerInstances[i]);
                containerInstances[i] = null;
            }
        }
        containerInstances.Clear();
    }
}