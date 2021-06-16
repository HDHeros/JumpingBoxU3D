using UnityEngine;
using UnityEngine.Events;


/// <summary>
/// Компонент изменяет Z и X  коллайдеров в зависимости от поворота камеры
/// </summary>
public class ColliderRotator : MonoBehaviour
{
    public UnityEvent ColliderSizeChanged;

    private GameObject _camera;
    private BoxCollider _collider;
    private Transform _transform;

    private void ChangePlatformCollider(bool cameraIsTurnToX)
    {
        if (cameraIsTurnToX)
        {
            _collider.size = _transform.rotation.y == 0 ? new Vector3(10f, 1f, 1f) : new Vector3(1f, 1f, 10f);
        }
        else
        {
            _collider.size = _transform.rotation.y == 0 ? new Vector3(1f, 1f, 10f) : new Vector3(10f, 1f, 1f);
        }
        ColliderSizeChanged.Invoke();
    }


    private void Start()
    {
        _collider = GetComponent<BoxCollider>();
        _camera = GameObject.FindGameObjectWithTag("MainCamera");
        _camera.GetComponent<CameraRotator>().CameraCompletedRotate.AddListener(ChangePlatformCollider);
        _transform = GetComponent<Transform>();
        ChangePlatformCollider(false);
    }
    
    private void OnDestroy()
    {
        try
        {
            _camera.GetComponent<CameraRotator>().CameraCompletedRotate.RemoveListener(ChangePlatformCollider);
        }
        catch
        {

        }
    }

}
