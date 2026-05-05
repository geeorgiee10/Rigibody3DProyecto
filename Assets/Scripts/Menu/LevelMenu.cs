using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelMenu : MonoBehaviour
{
    void Start()
    {
        Time.timeScale = 1f;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void NormalLevel()
    {
        SceneManager.LoadScene("Normal Level");
    }

    public void SpaceLevel()
    {
        SceneManager.LoadScene("Space Level");
    }

    public void WindLevel()
    {
        SceneManager.LoadScene("WindLevel");
    }

    public void VolverMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }
}