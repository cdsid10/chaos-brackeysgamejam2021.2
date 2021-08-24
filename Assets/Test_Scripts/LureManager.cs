using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class LureManager : MonoBehaviour
{
    private Test_Undecided _testUndecided;
    private SpawnManager _spawnManager;
    
    public bool canLure;

    public int numberOfLured; 
    
    public List<GameObject> normalsGameObjects = new List<GameObject>();

    [SerializeField] private GameObject undecided;

    // Start is called before the first frame update
    void Start()
    {
        _testUndecided = FindObjectOfType<Test_Undecided>();
        _spawnManager = FindObjectOfType<SpawnManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (normalsGameObjects.Count > 4)
        {
            canLure = false;
            normalsGameObjects.Clear();
            _spawnManager.canNormalSpawn = true;

            if (_testUndecided == null)
            {
                Instantiate(undecided, transform.position, quaternion.identity);
            }
        }
    }
}
