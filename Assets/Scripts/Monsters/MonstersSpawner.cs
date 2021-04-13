using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MonstersSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject container = default;

    [SerializeField]
    private Transform[] spawnPoints = default;

    private Queue<GameObject> monsters = default;
    private float spawnDelay = default;
    private int maxSpawnMonsterNumber = default;
    private WaitForSecondsRealtime waitTime = default;

    public void SetSpawnerConfig(Queue<GameObject> monsters, WaveConfig config)
    {
        this.monsters = monsters;
        spawnDelay = config.SpawnDelay;
        maxSpawnMonsterNumber = config.MaxSpawnMonstersAmount;
    }

    public void StartSpawn()
    {
        if (container == null)
        { 
            container = new GameObject("MonstersContainer");
        }

        waitTime = new WaitForSecondsRealtime(spawnDelay);
        StartCoroutine(SpawnMonster());
    }

    private IEnumerator SpawnMonster()
    {
        while (monsters.Count > 0)
        {
            for (int i = 0; i < Random.Range(1, maxSpawnMonsterNumber); i++)
            {
                var pointSelected = Random.Range(0, spawnPoints.Length);
                var pointToSpawn = spawnPoints[pointSelected].position;
                Instantiate(monsters.Dequeue(), pointToSpawn, Quaternion.identity, container.transform);
            }

            yield return waitTime;
        }
    }
}
