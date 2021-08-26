using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class PlayerTestActions : MonoBehaviour
{
    private PickupManager _pickupManager;
    private FameManager _fameManager;
    private Test_Normal _testNormal;

    [SerializeField] private Animator fillAnimator;
    
    private bool canInteractUndecided;
    public bool recruited;
    private bool canInteractOpportunists;
    private bool canInteractNormals;
    public bool hasUsed;
    public bool isFamed;

    private float holdTimer;
    public Image fillArea;

    [SerializeField] private GameObject opportunists;
    private static readonly int IsFilled = Animator.StringToHash("isFilled");


    // Start is called before the first frame update
    void Start()
    {
        _pickupManager = FindObjectOfType<PickupManager>();
        _fameManager = FindObjectOfType<FameManager>();
        _testNormal = FindObjectOfType<Test_Normal>();
        fillArea.fillAmount = 0;
        fillAnimator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        InteractWUndecided();
        InteractWOpportunists();
        //InteractWNormals();

        if (_pickupManager.oppurtunistsInBag > 0)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Instantiate(opportunists, transform.position, quaternion.identity);
                _pickupManager.oppurtunistsInBag--;
            }
        }

        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Undecided"))
        {
            canInteractUndecided = true;
        }
        
        if (other.CompareTag("Opportunists"))
        {
            canInteractOpportunists = true;
        }

        if (other.CompareTag("Normals"))
        {
            canInteractNormals = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Undecided"))
        {
            canInteractUndecided = false;
        }
        
        if (other.CompareTag("Opportunists"))
        {
            canInteractOpportunists = false;
        }
        
        if (other.CompareTag("Normals"))
        {
            canInteractNormals = false;
        }
    }

    void InteractWUndecided()
    {
        if (canInteractUndecided)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                holdTimer += Time.deltaTime;
                fillArea.fillAmount = holdTimer / 2;
            }
            
            if (Input.GetKeyUp(KeyCode.Space) && holdTimer > 2)
            {
                recruited = true;
                holdTimer = 0;
                fillArea.fillAmount = 0;
            }
            else if (Input.GetKeyUp(KeyCode.Space) && holdTimer < 2)
            {
                recruited = false;
                holdTimer = 0;
                fillArea.fillAmount = 0;
            }
        }
    }

    void InteractWOpportunists()
    {
        if (canInteractOpportunists)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                holdTimer += Time.deltaTime;
                fillArea.fillAmount = holdTimer / 2;
            }
            
            if (Input.GetKeyUp(KeyCode.Space) && holdTimer > 2)
            {
                hasUsed = true;
                _fameManager.fame -= _pickupManager.oppurtunistsCost;
                _pickupManager.oppurtunistsInBag++;
                holdTimer = 0;
                fillArea.fillAmount = 0;
            }

            if (Input.GetKeyUp(KeyCode.Space) && holdTimer < 2)
            {
                holdTimer = 0;
                fillArea.fillAmount = 0;
            }
            
            fillAnimator.SetBool(IsFilled, fillArea.fillAmount >= 1);
        }
        else
        {
            holdTimer = 0;
            fillArea.fillAmount = 0;
        }
    }

    void InteractWNormals()
    {
        if (!canInteractNormals) return;
        if (!_testNormal.isVulnerable) return;
        if (!Input.GetKeyDown(KeyCode.Space)) return;
        Debug.Log("famed");
        isFamed = true;
        _fameManager.fame += 10;
    }
}
