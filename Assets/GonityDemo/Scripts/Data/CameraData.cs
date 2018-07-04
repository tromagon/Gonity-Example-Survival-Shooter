using UnityEngine;

[CreateAssetMenu(fileName = "camera_data", menuName = "Data/Camera", order = 1)]
public class CameraData : ScriptableObject, ICameraData
{
    public float smoothing;
    public float camRayLength;

    float ICameraData.smoothing { get { return smoothing; } }
    float ICameraData.camRayLength { get { return camRayLength; } }
}