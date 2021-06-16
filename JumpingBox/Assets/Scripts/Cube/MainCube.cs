using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.Events;

public class MainCube : MonoBehaviour
{
    public UnityEvent CubeBecameInvisible;

    private Transform _camera;

    private void Start()
    {
        _camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Transform>();
    }

    private void OnBecameInvisible()
    {
        try
        {
            float cubePositionY = GetComponent<Transform>().position.y;
            float cameraPositionY = _camera.position.y;
            if (cubePositionY < cameraPositionY)
            {
                CubeBecameInvisible.Invoke();
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
        catch
        {

        }
    }
}



