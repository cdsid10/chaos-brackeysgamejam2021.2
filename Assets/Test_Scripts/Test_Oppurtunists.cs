using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_Oppurtunists : MonoBehaviour
{
    private FameManager _fameManager;
    private PickupManager _pickupManager;
    
    // Start is called before the first frame update
    void Start()
    {
        _fameManager = FindObjectOfType<FameManager>();
        _pickupManager = FindObjectOfType<PickupManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _fameManager.fame -= _pickupManager.oppurtunistsCost;
            _pickupManager.oppurtunistsInBag++;
        }
    }
}
