using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxHandler : MonoBehaviour
{

    // i think unity replaces this with the variables passed in on the unity editor
    public float despawntime = 0f;
    public float respawntime = 3f;


    private static Vector3 position;
    private static Quaternion rotation;

    void Start()
    {
        position = transform.position;
        rotation = transform.rotation;
    }


    void OnCollisionEnter(Collision col)
    {
        StartCoroutine(Respawn(respawntime));
    }

    IEnumerator Respawn(float timeToRespawn)
    {
        new WaitForSeconds(timeToRespawn);

        yield return true;
        //Destroy(gameObject);
    }

    private void Update()
    {

    }
}