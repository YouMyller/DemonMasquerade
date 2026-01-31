using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    private int playerHealth = 100;
    private int score = 0;

    [SerializeField]
    private TMP_Text scoreText;

    [SerializeField]
    private TMP_Text healthText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        healthText.text = playerHealth.ToString();
        scoreText.text = score.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HurtPlayer()
    {
        playerHealth -= 1;
        
        healthText.text = playerHealth.ToString();
        //print(playerHealth);
    }

    public void AddScore()
    {
        score += 1;
        scoreText.text = score.ToString();
    }
}
