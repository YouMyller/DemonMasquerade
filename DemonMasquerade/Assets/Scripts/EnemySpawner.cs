using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    //public GameObject enemy;
    private float timerToSpawn;
    private float spawnNow;

    private Vector3 boost;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timerToSpawn = 3;
        boost = new Vector3(0, 1, 0);
    }

    // Update is called once per frame
    void Update()
    {
        timerToSpawn += Time.deltaTime;


        if(timerToSpawn >= spawnNow)
        {
            //GameObject enemyInstance;
            //enemyInstance = Instantiate(enemy, this.transform.position, this.transform.rotation);

            GameObject Enemy = ObjectPool.SharedInstance.GetEnemy();
            Enemy.transform.position = this.transform.position + boost;
            //Enemy.transform.rotation = this.transform.rotation;
            Enemy.GetComponentInChildren<EnemyHealth>().SetHealth();

            //Ammo.transform.localScale = transform.localScale / 2;
            Enemy.SetActive(true);

            float NewSpawnTime = UnityEngine.Random.Range(5, 10);

            spawnNow = NewSpawnTime;
            timerToSpawn = 0;
        }
    }
}
