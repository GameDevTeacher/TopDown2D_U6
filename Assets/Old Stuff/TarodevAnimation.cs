using UnityEngine;

public class TarodevAnimation : MonoBehaviour
{
    [SerializeField] private float castAnimTime;
    [SerializeField] private float damageAnimTime;
    
    private float _lockedTill;
    private bool _casting;
    private bool _damaging;

    private Vector2 _direction;
    private bool _followPlayer;
    private Animator _animator;
    
    private void Awake() => _animator = GetComponent<Animator>();
    
    private void Update()
    {
        var state = GetState();
        _casting = false;
        _damaging = false;

        if (state == _currentState) return;
        _animator.CrossFade(state, 0, 0);
        _currentState = state;
    }

    private int GetState()
    {
        if (Time.time < _lockedTill) return _currentState;

        if (_damaging) return LockState(Damage, damageAnimTime);
        if (_casting) return LockState(Cast, castAnimTime);
        return _direction != Vector2.zero ? Walk : Idle;
        //return _followPlayer ? Walk : Idle;

        int LockState(int s, float t)
        {
            _lockedTill = Time.time + t;
            return s;
        }
    }
    
    private int _currentState;

    private static readonly int Idle = Animator.StringToHash("Idle");
    private static readonly int Walk = Animator.StringToHash("Walk");
    private static readonly int Damage = Animator.StringToHash("Damage");
    private static readonly int Cast = Animator.StringToHash("Cast");
}