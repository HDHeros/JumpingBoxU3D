using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformDestroyer : MonoBehaviour
{
    void OnBecameInvisible()
    {
        try
        {
            if (GetComponent<Transform>().position.y < GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Transform>().position.y)
                Destroy(gameObject);
        }
        catch
        {

        }

    }
}
