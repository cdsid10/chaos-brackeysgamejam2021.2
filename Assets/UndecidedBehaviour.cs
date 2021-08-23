using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class UndecidedBehaviour : MonoBehaviour
{
    private PlayerActions _playerActions;
    private NormalBehaviour _normalBehaviour;
    
    public bool canLure, limitReached;
    [SerializeField]
    int haveLured = 0;
    

    [SerializeField]
    private GameObject voidAnim;
    
    private NormalBehaviour[] arrayNormals;

    // Start is called before the first frame update
    void Start()
    {
        _playerActions = FindObjectOfType<PlayerActions>();
        _normalBehaviour = FindObjectOfType<NormalBehaviour>();
        arrayNormals = GameObject.FindObjectsOfType<NormalBehaviour>();
        foreach (var n in arrayNormals)
        {
            n._undecidedBehaviour = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_playerActions.isPressing)
        {
            _playerActions.Recruit();
        }

        if (_playerActions.isUndecidedRecruited)
        {
            canLure = true;
        }
    }
    

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Normals"))
        {
            if (canLure)
            {
                haveLured++;
            }
            
            if (haveLured >= 5)
            {
                limitReached = true;
                _playerActions.isUndecidedRecruited = false;
                canLure = false;
                gameObject.SetActive(false);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // if (other.CompareTag("Player"))
        // {
        //     canInteract = false;
        // }
    }
}
