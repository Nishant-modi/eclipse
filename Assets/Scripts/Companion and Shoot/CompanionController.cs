using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompanionController : MonoBehaviour
{
    public GameObject companionTarget;
    public float companionMoveSpeed;
    public PlayerController playerController;
    private bool playerFacingRight;
    // Start is called before the first frame update
    void Start()
    {
        playerFacingRight = playerController.m_FacingRight;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //playerFacingRight = playerController.m_FacingRight;
        Vector2 direction = companionTarget.transform.position - transform.position;
        transform.position = Vector2.MoveTowards(this.transform.position, companionTarget.transform.position, companionMoveSpeed * Time.deltaTime);

        if(playerFacingRight != playerController.m_FacingRight)
        {
            Flip();
        }
    }

    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        playerFacingRight = playerController.m_FacingRight;

        transform.Rotate(0, 180f, 0);
    }
}
