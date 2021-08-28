using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FameManager : MonoBehaviour
{
    private SpawnManager _spawnManager;
    
    [SerializeField]
    public Animator _animator;
    
    [SerializeField]
    private TextMeshProUGUI _fameText, _fameAddSubText;
    
    public int fame;

    private Color32 orange = new Color32(220, 140, 60, 255);
    private Color32 green = new Color32(50,160,80,255);
    private Color32 red = new Color32(150,30,40,255);
    

    // Start is called before the first frame update
    void Start()
    {
        _spawnManager = FindObjectOfType<SpawnManager>();
        //fame = 100;
    }

    // Update is called once per frame
    void Update()
    {
        _fameText.text = ("" + fame.ToString());

        if (fame >= 200)
        {
            _fameText.color = green;
        }
        else if (fame > 50 && fame < 200)
        {
            _fameText.color = orange;
        }
        else if (fame <= 50)
        {
            _fameText.color = red;
        }
        
        if (fame < 0)
        {
            Debug.Log("End");
        }
    }

    public void AddFame()
    {
        StartCoroutine(FameAdd());
    }

    public void SubFame()
    {
        StartCoroutine(FameSub());
    }

    public void SubOpFame()
    {
        StartCoroutine(SubOppFame());
    }

    IEnumerator SubOppFame()
    {
        _fameAddSubText.gameObject.SetActive(true);
        _fameAddSubText.color = red;
        _fameAddSubText.text = "-50";
        _animator.SetTrigger("change");
        fame -= 50;
        yield return new WaitForSeconds(1.5f);
        _fameAddSubText.gameObject.SetActive(false);
    }

    IEnumerator FameSub()
    {
        if (_spawnManager.huntersPerished >= 2)
        {
            _fameAddSubText.gameObject.SetActive(true);
            _fameAddSubText.color = red;
            _fameAddSubText.text = "-20";
            _animator.SetTrigger("change");
            fame -= 20;
            yield return new WaitForSeconds(1.5f);
            _fameAddSubText.gameObject.SetActive(false);
        }
        else if(_spawnManager.huntersPerished < 2)
        {
            _fameAddSubText.gameObject.SetActive(true);
            _fameAddSubText.color = red;
            _fameAddSubText.text = "-10";
            _animator.SetTrigger("change");
            fame -= 10;
            yield return new WaitForSeconds(1.5f);
            _fameAddSubText.gameObject.SetActive(false);
        }
    }

    IEnumerator FameAdd()
    {
        _fameAddSubText.gameObject.SetActive(true);
        _fameAddSubText.color = green;
        _fameAddSubText.text = "+10";
        _animator.SetTrigger("change");
        fame += 10;
        yield return new WaitForSeconds(1f);
        _fameAddSubText.gameObject.SetActive(false);
    }
    
}
