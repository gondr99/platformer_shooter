using UnityEngine;

public class PlayEffectFeedback : Feedback
{
    [SerializeField] private string effectItemName;

    public override void CreateFeedback()
    {
        EffectPlayer effect = PoolManager.Instance.Pop(effectItemName) as EffectPlayer;
        effect.SetPositionAndPlay(transform.position);
    }

    public override void FinishFeedback()
    {

    }
}
