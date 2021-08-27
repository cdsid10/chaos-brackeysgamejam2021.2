using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnManager : MonoBehaviour
{
    private PlayerMovement _playerMovement;
    private FameManager _fameManager;
    
    public bool hasHunterSpawned, hasOppSpawned;

    public int huntersPerished;

    [SerializeField] private GameObject normals;
    [SerializeField] private GameObject holyPeeps;
    [SerializeField] private GameObject[] obstacles;
    [SerializeField] private GameObject hunterSpawnPos;
    [SerializeField] private GameObject hunter, hunterKing;
    [SerializeField] private GameObject opportunists;
    [SerializeField] private GameObject flare;

    public List<GameObject> normalsInScene;

    // Start is called before the first frame update
    void Start()
    {
        _playerMovement = FindObjectOfType<PlayerMovement>();
        _fameManager = FindObjectOfType<FameManager>();

        for (var i = 0; i < 40; i++)
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

        for (var i = 0; i < 30; i++)
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
        if (normalsInScene.Count < 20)
        {
            for (var i = 0; i < 40; i++)
            {
                var spawnPos = new Vector3(Random.Range(-32, 32), Random.Range(-17, 17));
                if ((spawnPos - _playerMovement.transform.position).magnitude < 6)
                {
                    continue;
                }
                else
                {
                    Instantiate(normals, spawnPos, quaternion.identity);
                }
            }
        }

        if (_fameManager.fame >= 100 && !hasHunterSpawned && huntersPerished == 0)
        {
            hasHunterSpawned = true;
            Instantiate(hunter, hunterSpawnPos.transform.position, quaternion.identity);
        }
        else if (_fameManager.fame >= 150 && !hasHunterSpawned && huntersPerished == 1)
        {
            hasHunterSpawned = true;
            var opHunter = Instantiate(hunter, hunterSpawnPos.transform.position, quaternion.identity)as GameObject;
            opHunter.GetComponent<Test_Hunter>().speed = 2;
            opHunter.GetComponent<Test_Hunter>().health = 4;
            if (opHunter.GetComponent<Test_Hunter>().health <= 0)
            {
                hasHunterSpawned = false;
                huntersPerished++;
            }
        }
        else if (_fameManager.fame >= 200 && !hasHunterSpawned && huntersPerished == 2)
        {
            _playerMovement.moveSpeed = 4f;
            hasHunterSpawned = true;
            Instantiate(hunterKing, hunterSpawnPos.transform.position, quaternion.identity);
        }

        if (hasHunterSpawned && !hasOppSpawned)
        {
            var spawnPos = new Vector3(Random.Range(-32, 32), Random.Range(-17, 17));
            if ((spawnPos - _playerMovement.transform.position).magnitude > 10 &&
                (spawnPos - _playerMovement.transform.position).magnitude < 20)
            {
                Instantiate(opportunists, spawnPos, quaternion.identity);
                var flareObj =Instantiate(flare, spawnPos, quaternion.identity) as GameObject;
                flareObj.GetComponent<SpriteRenderer>().color = new Color32(159, 123, 53, 255);
                flareObj.GetComponent<Animator>().SetTrigger("spawn");
                Destroy(flareObj, 6);
            }
        }

        

    }
}
