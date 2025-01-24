using System;
using System.Collections;
using System.Collections.Generic;
using Leap;
using TMPro;
using UnityEngine;

public class GlobeController2 : MonoBehaviour
{
    [SerializeField]
    private Transform targetObject;

    [SerializeField]
    private float rotationSpeed = 50f;

    [SerializeField]
    private float zoomSpeed = 1f;
    
    [SerializeField]
    private TMPro.TextMeshProUGUI detectionText;

    private bool moveUp = false;
    private bool moveDown = false;
    private bool moveLeft = false;
    private bool moveRight = false;
    private bool zoomIn = false;
    private bool zoomOut = false;

    void Update()
    {
        if (targetObject == null)
        {
            Debug.LogWarning("No targetObject assigned to GlobeController.");
            return;
        }

        if (moveUp)
        {
            RotateUpward();
        }
        else if (moveDown)
        {
            RotateDownward();
        }
        else if (moveLeft)
        {
            RotateLeft();
        }
        else if (moveRight)
        {
            RotateRight();
        }
        else if (zoomIn)
        {
            ZoomIn();
        }
        else if (zoomOut)
        {
            ZoomOut();
        }
          

    }

    public void PoseDetected(string inputString)
    {
        ActivateMovement(inputString);

        if (detectionText != null)
        {
            detectionText.text = $"Pose Detected: {inputString}";
        }
    }

    public void PoseLost(string inputString)
    {
        DisactivateMovement(inputString);

        if (detectionText != null)
        {
            detectionText.text = "No pose detected";
        }
    }

    private void ActivateMovement(string inputString)
    {
        if (string.Equals(inputString, "thumbs_up", System.StringComparison.OrdinalIgnoreCase))
        {
            moveUp = true;
            moveDown = false;
            moveLeft = false;
            moveRight = false;
            zoomIn = false;
            zoomOut = false;
        }
        else if (string.Equals(inputString, "thumbs_down", System.StringComparison.OrdinalIgnoreCase))
        {
            moveDown = true;
            moveUp = false;
            moveLeft = false;
            moveRight = false;
            zoomIn = false;
            zoomOut = false;
        }
        else if (string.Equals(inputString, "point_left", System.StringComparison.OrdinalIgnoreCase))
        {
            moveDown = false;
            moveUp = false;
            moveLeft = true;
            moveRight = false;
            zoomIn = false;
            zoomOut = false;
        }
        else if (string.Equals(inputString, "point_right", System.StringComparison.OrdinalIgnoreCase))
        {
            moveDown = false;
            moveUp = false;
            moveLeft = false;
            moveRight = true;
            zoomIn = false;
            zoomOut = false;
        }
        else if (string.Equals(inputString, "zoom_in", System.StringComparison.OrdinalIgnoreCase))
        {
            moveDown = false;
            moveUp = false;
            moveLeft = false;
            moveRight = false;
            zoomIn = true;
            zoomOut = false;
        }
        else if (string.Equals(inputString, "zoom_out", System.StringComparison.OrdinalIgnoreCase))
        {
            moveDown = false;
            moveUp = false;
            moveLeft = false;
            moveRight = false;
            zoomIn = false;
            zoomOut = true;
        }

    }

    private void DisactivateMovement(string inputString)
    {
        if (string.Equals(inputString, "thumbs_up", System.StringComparison.OrdinalIgnoreCase) && moveUp)
        {
            moveUp = false;
        }
        else if (string.Equals(inputString, "thumbs_down", System.StringComparison.OrdinalIgnoreCase) && moveDown)
        {
            moveDown = false;
        }
        else if (string.Equals(inputString, "point_left", System.StringComparison.OrdinalIgnoreCase) && moveLeft)
        {
            moveLeft = false;
        }
        else if (string.Equals(inputString, "point_right", System.StringComparison.OrdinalIgnoreCase) && moveRight)
        {
            moveRight = false;
        }
        else if (string.Equals(inputString, "zoom_in", System.StringComparison.OrdinalIgnoreCase) && zoomIn)
        {
            zoomIn = false;
        }
        else if (string.Equals(inputString, "zoom_out", System.StringComparison.OrdinalIgnoreCase) && zoomOut)
        {
            zoomOut = false;
        }
    }

    private void RotateUpward()
    {
        targetObject.Rotate(Vector3.left * rotationSpeed * Time.deltaTime, Space.World);
    }

    private void RotateDownward()
    {
        targetObject.Rotate(Vector3.right * rotationSpeed * Time.deltaTime, Space.World);
    }
    private void RotateLeft()
    {
        targetObject.Rotate(Vector3.down * rotationSpeed * Time.deltaTime, Space.World);
    }
    private void RotateRight()
    {
        targetObject.Rotate(Vector3.up * rotationSpeed * Time.deltaTime, Space.World);
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
