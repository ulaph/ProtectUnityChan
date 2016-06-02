using UnityEngine;
using System.Collections;

public class EnemyAnimation : MonoBehaviour
{

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
                enemyNav.speed = 0;
                enemyAnim.SetTrigger("Dead");
                break;
        }
    }

}
