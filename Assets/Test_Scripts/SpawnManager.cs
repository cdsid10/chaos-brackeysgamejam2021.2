using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnManager : MonoBehaviour
{
    private PlayerMovement _playerMovement;
    private HolyDamage _holyDamage;
    
    public bool canNormalSpawn;

    [SerializeField] private GameObject normals;
    [SerializeField] private GameObject holyPeeps;
    [SerializeField] private GameObject[] obstacles;

    public List<GameObject> normalsInScene;
    // Start is called before the first frame update
    void Start()
    {
        _playerMovement = FindObjectOfType<PlayerMovement>();
        _holyDamage = FindObjectOfType<HolyDamage>();
        
        for (int i = 0; i < 40; i++)
        {
            var spawnPos = new Vector3(Random.Range(-28, 28), Random.Range(-14.5f, 14.5f));
            if ((spawnPos - _playerMovement.transform.position).magnitude < 4)
            {
                continue;
            }
            else
            {
                Instantiate(holyPeeps, spawnPos, quaternion.identity);
            }
        }

        for (int i = 0; i < 30; i++)
        {
            var spawnPos = new Vector3(Random.Range(-30, 30), Random.Range(-16, 16));
            if ((spawnPos - _playerMovement.transform.position).magnitude < 4 )
            {
                continue;
            }
            else
            {
                Instantiate(obstacles[Random.Range(0, obstacles.Length)], spawnPos, quaternion.identity);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (normalsInScene.Count < 10)
        {
            for (int i = 0; i < 35; i++)
            {
                Vector3 spawnPos = new Vector3(Random.Range(-32, 32), Random.Range(-17, 17));
                if ((spawnPos - _playerMovement.transform.position).magnitude < 5)
                {
                    continue;
                }
                else
                {
                    Instantiate(normals, spawnPos, quaternion.identity);
                }
            }
        }

        
    }
}
