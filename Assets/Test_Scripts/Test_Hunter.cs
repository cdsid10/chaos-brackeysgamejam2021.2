using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Test_Hunter : MonoBehaviour
{
    private FameManager _fameManager;
    private SpawnManager _spawnManager;
    
    private Rigidbody2D _rigidbody2D;
    private SpriteRenderer _spriteRenderer;
    private Animator _animator;

    [SerializeField]
    private Transform target;
    
    [SerializeField]
    public float speed;

    public int health;

    private bool canMove = true;

    [SerializeField] private Sprite[] sprites;

    [SerializeField] private GameObject particleHunter;

    // Start is called before the first frame update
    void Start()
    {
        _fameManager = FindObjectOfType<FameManager>();
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _spawnManager = FindObjectOfType<SpawnManager>();
        _animator = GetComponentInChildren<Animator>();
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
            _spawnManager.hasHunterSpawned = false;
            _spawnManager.huntersPerished++;
            Destroy(gameObject);
        }

        if (health >= 3)
        {
            _spriteRenderer.sprite = sprites[2];
        }
        else if (health == 2)
        {
            _spriteRenderer.sprite = sprites[1];
        }
        else if (health == 1)
        {
            _spriteRenderer.sprite = sprites[0];
        }
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Mine"))
        {
            canMove = false;
            Instantiate(particleHunter, transform.position, quaternion.identity);
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
        _animator.SetTrigger("staggered");
        transform.position = transform.position;
        yield return new WaitForSeconds(2);
        canMove = true;
    }
    
}
