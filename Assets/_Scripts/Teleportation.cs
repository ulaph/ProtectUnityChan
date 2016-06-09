using UnityEngine;
using System.Collections;
using UniRx;
using UniRx.Triggers;

public class MoveToTarget : MonoBehaviour
{

    Transform target;
    NavMeshAgent agent;

    // Use this for initialization
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("UnityChan").transform;
        agent = GetComponent<NavMeshAgent>();

        this.LateUpdateAsObservable()
            .Subscribe(_ => agent.SetDestination(target.position));
    }
        

}
