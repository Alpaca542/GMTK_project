using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class playerfollow : MonoBehaviour
{
    public Transform[] players;
    public float speed;

    private void LateUpdate()
    {
        float distX = Mathf.Abs(players[1].position.x - players[0].position.x);
        float distY = Mathf.Abs(players[1].position.y - players[0].position.y);
        float resolution = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth, Camera.main.pixelHeight, 0)).x / Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth, Camera.main.pixelHeight, 0)).x;
        float dist = Mathf.Max(distY, distX * resolution);
        Camera.main.orthographicSize = Mathf.Clamp(dist / 2 + 1, 5f, 100f);

        Vector3 centerPoint = players[0].position + (players[1].position - players[0].position) / 2f;
        centerPoint.z = centerPoint.z - 10;
        transform.position = Vector3.Lerp(transform.position, centerPoint, speed * Time.deltaTime);
    }
    /*
        private float zoom;
        private float zoomMultiplier = 4f;
        private float minZoom = 4f;
        private float maxZoom = 15f;
        private float velocity = 0f;
        private float smoothTime = 0.25f;
        private Camera cam;

        cam = GetComponent<Camera>(); 
        zoom = cam.orthographicSize;

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        zoom -= scroll * zoomMultiplier;
        zoom = Mathf.Clamp(zoom, minZoom, maxZoom);
        cam.orthographicSize = Mathf.SmoothDamp(cam.orthographicSize, zoom, ref velocity, smoothTime);
    */
}