using UnityEngine;

public class ParticleColorHandler : MonoBehaviour
{
    private ParticleSystem ps;
    public float rValue = 0.0F;
    public float gValue = 0.0F;
    public float bValue = 0.0F;
    public float hSliderValueA = 1.0F;

    void Start()
    {
        ps = GetComponent<ParticleSystem>();
    }

    void Update()
    {
        var main = ps.main;
        main.startColor = new Color(rValue, gValue, bValue, hSliderValueA);
    }

    public void ToRed()
    {
        rValue = 1.0f;
        gValue = 0.0f;
        bValue = 0.0f;
    }

    public void ToGreen()
    {
        rValue = 0.0f;
        gValue = 1.0f;
        bValue = 0.0f;
    }
}
