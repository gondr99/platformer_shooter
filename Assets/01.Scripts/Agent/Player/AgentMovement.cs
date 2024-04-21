using System;
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

    public Rigidbody2D RbCompo { get; private set; }
    public NotifyValue<bool> isGround = new NotifyValue<bool>();

    protected float _xMove;


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

    private void FixedUpdate()
    {
        isGround.Value = CheckGrounded();
        RbCompo.velocity = new Vector2(_xMove * moveSpeed, RbCompo.velocity.y);
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
