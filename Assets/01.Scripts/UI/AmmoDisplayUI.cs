using System;
using TMPro;
using UnityEngine;

public class AmmoDisplayUI : PlayerConnectUI
{
    private WeaponHolder _playerWeapon;
    private int _maxAmmo = 0;

    [SerializeField] private TextMeshProUGUI _tmpTotalAmmo; //ammo count;
    [SerializeField] private TextMeshProUGUI _tmpClipStatus; //clip count;


    public override void AfterFindPlayer()
    {
        _playerWeapon = _player.WeaponCompo;
        _playerWeapon.currentGun.OnValueChanged += HandleGunChanged;

        HandleGunChanged(null, _playerWeapon.currentGun.Value);

        
        _playerWeapon.currentTotalAmmo.OnValueChanged += HandleTotalAmmoChanged;
        HandleTotalAmmoChanged(0, _playerWeapon.currentTotalAmmo.Value);
    }

    private void HandleTotalAmmoChanged(int prev, int now)
    {
        _tmpTotalAmmo.text = now.ToString();
    }

    private void HandleGunChanged(Gun prev, Gun now)
    {
        if (prev != null)
        {
            prev.bulletCount.OnValueChanged -= HandleAmmoChanged;
        }
        if (now != null)
        {
            now.bulletCount.OnValueChanged += HandleAmmoChanged;
            _maxAmmo = now.gunData.maxAmmo;

            HandleAmmoChanged(0, now.bulletCount.Value);
        }
    }

    private void HandleAmmoChanged(int prev, int now)
    {
        _tmpClipStatus.text = $"{now}/{_maxAmmo}";
    }
}
