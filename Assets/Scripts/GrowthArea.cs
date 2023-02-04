using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowthArea : MonoBehaviour
{
    Root root;
    // Start is called before the first frame update
    void Start()
    {
        root = FindObjectOfType<Root>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown() {
        print("Adding Node");
        root.ClickToAddNode();
        this.gameObject.SetActive(false);
    }
}
