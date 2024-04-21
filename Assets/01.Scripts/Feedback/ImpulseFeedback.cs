using Cinemachine;
using UnityEngine;

[RequireComponent(typeof(CinemachineImpulseSource))]
public class ImpulseFeedback : Feedback
{
    [SerializeField] private float _impulsePower = 0.2f;
    private CinemachineImpulseSource _source;

    public override void CreateFeedback()
    {
        _source.GenerateImpulse(_impulsePower);
    }

    public override void FinishFeedback()
    {

    }

    private void Awake()
    {
        _source = GetComponent<CinemachineImpulseSource>();
    }
}
