using System.Collections.Generic;
using UnityEngine;

public class TestSpawner : MonoBehaviour
{
    public GameObject[] monsterPrefabs;  
    public Transform spawnPoint;  
    private List<GameObject> spawnedMonsters = new List<GameObject>();  

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SpawnMonster(0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SpawnMonster(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SpawnMonster(2);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            SpawnMonster(3);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            ClearMonsters();
        }
    }

    void SpawnMonster(int index)
    {
        if (index >= 0 && index < monsterPrefabs.Length)
        {
            GameObject monster = Instantiate(monsterPrefabs[index], spawnPoint.position, spawnPoint.rotation);
            spawnedMonsters.Add(monster);
        }
        else
        {
            Debug.LogWarning("Invalid monster index: " + index);
        }
    }

    void ClearMonsters()
    {
        foreach (GameObject monster in spawnedMonsters)
        {
            Destroy(monster);
        }
        spawnedMonsters.Clear();
    }
}
