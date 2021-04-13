using System;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    // TODO: create time'sup system by using WaveDuration in config

    [SerializeField]
    private MonstersSpawner spawner = default;

    [SerializeField]
    private WaveConfig[] waves = default;

    private int currentWaveIndex = default;
    private WaveConfig currentWaveConfig = default;
    private Queue<GameObject> monstersQueue = default;

    public void ActiveNextWave()
    {
        currentWaveIndex++;
        ActiveWave();
    }

    public void ActiveWave()
    {
        SetUpWave();
        spawner.StartSpawn();
    }

    private void Start()
    {
        // TODO: change to call ActiveWavve in GameManager
        ActiveWave();
    }

    private void SetUpWave()
    {
        currentWaveConfig = waves[currentWaveIndex];
        SetMonsterQueue(currentWaveConfig);
        spawner.SetSpawnerConfig(monstersQueue, currentWaveConfig);
    }

    private void SetMonsterQueue(WaveConfig waveConfig)
    {
        monstersQueue.Clear();
        MonsterConfig[] monsterConfigs = waveConfig.MonsterConfigs;

        if (waveConfig.IsSortSpawning)
        {
            foreach (MonsterConfig config in monsterConfigs)
            {
                for (int i = 0; i < config.MonsterAmount; i++)
                {
                    monstersQueue.Enqueue(config.Monster);
                }
            }
        }
        else
        {
            List<GameObject> monstersSorted = new List<GameObject>();

            foreach (MonsterConfig config in monsterConfigs)
            {
                for (int i = 0; i < config.MonsterAmount; i++)
                {
                    monstersSorted.Add(config.Monster);
                }
            }

            int count = monstersSorted.Count;

            for (int i = 0; i < count; i++)
            {
                int index = UnityEngine.Random.Range(0, monstersSorted.Count);
                monstersQueue.Enqueue(monstersSorted[index]);
                monstersSorted.RemoveAt(index);
            }
        }
    }
}

[Serializable]
public class WaveConfig
{
    public float WaveDuration = default;
    public float SpawnDelay = default;
    public int MaxSpawnMonstersAmount = default; // max monster amount that can spawn in the same time
    public bool IsSortSpawning = default; // spawn monster depends on MonsterConfig
    public MonsterConfig[] MonsterConfigs = default;
}

[Serializable]
public class MonsterConfig
{
    public GameObject Monster = default;
    public int MonsterAmount = default;
}
