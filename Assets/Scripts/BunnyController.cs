﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BunnyController : MonoBehaviour
{
    private Rigidbody2D myRigedBody;
    private Animator myAnim;
    public float bunnyJumpForce = 500f;
    private float bunnyHurtTime = -1;
    private Collider2D myCollider;
    public Text scoreText;
    private float startTime;
    private int jumpsLeft = 2;
    public AudioSource jumpSfx;
    public AudioSource deathSfx;

    // Start is called before the first frame update
    void Start()
    {
        myRigedBody = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<Animator>();
        myCollider = GetComponent<Collider2D>();

        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("Title");
        }
        if (bunnyHurtTime == -1)
        {
            if (Input.GetButtonUp("Jump") && jumpsLeft > 0)
            {
                if (myRigedBody.velocity.y < 0)
                {
                    myRigedBody.velocity = Vector2.zero;
                }

                if (jumpsLeft == 1)
                {
                    myRigedBody.AddForce(transform.up * bunnyJumpForce * 0.75f);
                }
                else
                {
                    myRigedBody.AddForce(transform.up * bunnyJumpForce);
                }

                jumpsLeft--;

                jumpSfx.Play();
            }

            myAnim.SetFloat("vVelocity", myRigedBody.velocity.y);
            scoreText.text = (Time.time - startTime).ToString("0.0");
        }
        else
        {
            if (Time.time > bunnyHurtTime + 2)
            {
                SceneManager.LoadScene("GameScene");
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            foreach (PrefabSpawner spawner in FindObjectsOfType<PrefabSpawner>())
            {
                spawner.enabled = false;
            }

            foreach (MoveLeft moveLefter in FindObjectsOfType<MoveLeft>())
            {
                moveLefter.enabled = false;
            }

            bunnyHurtTime = Time.time;
            myAnim.SetBool("bunnyHurt", true);
            myRigedBody.velocity = Vector2.zero;
            myRigedBody.AddForce(transform.up * bunnyJumpForce);
            myCollider.enabled = false;
            deathSfx.Play();
        }
        else if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            jumpsLeft = 2;
        }
    }
}
