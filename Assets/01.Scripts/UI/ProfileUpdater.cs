using UnityEngine;
using UnityEngine.UI;

public class ProfileUpdater : PlayerConnectUI
{
    private Image _profileImage;

    private SpriteRenderer _spriteRenderer;
    private Sprite _before = null;

    public override void AfterFindPlayer()
    {
        _spriteRenderer = _player.AnimatorCompo.GetComponent<SpriteRenderer>();
    }

    private void Awake()
    {
        _profileImage = GetComponent<Image>();
    }

    private void Update()
    {
        if (_player == null) return;

        if (_before != _spriteRenderer.sprite)
        {
            _before = _spriteRenderer.sprite;
            _profileImage.sprite = _before;
        }

    }
}
