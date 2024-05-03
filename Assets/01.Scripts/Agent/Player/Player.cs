using System;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class Player : Agent
{
    public InputReader PlayerInput;
    public UnityEvent JumpEvent;

    #region component region
    public WeaponHolder WeaponCompo { get; private set; }
    public PlayerAnimator PlayerAnimCompo { get; private set; }
    #endregion

    private bool _doubleJumpAvailable;

    protected override void Awake()
    {
        base.Awake();

        PlayerInput.JumpKeyEvent += HandleJumpKeydownEvent;
        JumpEvent.AddListener(HandleJumpEvent);

        //Handle character change event
        PlayerInput.OnCharacterChangeEvent += HandleCharacterChangeEvent;

        WeaponCompo = transform.Find("WeaponHolder").GetComponent<WeaponHolder>();
        WeaponCompo.Initialize(this);

        PlayerAnimCompo = transform.Find("Visual").GetComponent<PlayerAnimator>();
        PlayerAnimCompo.Initialize(this);
    }

    private void OnDisable()
    {
        PlayerInput.JumpKeyEvent -= HandleJumpKeydownEvent;
        JumpEvent.RemoveListener(HandleJumpEvent);
        PlayerInput.OnCharacterChangeEvent -= HandleCharacterChangeEvent;
    }

    private void Update()
    {
        SetUpMovementInput();
        CalculateInAirTime();
        HandleSpriteFlip(PlayerInput.MousePos);
    }

    private void FixedUpdate()
    {
        ApplyExtraGravity();
    }

    private void SetUpMovementInput()
    {
        MovementCompo.SetMovement(PlayerInput.Movement.x);
    }

    private void HandleJumpKeydownEvent()
    {
        if (MovementCompo.isGround.Value)
        {
            _doubleJumpAvailable = true;
            JumpEvent?.Invoke();
        }
        else if (_doubleJumpAvailable)
        {
            _doubleJumpAvailable = false;
            JumpEvent?.Invoke();
        }
    }

    private void HandleJumpEvent()
    {
        _timeInAir = 0;
        MovementCompo.Jump();
    }

    private void HandleCharacterChangeEvent(int index)
    {
        if (!WeaponCompo.CanChangeWeapon()) return;

        WeaponCompo.ChangeWeapon(index);
        PlayerAnimCompo.ChangeCharacter(index);
    }


    public override void SetDead()
    {
        // not yet
    }
}
