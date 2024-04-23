using System.Collections;
using UnityEngine;

public class BlinkFeedback : Feedback
{
    [SerializeField] private SpriteRenderer _targetRenderer;
    [SerializeField] private float _flashTime = 0.1f;
    private Material _mat;

    private readonly int BLINK_HASH = Shader.PropertyToID("_IsBlink");

    private void Awake()
    {
        _mat = _targetRenderer.material;
    }

    public override void CreateFeedback()
    {
        _mat.SetInt(BLINK_HASH, 1);
        StartCoroutine(DelayBlinkCoroutine());
    }

    private IEnumerator DelayBlinkCoroutine()
    {
        yield return new WaitForSeconds(_flashTime);
        _mat.SetInt(BLINK_HASH, 0);
    }

    public override void FinishFeedback()
    {
        StopAllCoroutines();
        _mat.SetInt(BLINK_HASH, 0);
    }
}
