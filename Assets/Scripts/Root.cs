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
    public enum State { Selecting, Building, Upgarding, Paused }
    public static State currentState = State.Building;
    /* ----------------------------------------------------------------
    Start is called before the first frame update.
    This creates the first node for the tree
    -----------------------------------------------------------------*/
    void Start()
    {
        AddNode(Vector3.zero);
        StartCoroutine(GrowthCheck());
    }

    private void Update() {
        if(TreeAttributes.storedNutrients < 0){
            LevelManager.LOADLEVEL("b02-Lose");
        }
    }

    private IEnumerator GrowthCheck(){
        while(true){
            TreeAttributes.GrowthIncrement();
            RootNodeAttributes[] nodes = FindObjectsOfType<RootNodeAttributes>();
            foreach (var node in nodes)
            {
                node.UpdateNutrientValue();
            }            
            yield return new WaitForSeconds(GrowthRate);
        }

    }

    /* ----------------------------------------------------------------
    PUBLIC FUNCTION CAN BE CALLED FROM OTHER SCRIPTS
    Called to add a node based on mouse position in the world. 
    -----------------------------------------------------------------*/
    public void ClickToInteractNode()
    {
        if(Input.GetMouseButtonDown(0)){
            ClickAction();
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
    public static RootNode GetActiveNode(){
        return activeNode;
    }

    private void ClickAction(){
        switch (currentState)
        {
            case State.Building:
                Vector3 pos = camControls.CalculateWorldPointOfMouseClick();
                AddNode(pos);
                TreeAttributes.GrowthIncrement();
                break;
            case State.Selecting:
                break;
            case State.Upgarding:
                break;
            default:
                break;
        }
    }

    public static void ChangeAction(State newState){
        if(newState != currentState){
            currentState = newState;
            switch (newState)
            {
                case State.Building:
                    activeNode.ChangeNode(Color.red, true);
                    break;
                case State.Selecting:
                    activeNode.ChangeNode(Color.red, false);
                    break;
                case State.Upgarding:
                    activeNode.ChangeNode(Color.red, false);
                    break;
                default:
                    break;
            }            
        }
    }
}
