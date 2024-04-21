using UnityEngine;

[CreateAssetMenu(menuName = "SO/Pool/Item")]
public class PoolItemSO : ScriptableObject
{
    public GameObject prefab;
    public int count;

    private void OnValidate()
    {
        if (prefab != null)
        {
            if (prefab.GetComponent<IPoolable>() == null)
            {
                Debug.LogWarning($"Cant find Poolable interface : check {prefab.name}");
                prefab = null;
            }
        }
    }
}

