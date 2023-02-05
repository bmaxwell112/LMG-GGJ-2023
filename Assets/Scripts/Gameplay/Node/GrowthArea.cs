using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowthArea : MonoBehaviour
{
    Root root;
    [SerializeField] RootNodeAttributes attributes;
    [SerializeField] RootNode node;
    TreeUI treeUI;
    CamControls camControls;
    // Start is called before the first frame update
    void Start()
    {
        treeUI = FindObjectOfType<TreeUI>();
        root = FindObjectOfType<Root>();
        camControls = FindObjectOfType<CamControls>();
    }

    private void OnMouseOver() {
        Vector3 pos = camControls.CalculateWorldPointOfMouseClick();
        float dist = Vector2.Distance(node.transform.position, pos);
        int cost = attributes.GetBuildCost(dist);        
        string costText = "Extend Vine\nCost: " + cost;

        treeUI.ToggleCostPanel(true, costText);

    }
    private void OnMouseExit() {
        treeUI.ToggleCostPanel(false);
    }

    private void OnMouseDown() {        
        root.ClickToInteractNode(node);        
    }
}
