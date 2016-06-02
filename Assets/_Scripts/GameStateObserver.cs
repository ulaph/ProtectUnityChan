using UnityEngine;
using System.Collections;
using UniRx;
using UniRx.Triggers;

public class GameStateObserver : MonoBehaviour
{
    ReactiveProperty<GameState> state = new ReactiveProperty<GameState>();

    void Start()
    {
        
    }

    public IObservable<GameState> GAmeStateAsObservable()
    {
        return state.AsObservable().Publish().RefCount();
    }

    public enum GameState
    {
        GameStart,
        GameOver,
        KillEnemy,
        ElapsedTime
    }
}
