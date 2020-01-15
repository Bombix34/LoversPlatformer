using UnityEngine;
using System.Collections;

public class CameraManager : MonoBehaviour
{

    private float decreaseFactor = 1.0f;
    private float shake = 0f;
    private float shakeAmount = 0.1f;

    private Vector3 initPosition;

    private Camera camera;

    void Start()
    {
        camera = this.GetComponent<Camera>();
        initPosition = transform.position;
    }

    void Update()
    {
        if (shake > 0)
        {
            transform.localPosition = Random.insideUnitSphere * shakeAmount + initPosition;
            shake -= Time.deltaTime * decreaseFactor;

        }
        else
        {
            shake = 0.0f;
            //
            transform.localPosition = initPosition;
        }
    }

    public void setShake(float value)
    {
        shake = value;
    }
}