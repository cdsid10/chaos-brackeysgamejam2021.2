using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class UndecidedBehaviour : MonoBehaviour
{
    private PlayerActions _playerActions;
    private NormalBehaviour _normalBehaviour;
    
    public bool canInteract;
    
    [SerializeField]
    private GameObject voidAnim;

    // Start is called before the first frame update
    void Start()
    {
        _playerActions = FindObjectOfType<PlayerActions>();
        _normalBehaviour = FindObjectOfType<NormalBehaviour>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_playerActions.isPressing)
        {
            _playerActions.Recruit();
            
            _normalBehaviour.isLured = true;
        }
    }
    

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canInteract = true;
        }

        if (other.CompareTag("Normals") && _normalBehaviour.isLured)
        {
            Instantiate(voidAnim, transform.position, quaternion.identity);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canInteract = false;
        }
    }
}
