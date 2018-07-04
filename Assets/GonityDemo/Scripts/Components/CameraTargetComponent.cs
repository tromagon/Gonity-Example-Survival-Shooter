using Gonity;
using UnityEngine;

public enum CameraTargetKey
{
    Target
}

public class CameraTargetComponent : ECSComponentMap
{
    public Vector3 offset;
}