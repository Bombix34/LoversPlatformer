using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitFX : MonoBehaviour
{

    public void Hit()
    {
        GetComponent<Renderer>().material.SetFloat("_FlashAmount", 1f);
        StartCoroutine(HitFXCoroutine());
    }

    IEnumerator HitFXCoroutine()
    {
        while(GetComponent<Renderer>().material.GetFloat("_FlashAmount")>0f)
        {
            GetComponent<Renderer>().material.SetFloat("_FlashAmount", GetComponent<Renderer>().material.GetFloat("_FlashAmount") - (Time.deltaTime*20f));
            yield return new WaitForSeconds(Time.deltaTime);
        }
        GetComponent<Renderer>().material.SetFloat("_FlashAmount", 0f);
    }
}
