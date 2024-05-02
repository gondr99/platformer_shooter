using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Portal : MonoBehaviour
{
    [field: SerializeField] public PortalDataSO Data { get; set; }
    [SerializeField] private Transform _spawnPosition;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Light2D _light;

    private void Awake()
    {
        _spriteRenderer.color = Data.portalColor;
        _light.color = Data.portalColor;
    }

    public void OpenPortal(Vector3 position)
    {
        transform.position = position;
        transform.localScale = Vector3.zero;

        Color transparentColor = Data.portalColor;
        transparentColor.a = 0;
        _spriteRenderer.color = transparentColor;

        int count = Data.GetRandomCount();

        Sequence seq = DOTween.Sequence();

        seq.Append(transform.DOScale(1, 1f));
        seq.Join(_spriteRenderer.DOFade(1, 1f));
        seq.AppendInterval(0.5f); //µô·¹ÀÌ ÈÄ
        seq.AppendCallback(() => StartCoroutine(SummonCoroutine(count)));
    }

    private IEnumerator SummonCoroutine(int count)
    {
        for (int i = 0; i < count; i++)
        {
            Enemy enemy = PoolManager.Instance.Pop(Data.enemyName) as Enemy;
            enemy.transform.position = _spawnPosition.position;
            enemy.MovementCompo.SetMovement(-Data.launchForce);

            float randomSec = Random.Range(Data.minTime, Data.maxTime);
            yield return new WaitForSeconds(randomSec);
        }
        yield return new WaitForSeconds(1f);
        ClosePortal();
    }

    public void ClosePortal()
    {
        Sequence seq = DOTween.Sequence();
        seq.Append(transform.DOScale(0, 1.5f));
        seq.Join(_spriteRenderer.DOFade(0, 1f));
        seq.OnComplete(() => Destroy(gameObject));
    }
}
