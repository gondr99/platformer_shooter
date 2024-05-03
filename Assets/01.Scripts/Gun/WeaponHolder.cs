using System;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHolder : MonoBehaviour
{
    [SerializeField] private CharDataListSO _charList;
    [SerializeField] private Gun _gunPrefab;
    [SerializeField] private ReloadUI _reloadUI;

    public NotifyValue<Gun> currentGun;

    public NotifyValue<int> currentTotalAmmo;
    private Dictionary<AmmoType, int> _ammoDictionary;

    private Transform _playerTrm;
    private InputReader _playerInput;
    private List<Gun> _gunList;

    private bool _isFiring = false;
    private bool _isReloading = false;

    public bool CanChangeWeapon()
    {
        //add another condition to this, later
        return _isReloading == false;
    }

    public void ChangeWeapon(int index)
    {
        currentGun.Value = _gunList[index];
    }

    public void Initialize(Player player)
    {
        _playerTrm = player.transform;
        _playerInput = player.PlayerInput;
        currentGun = new NotifyValue<Gun>();
        currentGun.OnValueChanged += HandleGunChanged; //Gun change event subscribe;

        currentTotalAmmo = new NotifyValue<int>();
        _ammoDictionary = new Dictionary<AmmoType, int>();
        foreach (AmmoType t in Enum.GetValues(typeof(AmmoType)))
            _ammoDictionary.Add(t, 0);


        MakeWeaponList();
        currentGun.Value = _gunList[0];//Set first gun
        _playerInput.OnFireKeyEvent += HandleFireKeyEvent;
        _playerInput.OnReloadKeyEvent += HandleReloadKeyEvent;

    }


    private void OnDestroy()
    {
        currentGun.OnValueChanged -= HandleGunChanged; //Gun change event unSubscribe;
        //_playerInput.OnCharacterChangeEvent -= HandleCharacterChangeEvent;
        _playerInput.OnFireKeyEvent -= HandleFireKeyEvent;
        _playerInput.OnReloadKeyEvent -= HandleReloadKeyEvent;
    }

    private void Update()
    {
        RotateGun();
        ShootingLogic();
    }

    private void ShootingLogic()
    {
        if (!_isReloading && _isFiring && currentGun.Value != null)
        {
            currentGun.Value.TryToShoot();
        }
    }

    private void MakeWeaponList()
    {
        _gunList = new List<Gun>(_charList.dataList.Count); //for perfomance
        foreach (Character c in _charList.dataList)
        {
            Gun gun = Instantiate(_gunPrefab, transform);
            gun.Initialize(c.gun);
            gun.gameObject.SetActive(false);
            _gunList.Add(gun);
        }
    }

    private void RotateGun()
    {
        Vector3 mousePos = _playerInput.MousePos;
        Vector2 direction = _playerTrm.InverseTransformPoint(mousePos);

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.localRotation = Quaternion.Euler(0, 0, angle);
    }


    #region Handler list
    private void HandleGunChanged(Gun prev, Gun now)
    {
        if (prev != null)
        {
            prev.gameObject.SetActive(false);
            prev.OnReloadEvent.RemoveListener(HandleReloadingStatus);
            prev.reloadTimer.OnValueChanged -= HandleReloadValueChange;
        }
        if (now != null)
        {
            now.gameObject.SetActive(true);
            now.OnReloadEvent.AddListener(HandleReloadingStatus);
            now.reloadTimer.OnValueChanged += HandleReloadValueChange;


            if (now.gunData.infiniteAmmo)
            {
                currentTotalAmmo.Value = 999;
            }
            else
            {
                currentTotalAmmo.Value = _ammoDictionary[now.gunData.ammoData.type];
            }
            
        }
    }


    private void HandleFireKeyEvent(bool value)
    {
        _isFiring = value;
    }

    private void HandleReloadKeyEvent()
    {
        if (_isReloading) return;

        //check Available ammo
        GunDataSO currentData = currentGun.Value.gunData;
        
        int ammo = currentTotalAmmo.Value;
        if (ammo <= 0) return;

        int filledAmmo = Mathf.Min(currentData.maxAmmo, ammo); //select remainammo or maxammo;
        ammo -= filledAmmo;

        _isFiring = false; //hit reload button when firing, stop firing
        currentGun.Value.Reload(filledAmmo);
        if (!currentData.infiniteAmmo)
        {
            currentTotalAmmo.Value = ammo;
            _ammoDictionary[currentData.ammoData.type] = ammo;
        }
    }

    private void HandleReloadingStatus(bool status)
    {
        _isReloading = status;
        _reloadUI.gameObject.SetActive(status);
    }

    private void HandleReloadValueChange(float prev, float next)
    {
        float totalTime = currentGun.Value.gunData.reloadTime;
        _reloadUI.SetBarNormalizedValue(next / totalTime);
    }
    #endregion

}
