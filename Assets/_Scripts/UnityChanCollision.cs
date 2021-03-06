﻿using UnityEngine;
using System.Collections;

public class UnityChanCollision : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip[] gameStart;
    [SerializeField] AudioClip[] onGame;
    [SerializeField] AudioClip[] gameOver;
    [SerializeField] Animator ownAnim;
    [SerializeField] GameController controller;

    void Start()
    {
        audioSource.PlayOneShot(gameStart[Random.Range(0, gameStart.Length)]);
        StartCoroutine(unityChanVoice());
    }

    void OnCollisionEnter(Collision collision)
    {
        switch (collision.transform.tag)
        {
            case "Enemy":
                audioSource.Stop();
                audioSource.PlayOneShot(gameOver[Random.Range(0, gameOver.Length)]);
                ownAnim.SetBool("GameOver", true);
                controller.IsGameOver = true;
                break;
        }
    }

    IEnumerator unityChanVoice()
    {
        yield return new WaitForSeconds(Random.Range(15, 31));

        if (!controller.IsGameOver)
        {
            audioSource.PlayOneShot(onGame[Random.Range(0, onGame.Length)]);
        }
        StartCoroutine(unityChanVoice());
    }
}
