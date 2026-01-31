using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float speed;

    [SerializeField]
    private Transform player;

    [SerializeField]
    private UIManager uiManager;
    //[SerializeField]
    //private Rigidbody rigidbody;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();

        //TODO:
        //find player and kill
        //die when shot at mask - this should be a separate script which is attached to the mask
        //spawn collectible - this also probs goes to mask
    }

    private void Move()
    {
        float velocity = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, player.position, velocity);
        //print(transform.position);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
            uiManager.HurtPlayer();
    }
}
