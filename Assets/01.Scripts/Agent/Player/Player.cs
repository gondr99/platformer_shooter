using UnityEngine.Events;

public class Player : Agent
{
    public InputReader PlayerInput;
    public UnityEvent JumpEvent;

    #region component region
    public WeaponHolder WeaponCompo { get; private set; }
    #endregion

    private bool _doubleJumpAvailable;

    protected override void Awake()
    {
        base.Awake();

        PlayerInput.JumpKeyEvent += HandleJumpKeydownEvent;
        JumpEvent.AddListener(HandleJumpEvent);

        WeaponCompo = transform.Find("WeaponHolder").GetComponent<WeaponHolder>();
        WeaponCompo.Initialize(this);
    }

    private void OnDisable()
    {
        PlayerInput.JumpKeyEvent -= HandleJumpKeydownEvent;
        JumpEvent.RemoveListener(HandleJumpEvent);
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
}
