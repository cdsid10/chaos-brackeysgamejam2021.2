using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_Hunter : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;

    [SerializeField]
    private Transform target;
    
    [SerializeField]
    private float speed;

    public int health;

    private bool canMove = true;
    
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        health = 3;
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.transform.position, 
                speed * Time.deltaTime);
        }

        if (health <= 0)
        {
            Destroy(gameObject);
        }
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Mine"))
        {
            canMove = false;
            StartCoroutine(StopASec());
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // if (other.CompareTag("Opportunists"))
        // {
        //     transform.position = Vector2.MoveTowards(transform.position, target.transform.position, 
        //         speed * Time.deltaTime);
        // }
    }

    IEnumerator StopASec()
    {
        transform.position = transform.position;
        yield return new WaitForSeconds(2);
        canMove = true;
    }
}
