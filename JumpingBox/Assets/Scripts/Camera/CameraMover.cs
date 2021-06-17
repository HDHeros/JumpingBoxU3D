using UnityEngine;

public class CameraMover : MonoBehaviour
{
    [SerializeField] private bool _followMode = false;
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
            _followMode = true;
        }
        else
        {
            _followMode = false;
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

    private void FollowToPlayer()
    {
        if (_mainCubeTransform.position.y > _cameraTransform.position.y + 2)
            _cameraTransform.Translate(Vector3.up * _liftingSpeed * 4);
    }

    private void CameraMove()
    {
        FollowToPlayer();
        
        if(!_followMode)
            _cameraTransform.Translate(Vector3.up * _liftingSpeed);
    }

    private void Update()
    {
        if (!_followMode) return;
        CameraMove();
    }

}
