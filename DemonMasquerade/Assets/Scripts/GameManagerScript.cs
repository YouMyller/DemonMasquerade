using UnityEngine;

public class GameManagerScript : MonoBehaviour
{

    public float speedMultiplier;
    public float reverseMovementMultiplier; //either 1 or -1
    public float JumpHeightMultiplier;
    private int maxHP;
    private bool spreadshot;
    //private float firerate;
    //private bool bulletSize;

    [SerializeField]
    private GameObject Player;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //speedMultiplier = 1;
        //reverseMovementMultiplier = 1;
        //JumpHeightMultiplier = 1;

    }

    // Update is called once per frame
    void Update()
    {
        //if (Player.activeInHierarchy == false)
          //  Player.SetActive(true);
    }
}
