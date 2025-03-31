using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI finalTimeText;

    void Start()
    {
        float finalTime = PlayerPrefs.GetFloat("FinalTime", 0);
        int minutes = Mathf.FloorToInt(finalTime / 60);
        int seconds = Mathf.FloorToInt(finalTime % 60);
        finalTimeText.text = $"{minutes:00}:{seconds:00}";
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void GoToCredits()
    {
        SceneManager.LoadScene("Credit");
    }

    public void BackToGameOver()
    {
        SceneManager.LoadScene("GameOverScene");
    }
}