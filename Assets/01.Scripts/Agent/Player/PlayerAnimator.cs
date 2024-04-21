using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private AgentMovement _agentMovement;

    private readonly int _velocityHash = Animator.StringToHash("Velocity");
    private readonly int _isGroundHash = Animator.StringToHash("IsGround");

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _agentMovement.isGround.OnValueChanged += HandleGroundChanged;
    }

    private void HandleGroundChanged(bool prev, bool now)
    {
        _animator.SetBool(_isGroundHash, now);
    }

    private void FixedUpdate()
    {
        float absVelocity = Mathf.Abs(_agentMovement.RbCompo.velocity.x);
        _animator.SetFloat(_velocityHash, absVelocity);
    }
}
