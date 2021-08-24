using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineDamage : MonoBehaviour
{
    private Test_Hunter _testHunter;
    
    // Start is called before the first frame update
    void Start()
    {
        _testHunter = FindObjectOfType<Test_Hunter>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Hunter"))
        {
            _testHunter.health--;
            Destroy(gameObject);
        }
    }
}
