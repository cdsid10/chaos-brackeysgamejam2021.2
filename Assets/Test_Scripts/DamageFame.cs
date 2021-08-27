using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageFame : MonoBehaviour
{
    private FameManager _fameManager;

    // Start is called before the first frame update
    void Start()
    {
        _fameManager = FindObjectOfType<FameManager>();
        StartCoroutine(Dps());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Dps()
    {
        yield return new WaitForSeconds(1);
        while (_fameManager.fame > 0)
        {
            _fameManager.SubFame();
            yield return new WaitForSeconds(1.5f);
        }
    }
}
