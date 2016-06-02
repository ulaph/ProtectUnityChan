using UnityEngine;
using System.Collections;
using UniRx;
using UniRx.Triggers;

public class LightningDestroyer : MonoBehaviour
{

    [SerializeField] Transform end;

    void Start()
    {
        this.UpdateAsObservable()
            .Where(_ => transform.position.y <= end.position.y)
            .Subscribe(_ =>
            {
                transform.position += Vector3.up * 0.1f;
            });
        this.UpdateAsObservable()
            .Where(_ => transform.position.y >= end.position.y)
            .Subscribe(_ =>
            {
                Destroy(transform.parent.gameObject);
            });
    }
}
