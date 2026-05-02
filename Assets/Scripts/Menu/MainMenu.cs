using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject menuControles;

    void Start()
    {
        menuControles.SetActive(false);
    }

    public void Jugar()
    {
        SceneManager.LoadScene("MenuNiveles"); 
    }

    public void Controles()
    {
        menuControles.SetActive(true);
    }

    public void CerrarControles()
    {
        menuControles.SetActive(false);
    }

    public void Salir()
    {
        Application.Quit();
    }
}