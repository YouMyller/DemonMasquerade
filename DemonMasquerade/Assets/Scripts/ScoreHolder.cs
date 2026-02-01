using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ScoreHolder : MonoBehaviour
{
    private TMP_Text scoreText;
    private int finalScore;
    private Scene currentScene;

    bool scoreDrawn;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        currentScene = SceneManager.GetActiveScene();

        if (currentScene.name == "GameOver" && scoreDrawn == false)
            DrawScore();

        if (currentScene.name == "Arena_new")
            scoreDrawn = false;
    }

    public void UpdateScore(int score)
    {
        scoreDrawn = false;
        finalScore = score;
        print(finalScore);
    }

    private void DrawScore()
    {
        scoreText = GameObject.Find("ScoreText").GetComponent<TMP_Text>();
        scoreText.text = finalScore.ToString();
        scoreDrawn = true;
        Destroy(gameObject);
    }
}
