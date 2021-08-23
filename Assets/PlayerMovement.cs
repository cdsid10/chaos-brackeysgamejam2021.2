using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;

    [SerializeField]
    private float moveSpeed;
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        var moveX = Input.GetAxis("Horizontal");
        var moveY = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(moveX, moveY);

        if (movement.sqrMagnitude > 1)
        {
            movement.Normalize();
        }
        
        _rigidbody2D.velocity = movement * (moveSpeed);
    }
}
