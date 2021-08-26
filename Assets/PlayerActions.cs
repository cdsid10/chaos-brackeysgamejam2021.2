using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    private UndecidedBehaviour _undecidedBehaviour;

    public bool isPressing;
    public bool canInteractUndecided, isUndecidedRecruited;
    
    
    // Start is called before the first frame update
    void Start()
    {
        _undecidedBehaviour = FindObjectOfType<UndecidedBehaviour>();
        InvokeRepeating(nameof(CheckUndecided), 5f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        if (canInteractUndecided)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                isPressing = true;
            }
            else if(Input.GetKeyUp(KeyCode.Space))
            {
                isPressing = false;
            }
        }
    }

    public void Recruit()
    {
        isUndecidedRecruited = true;
        _undecidedBehaviour.gameObject.GetComponent<SpriteRenderer>().color = Color.gray;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Undecided"))
        {
            canInteractUndecided = true;
        }

        
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Undecided"))
        {
            canInteractUndecided = false;
        }

        
    }
    
    void CheckUndecided()
    {
        _undecidedBehaviour = FindObjectOfType<UndecidedBehaviour>();
    }
}
