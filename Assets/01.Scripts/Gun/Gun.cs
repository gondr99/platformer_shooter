using UnityEngine;
using UnityEngine.Events;

public class Gun : MonoBehaviour
{
    public UnityEvent OnShootEvent;
    public UnityEvent OnEmptyBulletEvent;
    public UnityEvent OnReloadEvent;

    public Transform firePosTrm;
    public GunDataSO gunData;
    [SerializeField] private SpriteRenderer _gunSpriteRenderer;

    public NotifyValue<int> bulletCount;
    public NotifyValue<float> reloadTimer;

    private float _availableFireTime = 0;

    public void Initialize(GunDataSO gunData)
    {
        this.gunData = gunData;
        bulletCount = new NotifyValue<int>(gunData.maxAmmo);
        reloadTimer = new NotifyValue<float>(0);
        SetUpWeapon();
    }

    private void OnEnable()
    {
        OnShootEvent.AddListener(ShootProjectile);
    }

    private void OnDisable()
    {
        OnShootEvent.RemoveListener(ShootProjectile);
    }

    private void SetUpWeapon()
    {
        _gunSpriteRenderer.sprite = gunData.gunSprite;
        _gunSpriteRenderer.transform.localPosition = gunData.gunSpritePosition;
        firePosTrm.localPosition = gunData.firePosTrmPosition;

        gameObject.name = gunData.gunName;
    }

    public void TryToShoot()
    {
        if (_availableFireTime < Time.time)
        {
            if (bulletCount.Value <= 0)
            {
                OnEmptyBulletEvent?.Invoke();
            }
            else
            {
                bulletCount.Value -= 1;
                OnShootEvent?.Invoke();
            }
            ResetFireTime();
        }
    }

    private void ShootProjectile()
    {
        //Projectile newBullet = Instantiate(gunData.bulletPrefab);
        Bullet newBullet = PoolManager.Instance.Pop(gunData.bulletPrefab.name) as Bullet;
        newBullet.InitAndFire(firePosTrm, gunData.damage, gunData.knockBackPower);
    }

    private void ResetFireTime()
    {
        _availableFireTime = Time.time + gunData.cooldown;
    }


#if UNITY_EDITOR
    private void OnValidate()
    {
        UnityEditor.EditorApplication.delayCall += _OnValidate;
    }

    private void _OnValidate()
    {
        if (gunData != null && _gunSpriteRenderer != null)
        {
            SetUpWeapon();
        }
    }
#endif
}
