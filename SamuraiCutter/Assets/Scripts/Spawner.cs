using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    public GameObject spawningObject;

    public float timer;
    public float originTimer;
    // Start is called before the first frame update
    void Start()
    {
        timer = originTimer;
        
    }

    // Update is called once per frame
    void Update()
    {


        if (timer < 0)
        {
            Vector3 spawnPos = new Vector3(this.transform.position.x + Random.Range(-3,3),this.transform.position.y, this.transform.position.z);
            Instantiate(spawningObject, spawnPos,Quaternion.identity);
            timer = originTimer;
        }
        else
        {
            timer -= Time.deltaTime;
        }
        
    }
}
