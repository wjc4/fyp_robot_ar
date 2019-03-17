using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPuller : MonoBehaviour
{
    // i think unity replaces this with the variables passed in on the unity editor
    public float pullRadius = 2;
    public float pullForce = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach (Collider collider in Physics.OverlapSphere(transform.position, pullRadius)) {
            // calculate direction from target to me
            Vector3 forceDirection = transform.position - collider.transform.position;

            // apply force on target towards me
            //collider.GetComponent<Rigidbody>().AddForce(forceDirection.normalized * pullForce * Time.fixedDeltaTime);
            //collider.GetComponent<Rigidbody>().velocity = forceDirection.normalized * pullForce;
        }



    }
}
