using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField]
    private int health;

    [SerializeField]
    private GameObject maskCollectible;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (health == 0)
            health = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bullet")
            HurtEnemy();
    }

    private void HurtEnemy()
    {
        health -= 1;

        if (health <= 0)
            KillEnemy();
    }

    private void KillEnemy()
    {
        Vector3 pos = transform.parent.position;
        Instantiate(maskCollectible, pos, transform.rotation);

        Destroy(transform.parent.gameObject);
    }
}
