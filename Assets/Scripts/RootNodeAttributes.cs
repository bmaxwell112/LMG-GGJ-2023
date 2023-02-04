using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootNodeAttributes : MonoBehaviour
{
    // Used to determine Energy
    public int nutrient = 0;

    // Used to determine how many children nodes it can have
    public int thickness = 1;
    public int defaultThicknessCost = 100;


    public void DetermineNutrientValue(float rootLength){
        nutrient = Mathf.RoundToInt(rootLength * 10f);
    }

    public int GetNutrient(){
        return nutrient;
    }

    public int GetUpgradeCost(){
        return defaultThicknessCost * thickness;
    }
    public void UpgradeThickness(){
        int cost = GetUpgradeCost();
        if(TreeAttributes.storedNutrients >= cost){
            thickness++;
            TreeAttributes.storedNutrients -= cost;
        }
    }
    
    public int GetBuildCost(float distance){
        return Mathf.RoundToInt(distance * 100) - thickness;
    }
    
}
