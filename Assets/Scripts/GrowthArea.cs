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
        IsEnabled();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void IsEnabled(){        
        transform.localScale = new Vector3(TreeAttributes.growthScale.x, TreeAttributes.growthScale.y, 1);
    }

    private void OnMouseDown() {        
        root.ClickToAddNode();
        this.gameObject.SetActive(false);
    }
}
