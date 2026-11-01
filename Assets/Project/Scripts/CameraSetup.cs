using UnityEngine;
using Unity.Cinemachine;
public class CameraSetup : MonoBehaviour
{
    private CinemachineCamera cam;
    public static CameraSetup Instance { get; private set; }

    private void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        cam = GetComponent<CinemachineCamera>();
    }

    public void SetUp(Transform target)
    {
        if(cam.Target.TrackingTarget == null)
            cam.Target.TrackingTarget = target;
    }
}
