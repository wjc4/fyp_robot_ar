using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnHandler : MonoBehaviour
{

    public float respawntime = 2f;
    // Access-point for Box script.
    public static RespawnHandler instance;
    private static Vector3 position;
    private static Quaternion rotation;

    // Spawn the box when the game Starts.
    void Start()
    {
        instance = this;
    }

    IEnumerator SpawnBox()
    {
        //gameObject.transform.localPosition = new Vector3(0f, 0f, 0f);
        //gameObject.transform.localRotation = Quaternion.identity;
        //gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0f, 0f, 0f);
        yield return new WaitForSeconds(respawntime);
        instance.Appear();

    }

    public static void Respawn()
    {
        instance.Disappear();
        instance.StartCoroutine(instance.SpawnBox());
    }

    protected virtual void Disappear()
    {
        gameObject.GetComponent<Renderer>().enabled = false;
        gameObject.GetComponent<Collider>().enabled = false;
        gameObject.GetComponent<Canvas>().enabled = false;
    }

    protected virtual void Appear()
    {
        gameObject.GetComponent<Renderer>().enabled = true;
        gameObject.GetComponent<Collider>().enabled = true;
        gameObject.GetComponent<Canvas>().enabled = true;
    }
}