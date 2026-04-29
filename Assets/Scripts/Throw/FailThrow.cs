using System.Collections;
using UnityEngine;

public class FailThrow : MonoBehaviour
{
    public Transform areaRescate; //Area donde van caer las cajas si fallas y caen al suelo

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
            StartCoroutine(RescueBox(other));
        }
    }

    IEnumerator RescueBox(Collider other)
    {
        yield return new WaitForSeconds(2f); 

        other.transform.position = areaRescate.position;
    }
}
