using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTestActions : MonoBehaviour
{
    private bool canInteract;
    public bool recruited;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (canInteract)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                recruited = true;
            }
            else if(Input.GetKeyUp(KeyCode.Space))
            {
                recruited = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Undecided"))
        {
            canInteract = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Undecided"))
        {
            canInteract = false;
        }
    }
}
