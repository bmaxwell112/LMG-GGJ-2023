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
    [SerializeField] Button build;
    [SerializeField] Button select;
    [SerializeField] Button upgrade;

    RootNode active = null;

    private void Start() {
        active = Root.GetActiveNode();
        costPanel.SetActive(false);        

        build.onClick.AddListener(delegate {ChangeStateBtn(State.Building, build);});
        select.onClick.AddListener(delegate {ChangeStateBtn(State.Selecting, select);});
        upgrade.onClick.AddListener(delegate {ChangeStateBtn(State.Upgarding, upgrade);});
        build.interactable  = false;
    }
    private void Update() {
        if(active != Root.GetActiveNode()){
            active = Root.GetActiveNode();
        }
        int branchLen = active.GetAttributes().GetTopLevelRootNodesLength();        
        int thickness = active.GetAttributes().thickness;
        nodeName.text = active.gameObject.name;
        stats.text = "Nutrients: " + active.GetAttributes().GetNutrient() + "\n" +
                     "Root Thickness: " + thickness + "\n" +
                     "Branches: " + branchLen + "\n" +
                     "Branch Cap: " + (thickness * 2);
                        
                        
        score.text = "Stored: " + TreeAttributes.storedNutrients + "\n" +
                     "Incoming: " + TreeAttributes.nutrients + "\n" +
                     "Consuming: " + TreeAttributes.NaturalDrain();;

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

    public void ChangeStateBtn(State newState, Button newSelBtn){
        Root.ChangeAction(newState);
        build.interactable  = true;
        select.interactable  = true;
        upgrade.interactable  = true;
        newSelBtn.interactable  = false;
    }
}
