using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController2 : MonoBehaviour


{
    public float walkSpeed = 5f;
    public float runSpeed = 5f;


    private float h;
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        h = Input.GetAxisRaw("Horizontal");
        transform.Translate(h * Time.deltaTime * 7.0f, 0, 0);

        if (h > 0)
        {
            animator.SetBool("isMoving", true);
            GetComponent<SpriteRenderer>().flipX = false;
        }
        if (h < 0)
        {
            animator.SetBool("isMoving", true);
            GetComponent<SpriteRenderer>().flipX = true;
        }
        if (h == 0)
        {
            animator.SetBool("isMoving", false);
        }

        if (Input.GetKey(KeyCode.C))
        {
            runSpeed++;
            animator.SetBool("isMoving", false);
            animator.SetBool("isRunning", true);
            transform.Translate(h * Time.deltaTime * 10.0f, 0, 0);

        }

        if (Input.GetKey(KeyCode.V))
        {
           
            animator.SetBool("isRunning", false);
            animator.SetBool("isMoving", false);
            animator.SetBool("isSlide", true);
            transform.Translate(h * Time.deltaTime * 4.0f, 0, 0);

        }
    }



    
}