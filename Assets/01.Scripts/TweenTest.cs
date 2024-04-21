using DG.Tweening;
using UnityEngine;

public class TweenTest : MonoBehaviour
{
    private Sequence seq;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            float x = Random.Range(-5f, 5f);
            float y = Random.Range(-5f, 5f);

            if(seq != null && seq.IsActive())
            {
                seq.Kill();
            }
            
            seq = DOTween.Sequence();
            seq.Append(transform.DOMove(new Vector3(x, y), 1f));
            seq.Append(transform.DORotate(new Vector3(0, 0, 360f), 0.35f, RotateMode.FastBeyond360));
            seq.Append(transform.DOShakePosition(0.3f, 0.5f));
        }
        
    }

    private void OnDestroy()
    {
        transform.DOKill();
    }
}
