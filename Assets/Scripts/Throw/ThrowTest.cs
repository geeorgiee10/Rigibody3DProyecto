using UnityEngine;
using UnityEngine.InputSystem;

public class ThrowTest : MonoBehaviour
{
    [Header("Referencias")]
    public Camera cam;
    public Transform holdPoint;
    public LineRenderer line;
    public PowerBar powerBar;


    [Header("Fuerza")]
    public float maxForce = 15f;
    private float force;
    private bool charging;

    [Header("Trayectoria")]
    public int points = 25;
    public float timeStep = 0.1f;

    private Rigidbody heldObject;

    private enum State { Aiming, Power};
    private State currentState = State.Aiming;

    void Start()
    {
        line.maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
    }


    void Update()
    {
        if (Mouse.current == null) return;

        //Aim();
        if(currentState == State.Aiming)
        {
           // Click: intentar coger
            if (Mouse.current.leftButton.wasPressedThisFrame)
            {
                if (heldObject == null)
                    TryPickUp();

                charging = true;
            }

            // Mantener: cargar + parábola
            if (Mouse.current.leftButton.isPressed && charging && heldObject != null)
            {
                DrawTrajectory();
            }

            // Soltar: lanzar
            if (Mouse.current.leftButton.wasReleasedThisFrame && heldObject != null)
            {
                currentState = State.Power;
                powerBar.StartPower();
                line.positionCount = 0;
            } 
        } 

        else if (currentState == State.Power)
        {
            // Cuando haces click para elegir fuerza
            if (!powerBar.IsActive)
            {
                float finalForce = powerBar.CurrentForce;

                Throw(finalForce);

                powerBar.StopPower();
                currentState = State.Aiming;
            }
        }   
        
    }

    // Apuntar con ratón (Input System)
    /*void Aim()
    {
        Vector2 delta = Mouse.current.delta.ReadValue();
        transform.Rotate(-delta.y * 0.1f, delta.x * 0.1f, 0f);
    }*/

    // COGER OBJETO
    void TryPickUp()
    {
        line.maskInteraction = SpriteMaskInteraction.VisibleOutsideMask;

        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));

        if (Physics.Raycast(ray, out RaycastHit hit, 3f))
        {
            if (hit.collider.CompareTag("Pickable"))
            {
                heldObject = hit.rigidbody;

                // 🔒 desactivar física
                heldObject.useGravity = false;
                heldObject.isKinematic = true;

                Physics.IgnoreCollision(
                    heldObject.GetComponent<Collider>(),
                    GetComponent<Collider>(),
                    true
                );


                // mover a mano
                heldObject.transform.SetParent(holdPoint);
                heldObject.transform.localPosition = Vector3.zero;
                heldObject.transform.localRotation = Quaternion.identity;

                // limpiar movimiento
                heldObject.linearVelocity = Vector3.zero;
                heldObject.angularVelocity = Vector3.zero;
            }
        }
    }


    // DIBUJAR PARÁBOLA
    void DrawTrajectory()
    {
        Vector3 startPos = holdPoint.position;

        Vector3 direction = Quaternion.Euler(
            cam.transform.eulerAngles.x,
            cam.transform.eulerAngles.y,
            0
        ) * Vector3.forward;

        float previewForce = maxForce * 0.7f; 

        Vector3 velocity = direction * previewForce;

        Vector3 gravity = Physics.gravity;

        Vector3[] positions = new Vector3[points];

        for (int i = 0; i < points; i++)
        {
            float t = i * timeStep;

            positions[i] = startPos +
                        velocity * t +
                        0.5f * gravity * t * t;
        }

        line.positionCount = points;
        line.SetPositions(positions);
    }

    // LANZAR OBJETO
    void Throw(float force)
    {
        if (heldObject == null) return;

        heldObject.transform.SetParent(null);

        heldObject.isKinematic = false;
        heldObject.useGravity = true;

        Physics.IgnoreCollision(
            heldObject.GetComponent<Collider>(),
            GetComponent<Collider>(),
            false
        );

        Vector3 direction = cam.transform.forward;

        if(LevelManager.Instance.tipoNivel == LevelManager.TipoNivel.Espacio)
        {
            float multiplier = Physics.gravity.y > -5f ? 1.5f : 1f;
        
            heldObject.AddForce(direction * force * multiplier, ForceMode.Impulse);
        }
        else
        {
            heldObject.AddForce(direction * force, ForceMode.Impulse);
        }

        


        heldObject = null;
    }
}