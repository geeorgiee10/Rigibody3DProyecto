using UnityEngine;

public class LevelManager : MonoBehaviour
{   

    public static LevelManager Instance { get; private set; }

    public void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    public enum TipoNivel
    {
        Normal,
        Espacio,
        Viento
    }

    public TipoNivel tipoNivel;

    [Header("Viento")]
    public float tiempoCalma = 3f;
    public float tiempoRafaga = 2f;

    public float fuerzaViento = 10f;

    private float timer;
    private bool enRafaga = false;
    private Vector3 direccionViento;

    public Vector3 VientoActual { get; private set; }

    void Start()
    {
        AplicarGravedad();

        if (tipoNivel == TipoNivel.Viento)
        {
            timer = tiempoCalma; 
            VientoActual = Vector3.zero;
        }
    }

    void Update()
    {
        if(tipoNivel != TipoNivel.Viento) return;

        timer -= Time.deltaTime;

        if(timer <= 0f)
        {
            enRafaga = !enRafaga;

            if(enRafaga)
            {
                NuevaRafaga();
                timer = tiempoRafaga;
            } 
            else
            {
                VientoActual = Vector3.zero;
                timer = tiempoCalma;
            }
        }
    }

    void NuevaRafaga()
    {
        float dir = Random.value > 0.5f ? 1f : -1f;

        direccionViento = new Vector3(dir, 0, 0);

        VientoActual = direccionViento * fuerzaViento;
    }

    void AplicarGravedad()
    {
        if (tipoNivel == TipoNivel.Normal)
        {
            Physics.gravity = new Vector3(0, -9.81f, 0);
        }
        else if (tipoNivel == TipoNivel.Espacio)
        {
            Physics.gravity = new Vector3(0, -2f, 0);
        }
        else if (tipoNivel == TipoNivel.Viento)
        {
            Physics.gravity = new Vector3(0, -9.81f, 0);
            
        } 
    }
}