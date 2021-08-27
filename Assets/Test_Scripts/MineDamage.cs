using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MineDamage : MonoBehaviour
{
    private Test_Hunter _testHunter;
    private Test_HunterKing _testHunterKing;
    
    [SerializeField]
    private GameObject oppUi;


    private void Awake()
    {
        InvokeRepeating(nameof(CheckHunter), 2, 2);
        InvokeRepeating(nameof(CheckHunterKing), 2, 2);
    }

    // Start is called before the first frame update
    void Start()
    {
        _testHunter = FindObjectOfType<Test_Hunter>();
        _testHunterKing = FindObjectOfType<Test_HunterKing>();
        oppUi = GameObject.FindGameObjectWithTag("MineUI");
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
            //oppUi.GetComponent<Image>().color = new Color(1, 1, 1, 0);
            Destroy(gameObject);
        }

        if (other.CompareTag("HunterKing"))
        {
            _testHunterKing.health--;
            //oppUi.GetComponent<Image>().color = new Color(1, 1, 1, 0);
            Destroy(gameObject);
        }
    }
    
    void CheckHunter()
    {
        _testHunter = FindObjectOfType<Test_Hunter>();
    }

    void CheckHunterKing()
    {
        _testHunterKing = FindObjectOfType<Test_HunterKing>();
    }
}
