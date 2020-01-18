using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    const float MIN_ZOOM_FACTOR = 0.3f;
    const float MAX_ZOOM_FACTOR = 5f;
    [SerializeField]
    private Transform primaryAnchor;
    [SerializeField]
    private float speed;
    [SerializeField]
    [Range(MIN_ZOOM_FACTOR, MAX_ZOOM_FACTOR)]
    private float zoomFactor = 1.0f;
    [SerializeField]
    private float zoomSpeed = 5.0f;

    public Transform PrimaryAnchor { get => primaryAnchor; set => primaryAnchor = value; }
    public Transform SecondaryAnchor { get; set; }
    public float Speed { get => speed; set => speed = value; }
    public float ZoomFactor { 
        get => zoomFactor;
        set
        {
            if (value < MIN_ZOOM_FACTOR)
            {
                zoomFactor = MIN_ZOOM_FACTOR;
                return;
            }
            if (value > MAX_ZOOM_FACTOR)
            {
                zoomFactor = MAX_ZOOM_FACTOR;
                return;
            }
            zoomFactor = value;
        }
    }
    public float ZoomSpeed { get => zoomSpeed; set => zoomSpeed = value; }
    public float OriginalSize { get; set; }
    private Camera Camera { get; set; }

    private void Start()
    {
        this.Camera = GetComponent<Camera>();
        this.OriginalSize = Camera.orthographicSize;
    }

    // Update is called once per frame
    void Update()
    {
        if (this.PrimaryAnchor == null)
        {
            return;
        }

        this.DoFollow();
        this.DoZoom();
    }

    private void DoFollow()
    {
        transform.position = new Vector3(this.PrimaryAnchor.position.x, this.PrimaryAnchor.position.y, transform.position.z);
    }

    private void DoLerpFollow()
    {
        float xTarget = PrimaryAnchor.position.x;
        float yTarget = PrimaryAnchor.position.y;

        float xNew = Mathf.Lerp(transform.position.x, xTarget, Time.deltaTime * speed);
        float yNew = Mathf.Lerp(transform.position.y, yTarget, Time.deltaTime * speed);

        transform.position = new Vector3(xNew, yNew, transform.position.z);
    }

    private void DoZoom()
    {
        float targetSize = this.OriginalSize * zoomFactor;
        if (targetSize != this.Camera.orthographicSize)
        {
            this.Camera.orthographicSize = Mathf.Lerp(this.Camera.orthographicSize, targetSize, Time.deltaTime * zoomSpeed);
        }
    }
}
