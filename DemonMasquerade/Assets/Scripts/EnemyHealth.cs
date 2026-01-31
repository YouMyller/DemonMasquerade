using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField]
    private int health;

    [SerializeField]
    private GameObject maskCollectible;

    [SerializeField]
    int spawnChance;
    int maxSpawnChance = 100;
    int minSpawnChance = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (health == 0)
            health = 1;

        if (spawnChance == 0)
            spawnChance = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            HurtEnemy();
            other.gameObject.SetActive(false);
        }
    }

    private void HurtEnemy()
    {
        health -= 1;

        if (health <= 0)
            KillEnemy();
    }

    private void KillEnemy()
    {
        int spawnValue = Random.Range(minSpawnChance, maxSpawnChance);

        print(spawnValue);

        if (spawnValue <= spawnChance)
        {
            Vector3 pos = transform.parent.position;
            //Instantiate(maskCollectible, pos, Quaternion.identity);

            GameObject Mask = ObjectPool.SharedInstance.GetMask();
            Mask.transform.position = transform.parent.position;
            //Ammo.transform.rotation = pos.rotation;
            //Ammo.transform.localScale = transform.localScale / 2;
            Mask.SetActive(true);
        }

        //Destroy(transform.parent.gameObject);
        transform.parent.gameObject.SetActive(false);
    }
}
