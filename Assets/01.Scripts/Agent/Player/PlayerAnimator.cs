using UnityEngine;
using UnityEngine.U2D.Animation;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private CharDataListSO _charDataList;

    
    private readonly int _velocityHash = Animator.StringToHash("Velocity");
    private readonly int _isGroundHash = Animator.StringToHash("IsGround");

    private Animator _animator;
    private SpriteLibrary _spriteLibrary;
    private Player _owner;

    public void Initialize(Player player)
    {
        _owner = player;
        _animator = GetComponent<Animator>();
        _spriteLibrary = GetComponent<SpriteLibrary>();
        _owner.MovementCompo.isGround.OnValueChanged += HandleGroundChanged;
    }

    private void HandleGroundChanged(bool prev, bool now)
    {
        _animator.SetBool(_isGroundHash, now);
    }

    private void FixedUpdate()
    {
        float xVelocity = _owner.MovementCompo.RbCompo.velocity.x;
        float absVelocity = Mathf.Abs(xVelocity);
        _animator.SetFloat(_velocityHash, absVelocity);
    }

    public void ChangeCharacter(int index)
    {
        _spriteLibrary.spriteLibraryAsset = _charDataList.dataList[index].sprites; 
    }
}
