using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText;

    private float elapsedTime;
    bool isLevelFinished = false;
    void Update()
    {
        if (!isLevelFinished)
        {
            elapsedTime += Time.deltaTime;
            int minutes = Mathf.FloorToInt(elapsedTime / 60);
            int seconds = Mathf.FloorToInt(elapsedTime % 60);
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }
    public void StopTimer()
    {
        isLevelFinished = true;
        PlayerPrefs.SetFloat("FinalTime", elapsedTime);
        PlayerPrefs.Save();
        SceneManager.LoadScene("GameOverScene");
    }
}
