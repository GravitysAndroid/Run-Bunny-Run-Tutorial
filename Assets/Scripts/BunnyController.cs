using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BunnyController : MonoBehaviour
{
    private Rigidbody2D myRigedBody;
    public float bunnyJumpForce = 500f;
    // Start is called before the first frame update
    void Start()
    {
        myRigedBody = GetComponent<Rigidbody2D>(); 
    }

    // Update is called once per frame
    void Update()
    {
       if (Input.GetButtonUp("Jump"))
        {
            myRigedBody.AddForce(transform.up * bunnyJumpForce);
        }
    }
}
