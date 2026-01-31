using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float speed;

    //[SerializeField]
    private Transform player;

    //[SerializeField]
    private UIManager uiManager;

    private GameObject playerGO;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
        playerGO = GameObject.FindWithTag("Player");
        player = playerGO.GetComponent<Transform>();

    }

    // Update is called once per frame
    void Update()
    {
        Turn();
        Move();

        if(playerGO == null)
        {
            Debug.Log("No Player");
        }

        if(player == null)
        {
            Debug.Log("No player Transform");
        }

        //TODO:
        //die when shot at mask - this should be a separate script which is attached to the mask
        //spawn collectible - this also probs goes to mask
    }

    private void Turn()
    {
        Vector3 targetDirection = player.position - transform.position;

        float singleStep = speed * Time.deltaTime;

        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);

        //Debug.DrawRay(transform.position, newDirection, Color.red);

        transform.rotation = Quaternion.LookRotation(newDirection);
    }

    private void Move()
    {
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, player.position, step);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            uiManager.HurtPlayer();
        }
           
    }
}
