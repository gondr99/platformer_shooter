using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    [SerializeField] protected LayerMask _targetLayer;

    protected bool _isDead = false;
    protected float _timer = 0;

    protected Rigidbody2D _rigidBody;

    public abstract void InitAndFire(Transform firePosTrm, int damage, float knockBackPower);

    protected virtual void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
    }


    //차후 풀매니징을 위한 코드
    public void ResetItem()
    {
        _isDead = false;
        _timer = 0;
    }
}
