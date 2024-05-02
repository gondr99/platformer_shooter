using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Grenade : Projectile, IPoolable
{
    public UnityEvent OnExplosionEvent;

    [SerializeField] private float _launchPower, _torguePower;
    [SerializeField] private float _lifeTime = 5f, _explosionRadius = 3.5f;
    [SerializeField] private int _maxDamgedEnemy = 8;

    
    private int _damageAmount;
    private float _knockBackPower;

    private SpriteRenderer _spriteRenderer;
    private Material _material;
    private float _blinkTime = 1f;

    private readonly int _intensityHash = Shader.PropertyToID("_Intensity");

    private Collider2D[] _resultColliders;
    private ContactFilter2D _contactFilter;

    public string ItemName => "Grenade";

    protected override void Awake()
    {
        base.Awake();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _material = _spriteRenderer.material;
        _resultColliders = new Collider2D[_maxDamgedEnemy];
        _contactFilter = new ContactFilter2D { layerMask = _targetLayer, useLayerMask = true };
    }

    public override void InitAndFire(Transform firePosTrm, int damage, float knockBackPower)
    {
        _damageAmount = damage;
        _knockBackPower = knockBackPower;
        transform.position = firePosTrm.position;
        _rigidBody.AddForce(firePosTrm.right * _launchPower, ForceMode2D.Impulse);
        _rigidBody.AddTorque(_torguePower, ForceMode2D.Impulse);
        _blinkTime = 1.5f;

        StartCoroutine(BlinkCoroutine());
    }

    private IEnumerator BlinkCoroutine()
    {
        _material.SetFloat(_intensityHash, 1.6f);
        yield return new WaitForSeconds(_blinkTime * 0.5f);
        _material.SetFloat(_intensityHash, 0f);
        yield return new WaitForSeconds(_blinkTime * 0.5f);
        _blinkTime *= 0.6f; //60프로씩 감소
        _blinkTime = Mathf.Clamp(_blinkTime, 0.3f, 1.5f);

        if (!_isDead)
        {
            StartCoroutine(BlinkCoroutine());
        }
    }

    private void Update()
    {
        _timer += Time.deltaTime;

        //지연시간이 지났다면 폭발
        if (_timer >= _lifeTime)
        {
            Explosion();
        }
    }

    private void Explosion()
    {
        OnExplosionEvent?.Invoke();

        int count = Physics2D.OverlapCircle(transform.position, _explosionRadius, _contactFilter, _resultColliders);

        for (int i = 0; i < count; i++)
        {
            if (_resultColliders[i].TryGetComponent<Health>(out Health health))
            {
                Vector2 direction = health.transform.position - transform.position;
                health.TakeDamage(_damageAmount,
                    direction.normalized * -1,
                    health.transform.position,
                    _damageAmount);
            }
        }

        StopAllCoroutines();
        _isDead = true;
        PoolManager.Instance.Push(this);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        int collisionLayerMask = 1 << collision.gameObject.layer;
        if ((collisionLayerMask & _targetLayer) > 1)  //피격당한 녀석의 타겟에 포함
        {
            Explosion();
        }
    }

    public GameObject GetGameObject()
    {
        return gameObject;
    }
}
