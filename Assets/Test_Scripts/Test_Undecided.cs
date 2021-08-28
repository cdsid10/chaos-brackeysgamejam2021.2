using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_Undecided : MonoBehaviour
{
    private PlayerTestActions _playerTestActions;
    private LureManager _lureManager;

    [SerializeField]
    private bool canInteractPlayer;

    private int lureCount;
    public bool isRecruited;
    

    [SerializeField]
    private Sprite selectedSprite, normalSprite;

    
    
    // Start is called before the first frame update
    void Start()
    {
        _playerTestActions = FindObjectOfType<PlayerTestActions>();
        _lureManager = FindObjectOfType<LureManager>();
        _lureManager.hasSpawned = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (_playerTestActions.recruited && canInteractPlayer)
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.gray;
            isRecruited = true;
            _lureManager.canLure = true;
        }
        
        if (_lureManager.lureCount > 4)
        {
            isRecruited = false;
            _lureManager.lureCount = 0;
            _lureManager.canLure = false;
            _lureManager.hasSpawned = false;
            Destroy(gameObject);
        }

        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canInteractPlayer = true;
            gameObject.GetComponent<SpriteRenderer>().sprite = selectedSprite;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canInteractPlayer = false;
            gameObject.GetComponent<SpriteRenderer>().sprite = normalSprite;
        }
    }

    
}
