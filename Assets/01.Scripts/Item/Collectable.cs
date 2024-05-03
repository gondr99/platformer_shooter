using UnityEngine;

public abstract class Collectable : MonoBehaviour
{
    [SerializeField] protected ItemSO _itemData;

    protected bool _alreadyCollected;
    protected Rigidbody2D _rbCompo;

    private void Awake()
    {
        _rbCompo = GetComponent<Rigidbody2D>();
    }

    public void DropIt(Vector3 position, Vector2 force)
    {
        transform.position = position;
        _rbCompo.AddForce(force, ForceMode2D.Impulse);
    }

    public abstract void Collect();

}
