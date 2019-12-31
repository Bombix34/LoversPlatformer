using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReloadDebug : MonoBehaviour
{
    PlayerInputManager inputs;

    float loadAmount = 0f;

    void Awake()
    {
        inputs = GetComponent<PlayerInputManager>();
    }

    void Update()
    {
        if(inputs.GetStartInput())
        {
            loadAmount += Time.fixedDeltaTime;
            if (loadAmount > 1f)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                loadAmount = 0f;
            }
        }
    }
}
