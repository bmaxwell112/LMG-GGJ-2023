using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Root : MonoBehaviour
{

    [SerializeField] private float treeLength;
    [SerializeField] private RootNode node;
    [SerializeField] public static RootNode activeNode = null;
    [SerializeField] private CamControls camControls;
    // Start is called before the first frame update
    void Start()
    {
        AddNode(Vector3.zero);
    }

    // Update is called once per frame
    public void ClickToAddNode()
    {
        if(Input.GetMouseButtonDown(0)){
            Vector3 pos = camControls.CalculateWorldPointOfMouseClick();

            AddNode(pos);
        }
    }

    public void AddNode(Vector3 position){
        RootNode newNode = Instantiate(node, position, Quaternion.identity);
        if(activeNode != null){
            newNode.Initialize(activeNode.GetGeneration(), activeNode);
        } else {
            print("Initial");
            newNode.Initialize(0);
        }
    }
}
