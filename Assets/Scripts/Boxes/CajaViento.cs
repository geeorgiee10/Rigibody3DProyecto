using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CajaViento : MonoBehaviour
{
    private Rigidbody rb;

    [Header("Ajustes")]
    public float multiplicadorViento = 1f;
    public float velocidadMinima = 0.1f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        
        if (LevelManager.Instance == null) return;

        
        if (LevelManager.Instance.tipoNivel != LevelManager.TipoNivel.Viento) return;

        
        if (rb.isKinematic) return;

        
        if (rb.linearVelocity.magnitude < velocidadMinima) return;

        Vector3 viento = LevelManager.Instance.VientoActual;

        
        rb.AddForce(viento * multiplicadorViento, ForceMode.Force);
        
    }
}