using UnityEngine;

public class Bullet : Projectile, IPoolable
{
    [SerializeField] private float _moveSpeed = 15f;
    [SerializeField] private float _lifeTime = 2f;

    private int _damage;
    private float _knockBackPower;
    private Vector2 _fireDirection;

    public string ItemName => "Bullet";

    public override void InitAndFire(Transform firePosTrm, int damage, float knockBackPower)
    {
        _damage = damage;
        _knockBackPower = knockBackPower;
        transform.SetPositionAndRotation(firePosTrm.position, firePosTrm.rotation);
        _fireDirection = firePosTrm.right;
    }

    private void FixedUpdate()
    {
        _rigidBody.velocity = _fireDirection * _moveSpeed;
        _timer += Time.fixedDeltaTime;

        if (_timer >= _lifeTime)
        {
            _isDead = true;
            DestroyBullet();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_isDead) return;

        _isDead = true;
        var effect = PoolManager.Instance.Pop("BulletImpact") as EffectPlayer;
        effect.SetPositionAndPlay(transform.position);

        DestroyBullet();
    }

    private void DestroyBullet()
    {
        PoolManager.Instance.Push(this);
    }

    public GameObject GetGameObject()
    {
        return gameObject;
    }
}
