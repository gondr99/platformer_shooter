using DG.Tweening;
using UnityEngine;

public class RecoilFeedback : Feedback
{
    [SerializeField] private Transform _targetTrm;
    [SerializeField] private float _recoilAmount = 0.15f, _recoilTime = 0.05f;

    private Vector3 _initPos;
    private Tween _recoilTween;

    private void Awake()
    {
        _initPos = _targetTrm.localPosition;
    }

    public override void CreateFeedback()
    {
        float targetX = _initPos.x - _recoilAmount;
        _recoilTween = _targetTrm.DOLocalMoveX(targetX, _recoilTime).SetLoops(2, LoopType.Yoyo);
    }

    public override void FinishFeedback()
    {
        if (_recoilTween != null && _recoilTween.IsActive())
        {
            _recoilTween.Kill();
            transform.position = _initPos;
        }
    }
}
