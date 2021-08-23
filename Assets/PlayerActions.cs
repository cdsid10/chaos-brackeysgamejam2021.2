using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    private UndecidedBehaviour _undecidedBehaviour;

    public bool isPressing;
    
    // Start is called before the first frame update
    void Start()
    {
        _undecidedBehaviour = FindObjectOfType<UndecidedBehaviour>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_undecidedBehaviour.canInteract)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                isPressing = true;
            }
        }
    }

    public void Recruit()
    {
        
        _undecidedBehaviour.gameObject.GetComponent<SpriteRenderer>().color = Color.gray;
    }

    
}
