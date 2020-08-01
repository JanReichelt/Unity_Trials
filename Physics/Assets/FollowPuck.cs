using UnityEngine;

public class FollowPuck : MonoBehaviour {

    public Transform puck;
    public Vector3 offset;

    void Update() {
        transform.position = puck.position + offset;
    }
}
