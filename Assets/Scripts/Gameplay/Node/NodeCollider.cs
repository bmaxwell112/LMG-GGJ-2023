using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeCollider : MonoBehaviour
{
    RootNode node;
    [SerializeField] Sprite[] sprites;
    [SerializeField] RootNodeAttributes attributes;
    SpriteRenderer sr;
    Root root;
    TreeUI treeUI;
    CamControls camControls;
    // Start is called before the first frame update
    void Start()
    {
        node = GetComponentInParent<RootNode>();
        root = FindObjectOfType<Root>();
        sr = GetComponentInChildren<SpriteRenderer>();
        treeUI = FindObjectOfType<TreeUI>();
        camControls = FindObjectOfType<CamControls>();
        int r = Random.Range(0, sprites.Length-1);
        sr.sprite = sprites[r];        
    }

    private void OnMouseDown() {
        root.ClickToInteractNode(node);
    }
    private void OnMouseOver() {
        if(Root.currentState == State.Upgarding){
            int lvl = attributes.thickness;
            int cost = attributes.GetUpgradeCost();
            string costText = "";
            print(attributes.ThinnerThanParent());
            if(attributes.ThinnerThanParent()){
                costText = "Upgrade Vine\nCost: " + cost;            
            }else{
                costText = "No Upgrade\nAvailible";
            }
            treeUI.ToggleCostPanel(true, costText);
        }
    }
    private void OnMouseExit() {
        if(Root.currentState == State.Upgarding){
            treeUI.ToggleCostPanel(false);
        }
    }
}
