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
    private SpawnManager _spawnManager;
    private AudioManager _audioManager;

    [SerializeField] private Animator fillAnimator;
    
    private bool canInteractUndecided;
    public bool recruited;
    private bool canInteractOpportunists;
    private bool canInteractNormals;
    public bool hasUsed;
    public bool isFamed;

    public int firstTriggerUnd = 0;
    public int firstTriggerOpp = 0;

    [SerializeField]
    private float holdTimer;
    public Image fillArea;
    private GameObject oppUi;

    [SerializeField] private GameObject opportunists;
    [SerializeField] private GameObject ControlsImgUi;
    [SerializeField] private GameObject tutorialText, tutorialTextOpp;
    [SerializeField] private GameObject fameText, fameKingText, costText;

    private static readonly int IsFilled = Animator.StringToHash("isFilled");


    // Start is called before the first frame update
    void Start()
    {
        _pickupManager = FindObjectOfType<PickupManager>();
        _fameManager = FindObjectOfType<FameManager>();
        _testNormal = FindObjectOfType<Test_Normal>();
        fillArea.fillAmount = 0;
        fillAnimator = GetComponentInChildren<Animator>();
        oppUi = GameObject.FindGameObjectWithTag("MineUI");
        _spawnManager = FindObjectOfType<SpawnManager>();
        _audioManager = FindObjectOfType<AudioManager>();
        StartCoroutine(ControlsUi());
    }

    // Update is called once per frame
    void Update()
    {
        InteractWUndecided();
        InteractWOpportunists();
        InteractWNormals();
        
        if (firstTriggerUnd == 1)
        {
            StartCoroutine(FirstTrigger());
        }

        if (firstTriggerOpp == 1)
        {
            StartCoroutine(FirstTriggerOpp());
        }

        

        if (_pickupManager.oppurtunistsInBag > 0)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Instantiate(opportunists, transform.position, quaternion.identity);
                oppUi.GetComponent<Image>().color = new Color(1, 1, 1, 0);
                _pickupManager.oppurtunistsInBag--;
            }
        }

        if (_spawnManager.huntersPerished == 1 && !_spawnManager.hasHunterSpawned)
        {
            StartCoroutine(FirstHunterDead());
        }
        else if (_spawnManager.huntersPerished == 2 && !_spawnManager.hasHunterSpawned)
        {
            StartCoroutine(SecondHunterDead());
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Undecided"))
        {
            canInteractUndecided = true;
            firstTriggerUnd++;
        }
        
        if (other.CompareTag("Opportunists"))
        {
            canInteractOpportunists = true;
            firstTriggerOpp++;
            if (_fameManager.fame < 50)
            {
                StartCoroutine(CostShow());
            }
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
            recruited = Input.GetKeyDown(KeyCode.Space) ? true : false;
        }
    }

    void InteractWOpportunists()
    {
        if (canInteractOpportunists)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                if (_fameManager.fame >= 50)
                {
                    holdTimer += Time.deltaTime;
                    if (_spawnManager.huntersPerished >= 2)
                    {
                        fillArea.fillAmount = holdTimer / 1.5f;
                    }
                    else
                    {
                        fillArea.fillAmount = holdTimer / 2;
                    }
                    fillAnimator.SetBool(IsFilled, fillArea.fillAmount >= 1);
                }
                else
                {
                    //Ui to show cant interact, low fame.
                }
            }

            if (Input.GetKeyUp(KeyCode.Space) && holdTimer >= 1.5f && _spawnManager.huntersPerished >= 2)
            {
                hasUsed = true;
                _fameManager.SubOpFame();
                _pickupManager.oppurtunistsInBag++;
                holdTimer = 0;
                fillArea.fillAmount = 0;
            }
            else if (Input.GetKeyUp(KeyCode.Space) && holdTimer >= 2 && _spawnManager.huntersPerished < 2)
            {
                hasUsed = true;
                _fameManager.SubOpFame();
                _pickupManager.oppurtunistsInBag++;
                holdTimer = 0;
                fillArea.fillAmount = 0;
            }

            if (Input.GetKeyUp(KeyCode.Space) && holdTimer < 1.5f && _spawnManager.huntersPerished >= 2)
            {
                holdTimer = 0;
                fillArea.fillAmount = 0;
            }
            else if (Input.GetKeyUp(KeyCode.Space) && holdTimer < 2 && _spawnManager.huntersPerished < 2)
            {
                holdTimer = 0;
                fillArea.fillAmount = 0;
            }
            
            
        }
        else
        {
            holdTimer = 0;
            fillArea.fillAmount = 0;
        }
    }

    void InteractWNormals()
    {
        // if (!canInteractNormals) return;
        // if (!_testNormal.isVulnerable) return;
        // if (!Input.GetKeyDown(KeyCode.Space)) return;
        // Debug.Log("famed");
        // isFamed = true;
        // _fameManager.fame += 10;

        if (canInteractNormals)
        {
            if (_testNormal.isVulnerable)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    Debug.Log("fame");
                }
            }
        }
    }

    IEnumerator ControlsUi()
    {
        yield return new WaitForSeconds(4);
        ControlsImgUi.SetActive(false);
    }
    
    IEnumerator FirstTrigger()
    {
        tutorialText.SetActive(true);
        _audioManager.PlayUiSound();
        yield return new WaitForSeconds(4);
        tutorialText.SetActive(false);
    }
    
    IEnumerator FirstTriggerOpp()
    {
        tutorialTextOpp.SetActive(true);
        _audioManager.PlayUiSound();
        yield return new WaitForSeconds(4);
        tutorialTextOpp.SetActive(false);
    }

    IEnumerator FirstHunterDead()
    {
        fameText.SetActive(true);
        yield return new WaitForSeconds(4);
        Destroy(fameText);
    }

    IEnumerator SecondHunterDead()
    {
        fameKingText.SetActive(true);
        yield return new WaitForSeconds(4);
        Destroy(fameKingText);
    }
    
    IEnumerator CostShow()
    {
        costText.SetActive(true);
        yield return new WaitForSeconds(3);
        costText.SetActive(false);
    }
}
