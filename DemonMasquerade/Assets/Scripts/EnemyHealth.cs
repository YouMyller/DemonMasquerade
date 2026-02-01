using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    
    public int health;

    [SerializeField]
    private GameObject maskCollectible;

    [SerializeField]
    int spawnChance;
    int maxSpawnChance = 100;
    int minSpawnChance = 0;

    public GameObject HitSFXHolder;
    public AudioSource HitSFX;

    public GameObject MonsterDiesSFXHolder;
    public AudioSource MonsterDiesSFX;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        HitSFXHolder = GameObject.FindGameObjectWithTag("HitSFX");
        HitSFX = HitSFXHolder.GetComponent<AudioSource>();

        MonsterDiesSFXHolder = GameObject.FindGameObjectWithTag("MonsterDeadSFX");
        MonsterDiesSFX = MonsterDiesSFXHolder.GetComponent<AudioSource>();

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
        {
            KillEnemy();
        }
            
        else
        {
            HitSFX.Play();
        }
    }

    private void KillEnemy()
    {
        int spawnValue = Random.Range(minSpawnChance, maxSpawnChance);

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


        //MonsterDiesSFX.Play();
        //Destroy(transform.parent.gameObject);
        transform.parent.gameObject.SetActive(false);
    }

    public void SetHealth()
    {
        health = 3;
    }
}
