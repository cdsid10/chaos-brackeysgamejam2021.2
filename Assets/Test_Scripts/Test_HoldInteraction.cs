using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Test_HoldInteraction : MonoBehaviour
{
    public float holdTimer = 0f;

    public Image fillArea;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            holdTimer += Time.deltaTime;
            fillArea.fillAmount = 0.5f * holdTimer;
        }

        if (Input.GetKeyUp(KeyCode.Space) && holdTimer > 2)
        {
            Debug.Log("Interacted");
            holdTimer = 0;
            fillArea.fillAmount = 0;
        }

        if (Input.GetKeyUp(KeyCode.Space) && holdTimer < 2)
        {
            holdTimer = 0;
            fillArea.fillAmount = 0;
        }
            
    }
}
