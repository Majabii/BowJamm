using UnityEngine;

public class XROriginManager : MonoBehaviour
{
    private static Transform xrOriginTransform;

    void Awake()
    {
        // Zorg ervoor dat we de XR Origin transform bijhouden in de scene
        xrOriginTransform = this.transform;
    }

    public static Transform GetXROriginTransform()
    {
        return xrOriginTransform;
    }
}
