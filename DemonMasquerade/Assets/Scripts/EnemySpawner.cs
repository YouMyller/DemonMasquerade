using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemy;
    private float timerToSpawn;
    private float spawnNow;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timerToSpawn = 3;
    }

    // Update is called once per frame
    void Update()
    {
        timerToSpawn += Time.deltaTime;


        if(timerToSpawn >= spawnNow)
        {
            GameObject enemyInstance;
            enemyInstance = Instantiate(enemy, this.transform.position, this.transform.rotation);

            float NewSpawnTime = UnityEngine.Random.Range(5, 10);

            spawnNow = NewSpawnTime;
            timerToSpawn = 0;
        }
    }
}
