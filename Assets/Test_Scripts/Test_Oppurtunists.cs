using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_Oppurtunists : MonoBehaviour
{
    private PlayerTestActions _playerTestActions;
    private FameManager _fameManager;
    private PickupManager _pickupManager;


    // Start is called before the first frame update
    void Start()
    {
        _playerTestActions = FindObjectOfType<PlayerTestActions>();
        _fameManager = FindObjectOfType<FameManager>();
        _pickupManager = FindObjectOfType<PickupManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_playerTestActions.hasUsed)
        {
            _playerTestActions.hasUsed = false;
            Destroy(gameObject);
        }
    }
}
