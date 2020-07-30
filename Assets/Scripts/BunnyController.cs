using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BunnyController : MonoBehaviour
{
    private Rigidbody2D myRigedBody;
    private Animator myAnim;
    public float bunnyJumpForce = 500f;
    // Start is called before the first frame update
    void Start()
    {
        myRigedBody = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
       if (Input.GetButtonUp("Jump"))
        {
            myRigedBody.AddForce(transform.up * bunnyJumpForce);
        }

        myAnim.SetFloat("vVelocity", myRigedBody.velocity.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            Application.LoadLevel(Application.loadedLevel);
        }
    }
}
