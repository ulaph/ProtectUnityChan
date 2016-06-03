using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ResultUIPresenter : MonoBehaviour
{
    [SerializeField] Text resultText;

    void Start()
    {
        resultText.text = "結果\n\n" +
        "ユニティちゃんを守った時間\n" +
        "" + PlayerPrefs.GetFloat("ELAPEDTIME", 0) + "\n" +
        "倒した敵の数\n" +
        "" + PlayerPrefs.GetInt("KILLENEMY", 0) + " 体\n" +
        "最終スコア\n" +
        "" + PlayerPrefs.GetInt("SCORE", 0) + "\n";
    }
}
