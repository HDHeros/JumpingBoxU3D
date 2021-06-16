using UnityEngine;

public class CameraMover : MonoBehaviour
{
    [SerializeField] private bool _isActive = false;
    [SerializeField] private float _startLiftingSpeed;
    [SerializeField] private ScoreCounter _scoreCouner;
    [SerializeField] private Transform _mainCubeTransform;
    [SerializeField] private GameState _gameState;

    private float _liftingSpeed;
    private Transform _cameraTransform;

    private void OnGameStateChanged()
    {
        if (_gameState.State == GameStates.GameIsOn)
        {
            _isActive = true;
        }
        else
        {
            _isActive = false;
        }
    }

    public void OnScoreChanged(int _score)
    {
        _liftingSpeed = _startLiftingSpeed + (float)_score / 10000;
    }

    private void Start()
    {
        _cameraTransform = GetComponent<Transform>();
        _startLiftingSpeed = _startLiftingSpeed == 0 ? 0.01f : _startLiftingSpeed;
        _liftingSpeed = _startLiftingSpeed;
        _scoreCouner.ScoreChanged.AddListener(OnScoreChanged);
        _gameState = GetComponent<GameState>();
        _gameState.OnGameStateChanged.AddListener(OnGameStateChanged);
    }

    private void CameraMove()
    {
        if (_mainCubeTransform.position.y > _cameraTransform.position.y + 2)
            _cameraTransform.Translate(Vector3.up * _liftingSpeed * 4);

        _cameraTransform.Translate(Vector3.up * _liftingSpeed);
    }

    private void Update()
    {
        if (!_isActive) return;
        CameraMove();
    }

}
