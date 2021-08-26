using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using Random = UnityEngine.Random;

public class Test_Normal : MonoBehaviour
{
    private PlayerMovement _playerMovement;
    private LureManager _lureManager;
    private Test_Undecided _testUndecided;
    private SpawnManager _spawnManager;
    private PlayerTestActions _playerTestActions;

    private Rigidbody2D _rigidbody2D;
    private CircleCollider2D _circleCollider2D;
    public bool isVulnerable;
    [SerializeField]
    private bool canInteractWPlayer;

    [SerializeField] private float moveSpeed;


    private void Awake()
    {
        if (_testUndecided == null)
        {
            InvokeRepeating(nameof(CheckUndecided), 5f, 1f);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        _playerMovement = FindObjectOfType<PlayerMovement>();
        _lureManager = FindObjectOfType<LureManager>();
       _testUndecided = FindObjectOfType<Test_Undecided>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _circleCollider2D = GetComponent<CircleCollider2D>();
        _spawnManager = FindObjectOfType<SpawnManager>();
        _playerTestActions = FindObjectOfType<PlayerTestActions>();
        _spawnManager.normalsInScene.Add(gameObject.transform);
    }

    // Update is called once per frame
    void Update()
    {
        var position = transform.position;
        float distance = Vector3.Distance(position, _playerMovement.transform.position);

        if (!_lureManager.canLure)
        {
            if (distance > 0 && distance < 3)
            {
                transform.position = Vector2.MoveTowards(position, _playerMovement.transform.position,
                    -1 * Random.Range(1f, moveSpeed) * Time.deltaTime);
            }
            else if(distance > 3 && distance < 3.5f)
            {
                transform.position = position;
            }
            else if (distance > 3.5f && distance < 6)
            {
                transform.position = Vector2.MoveTowards(position, _playerMovement.transform.position,
                    Random.Range(1f, moveSpeed) * Time.deltaTime);
            }
            else if (distance > 6)
            {
                transform.position = transform.position;
            }
        }
        else
        {
            if (_testUndecided.isRecruited && distance < 6)
            {
                if (_testUndecided == null)
                {
                    CheckUndecided();
                }
                var undecidedPosition = _testUndecided.transform.position;
                float undecidedDistance = Vector3.Distance(position, undecidedPosition);

                if (undecidedDistance < 2)
                {
                    transform.position = position;
                    isVulnerable = true;
                    gameObject.GetComponent<SpriteRenderer>().color = Color.gray;
                }
                else
                {
                    transform.position = Vector3.MoveTowards(transform.position, undecidedPosition,
                        Random.Range(0.2f, 0.8f) * Time.deltaTime);
                }
            }
        }

        if (canInteractWPlayer)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                //_playerTestActions.isFamed = false;
                Destroy(gameObject);
            }
            
        }
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        _rigidbody2D.velocity = new Vector2(0,0);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Normals"))
        {
            _circleCollider2D.radius = 0.65f;
        }

        if (other.CompareTag("Undecided") && _testUndecided.isRecruited)
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }

        if (other.CompareTag("Player"))
        {
            if (isVulnerable)
            {
                canInteractWPlayer = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Normals"))
        {
            _circleCollider2D.radius = 0.5f;
        }
    }
    
    void CheckUndecided()
    {
        _testUndecided = FindObjectOfType<Test_Undecided>();
    }

    
}
