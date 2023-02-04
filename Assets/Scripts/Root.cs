using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Root : MonoBehaviour
{

    [SerializeField] private float treeLength;
    [SerializeField] private RootNode node;
    [SerializeField] private static RootNode activeNode;
    [SerializeField] private CamControls camControls;
    // Start is called before the first frame update
    void Start()
    {
        AddNode(Vector3.zero);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0)){
            Vector3 pos = camControls.CalculateWorldPointOfMouseClick();
            print("Mouse Pressed at " + pos);

            AddNode(pos, activeNode.GetFamily());
        }
    }

    public void AddNode(Vector3 position, List<RootNode> family = null){
        RootNode newNode = Instantiate(node, position, Quaternion.identity);
        if(family != null){
            newNode.Initialize(activeNode.GetGeneration(), family);
        } else {
            newNode.Initialize(0);
        }
    }
    
    public static void SetActiveNode(RootNode n){
        activeNode = n;
    }
}
