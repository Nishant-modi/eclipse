using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public PlayerController controller;
    public float runSpeed = 40f;
    public bool canMove = true;
    bool jump = false;
    float horizontalMove = 0;
    public GameObject gameWinUI;

    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        if(Input.GetButtonDown("Jump"))
        {
            jump = true;
        }

    }

    private void FixedUpdate()
    {
        if (canMove)
        {
            controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump);
        }
        else
        {
            controller.Move(0,false,false);
        }
        jump = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "FinishLine")
        {
            canMove = false;
            gameWinUI.SetActive(true);
            Debug.Log("finish line reached");
        }
    }
}
