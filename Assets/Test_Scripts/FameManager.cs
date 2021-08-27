using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FameManager : MonoBehaviour
{
    [SerializeField]
    public Animator _animator;
    
    [SerializeField]
    private TextMeshProUGUI _fameText, _fameAddSubText;
    
    public int fame;

    private Color32 orange = new Color32(209, 134, 70, 255);
    private Color32 white = new Color32(255, 255, 255, 255);

    // Start is called before the first frame update
    void Start()
    {
        fame = 100;
    }

    // Update is called once per frame
    void Update()
    {
        _fameText.text = ("FAME: " + fame.ToString());

        // if (fame >= 100)
        // {
        //     _fameText.color = Color.green;
        // }
        // else if (fame > 50 && fame < 100)
        // {
        //     _fameText.color = orange;
        // }
        // else if (fame <= 50)
        // {
        //     _fameText.color = Color.red;
        // }
        
        if (fame <= 0)
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

    IEnumerator FameSub()
    {
        _fameAddSubText.gameObject.SetActive(true);
        _fameAddSubText.color = Color.red;
        _fameAddSubText.text = "-10";
        _animator.SetTrigger("change");
        fame -= 10;
        yield return new WaitForSeconds(1.5f);
        _fameAddSubText.gameObject.SetActive(false);
    }

    IEnumerator FameAdd()
    {
        _fameAddSubText.gameObject.SetActive(true);
        _fameAddSubText.color = Color.green;
        _fameAddSubText.text = "+10";
        _animator.SetTrigger("change");
        fame += 10;
        yield return new WaitForSeconds(1.5f);
        _fameAddSubText.gameObject.SetActive(false);
    }
    
}
