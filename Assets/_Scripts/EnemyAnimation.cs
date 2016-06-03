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

    void Start()
    {
        enemyAnim = GetComponent<Animator>();
        enemyNav = GetComponent<NavMeshAgent>();
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
                break;
            case "Uni":
                audioSource.PlayOneShot(hitUni);
                audioSource.PlayOneShot(death);
                enemyNav.speed = 0;
                enemyAnim.SetTrigger("Dead");
                break;
        }
    }

}
