using BBO.BBO.MonsterManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonstersSpawner : MonoBehaviour
{
    [SerializeField]
    private WaveManager waveManager = default;

    [SerializeField]
    private GameObject container = default;

    [SerializeField]
    private Transform[] spawnPoints = default;

    private Queue<MonsterCharacter> monsters = default;
    private float spawnDelay = default;
    private int maxSpawnMonsterNumber = default;
    private WaitForSecondsRealtime waitTime = default;

    public void SetSpawnerConfig(Queue<MonsterCharacter> monsters, WaveConfig config)
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
                MonsterCharacter monster = Instantiate(monsters.Dequeue(), pointToSpawn, Quaternion.identity, container.transform);
                monster.Dead += waveManager.OnMonsterDead;
            }

            yield return waitTime;
        }
    }
}
