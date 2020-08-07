using UnityEngine;

public class FollowPuck : MonoBehaviour {

    public Transform puck;
    public Vector3 offset;
    public Camera camera;

    void Start() {
        camera = Camera.main;
        // camera = gameObject.getComponent<Camera>();
        Debug.Log(camera);
        // camera.lookAt(puck);
    }

    void Update() {
        transform.position = puck.position + offset;
    }
}
