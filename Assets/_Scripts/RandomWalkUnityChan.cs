using UnityEngine;
using System.Collections;
using UniRx;
using UniRx.Triggers;

public class RandomWalkUnityChan : MonoBehaviour
{
    const string Standing = "Standing";
    const string Walking = "Walking";
    const string Running = "Running";

    [SerializeField] float walkingSpeed = 1.0F;
    [SerializeField] float runningSpeed = 2.0F;
    [SerializeField] float rotateSpeed = 2.0F;

    string nowAnimState;
    float rotateDirection;

    Animator ownAnim;

    void Start()
    {
        ownAnim = GetComponent<Animator>();
        nowAnimState = Standing;
        StartCoroutine(randomState());

        this.UpdateAsObservable()
            .Subscribe(_ => moveUnityChan());
        
        this.UpdateAsObservable()
            .Where(_ => nowAnimState == Walking || nowAnimState == Running)
            .Subscribe(_ => rotateUnityChan());
    }

    void moveUnityChan()
    {
        switch (nowAnimState)
        {
            case Walking:
                transform.position += transform.forward * walkingSpeed * Time.deltaTime;
                break;
            case Running:
                transform.position += transform.forward * runningSpeed * Time.deltaTime;
                break;
        }
    }

    void rotateUnityChan()
    {
        transform.Rotate(0, rotateDirection * rotateSpeed, 0);   
    }

    IEnumerator randomState()
    {
        var randomTime = Random.Range(3F, 7F);
        yield return new WaitForSeconds(randomTime);
        var randomNum = Random.Range(0, 2);
        switch (nowAnimState)
        {
            case Standing:
                nowAnimState = randomNum == 0 ? Walking : Running;
                break;
            case Walking:
                nowAnimState = randomNum == 0 ? Running : Standing;
                rotateDirection = Random.Range(-1.0F, 1.0F);
                break;
            case Running:
                nowAnimState = randomNum == 0 ? Standing : Walking;
                rotateDirection = Random.Range(-0.6F, 0.6F);
                break;
        }
        ownAnim.SetTrigger(nowAnimState);

        StartCoroutine(randomState());

    }
}
