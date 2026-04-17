using UnityEngine;
using UnityEngine.InputSystem;

public class ThrowTest : MonoBehaviour
{
    [Header("Referencias")]
    public Camera cam;
    public Transform holdPoint;
    public LineRenderer line;

    [Header("Fuerza")]
    public float maxForce = 15f;
    private float force;
    private bool charging;

    [Header("Trayectoria")]
    public int points = 25;
    public float timeStep = 0.1f;

    private Rigidbody heldObject;

    void Update()
    {
        if (Mouse.current == null) return;

        //Aim();

        // 🖱️ Click: intentar coger
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            if (heldObject == null)
                TryPickUp();

            charging = true;
        }

        // ⚡ Mantener: cargar + parábola
        if (Mouse.current.leftButton.isPressed && charging && heldObject != null)
        {
            ChargeForce();
            DrawTrajectory();
        }

        // 🚀 Soltar: lanzar
        if (Mouse.current.leftButton.wasReleasedThisFrame && charging)
        {
            Throw();
            charging = false;
            force = 0;
            line.positionCount = 0;
        }
    }

    // 🎯 Apuntar con ratón (Input System)
    /*void Aim()
    {
        Vector2 delta = Mouse.current.delta.ReadValue();
        transform.Rotate(-delta.y * 0.1f, delta.x * 0.1f, 0f);
    }*/

    // 📦 COGER OBJETO
    void TryPickUp()
    {
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


                // 📌 mover a mano
                heldObject.transform.SetParent(holdPoint);
                heldObject.transform.localPosition = Vector3.zero;
                heldObject.transform.localRotation = Quaternion.identity;

                // 🧹 limpiar movimiento
                heldObject.linearVelocity = Vector3.zero;
                heldObject.angularVelocity = Vector3.zero;
            }
        }
    }

    // ⚡ Cargar fuerza
    void ChargeForce()
    {
        force += Time.deltaTime * 10f;
        force = Mathf.Clamp(force, 0, maxForce);
    }

    // 🟢 DIBUJAR PARÁBOLA
    void DrawTrajectory()
    {
        Vector3 startPos = holdPoint.position;

        Vector3 direction = Quaternion.Euler(
            cam.transform.eulerAngles.x,
            cam.transform.eulerAngles.y,
            0
        ) * Vector3.forward; // 👈 CLAVE

        Vector3 velocity = direction * force;

        Vector3 gravity = Physics.gravity * 1.5f;

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

    // 🚀 LANZAR OBJETO
    void Throw()
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

        heldObject.AddForce(direction * force, ForceMode.Impulse);

        heldObject = null;
    }
}