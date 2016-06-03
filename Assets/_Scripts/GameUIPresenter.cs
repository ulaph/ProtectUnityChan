using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UniRx;
using UniRx.Triggers;

public class GameUIPresenter : MonoBehaviour
{

    [SerializeField] Text time;
    [SerializeField] Text kill;
    [SerializeField] GameController controller;

    void Start()
    {
        this.LateUpdateAsObservable()
            .Where(_ => !controller.IsGameOver)
            .Subscribe(_ =>
            {
                controller.ElapsedTime += Time.deltaTime;
                time.text = "TIME  " + string.Format("{0:##.##}", Mathf.Floor(controller.ElapsedTime * 100) / 100);
                kill.text = "KILL  " + controller.KillEnemy + "  体";
            });
        this.LateUpdateAsObservable()
            .Where(_ => controller.IsGameOver)
            .Subscribe(_ =>
            {
                PlayerPrefs.SetFloat("ELAPEDTIME", Mathf.Floor(controller.ElapsedTime * 100) / 100);
                PlayerPrefs.SetInt("KILLENEMY", controller.KillEnemy);
                PlayerPrefs.SetInt("SCORE", (int)(controller.KillEnemy * 100 + controller.ElapsedTime * 100));
            });
    }
}
