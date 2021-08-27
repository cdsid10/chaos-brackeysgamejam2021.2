using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Test_Oppurtunists : MonoBehaviour
{
    private PlayerTestActions _playerTestActions;
    private FameManager _fameManager;
    private PickupManager _pickupManager;

    [SerializeField]
    private GameObject oppUi;
    
    [SerializeField]
    private Sprite selectedSprite, normalSprite;
    
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
            oppUi.SetActive(true);
            Destroy(gameObject);
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = selectedSprite;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = normalSprite;
        }
    }
}
