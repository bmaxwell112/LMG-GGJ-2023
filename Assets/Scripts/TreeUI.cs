using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TreeUI : MonoBehaviour
{
    [SerializeField] Text nodeName;
    [SerializeField] Text stats;
    [SerializeField] Text score;   
    [SerializeField] Text cost;
    [SerializeField] GameObject costPanel;

    RootNode active = null;

    private void Start() {
        active = Root.GetActiveNode();
        costPanel.SetActive(false);
    }
    private void Update() {
        if(active != Root.GetActiveNode()){
            active = Root.GetActiveNode();
        }
        int branchLen = active.GetAttributes().GetTopLevelRootNodesLength();
        int branchCap = active.GetAttributes().thickness * 2;
        nodeName.text = active.gameObject.name;
        stats.text = "Nutrients: " + active.GetAttributes().GetNutrient() + "\n" +
                     "Branches: " + branchLen + "\n" +
                     "Branch Cap: " + branchCap;
                        
                        
        score.text = "Stored: " + TreeAttributes.storedNutrients + "\n" +
                     "Gains: " + TreeAttributes.nutrients;
    }

    public void UpgradeBtnPress(){
        active.GetAttributes().UpgradeThickness();
    }    

    public void ToggleCostPanel(bool active, string costText = ""){
        costPanel.gameObject.SetActive(active);
        if(costPanel.activeSelf){
            cost.text = costText;  
        }              
    }
}
