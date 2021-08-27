using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;
using Vector3 = UnityEngine.Vector3;

public class LureManager : MonoBehaviour
{
    private PlayerMovement _playerMovement;
    private Test_Undecided _testUndecided;
    private SpawnManager _spawnManager;
    
    public bool canLure;
    public bool hasSpawned = true;
    
    public int lureCount = 0;
    
    public List<GameObject> normalsGameObjects = new List<GameObject>();

    [SerializeField] private GameObject undecided;
    [SerializeField] private GameObject flare;

    // Start is called before the first frame update
    void Start()
    {
        _playerMovement = FindObjectOfType<PlayerMovement>();
        _testUndecided = FindObjectOfType<Test_Undecided>();
        _spawnManager = FindObjectOfType<SpawnManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasSpawned)
        {
            Vector3 spawnPos = new Vector3(Random.Range(-32, 32), Random.Range(-17, 17));
            if ((spawnPos - _playerMovement.transform.position).magnitude > 10 && (spawnPos - _playerMovement.transform.position).magnitude < 20)
            {
                Instantiate(undecided, spawnPos, quaternion.identity);
                var flareObj =Instantiate(flare, spawnPos, quaternion.identity) as GameObject;
                flareObj.GetComponent<Animator>().SetTrigger("spawn");
                Destroy(flareObj, 6);
                
            }
        }
        
        // if (normalsGameObjects.Count > 4)
        // {
        //     canLure = false;
        //     normalsGameObjects.Clear();
        //     _spawnManager.canNormalSpawn = true;
        //
        //     if (_testUndecided == null)
        //     {
        //         Instantiate(undecided, transform.position, quaternion.identity);
        //     }
        // }
    }
}
