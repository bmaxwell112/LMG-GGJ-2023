using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeAttributes : MonoBehaviour
{
    // Progress between levels
    public static int growth = 0;
    // Current stage of growth of the tree
    public static int treeLevel = 0;    
    // Current amount of points gained from all nodes
    public static int nutrients = 0;
    // Used to scale the growth clicking bounding box
    public static int storedNutrients = 0;
    public static Vector2 growthScale = new Vector2(0.25f, 0.25f);
    
    private static RootNodeAttributes[] allNodes = new RootNodeAttributes[]{};
    private static Vector2 growthScaleThreshold = new Vector2(0.25f, 0.01f);
    private static int growthThreshold = 100;

    private static void CheckTreeGrowth(){
        // Check Tree Level and increase if needed
        if((growth >= growthThreshold * (treeLevel + 1)) && treeLevel < 5){
            treeLevel++;
            growth = 0;
            growthScale += growthScaleThreshold;
        }
    }

    public static void GrowthIncrement(){
        allNodes = FindObjectsOfType<RootNodeAttributes>();
        int newNetVal = 0;
        foreach (var node in allNodes) 
        {
            newNetVal += node.GetNutrient();
        }
        nutrients = newNetVal;
        storedNutrients += nutrients;
        //growth += nutrients;
        //CheckTreeGrowth();
    }    
}
