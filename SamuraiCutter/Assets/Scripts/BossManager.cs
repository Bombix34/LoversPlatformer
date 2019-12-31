using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManager : MonoBehaviour
{
    [SerializeField] GameObject boss1, boss2;
    Vector3 posBoss1, posBoss2;

    [SerializeField] GameObject bossPrefab;


    bool canSpawnBoss = true;

    void Start()
    {
        posBoss1 = boss1.transform.position;
        posBoss2 = boss2.transform.position;
    }

    void Update()
    {
        if(boss1==null && canSpawnBoss)
        {
            StartCoroutine(PopBoss(true));
        }
        if(boss2==null && canSpawnBoss)
        {
            StartCoroutine(PopBoss(false));
        }
        if(boss1==null && boss2==null)
        {
            print("WIN");
            canSpawnBoss = false;
        }
    }

    public void RepopBoss(GameObject curBoss)
    {
        StartCoroutine(PopBoss(curBoss == boss1));
    }

    IEnumerator PopBoss(bool isBoss1)
    {
        canSpawnBoss = false;
        yield return new WaitForSeconds(0.3f);
        if(isBoss1)
        {
            boss1 = Instantiate(bossPrefab, posBoss1, Quaternion.identity);
            boss1.transform.parent = this.gameObject.transform;
            boss1.GetComponent<BossBehavior>().Init(this);
        }
        else
        {
            boss2 = Instantiate(bossPrefab, posBoss2, Quaternion.identity);
            boss2.transform.parent = this.gameObject.transform;
            boss2.GetComponent<BossBehavior>().Init(this);
        }
        canSpawnBoss =true;
    }

}
