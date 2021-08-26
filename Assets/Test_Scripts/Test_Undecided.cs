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

    private bool canLure;
    public bool isRecruited;

    [SerializeField]
    private List<GameObject> normalsGameObjects = new List<GameObject>();

    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        _playerTestActions = FindObjectOfType<PlayerTestActions>();
        _lureManager = FindObjectOfType<LureManager>();
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
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canInteractPlayer = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canInteractPlayer = false;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        
    }

    IEnumerator AddToList()
    {
        _lureManager.normalsGameObjects.Add(FindObjectOfType<Test_Normal>().gameObject);
        // _lureManager.numberOfLured++;
        // if (_lureManager.normalsGameObjects.Count > _lureManager.numberOfLured)
        // {
        //     _lureManager.normalsGameObjects.Remove(FindObjectOfType<Test_Normal>().gameObject);
        // }
        if (_lureManager.normalsGameObjects.Count > 4)
        {
            yield return new WaitForSeconds(0.5f);
            Destroy(gameObject);
        }
    }

    IEnumerator Timer()
    {
        yield return new WaitForSeconds(2);
        
    }
    
}
