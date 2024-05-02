using System;
using UnityEngine;

public class ReloadUI : MonoBehaviour
{
    [SerializeField] private Player _owner;

    [SerializeField] private Transform _barTrm;


    private void Awake()
    {
        _owner.OnFlipEvent += HandleFlipEvent;
        HandleFlipEvent();
    }

    private void HandleFlipEvent()
    {
        if(_owner.IsFacingRight())
        {
            transform.localRotation = Quaternion.identity;
        }else
        {
            transform.localRotation = Quaternion.Euler(0, -180f, 0);
        }
    }

    public void SetBarNormalizedValue(float value)
    {
        _barTrm.localScale = new Vector3(value, 1, 1);
    }
}
