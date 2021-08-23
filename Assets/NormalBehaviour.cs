using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.Mathematics;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class NormalBehaviour : MonoBehaviour
{
    private PlayerMovement _playerMovement;
    private UndecidedBehaviour _undecidedBehaviour;
    private Rigidbody2D _rigidbody2D;
    
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float distanceToFlee;

    public bool isLured;

    
    
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _playerMovement = FindObjectOfType<PlayerMovement>();
        _undecidedBehaviour = FindObjectOfType<UndecidedBehaviour>();
    }

    // Update is called once per frame
    void Update()
    {
        var position = transform.position;
        float distance = Vector3.Distance(position, _playerMovement.transform.position);
//        float distanceToLured = Vector3.Distance(position, _undecidedBehaviour.transform.position);

        if (!isLured)
        {
            if(distance > 1 && distance < 3)
            {
                transform.position = Vector2.MoveTowards(transform.position, _playerMovement.transform.position,
                    -1 * moveSpeed * Time.deltaTime);
            }
            else if (distance > 4)
            {
                transform.position = Vector2.MoveTowards(transform.position, _playerMovement.transform.position,
                    moveSpeed * Time.deltaTime);
            }
        }
        else
        {
            var undecidedPosition = _undecidedBehaviour.transform.position;
            transform.position = Vector3.MoveTowards(transform.position, undecidedPosition,
                    moveSpeed * Time.deltaTime);

            //Destroy(gameObject);
        }
        
    }
}
