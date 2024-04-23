using System.Collections;
using UnityEngine;

public class AgentMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform _groundCheckerTrm;

    [Header("Settings")]
    public float moveSpeed = 5f;
    public float jumpPower = 7f;
    [SerializeField] private LayerMask _whatIsGround;
    [SerializeField] private Vector2 _groundCheckerSize;
    [SerializeField] private float _knockbackTime = 0.2f;

    public Rigidbody2D RbCompo { get; private set; }
    public NotifyValue<bool> isGround = new NotifyValue<bool>();

    protected float _xMove;
    protected bool _canMove = true;
    protected Coroutine _knockbackCoroutine;

    private void Awake()
    {
        RbCompo = GetComponent<Rigidbody2D>();
    }

    public void SetMovement(float xMove)
    {
        _xMove = xMove;
    }

    public void StopImmediately(bool isYStop = false)
    {
        _xMove = 0;
        if (isYStop)
            RbCompo.velocity = Vector2.zero;
        else
            RbCompo.velocity = new Vector2(_xMove, RbCompo.velocity.y);
    }

    public void Jump(float multiplier = 1f)
    {
        RbCompo.velocity = Vector2.zero;
        RbCompo.AddForce(Vector2.up * jumpPower * multiplier, ForceMode2D.Impulse);
    }

    public void JumpTo(Vector2 force)
    {
        SetMovement(force.x);
        RbCompo.AddForce(force, ForceMode2D.Impulse);
    }

    private void FixedUpdate()
    {
        CheckGround();
        ApplyXMove();
    }

    private void ApplyXMove()
    {
        if (!_canMove) return;
        RbCompo.velocity = new Vector2(_xMove * moveSpeed, RbCompo.velocity.y);
    }

    private void CheckGround()
    {
        isGround.Value = CheckGrounded();
    }

    public bool CheckGrounded()
    {
        Collider2D collider = Physics2D.OverlapBox(_groundCheckerTrm.position, _groundCheckerSize, 0, _whatIsGround);

        return collider;
    }

    public void AdditionalGravityForce(Vector2 force)
    {
        RbCompo.AddForce(force);
    }


    #region Knockback Section
    public void GetKnockedBack(Vector3 direction, float power)
    {
        Vector3 difference = direction * power * RbCompo.mass;
        RbCompo.AddForce(difference, ForceMode2D.Impulse);
        if (_knockbackCoroutine != null)
        {
            StopCoroutine(_knockbackCoroutine);
        }
        _knockbackCoroutine = StartCoroutine(KnockbackRoutine());
    }

    private IEnumerator KnockbackRoutine()
    {
        _canMove = false;
        yield return new WaitForSeconds(_knockbackTime);
        RbCompo.velocity = Vector2.zero;
        _canMove = true;
    }

    public void ClearKnockback()
    {
        RbCompo.velocity = Vector2.zero;
        _canMove = true;
    }
    #endregion


#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        if (_groundCheckerTrm == null) return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(_groundCheckerTrm.position, _groundCheckerSize);
        Gizmos.color = Color.white;
    }


#endif
}
