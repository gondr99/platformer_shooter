using UnityEngine;

public class DropFeedback : Feedback
{
    [SerializeField] private DropTableSO _dropTable;
    [SerializeField] private float _dropPower;

    public override void CreateFeedback()
    {
        foreach (DropInfo info in _dropTable.tables)
        {
            DropItem(info);
        }
    }

    private void DropItem(DropInfo info)
    {
        if (info.dropRate > Random.value) //drop hit!
        {
            Collectable item = PoolManager.Instance.Pop(info.item.prefab.name) as Collectable;
            Vector3 dropDirection = Quaternion.Euler(0, 0, Random.Range(-50f, 50f)) * Vector3.up;
            item.DropIt(transform.position, dropDirection * _dropPower);
        }
    }

    public override void FinishFeedback()
    {

    }
}
