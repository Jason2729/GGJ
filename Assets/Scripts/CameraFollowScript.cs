using UnityEngine;

public class CameraFollowScript : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset;
    [SerializeField] private Transform leftBoundary, rightBoundary, topBoundary, bottomBoundary;
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleCameraUpdate();
    }

    void HandleCameraUpdate()
    {
        Vector3 newPosition = transform.position;
        newPosition.x = Mathf.Clamp(target.transform.position.x, leftBoundary.position.x, rightBoundary.position.x);
        newPosition.y = target.transform.position.y;
        mainCamera.transform.position = newPosition;

    }
}
