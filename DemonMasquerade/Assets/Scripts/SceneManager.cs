using UnityEngine;
using UnityEngine.SceneManagement;

public class StateManager : MonoBehaviour
{
    private GameObject titleStuff;
    private GameObject creditsStuff;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Scene currentScene = SceneManager.GetActiveScene();

        if (currentScene.name == "Title")
        {
            titleStuff = GameObject.Find("TitleStuff");
            creditsStuff = GameObject.Find("CreditsStuff");

            creditsStuff.SetActive(false);
        }
    }

    void Update()
    {
        Cursor.lockState = CursorLockMode.None;
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Arena_new");
    }

    public void ViewCredits()
    {
        titleStuff.SetActive(false);
        creditsStuff.SetActive(true);
    }

    public void BackToTitle()
    {
        creditsStuff.SetActive(false);
        titleStuff.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
