using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class TitleStatePresenter : MonoBehaviour
{

    void OnCollisionEnter(Collision collision)
    {
        switch (collision.transform.tag)
        {
            case "Cube":
            case "Uni":
                SceneManager.LoadScene("Game");
                break;
        }
    }
}
