using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompanionController : MonoBehaviour
{
    public GameObject companionTarget;
    public float companionMoveSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 direction = companionTarget.transform.position - transform.position;
        transform.position = Vector2.MoveTowards(this.transform.position, companionTarget.transform.position, companionMoveSpeed * Time.deltaTime);
    }
}
