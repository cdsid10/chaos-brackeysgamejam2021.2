using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupManager : MonoBehaviour
{
    private FameManager _fameManager;
    
    public int oppurtunistsInBag;
    public int oppurtunistsCost;
    
    // Start is called before the first frame update
    void Start()
    {
        _fameManager = FindObjectOfType<FameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
