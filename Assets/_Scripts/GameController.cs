using UnityEngine;
using System.Collections;
using UniRx;
using UniRx.Triggers;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public bool IsGameOver { get; set; }

    public int KillEnemy { get; set; }

    public float ElapsedTime { get; set; }

    void Awake()
    {
        IsGameOver = false;
        KillEnemy = 0;
        ElapsedTime = 0;
    }

    void Start()
    {
        this.LateUpdateAsObservable()
            .Where(_ => IsGameOver)
            .Subscribe(_ => StartCoroutine(resultPresenter()));
    }

    IEnumerator resultPresenter()
    {
        IsGameOver = false;
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene("Result");
    }
}
