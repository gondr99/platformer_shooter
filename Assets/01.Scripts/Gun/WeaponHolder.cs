using System;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHolder : MonoBehaviour
{
    [SerializeField] private CharDataListSO _charList;
    [SerializeField] private Gun _gunPrefab;

    public NotifyValue<Gun> currentGun;

    private Transform _playerTrm;
    private InputReader _playerInput;
    private List<Gun> _gunList;

    private bool _isFiring = false;

    public void Initialize(Player player)
    {
        _playerTrm = player.transform;
        _playerInput = player.PlayerInput;
        currentGun = new NotifyValue<Gun>();
        currentGun.OnValueChanged += HandleGunChanged; //Gun change event subscribe;

        MakeWeaponList();
        currentGun.Value = _gunList[0];//Set first gun
        _playerInput.OnCharacterChangeEvent += HandleCharacterChangeEvent;
        _playerInput.OnFireKeyEvent += HandleFireKeyEvent;
    }

    private void OnDestroy()
    {
        currentGun.OnValueChanged -= HandleGunChanged; //Gun change event unSubscribe;
        _playerInput.OnCharacterChangeEvent -= HandleCharacterChangeEvent;
        _playerInput.OnFireKeyEvent -= HandleFireKeyEvent;
    }

    private void Update()
    {
        RotateGun();
        ShootingLogic();
    }

    private void ShootingLogic()
    {
        if(_isFiring && currentGun.Value != null)
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
        }
        if (now != null)
        {
            now.gameObject.SetActive(true);
        }
    }

    private void HandleCharacterChangeEvent(int index)
    {
        currentGun.Value = _gunList[index];
    }

    private void HandleFireKeyEvent(bool value)
    {
        _isFiring = value;
    }

    #endregion

}
