using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum State { Selecting, Building, Upgarding, Paused }

public class Root : MonoBehaviour
{

    [SerializeField] private float treeLength;
    [SerializeField] private RootNode node;
    [SerializeField] private static RootNode activeNode = null;
    [SerializeField] private CamControls camControls;
    [SerializeField] private float GrowthRate = 1;    
    [SerializeField] Sprite[] treeGrowthSprites;
    [SerializeField] float[] treeGrowTimeInSeconds = new float[] { 30f, 90f, 180f };
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip[] rootSFX;
    SpriteRenderer sr;        
    public static State currentState = State.Building;
    /* ----------------------------------------------------------------
    Start is called before the first frame update.
    This creates the first node for the tree
    -----------------------------------------------------------------*/
    void Start()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
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

            // Increase difficulty over time
            if(Time.timeSinceLevelLoad > treeGrowTimeInSeconds[TreeAttributes.treeLevel]){
                TreeAttributes.treeLevel++;
                if(TreeAttributes.treeLevel < treeGrowthSprites.Length){
                    sr.sprite = treeGrowthSprites[TreeAttributes.treeLevel];
                    if(TreeAttributes.treeLevel < 4){
                        camControls.WidenField(2);
                    }
                    
                } else {
                    LevelManager.LOADLEVEL("b01-Win");
                }
            }
            
            yield return new WaitForSeconds(GrowthRate);
        }

    }

    /* ----------------------------------------------------------------
    PUBLIC FUNCTION CAN BE CALLED FROM OTHER SCRIPTS
    Called to add a node based on mouse position in the world. 
    -----------------------------------------------------------------*/
    public void ClickToInteractNode(RootNode node = null)
    {
        if(Input.GetMouseButtonDown(0)){
            if(node == null){
                Debug.Log("No Node was selected in this action");
                return;
            }
            switch (currentState)
            {
                case State.Building:
                    NodeBuildFunction(node);
                    break;
                case State.Selecting:
                    ChangeActiveNode(node);
                    break;
                case State.Upgarding:
                    NodeUpgradeFunction(node);
                    break;
                default:
                    break;
            }
        }
    }

    private void NodeUpgradeFunction(RootNode node)
    {
        if (node.GetAttributes().CanUpgrade())
        {
            node.GetAttributes().UpgradeThickness();
            GetRootSound();
        }
    }

    private void NodeBuildFunction(RootNode node)
    {
        Vector3 pos = camControls.CalculateWorldPointOfMouseClick();
        float dist = Vector2.Distance(activeNode.transform.position, pos);
        int cost = activeNode.GetAttributes().GetBuildCost(dist);
        if (TreeAttributes.storedNutrients >= cost)
        {
            node.ChangeNode(false, false);
            AddNode(pos);
            GetRootSound();
            TreeAttributes.storedNutrients -= cost;
        }
        TreeAttributes.GrowthIncrement();
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
    }

    /* ----------------------------------------------------------------
    Changes the active node by disabling some function of the current
    active node, assigning the input node to active then enabling it's
    functions.
    -----------------------------------------------------------------*/
    public static void ChangeActiveNode(RootNode node){
        if(activeNode != null){
            activeNode.ChangeNode(false, false);
        }
        activeNode = node;
        bool buildMode = (currentState == State.Building);
        activeNode.ChangeNode(true, buildMode);
    }
    public static RootNode GetActiveNode(){
        return activeNode;
    }

    public static void ChangeAction(State newState){
        if(newState != currentState){
            currentState = newState;
            switch (newState)
            {
                case State.Building:
                    activeNode.ChangeNode(true, true);
                    break;
                case State.Selecting:
                    activeNode.ChangeNode(true, false);
                    break;
                case State.Upgarding:
                    activeNode.ChangeNode(true, false);
                    break;
                default:
                    break;
            }            
        }
    }

    public void GetRootSound(){
        int r = Random.Range(0, rootSFX.Length-1);
        SoundFXManager.instance.PlaySound(rootSFX[r]);
    }
}
