using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class Cinematica : MonoBehaviour
{
    public PlayableDirector director;
    public GameObject cineObj;
    public GameObject gameplayObj;
    
    [Header("Configuración")]
    public bool reproducirAlInicio = false; 

    [Header("Cambio de escena")]
    public bool cambiarEscenaAlFinal = false;


    private float duracionCinematica;
    private float t;
    private bool isPlaying = false;

    void Start()
    {
        duracionCinematica = (float)director.duration;

        if (reproducirAlInicio) 
        {
            PlayCinematica();
        }
        else 
        {
            cineObj.SetActive(false);
            isPlaying = false;
        }
    }

    public void PlayCinematica()
    {
        t = 0; 
        isPlaying = true;
        cineObj.SetActive(true);
        if(gameplayObj != null) gameplayObj.SetActive(false); 
        
        director.Play();
    }

    void Update()
    {
        if (!isPlaying) return;

        t += Time.deltaTime;

        if (t >= duracionCinematica)
        {
            TerminarCinematica();
        }
    }

    void TerminarCinematica()
    {
        isPlaying = false;
        director.Stop(); // Aseguramos que el director se detenga
        cineObj.SetActive(false);
        if(gameplayObj != null) gameplayObj.SetActive(true);

        if (cambiarEscenaAlFinal)
        {
            SceneManager.LoadScene("MenuScene");
        }
    }
}