using UnityEngine;

public class MakeSplatterFeedback : Feedback
{
    [SerializeField] private Transform _targetTrm;
    [SerializeField] private string _prefabName = "Splatter";
    [SerializeField][ColorUsage(true, true)] private Color _color;

    public override void CreateFeedback()
    {
        Splatter splatter = PoolManager.Instance.Pop(_prefabName) as Splatter;
        Quaternion randomRotation = Quaternion.Euler(0, 0, Random.Range(0, 360f));
        splatter.SetUpAndApear(_color, _targetTrm.position, randomRotation);
    }

    public override void FinishFeedback()
    {

    }
}
