using UnityEngine;

public class DamageCaster : MonoBehaviour
{
    public LayerMask whatIsEnemy;
    public float damageRadius;
    public int detectCount = 1;

    private ContactFilter2D _filter;
    private Collider2D[] _resultArr;

    private void Awake()
    {
        _resultArr = new Collider2D[detectCount];
        _filter = new ContactFilter2D { layerMask = whatIsEnemy, useLayerMask = true };
    }

    public bool CastDamage(int damage, float knockBackPower)
    {
        int cnt = Physics2D.OverlapCircle(transform.position, damageRadius, _filter, _resultArr);

        for (int i = 0; i < cnt; i++)
        {
            if (_resultArr[i].TryGetComponent<Health>(out Health health))
            {
                Vector2 direction = _resultArr[i].transform.position - transform.position;

                RaycastHit2D hit = Physics2D.Raycast(transform.position, direction.normalized, direction.magnitude, whatIsEnemy);

                health.TakeDamage(damage, hit.normal, hit.point, knockBackPower);
            }
        }

        return cnt > 0;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, damageRadius);
        Gizmos.color = Color.white;
    }
}
