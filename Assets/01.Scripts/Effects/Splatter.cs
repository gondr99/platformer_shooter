using DG.Tweening;
using UnityEngine;

public class Splatter : MonoBehaviour, IPoolable
{
    [SerializeField] private float _dissapearTime = 10f;
    private SpriteRenderer _spriteRenderer;

    public string ItemName => "Splatter";
    private readonly int _dissolveStep = Shader.PropertyToID("_DissolveStep");

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void SetUpAndApear(Color color, Vector3 position, Quaternion rotation)
    {
        color.a = 0; //begin with transparent
        _spriteRenderer.color = color;
        transform.SetPositionAndRotation(position, rotation);

        FadeAndScale(scale: 1.1f, time: 0.3f);
    }

    private void FadeAndScale(float scale, float time)
    {
        Sequence seq = DOTween.Sequence();
        seq.Append(_spriteRenderer.DOFade(1f, time));
        seq.Append(transform.DOScale(Vector3.one * scale, time).SetEase(Ease.OutExpo));
        seq.AppendInterval(_dissapearTime);
        seq.Append(_spriteRenderer.material.DOFloat(1.1f, _dissolveStep, time));
        seq.Join(_spriteRenderer.DOFade(0f, time));
        seq.AppendCallback(() => PoolManager.Instance.Push(this));
    }

    public GameObject GetGameObject()
    {
        return gameObject;
    }

    public void ResetItem()
    { 
        _spriteRenderer.material.SetFloat(_dissolveStep, 0);
        transform.localScale = Vector3.one * 0.3f;
    }
}
