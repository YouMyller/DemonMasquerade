using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManager : MonoBehaviour
{
    private int playerHealth = 5;
    private int score = 0;

    [SerializeField]
    private TMP_Text scoreText;

    [SerializeField]
    private TMP_Text healthText;

    private ScoreHolder scoreHolder;

    [SerializeField]
    private AudioSource playerHitSFX;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        scoreHolder = GameObject.Find("ScoreHolder").GetComponent<ScoreHolder>();
        healthText.text = playerHealth.ToString();
        scoreText.text = score.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        //Cursor.visible = true;
    }

    
    public void UpdateHealth(int updatedHealth)
    {
        playerHealth = updatedHealth;
        healthText.text = playerHealth.ToString();
    }

    public void HurtPlayer()
    {
        playerHitSFX.Play();
        playerHealth -= 1;

        //healthText.text = playerHealth.ToString();
        //print(playerHealth);

        if (playerHealth <= 0)
        {
            scoreHolder.UpdateScore(score);
            SceneManager.LoadScene("GameOver");
        }
    }

    public void AddScore()
    {
        score += 1;
        scoreText.text = score.ToString();
    }
}
