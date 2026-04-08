using UnityEngine;

public class ResponsiveCamera : MonoBehaviour
{
    public Transform leftLimit;
    public Transform rightLimit;

    void Start()
    {
        UpdateCameraSize();
    }

    void UpdateCameraSize()
    {
        float worldWidth = Vector3.Distance(leftLimit.position, rightLimit.position);
        float aspectRatio = Camera.main.aspect;
        float targetSize = (worldWidth / (2f * aspectRatio));

        Camera.main.orthographicSize = Mathf.Max(targetSize, Camera.main.orthographicSize);

        // Căn giữa camera giữa hai điểm
        Vector3 midPoint = (leftLimit.position + rightLimit.position) / 2f;
        Camera.main.transform.position = new Vector3(midPoint.x, midPoint.y, Camera.main.transform.position.z);
    }
}