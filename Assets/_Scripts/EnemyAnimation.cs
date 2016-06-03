using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class EnemyAnimation : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip hitUni;
    [SerializeField] AudioClip death;
    Animator enemyAnim;
    NavMeshAgent enemyNav;
    GameStateObserver state;

    void Start()
    {
        enemyAnim = GetComponent<Animator>();
        enemyNav = GetComponent<NavMeshAgent>();
        state = GameStateObserver.Instance;
    }

    void StartSinking()
    {
    }

    void OnCollisionEnter(Collision collision)
    {
        switch (collision.transform.tag)
        {
            case "UnityChan":
                enemyNav.speed = 0;
                enemyAnim.SetTrigger("PlayerDead");
                state.IsGameOver = true;
                StartCoroutine(resultPresenter());
                break;
            case "Uni":
                audioSource.PlayOneShot(hitUni);
                audioSource.PlayOneShot(death);
                enemyNav.speed = 0;
                enemyAnim.SetTrigger("Dead");
                state.KillEnemy++;
                break;
        }
    }

    IEnumerator resultPresenter()
    {
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene("Result");
    }

}
