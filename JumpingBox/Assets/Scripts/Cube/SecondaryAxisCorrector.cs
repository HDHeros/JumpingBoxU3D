using UnityEngine;

/// <summary>
/// Устанавливает не активную в данный момент ось в значение платформы, которой коснулся куб
/// </summary>
public class SecondaryAxisCorrector : MonoBehaviour
{
    [SerializeField] private CameraRotator _cameraRotator;

    private Transform _transform;

    private void Start()
    {
        _transform = GetComponent<Transform>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "Platform") return;

        float positionX = _cameraRotator.CameraIsTurnToX ? collision.transform.position.x : _transform.position.x;
        float positionY = _transform.position.y;
        float positionZ = _cameraRotator.CameraIsTurnToX ? _transform.position.z : collision.transform.position.z;

        _transform.position = new Vector3(positionX, positionY, positionZ);


    }
}
