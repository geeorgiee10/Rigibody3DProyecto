using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerLook : MonoBehaviour
{

    [Header("Sensitivity")]
        public float mouseSensitivity = 0.15f;

    [Header("Pitch clamp")]
        public float minPitch = -60f;
        public float maxPitch = 80f;

    private Vector2 lookInput;
    private float pitch;

    private Transform cameraTransform;
    private Camera currentCamera;
    private bool canMove = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UpdateActiveCamera();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void OnLook(InputValue value)
    {
        lookInput = value.Get<Vector2>();
    }

    private void OnEnable()
    {
        lookInput = Vector2.zero;
    }

    private void UpdateActiveCamera()
    {
        if (Camera.main != currentCamera)
        {
            currentCamera = Camera.main;
            if(currentCamera != null)
            {
                cameraTransform = currentCamera.transform;
                pitch = cameraTransform.localRotation.eulerAngles.x;
                if(pitch > 180f) pitch -= 360f;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(!canMove) return;

        UpdateActiveCamera();

        if(cameraTransform == null) return;

        //Rotacion horizontal
        float yaw = lookInput.x * mouseSensitivity;
        transform.Rotate(0, yaw, 0, Space.Self);

        //Rotacion cámara
        pitch -= lookInput.y * mouseSensitivity;
        pitch = Mathf.Clamp(pitch, minPitch, maxPitch);
        cameraTransform.localRotation = Quaternion.Euler(pitch, 0, 0);
        
    }

    public void SetCanMove(bool value)
    {
        canMove = value;
    }
}
