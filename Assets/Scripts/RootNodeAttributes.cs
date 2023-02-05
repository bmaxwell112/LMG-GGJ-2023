using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootNodeAttributes : MonoBehaviour
{
    // Used to determine Energy
    public int nutrient = 0;
    public int henderance = 0;
    private float rootLength = 0;

    // Used to determine how many children nodes it can have
    public int thickness = 1;    
    public int defaultThicknessCost = 100;

    public void UpdateNutrientValue(){
        var parents = GetComponentsInParent<RootNodeAttributes>();
        int outLen = 0;
        float totalLen = 0;
        foreach (var child in parents)
        {
            int len = GetTopLevelRootNodesLength();
            if(len > (2 * child.thickness) ){
                outLen += (len - (2 * child.thickness));
            }
            float dist = 0;
            if(child.transform.parent != null) {
                dist = Vector2.Distance(child.transform.position, child.transform.parent.position);
            }
            totalLen += dist;
        }
        int distOffset = 0;
        if(transform.parent != null){
            distOffset = transform.parent.GetComponentInParent<RootNodeAttributes>().thickness - Mathf.RoundToInt(totalLen);
        }
        henderance = outLen;
        nutrient = Mathf.RoundToInt(rootLength * 10f) - henderance + distOffset;
    }

    public int GetTopLevelRootNodesLength(){
        int len = 0;
        var tlc = Util.GetTopLevelChildren(this.transform);
        foreach (var c in tlc)
        {
            if (c.GetComponent<RootNodeAttributes>()){
                len++;
            }
        }
        return len;
    }

    public void DetermineNutrientValue(float rootLength){
        this.rootLength = rootLength;
        UpdateNutrientValue();        
    }

    public int GetNutrient(){        
        return nutrient;
    }

    public int GetUpgradeCost(){
        return defaultThicknessCost * thickness;
    }
    public void UpgradeThickness(){
        int cost = GetUpgradeCost();
        bool lowerThanParent = true;
        if(transform.parent != null){
            RootNodeAttributes parent = transform.parent.GetComponent<RootNodeAttributes>();
            lowerThanParent = (thickness < parent.thickness);
        }        
        if(TreeAttributes.storedNutrients >= cost && lowerThanParent){
            thickness++;
            TreeAttributes.storedNutrients -= cost;
            GetComponent<RootNode>().DrawLine((thickness / 8f));
        }
    }
    
    public int GetBuildCost(float distance){
        int rootLen = GetComponentsInParent<RootNodeAttributes>().Length;
        return Mathf.RoundToInt(distance * 100 * rootLen);
    }    
}
