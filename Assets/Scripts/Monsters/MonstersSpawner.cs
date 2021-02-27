using UnityEngine;
using System.Collections;

public class MonstersSpawner : MonoBehaviour
{

    [SerializeField]
    private int numbersOfMonsters = default;

    [SerializeField]
    private Transform[] SpawnPoints;

    [SerializeField]
    private float _spawnDelay = default;

    private WaitForSecondsRealtime _waitTime;

    [SerializeField]
    public GameObject monster;

    private void Start()
    {
        _waitTime = new WaitForSecondsRealtime(_spawnDelay);

        StartCoroutine(SpawnMonster());
    }

    private IEnumerator SpawnMonster()
    {
        while (numbersOfMonsters >= 1)
        {
            var pointSelected = Random.Range(0, 1); //To use as index for reaching array of spawn points.
            var pointToSpawn = SpawnPoints[pointSelected].position;

            var clonePrefab = Instantiate(monster, pointToSpawn, Quaternion.identity);

            var monsterSprite = clonePrefab.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>();

            if (monsterSprite != null)
            {
                monsterSprite.flipX = pointSelected == 1;
            }

            numbersOfMonsters--;

            yield return _waitTime;
        }

        Destroy(gameObject);
    }
}
