using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.Mathematics;
using UnityEngine;
using Object = System.Object;
using Random = UnityEngine.Random;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class NormalBehaviour : MonoBehaviour
{
    private PlayerMovement _playerMovement;
    public UndecidedBehaviour _undecidedBehaviour;
    private Rigidbody2D _rigidbody2D;
    
    [SerializeField]
    private float moveSpeed;
    
    [SerializeField]
    private GameObject voidAnim;
    
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _playerMovement = FindObjectOfType<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        var position = transform.position;
        float distance = Vector3.Distance(position, _playerMovement.transform.position);

        if(_undecidedBehaviour == null) return;
        
        if (!_undecidedBehaviour.canLure || _undecidedBehaviour.limitReached)
        {
            if(distance > 1 && distance < 3)
            {
                transform.position = Vector2.MoveTowards(transform.position, _playerMovement.transform.position,
                    -1 * Random.Range(1f, moveSpeed) * Time.deltaTime);
            }
            else if (distance >= 4)
            {
                transform.position = Vector2.MoveTowards(transform.position, _playerMovement.transform.position,
                     Random.Range(1f, moveSpeed) * Time.deltaTime);
            }
        }
        else
        {
            if (!_undecidedBehaviour.limitReached || _undecidedBehaviour.canLure)
            {
                var undecidedPosition = _undecidedBehaviour.transform.position;
                transform.position = Vector3.MoveTowards(transform.position, undecidedPosition,
                    Random.Range(0.2f, 1f) * Time.deltaTime);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Undecided"))
        {
            StartCoroutine(DeathLoop());
        }
        
    }

    IEnumerator DeathLoop()
    {
        if (_undecidedBehaviour.canLure)
        {
            Destroy(gameObject);
            Instantiate(voidAnim, transform.position, quaternion.identity);
            _undecidedBehaviour.haveLured++;
            yield break;
        }
    }

    void CheckUndecided()
    {
        _undecidedBehaviour = FindObjectOfType<UndecidedBehaviour>();
    }
}
