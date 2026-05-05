using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Rigidbody rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(!animator) animator = GetComponent<Animator>();
        if(!rb) rb = GetComponent<Rigidbody>();

    }

    public void AnimacionSaltar1()
    {
        animator.SetTrigger("Saltar");
    }

    void FixedUpdate()
    {
        Vector3 vWorld = rb.linearVelocity;
        Vector3 vLocal = transform.InverseTransformDirection(vWorld);

        animator.SetFloat("X", vLocal.x);
        animator.SetFloat("Y", vLocal.z);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
