using UnityEngine;
using System.Collections;
using UniRx;
using UniRx.Triggers;

public class CubeStateChanger : MonoBehaviour
{

    Rigidbody cubeRb;
    Renderer cubeRen;
    BoxCollider cubeCol;

    ObjectHolder obj;

    void Start()
    {
        obj = ObjectHolder.Instance;

        cubeRb = GetComponent<Rigidbody>();
        cubeRen = GetComponent<Renderer>();
        cubeCol = GetComponent<BoxCollider>();
        cubeRb.AddForceAtPosition(transform.up, new Vector3(Random.Range(-1F, 1F), 0, Random.Range(-1F, 1F)));
        StartCoroutine(ownStateChanger());
        StartCoroutine(ownDestroyer());

        this.UpdateAsObservable()
            .Where(_ => cubeRb.velocity.y <= 0)
            .Subscribe(_ => cubeRb.constraints = RigidbodyConstraints.FreezeAll)
            .Dispose();
    }

    IEnumerator ownStateChanger()
    {
        var randomNum = Random.Range(0, 6);
        yield return new WaitForSeconds(12);
        switch (randomNum)
        {
            case 0:
                cubeCol.isTrigger = true;
                obj.EnemyInstance(transform.position, 0);
                Destroy(cubeRb);
                cubeRen.material = obj.setMat("Blue");
                break;
            case 1:
                cubeCol.isTrigger = true;
                obj.EnemyInstance(transform.position, 1);
                Destroy(cubeRb);
                cubeRen.material = obj.setMat("Red");
                break;
        }
    }

    IEnumerator ownDestroyer()
    {
        yield return new WaitForSeconds(15);
        Destroy(this.gameObject);
    }
}
