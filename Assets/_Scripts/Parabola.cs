using UnityEngine;
using System.Collections;
using UniRx;
using UniRx.Triggers;

public class Parabola : MonoBehaviour
{

    [SerializeField] GameObject targetPointer;
    [SerializeField] GameObject dot;
    [SerializeField] float initialVelocity = 10;
    GameObject[] dots;
    int dotsNum;
    float gravity = 9.81F;

    void Start()
    {
        dotsNum = 100;
        dots = new GameObject[dotsNum];
        for (int i = 0; i < dotsNum; i++)
        {
            dots[i] = Instantiate(dot);
        }
        this.LateUpdateAsObservable()
            .Where(_ => targetPointer.activeSelf)
            .Subscribe(_ =>
            {
                dot.SetActive(true);
                dot.transform.position = this.transform.position;
                showDots();
            });
        this.LateUpdateAsObservable()
            .Where(_ => !targetPointer.activeSelf)
            .Subscribe(_ => dot.SetActive(false));
    }

    float targetDistance()
    {
        return Vector3.Distance(transform.position, targetPointer.transform.position);
    }

    float setAngle()
    {
        //var ownHeight = transform.position.y;
        var distance = targetDistance();
        initialVelocity = distance;
        var a = (gravity * Mathf.Pow(distance, 2)) / (2F * Mathf.Pow(initialVelocity, 2));
        var b = -distance;
        var c = a;
        var d = Mathf.Abs(Mathf.Pow(b, 2) - 4F * a * c);
        d = Mathf.Sqrt(d);
        var tan = (-b + d) / (2F * a);
        var rad = Mathf.Atan(tan);
        Debug.Log(a);
        Debug.Log(b);
        Debug.Log(c);
        Debug.Log(d);
        return rad;
    }

    void showDots()
    {
        var delta = targetDistance() / dotsNum;
        this.transform.LookAt(targetPointer.transform.position);
        for (int i = 0; i < dotsNum; i++)
        {
            var angle = setAngle();
            var x = delta * i;
            var y = Mathf.Tan(angle) * x - (gravity * x * x) / (2F * initialVelocity * initialVelocity * Mathf.Cos(angle) * Mathf.Cos(angle));
            dots[i].transform.position = transform.position + transform.forward * x + transform.up * y;
        }
    }
}
