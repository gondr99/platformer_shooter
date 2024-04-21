using UnityEngine;

public class Agent : MonoBehaviour
{

    [Header("Settings")]
    [SerializeField] protected float _extraGravity = 200f;
    [SerializeField] protected float _gravityDelay = 0.15f;

    #region component region
    public AgentMovement MovementCompo { get; protected set; }
    public Animator AnimatorCompo { get; protected set; }
    #endregion

    public bool isDead;

    protected float _timeInAir;

    protected virtual void Awake()
    {
        MovementCompo = GetComponent<AgentMovement>();
        AnimatorCompo = transform.Find("Visual").GetComponent<Animator>();
    }

    #region more effective gravity section
    protected void CalculateInAirTime()
    {
        if (!MovementCompo.isGround.Value)
        {
            _timeInAir += Time.deltaTime;
        }
        else
        {
            _timeInAir = 0;
        }
    }

    protected void ApplyExtraGravity()
    {
        if (_timeInAir > _gravityDelay)
        {
            MovementCompo.AdditionalGravityForce(new Vector2(0, -_extraGravity));
        }
    }
    #endregion

    #region Flip Character
    public bool IsFacingRight()
    {
        return Mathf.Approximately(transform.eulerAngles.y, 0);
    }

    public void HandleSpriteFlip(Vector3 targetPosition)
    {
        if (targetPosition.x < transform.position.x)
        {
            transform.eulerAngles = new Vector3(0f, -180f, 0f);
        }
        else
        {
            transform.eulerAngles = new Vector3(0f, 0f, 0f);
        }
    }
    #endregion
}
