using UnityEngine;
using UnityEngine.Events;


/// <summary>
/// Компонент изменяет Z и X  коллайдеров в зависимости от поворота камеры
/// </summary>
public class ColliderRotator : MonoBehaviour
{
    public UnityEvent ColliderSizeChanged;

    private CameraRotator _cameraRotator;
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
        _cameraRotator = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraRotator>(); ;
        _cameraRotator.CameraCompletedRotate.AddListener(ChangePlatformCollider);
        _transform = GetComponent<Transform>();
        ChangePlatformCollider(_cameraRotator.CameraIsTurnToX);
    }
    
    private void OnDestroy()
    {
        try
        {
            _cameraRotator.GetComponent<CameraRotator>().CameraCompletedRotate.RemoveListener(ChangePlatformCollider);
        }
        catch
        {

        }
    }

}
