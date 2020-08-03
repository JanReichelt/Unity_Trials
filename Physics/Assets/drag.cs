using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class drag : MonoBehaviour
{
    // Reference: https://www.youtube.com/watch?v=0yHBDZHLRbQ

    private Vector3 mOffset;
    private float mZCoord;

    private void OnMouseDrag() {
        transform.position = GetMouseWorldPos() + mOffset;
    }

    private void OnMouseDown() {
        mOffset = gameObject.transform.position - GetMouseWorldPos();
        mZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
    }

    private Vector3 GetMouseWorldPos() {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = mZCoord;

        return Camera.main.ScreenToWorldPoint(mousePoint);
    }
}
