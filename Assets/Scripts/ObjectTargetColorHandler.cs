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
    }

    public void ToRed()
    {
        var particleColorComponents = GetComponentsInChildren<ParticleColorHandler>(true);
        var materialColorComponents = GetComponentsInChildren<MaterialColorHandler>(true);
        foreach (var component in particleColorComponents)
            component.ToRed();
        foreach (var component in materialColorComponents)
            component.ToRed();
    }
}
