using UnityEngine.UI;

public class HealthBarUI : PlayerConnectUI
{
    private Image _fillImage;
    private Health _targetHealth;

    private void Awake()
    {
        _fillImage = transform.Find("Fill").GetComponent<Image>();
    }

    public override void AfterFindPlayer()
    {
        _targetHealth = _player.HealthCompo;
        _targetHealth.HitEvent.AddListener(HandleHitEvent);
    }

    private void HandleHitEvent()
    {
        _fillImage.fillAmount = _targetHealth.GetNormalizedHealth();
    }
}
