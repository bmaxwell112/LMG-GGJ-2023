using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Root : MonoBehaviour
{

    [SerializeField] private float treeLength;
    [SerializeField] private RootNode node;
    [SerializeField] private static RootNode activeNode = null;
    [SerializeField] private CamControls camControls;
    [SerializeField] private float GrowthRate = 1;
    /* ----------------------------------------------------------------
    Start is called before the first frame update.
    This creates the first node for the tree
    -----------------------------------------------------------------*/
    void Start()
    {
        AddNode(Vector3.zero);
    }

    private void Update() {
        string s = "Tree Level: " + TreeAttributes.treeLevel + "\n" +
                   "Total Nutrients: " + TreeAttributes.nutrients + "\n" +
                   "Total Growth: " + TreeAttributes.growth + "\n";
        print(s);
    }

    private IEnumerator GrowthCheck(){
        while(true){
            TreeAttributes.GrowthIncrement();
            yield return new WaitForSeconds(GrowthRate);
        }

    }

    /* ----------------------------------------------------------------
    PUBLIC FUNCTION CAN BE CALLED FROM OTHER SCRIPTS
    Called to add a node based on mouse position in the world. 
    -----------------------------------------------------------------*/
    public void ClickToAddNode()
    {
        if(Input.GetMouseButtonDown(0)){
            Vector3 pos = camControls.CalculateWorldPointOfMouseClick();
            AddNode(pos);
            TreeAttributes.GrowthIncrement();
        }
    }

    /* ----------------------------------------------------------------
    Instantiates a new  node at a location provided. Currently only
    called by Start and ClickToAddNode
    -----------------------------------------------------------------*/
    private void AddNode(Vector3 position){
        RootNode newNode = Instantiate(node, position, Quaternion.identity);
        if(activeNode != null){
            newNode.Initialize(activeNode.GetGeneration(), activeNode);            
        } else {
            print("Initial");
            newNode.Initialize(0);
        }
        ChangeActiveNode(newNode);
        camControls.WidenField(newNode);
    }

    /* ----------------------------------------------------------------
    Changes the active node by disabling some function of the current
    active node, assigning the input node to active then enabling it's
    functions.
    -----------------------------------------------------------------*/
    public static void ChangeActiveNode(RootNode node){
        if(activeNode != null){
            activeNode.ChangeNode(Color.white, false);
        }
        activeNode = node;
        activeNode.ChangeNode(Color.red, true);
    }
}
