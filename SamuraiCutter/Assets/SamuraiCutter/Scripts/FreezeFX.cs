using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeFX : MonoBehaviour
{
    public float duration = 0.05f;

    bool isFreeze = false;

    float pendingFreezeDuration = 0f;

    void Update()
    {
        if(pendingFreezeDuration>0f && !isFreeze)
        {
            StartCoroutine(DoFreeze());
        }
    }

    public void FreezeScreen()
    {
        pendingFreezeDuration = duration;
    }

    IEnumerator DoFreeze()
    {
        isFreeze = true;
        float timeScale = Time.timeScale;
        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(duration);
        Time.timeScale = timeScale;
        pendingFreezeDuration = 0f;
        isFreeze = false;
    }
}
