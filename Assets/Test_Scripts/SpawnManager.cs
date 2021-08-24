using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnManager : MonoBehaviour
{
    private PlayerMovement _playerMovement;
    
    public bool canNormalSpawn;

    [SerializeField] private GameObject normals;

    public List<Transform> normalsInScene;
    // Start is called before the first frame update
    void Start()
    {
        _playerMovement = FindObjectOfType<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        while (canNormalSpawn)
        {
            for (int i = 0; i < 5; i++)
            {
                Vector3 spawnPos = new Vector3(Random.Range(-8, 8), Random.Range(-4, 4), 0);
                if ((spawnPos - _playerMovement.transform.position).magnitude < 2)
                {
                    continue;
                }
                else
                {
                    Instantiate(normals, spawnPos, quaternion.identity);
                    canNormalSpawn = false;
                }
            }
        }
    }
}
