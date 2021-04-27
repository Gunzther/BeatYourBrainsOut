using BBO.BBO.MonsterManagement;
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

    // wave data
    private int currentWaveIndex = default;
    private WaveConfig currentWaveConfig = default;
    private Queue<MonsterCharacter> monstersQueue = default;
    private bool isCompleted => (currentDeadMonsterAmount == monsterAmount)
                     && (currentWaveIndex >= waves.Length);

    // monster amount
    private int monsterAmount = default;
    private int currentDeadMonsterAmount = default;

    // wave timer
    private bool isCountdown = false;
    private float timer = default;

    public void ActiveWave()
    {
        SetUpWave();
        spawner.StartSpawn();
        isCountdown = true;
    }

    public void ActiveNextWave()
    {
        Debug.Log($"[{nameof(WaveManager)}] Active next wave!");
        currentWaveIndex++;

        if (currentWaveIndex < waves.Length)
        {
            ActiveWave();
        }
    }

    public void OnMonsterDead()
    {
        currentDeadMonsterAmount++;

        if (currentDeadMonsterAmount == monsterAmount)
        {
            ActiveNextWave();
        }
        if (isCompleted)
        {
            Debug.Log($"[{nameof(WaveManager)}] Stage Complete!");
        }
    }

    private void Start()
    {
        monstersQueue = new Queue<MonsterCharacter>();
        ActiveWave();
    }

    private void FixedUpdate()
    {
        if (isCountdown)
        {
            timer += Time.deltaTime;
        }
        if (timer > currentWaveConfig.WaveDuration)
        {
            ActiveNextWave();
            timer = 0;
        }
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
            List<MonsterCharacter> monstersSorted = new List<MonsterCharacter>();

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

        monsterAmount = monstersQueue.Count;
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
    public MonsterCharacter Monster = default;
    public int MonsterAmount = default;
}
