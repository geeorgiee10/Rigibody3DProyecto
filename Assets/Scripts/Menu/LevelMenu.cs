using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelMenu : MonoBehaviour
{
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
        SceneManager.LoadScene("Wind Level");
    }

    public void VolverMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }
}