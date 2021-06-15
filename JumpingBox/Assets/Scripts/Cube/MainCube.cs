using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MainCube : MonoBehaviour
{
    [SerializeField] private ScoreCounter _scoreCounter;
    void OnBecameInvisible()
    {
        try
        {
            if (GetComponent<Transform>().position.y < GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Transform>().position.y)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
        catch
        {

        }

    }
}



