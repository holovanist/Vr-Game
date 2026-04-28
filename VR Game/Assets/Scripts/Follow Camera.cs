using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public GameObject Camera;
    public void Update()
    {
        FollowCam();
    }
    public void FollowCam()
    {
        Vector3 rotation = transform.eulerAngles;
        rotation.y = Camera.transform.eulerAngles.y;
        transform.eulerAngles = rotation;
            Quaternion targetRot = Camera.transform.rotation;
            Vector3 euler = targetRot.eulerAngles;
            transform.rotation = Quaternion.Euler(0f, euler.y, 0f);
    }
}
