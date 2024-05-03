using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    public UnityEvent HitEvent;
    public UnityEvent DeadEvent;

    [SerializeField] private int _maxHealth = 150;

    private int _currentHealth;

    private Agent _owner;

    public void Initialize(Agent owner)
    {
        _owner = owner;
        ResetHealth();
    }

    public void ResetHealth()
    {
        _currentHealth = _maxHealth;
    }

    public void TakeDamage(int amount, Vector2 normal, Vector2 point, float knockBackPower)
    {
        _currentHealth -= amount;
        HitEvent?.Invoke();

        //normal과 point, knockback는 차후에 쓰이게 됩니다.

        if(knockBackPower > 0)
        {
            _owner.MovementCompo.GetKnockedBack(normal * -1, knockBackPower);
        }

        if (_currentHealth <= 0)
        {
            DeadEvent?.Invoke();
        }
    }

    public float GetNormalizedHealth()
    {
        return _currentHealth / (float)_maxHealth;
    }
}
