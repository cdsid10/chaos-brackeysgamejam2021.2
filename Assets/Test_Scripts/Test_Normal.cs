using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.UI;

public class Test_Normal : MonoBehaviour
{
    private PlayerMovement _playerMovement;
    private LureManager _lureManager;
    private Test_Undecided _testUndecided;
    private SpawnManager _spawnManager;
    private PlayerTestActions _playerTestActions;
    private FameManager _fameManager;

    private Rigidbody2D _rigidbody2D;
    private CircleCollider2D _circleCollider2D;
    public bool isVulnerable;
    [SerializeField]
    private bool canInteractWPlayer;
    
    [SerializeField] GameObject voidObj;

    [SerializeField] private float CDTimer;

    [SerializeField] private float moveSpeed;
    [SerializeField] Image fillArea;

    [SerializeField] private List<Sprite> sprites;


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
        _fameManager = FindObjectOfType<FameManager>();
        _spawnManager.normalsInScene.Add(gameObject);
        gameObject.GetComponent<SpriteRenderer>().sprite = sprites[Random.Range(0, sprites.Count)];
    }

    // Update is called once per frame
    void Update()
    {
        var position = transform.position;
        float distance = Vector3.Distance(position, _playerMovement.transform.position);

        if (!_lureManager.canLure)
        {
            if (distance > 0 && distance <= 3)
            {
                transform.position = Vector2.MoveTowards(position, _playerMovement.transform.position,
                    -1 * moveSpeed * Time.deltaTime);
            }
            else if(distance > 3 && distance <= 5)
            {
                transform.position = position;
            }
            else if (distance > 5 && distance <= 8)
            {
                transform.position = Vector2.MoveTowards(position, _playerMovement.transform.position,
                    Random.Range(0.5f, moveSpeed) * Time.deltaTime);
            }
            else if (distance > 8 && distance < 14)
            {
                transform.position = transform.position;
            }
            else if (distance >= 14)
            {
                transform.position = Vector2.MoveTowards(position, _playerMovement.transform.position,
                    (0.25f) * Time.deltaTime);
            }
        }
        else
        {
            if (_testUndecided.isRecruited && distance < 10)
            {
                if (_testUndecided == null)
                {
                    CheckUndecided();
                }
                var undecidedPosition = _testUndecided.transform.position;
                float undecidedDistance = Vector3.Distance(position, undecidedPosition);

                if (undecidedDistance < Random.Range(2, 3))
                {
                    transform.position = position;
                    isVulnerable = true;
                    gameObject.GetComponent<SpriteRenderer>().color = Color.gray;
                }
                else
                {
                    transform.position = Vector3.MoveTowards(transform.position, undecidedPosition,
                        Random.Range(0.8f, 1.6f) * Time.deltaTime);
                }
            }
        }

        if (isVulnerable)
        {
            CDTimer += Time.deltaTime;
            fillArea.fillAmount = CDTimer / 3f;
            if (CDTimer > Random.Range(3f, 4f) && _lureManager.canLure)
            {
                _fameManager.AddFame();
                _spawnManager.normalsInScene.Remove(this.gameObject);
                _lureManager.lureCount++;
                Instantiate(voidObj, transform.position, quaternion.identity);
                Destroy(gameObject);
            }
            else if(!_lureManager.canLure)
            {
                CDTimer = 0;
                fillArea.fillAmount = CDTimer;
                isVulnerable = false;
                gameObject.GetComponent<SpriteRenderer>().color = Color.white;
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
