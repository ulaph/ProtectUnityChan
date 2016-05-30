using UnityEngine;
using System.Collections;

public class UnityChanCollision : MonoBehaviour
{

    [SerializeField] Animator ownAnim;

    void OnCollisionEnter(Collision collision)
    {
        switch (collision.transform.tag)
        {
            case "Enemy":
                ownAnim.SetBool("GameOver", true);
                break;
        }
    }
}
