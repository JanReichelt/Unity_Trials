// De/Activating CodeLens: https://stackoverflow.com/questions/34356510/how-to-disable-codelens-in-vs-code#:~:text=Go%20to%20File%20%3E%20Preferences%20%3E%20Settings,Code%20Lens%20and%20disable%20it.
// Used Tutorial Part 1: https://www.youtube.com/watch?v=s09OZLsO0Tc
// Used Tutorial Part 2: https://youtu.be/hKUmzbYrztk?t=952

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownCamera : MonoBehaviour
{
    public Transform target;

    [System.Serializable]
    public class PositionSettings {
        public float distanceFromTarget = -50;
        public bool allowZoom = true;
        public float zoomSmooth = 100;
        public float zoomStep = 2;
        public float maxZoom = -30;
        public float minZoom = -60;
        public bool smoothFollow = true;
        public float smooth = 0.05f;

        [HideInInspector]
        public float newDistance = -50;
    }

    [System.Serializable]
    public class OrbitSettings {
        public float xRotation = -30;
        public float yRotation = -180;
        public bool allowOrbit = true;
        public float yOrbitSmooth = 0.5f;
        public float orbitSpeed = 5f;
    }

    [System.Serializable]
    public class InputSettings {
        // Können angepasst werden in Unity/Edit/ProjectSettings/InputManager
        public string MOUSE_ORBIT = "MouseOrbit";
        public string ZOOM = "Mouse ScrollWheel";
    }

    public PositionSettings position = new PositionSettings();
    public OrbitSettings orbit = new OrbitSettings();
    public InputSettings input = new InputSettings();

    Vector3 destination = Vector3.zero;
    Vector3 camVelocity = Vector3.zero;
    Vector3 currentMousePosition = Vector3.zero;
    Vector3 PreviousMousePosition = Vector3.zero;
    float mouseOrbitInput, zoomInput;

    void Start() {
        SetCameraTarget(target);
        if (target) {
            MoveToTarget();
        }
    } 

    public void SetCameraTarget(Transform t) {
        target = t;
        if (target == null) {
            Debug.LogError("Your Camera needs a target.");
        }
    }

    void GetInput() {
        mouseOrbitInput = Input.GetAxisRaw(input.MOUSE_ORBIT);
        zoomInput = Input.GetAxisRaw(input.ZOOM);
    }

    void Update() {
        GetInput();
        if (position.allowZoom) {
            ZoomInOnTarget();
        }
    }

    void FixedUpdate() {
        if (target) {
            MoveToTarget();
            LookAtTarget();
            MouseOrbitTarget();
        }
    }

    void MoveToTarget() {
        destination = target.position;
        destination += Quaternion.Euler(orbit.xRotation, orbit.yRotation, 0) * -Vector3.forward * position.distanceFromTarget;

        if (position.smoothFollow) {
            transform.position = Vector3.SmoothDamp(transform.position, destination, ref camVelocity, position.smooth);
        } else {
            transform.position = destination;
        }
    }

    void LookAtTarget() {
        Quaternion targetRotation = Quaternion.LookRotation(target.position - transform.position);
        transform.rotation = targetRotation;
    }

    void MouseOrbitTarget() {
        PreviousMousePosition = currentMousePosition;
        currentMousePosition = Input.mousePosition;

        // MouseOrbit entfernt, da dies mit der Spielsteuerung kollidiert
        // if (mouseOrbitInput > 0) {
        //     orbit.yRotation += (currentMousePosition.x - PreviousMousePosition.x) * orbit.yOrbitSmooth;
        // }

        if (Input.GetKey("left")){
            orbit.yRotation -= orbit.orbitSpeed * orbit.yOrbitSmooth;
        }

        if (Input.GetKey("right")){
            orbit.yRotation += orbit.orbitSpeed * orbit.yOrbitSmooth;
        }
    }

    void ZoomInOnTarget() {
        position.newDistance += position.zoomStep * zoomInput;

        position.distanceFromTarget = Mathf.Lerp(position.distanceFromTarget, position.newDistance, position.zoomSmooth*Time.deltaTime);

        if (position.distanceFromTarget > position.maxZoom) {
            position.distanceFromTarget = position.maxZoom;
            position.newDistance = position.maxZoom;
        }
        if (position.distanceFromTarget < position.minZoom) {
            position.distanceFromTarget = position.minZoom;
            position.newDistance = position.minZoom;
        }
    }
}
