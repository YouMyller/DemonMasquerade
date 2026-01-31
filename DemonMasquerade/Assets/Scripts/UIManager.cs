using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    private int playerHealth = 100;
    private int score;

    [SerializeField]
    private TMP_Text scoreText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        scoreText.text = playerHealth.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HurtPlayer()
    {
        playerHealth -= 1;
        
        scoreText.text = playerHealth.ToString();
        //print(playerHealth);
    }
}
