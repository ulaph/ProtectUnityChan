using UnityEngine;
using System.Collections;

public class UniCollision : MonoBehaviour
{

    [SerializeField] GameObject lightning;
    [SerializeField] GameController controller;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Enemy")
        {
            lightning.SetActive(true);
            lightning.transform.SetParent(collision.transform);
            lightning.transform.position = collision.transform.position;
            controller.KillEnemy++;
            Destroy(this.gameObject);

        }
    }
	

}
