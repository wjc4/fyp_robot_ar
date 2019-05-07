using UnityEngine;
using System.Collections;

public class ObjectTargetColorHandler : MonoBehaviour
{
    void Start()
    {
    }

    public void ToGreen()
    {
        var particleColorComponents = GetComponentsInChildren<ParticleColorHandler>(true);
        var materialColorComponents = GetComponentsInChildren<MaterialColorHandler>(true);
        foreach (var component in particleColorComponents)
            component.ToGreen();
        foreach (var component in materialColorComponents)
            component.ToGreen();

        foreach (Transform child in transform)
        {
            //implement regex
            if (child.name.Contains("Cube"))
            {
                Appear(child.gameObject);
            }
        }
    }

    public void ToRed()
    {
        var particleColorComponents = GetComponentsInChildren<ParticleColorHandler>(true);
        var materialColorComponents = GetComponentsInChildren<MaterialColorHandler>(true);
        foreach (var component in particleColorComponents)
            component.ToRed();
        foreach (var component in materialColorComponents)
            component.ToRed();

        foreach (Transform child in transform)
        {
            //implement regex
            if (child.name.Contains("Cube"))
            {
                Disappear(child.gameObject);
            }
        }
    }

    protected virtual void Disappear(GameObject obj)
    {
        obj.SetActive(false);
    }

    protected virtual void Appear(GameObject obj)
    {
        obj.SetActive(true);
    }
}
