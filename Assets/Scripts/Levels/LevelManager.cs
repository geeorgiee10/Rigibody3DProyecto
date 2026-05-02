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
        Espacio
    }

    public TipoNivel tipoNivel;

    void Start()
    {
        AplicarGravedad();
    }

    void AplicarGravedad()
    {
        if (tipoNivel == TipoNivel.Normal)
        {
            Physics.gravity = new Vector3(0, -9.81f, 0);
        }
        else if (tipoNivel == TipoNivel.Espacio)
        {
            Physics.gravity = new Vector3(0, -2f, 0); // gravedad baja
        }
    }
}