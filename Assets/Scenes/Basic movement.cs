using UnityEngine;

public class SphereControl : MonoBehaviour
{
    public float rotationSpeed = 100f;
    public float zoomSpeed = 1f;
    private Transform targetObject;

    void Start()
    {
        targetObject = transform;
    }

    void Update()
    {
        HandleRotation();

        if (Input.GetKey(KeyCode.Z))
        {
            ZoomIn();
        }
        else if (Input.GetKey(KeyCode.X))
        {
            ZoomOut();
        }
    }

    private void HandleRotation()
    {
        Vector3 rotation = Vector3.zero;

        if (Input.GetKey(KeyCode.UpArrow))
        {
            rotation = Vector3.left;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            rotation = Vector3.right;
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            rotation = Vector3.down;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            rotation = Vector3.up;
        }

        transform.Rotate(rotation * rotationSpeed * Time.deltaTime);
    }

    private void ZoomIn()
    {
        float maxScale = 3f;

        if (targetObject.localScale.x < maxScale)
        {
            targetObject.localScale += Vector3.one * zoomSpeed * Time.deltaTime;
            targetObject.localScale = Vector3.Min(targetObject.localScale, Vector3.one * maxScale);
        }
    }

    private void ZoomOut()
    {
        float minScale = 0.8f;

        if (targetObject.localScale.x > minScale)
        {
            targetObject.localScale -= Vector3.one * zoomSpeed * Time.deltaTime;
            targetObject.localScale = Vector3.Max(targetObject.localScale, Vector3.one * minScale);
        }
    }
}
