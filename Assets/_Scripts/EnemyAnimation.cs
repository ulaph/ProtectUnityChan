using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class EnemyAnimation : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip hitUni;
    [SerializeField] AudioClip death;
    [SerializeField] CapsuleCollider cCol;
    [SerializeField] SphereCollider sCol;
    [SerializeField] Rigidbody enemyRb;

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
                cCol.isTrigger = true;
                sCol.isTrigger = true;
                Destroy(enemyRb);
                audioSource.PlayOneShot(hitUni);
                audioSource.PlayOneShot(death);
                enemyNav.speed = 0;
                enemyAnim.SetTrigger("Dead");
                StartCoroutine(destroyThis());
                break;
        }
    }

    IEnumerator destroyThis()
    {
        yield return new WaitForSeconds(3);
        Destroy(this.gameObject);
    }

}
