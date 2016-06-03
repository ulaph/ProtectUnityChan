using UnityEngine;
using System.Collections;

public sealed class GameStateObserver : MonoBehaviour
{
    private static GameStateObserver _singleInstance;

    public bool IsGameOver { get; set; }

    public int KillEnemy { get; set; }

    public float ElapsedTime { get; set; }

    public static GameStateObserver Instance
    {
        get
        {
            if (_singleInstance == null)
            {

                GameObject status = new GameObject("GameStateObserver");
                _singleInstance = status.AddComponent<GameStateObserver>();
            }
            return _singleInstance;
        }
    }

    void Awake()
    {
        IsGameOver = false;
        KillEnemy = 0;
        ElapsedTime = 0;
    }
}
