using UnityEngine;

public class dragflip : MonoBehaviour {

    public float flipForce;
    private float mZCoord;
    private Vector3 mOffset;
    private Vector3 draggedDistance;
    private Rigidbody rb;

    private void Start() {
        rb        = this.GetComponent<Rigidbody>();
    }

    private void OnMouseDown() {

        mZCoord = Camera.main.WorldToScreenPoint(transform.position).z;
        mOffset = transform.position - GetMouseWorldPos();
    }

      private Vector3 GetMouseWorldPos() {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = mZCoord;
        mousePoint.y = 0;

        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

    private void OnMouseDrag() {
        draggedDistance = transform.position - GetMouseWorldPos();
    }

    private void OnMouseUp() {
        rb.AddForce(draggedDistance*flipForce);
        //transform.position += draggedDistance;
    }
}
