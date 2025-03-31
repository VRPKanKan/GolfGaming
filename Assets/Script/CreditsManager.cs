using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsManager : MonoBehaviour
{
    public void BackToGameOver()
    {
        SceneManager.LoadScene("GameOverScene");
    }
}
