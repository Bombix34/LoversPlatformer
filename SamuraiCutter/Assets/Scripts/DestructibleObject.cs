using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleObject : MonoBehaviour
{
    public void DestroyObject()
    {
        StartCoroutine(DestroyCoroutine());
    }

    IEnumerator DestroyCoroutine()
    {
        GetComponent<Collider2D>().enabled = false;
        yield return new WaitForSeconds(0.3f);
        float amount = transform.localScale.x;
        while (transform.localScale.x>0.05f)
        {
            amount-= Time.fixedDeltaTime;
            transform.localScale = new Vector3(amount,amount,amount);
            yield return new WaitForSeconds(Time.fixedDeltaTime);
        }
        Destroy(this.gameObject);
    }
}
