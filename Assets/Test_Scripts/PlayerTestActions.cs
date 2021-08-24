using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PlayerTestActions : MonoBehaviour
{
    private PickupManager _pickupManager;
    private FameManager _fameManager;
    
    private bool canInteract;
    public bool recruited;

    [SerializeField] private GameObject opportunists;
    
    // Start is called before the first frame update
    void Start()
    {
        _pickupManager = FindObjectOfType<PickupManager>();
        _fameManager = FindObjectOfType<FameManager>();
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

        if (_pickupManager.oppurtunistsInBag > 0)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Instantiate(opportunists, transform.position, quaternion.identity);
                _pickupManager.oppurtunistsInBag--;
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
