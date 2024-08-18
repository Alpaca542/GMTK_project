using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerfollow : MonoBehaviour
{
    public GameObject player;
    private Vector3 playervector;
    public float speed;
    private float zoom;
    private float zoomMultiplier = 4f;
    private float minZoom = 4f;
    private float maxZoom = 15f;
    private float velocity = 0f;
    private float smoothTime = 0.25f;
    private Camera cam;

    private void Start()
    {
        cam = GetComponent<Camera>();
        zoom = cam.orthographicSize;
    }

    private void Update()
    {
        playervector = player.transform.position;
        playervector.z = playervector.z - 10;
        transform.position = Vector3.Lerp(transform.position, playervector, speed * Time.deltaTime);

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        zoom -= scroll * zoomMultiplier;
        zoom = Mathf.Clamp(zoom, minZoom, maxZoom);
        cam.orthographicSize = Mathf.SmoothDamp(cam.orthographicSize, zoom, ref velocity, smoothTime);
    }
}