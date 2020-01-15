using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBehavior : MonoBehaviour
{
    float direction = -1f;
    public float speed = 1f;

    [SerializeField] BossManager manager;

    public void Init(BossManager manager)
    {
        this.manager = manager; 
    }


    void Update()
    {
        transform.Translate(new Vector3(0f, Time.fixedDeltaTime * direction * speed, 0f));   
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Wall"))
        {
            direction *= -1f;
        }
    }
}