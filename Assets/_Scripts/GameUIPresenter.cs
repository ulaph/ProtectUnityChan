using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UniRx;
using UniRx.Triggers;

public class GameUIPresenter : MonoBehaviour
{

    GameStateObserver state;
    [SerializeField] Text time;
    [SerializeField] Text kill;

    void Start()
    {
        state = GameStateObserver.Instance;
        this.LateUpdateAsObservable()
            .Where(_ => !state.IsGameOver)
            .Subscribe(_ =>
            {
                state.ElapsedTime += Time.deltaTime;
                time.text = "TIME  " + string.Format("{0:##.##}", Mathf.Floor(state.ElapsedTime * 100) / 100);
                kill.text = "KILL  " + state.KillEnemy + "  体";
            });
        this.LateUpdateAsObservable()
            .Where(_ => state.IsGameOver)
            .Subscribe(_ =>
            {
                PlayerPrefs.SetFloat("ELAPEDTIME", Mathf.Floor(state.ElapsedTime * 100) / 100);
                PlayerPrefs.SetInt("KILLENEMY", state.KillEnemy);
                PlayerPrefs.SetInt("SCORE", (int)(state.KillEnemy * 100 + state.ElapsedTime * 100));
            });
    }
}
