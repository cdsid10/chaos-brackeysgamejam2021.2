using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_Hunter : MonoBehaviour
{
    private FameManager _fameManager;
    
    private Rigidbody2D _rigidbody2D;

    [SerializeField]
    private Transform target;
    
    [SerializeField]
    private float speed;

    public int health;

    private bool canMove = true;
    [SerializeField]
    private float timer;
    
    // Start is called before the first frame update
    void Start()
    {
        _fameManager = FindObjectOfType<FameManager>();
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
        
        if (other.CompareTag("Player"))
        {
            _fameManager.gameObject.AddComponent<DamageFame>();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(_fameManager.gameObject.GetComponent<DamageFame>());
        }
    }

    IEnumerator StopASec()
    {
        transform.position = transform.position;
        yield return new WaitForSeconds(2);
        canMove = true;
    }

    IEnumerator DamagePerSec()
    {
        yield return new WaitForSeconds(1f);
        _fameManager.fame -= 10;
    }
}
