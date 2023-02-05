using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinScreenUI : MonoBehaviour
{

    [SerializeField] Text winText;

    // Start is called before the first frame update
    void Start()
    {
        winText.text = "Stored Nutrients: " + TreeAttributes.storedNutrients;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
