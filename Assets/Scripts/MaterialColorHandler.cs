using UnityEngine;
using System.Collections;

public class MaterialColorHandler : MonoBehaviour
{
    public float rValue = 0.0F;
    public float gValue = 0.0F;
    public float bValue = 0.0F;
    private Material mat;

    void Start()
    {
        mat = GetComponent<Renderer>().material;
    }

    void Update()
    {
        mat.color = new Color(rValue, gValue, bValue);
    }

    public void ToRed()
    {
        rValue = 0.5f;
        gValue = 0.0f;
        bValue = 0.0f;
    }

    public void ToGreen()
    {
        rValue = 0.0f;
        gValue = 0.5f;
        bValue = 0.0f;
    }
}
