using UnityEngine;

public class UIManager : MonoBehaviour
{
    private int playerHealth = 100;
    private int score;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HurtPlayer()
    {
        playerHealth -= 1;
        //print(playerHealth);
    }
}
