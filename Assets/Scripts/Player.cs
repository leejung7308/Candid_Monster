using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed;
    void FixedUpdate()
    {
        float xinput = Input.GetAxis("Horizontal");
        float yinput = Input.GetAxis("Vertical");

        Vector2 newVelocity = new Vector2(xinput, yinput);
        gameObject.GetComponent<Rigidbody2D>().velocity = newVelocity*moveSpeed;
    }
}
