using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class DamageFame : MonoBehaviour
{
    private FameManager _fameManager;
    private AudioManager _audioManager;
    private PlayerMovement _playerMovement;

    // Start is called before the first frame update
    void Start()
    {
        _fameManager = FindObjectOfType<FameManager>();
        _audioManager = FindObjectOfType<AudioManager>();
        _playerMovement = FindObjectOfType<PlayerMovement>();
        StartCoroutine(Dps());
    }
    
    IEnumerator Dps()
    {
        yield return new WaitForSeconds(1f);
        while (_fameManager.fame > -10)
        {
            _fameManager.SubFame();
            _audioManager.PlayHit();
            yield return new WaitForSeconds(1f);
        }
    }
}
