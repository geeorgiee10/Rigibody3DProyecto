using UnityEngine;
using UnityEngine.Playables;

public class TruckTrailer : MonoBehaviour
{

    public Transform initialStackPoint;

    public Transform stackContainer;
    public Vector3 offset = new Vector3(0.5f, 0f, 0.5f);
    private int count = 0;

    public Cinematica  cinematicaIrse;
    public int maxObjectos = 3;

    private bool cinematicaEmpezada = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Pickable"))
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();
            if(rb != null)            
            {

                Collider col = other.GetComponent<Collider>();

                Collider[] truckColliders = GetComponentsInParent<Collider>();
                foreach (Collider truckCol in truckColliders)
                {
                    Physics.IgnoreCollision(col, truckCol);
                }

                col.enabled = false;

                rb.linearVelocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
                rb.isKinematic = true;
                rb.useGravity = false;

                other.transform.SetParent(stackContainer);

                int row = count / 5;
                int column = count % 5;

                Vector3 position = initialStackPoint.position + new Vector3(column * offset.x, row * offset.y, 0);

                other.transform.position = position;
                other.transform.rotation = Quaternion.identity;

                count++;

                if(count >= maxObjectos && !cinematicaEmpezada)
                {
                    Debug.Log("Iniciando cinemática de salida...");
                    cinematicaIrse.PlayCinematica(); // Llamamos al nuevo método
                    cinematicaEmpezada = true;
                }
            }
        }
    }
}

