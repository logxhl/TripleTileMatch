using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class toolSpawnParentContainer : MonoBehaviour
{
    public GameObject containerFloor1;
    public GameObject containerFloor2;
    public GameObject containerFloor3;
    public GameObject containerFloor4;
    public GameObject containerFloor5;
    public GameObject containerFloor1Instance;
    public GameObject containerFloor2Instance;
    public GameObject containerFloor3Instance;
    public GameObject containerFloor4Instance;
    public GameObject containerFloor5Instance;
    public void SpawnContainer()
    {
        if (containerFloor1Instance == null || !containerFloor1Instance)
        {
            containerFloor1Instance = Instantiate(containerFloor1);
            containerFloor1Instance.name = containerFloor1.name;
        }
        if (containerFloor2Instance == null || !containerFloor2Instance)
        {
            containerFloor2Instance = Instantiate(containerFloor2);
            containerFloor2Instance.name = containerFloor2.name;
        }
        if (containerFloor3Instance == null || !containerFloor3Instance)
        {
            containerFloor3Instance = Instantiate(containerFloor3);
            containerFloor3Instance.name = containerFloor3.name;
        }
        if (containerFloor4Instance == null || !containerFloor4Instance)
        {
            containerFloor4Instance = Instantiate(containerFloor4);
            containerFloor4Instance.name = containerFloor4.name;
        }
        if (containerFloor5Instance == null || !containerFloor5Instance)
        {
            containerFloor5Instance = Instantiate(containerFloor5);
            containerFloor5Instance.name = containerFloor5.name;
        }
    }
}
