using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnManager : MonoBehaviour
{
    private PlayerMovement _playerMovement;
    private FameManager _fameManager;

    [SerializeField]
    private Animator _animator;
    
    public bool hasHunterSpawned, hasOppSpawned;

    public int huntersPerished;

    [SerializeField] private GameObject normals;
    [SerializeField] private GameObject holyPeeps;
    [SerializeField] private GameObject[] obstacles;
    [SerializeField] private GameObject hunterSpawnPos;
    [SerializeField] private GameObject hunter, hunterKing;
    [SerializeField] private GameObject opportunists;
    [SerializeField] private GameObject flare;
    [SerializeField] private GameObject hunterText;
    [SerializeField] private GameObject hunterKingText;
    [SerializeField] private GameObject hunterImg2Hp;
    [SerializeField] private GameObject hunterImg3Hp;
    [SerializeField] private GameObject hunterImgKing;

    public List<GameObject> normalsInScene;
    private static readonly int CanShow = Animator.StringToHash("canShow");

    // Start is called before the first frame update
    void Start()
    {
        _playerMovement = FindObjectOfType<PlayerMovement>();
        _fameManager = FindObjectOfType<FameManager>();
        HolySpawn();
        ObstacleSpawn();
    }

    // Update is called once per frame
    void Update()
    {
        NormalsSpawn();
        HuntersSpawn();
        OpportunistsSpawn();
    }

    void HolySpawn()
    {
        for (var i = 0; i < 40; i++)
        {
            var spawnPos = new Vector3(Random.Range(-28, 28), Random.Range(-14.5f, 14.5f));
            if ((spawnPos - _playerMovement.transform.position).magnitude < 6)
            {
                continue;
            }
            else
            {
                Instantiate(holyPeeps, spawnPos, quaternion.identity);
            }
        }
    }

    void ObstacleSpawn()
    {
        for (var i = 0; i < 30; i++)
        {
            var spawnPos = new Vector3(Random.Range(-30, 30), Random.Range(-16, 16));
            if ((spawnPos - _playerMovement.transform.position).magnitude < 6 )
            {
                continue;
            }
            else
            {
                Instantiate(obstacles[Random.Range(0, obstacles.Length)], spawnPos, quaternion.identity);
            }
        }
    }

    void NormalsSpawn()
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
    }

    void HuntersSpawn()
    {
        if (_fameManager.fame >= 100 && !hasHunterSpawned && huntersPerished == 0)
        {
            hasHunterSpawned = true;
            StartCoroutine(Hunter2Ui());
            Instantiate(hunter, hunterSpawnPos.transform.position, quaternion.identity);
        }
        else if (_fameManager.fame >= 150 && !hasHunterSpawned && huntersPerished == 1)
        {
            hasHunterSpawned = true;
            StartCoroutine(Hunter3Ui());
            var opHunter = Instantiate(hunter, hunterSpawnPos.transform.position, quaternion.identity)as GameObject;
            opHunter.GetComponent<Test_Hunter>().speed = 2;
            opHunter.GetComponent<Test_Hunter>().health = 3;
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
            StartCoroutine(HunterKingUi());
            Instantiate(hunterKing, hunterSpawnPos.transform.position, quaternion.identity);
        }
    }

    void OpportunistsSpawn()
    {
        if (hasHunterSpawned && !hasOppSpawned)
        {
            var spawnPos = new Vector3(Random.Range(-28, 28), Random.Range(-14, 14));
            if ((spawnPos - _playerMovement.transform.position).magnitude > 10 &&
                (spawnPos - _playerMovement.transform.position).magnitude < 20)
            {
                if (huntersPerished <= 2)
                {
                    Instantiate(opportunists, spawnPos, quaternion.identity);
                    var flareObj =Instantiate(flare, spawnPos, quaternion.identity) as GameObject;
                    flareObj.GetComponent<SpriteRenderer>().color = new Color32(159, 123, 53, 150);
                    flareObj.GetComponent<Animator>().SetTrigger("spawn");
                    Destroy(flareObj, 6);
                }
            }
        }
    }

    IEnumerator Hunter2Ui()
    {
        hunterText.SetActive(true);
        hunterImg2Hp.SetActive(true);
        _animator.SetBool(CanShow, true);
        yield return new WaitForSeconds(4);
        hunterText.SetActive(false);
        hunterImg2Hp.SetActive(false);
        _animator.SetBool(CanShow, false);
    }
    
    IEnumerator Hunter3Ui()
    {
        hunterText.SetActive(true);
        hunterImg3Hp.SetActive(true);
        _animator.SetBool(CanShow, true);
        yield return new WaitForSeconds(4);
        hunterText.SetActive(false);
        hunterImg3Hp.SetActive(false);
        _animator.SetBool(CanShow, false);
    }
    
    IEnumerator HunterKingUi()
    {
        hunterKingText.SetActive(true);
        hunterImgKing.SetActive(true);
        _animator.SetBool(CanShow, true);
        yield return new WaitForSeconds(4);
        hunterKingText.SetActive(false);
        hunterImgKing.SetActive(false);
        _animator.SetBool(CanShow, false);
    }
}
