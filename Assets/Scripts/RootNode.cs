using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class RootNode : MonoBehaviour
{    
    [SerializeField] private int generation;
    [SerializeField] private SpriteRenderer nodeSprite;
    [SerializeField] private GameObject nodeClick;
    [SerializeField] private Texture[] textures;
    [SerializeField] private RootNodeAttributes nodeAttributes;
    private List<Vector3> family = new List<Vector3>();
    private LineRenderer lineRenderer;
    private float rotation;    

    /* ----------------------------------------------------------------
    Startup and update
    -----------------------------------------------------------------*/
    public void Initialize(int lastGen, RootNode activeRoot = null){

        if(activeRoot != null){
            this.transform.parent = activeRoot.gameObject.transform;
        } 
        generation = lastGen + 1;

        RotateGrowthField();

        family.Add(this.transform.position);
        this.name = "Node" + generation;
        List<Vector3> nodes = new List<Vector3>();
        nodes.Add(this.transform.position);
        if(this.transform.parent != null){
            nodes.Add(transform.parent.position);
            float dist = Vector2.Distance(transform.position, transform.parent.position);
            nodeAttributes.DetermineNutrientValue(dist);
        }
        DrawLine(nodes);        
    }

    /* ----------------------------------------------------------------
    PUBLIC FUNCTION CAN BE CALLED FROM OTHER SCRIPTS
    Returns the latest generation. Not currently used for anything
    -----------------------------------------------------------------*/
    public int GetGeneration(){
        return generation;
    }

    /* ----------------------------------------------------------------
    PUBLIC FUNCTION CAN BE CALLED FROM OTHER SCRIPTS
    Change all changeable parameters of a node. As the node must be
    accessible to use this function for unneeded changes pass through
    node.associatedValue.
    -----------------------------------------------------------------*/
    public void ChangeNode(Color color, bool isEnabled){
        nodeSprite.color = color;
        nodeClick.SetActive(isEnabled);
        nodeClick.GetComponent<GrowthArea>().IsEnabled();
    }    

    /* ----------------------------------------------------------------
    Draws a line based on given list of points. 
    -----------------------------------------------------------------*/
    void DrawLine(List<Vector3> points){
        // if(this.transform.parent){
        //     Destroy(this.transform.parent.GetComponent<LineRenderer>());
        // }
        print(points.Count());
        if(points.Count() > 1){
            lineRenderer = GetComponent<LineRenderer>();
            int rand = Random.Range(0, textures.Length-1);
            lineRenderer.material.SetTexture("_MainTex", textures[rand]);
            lineRenderer.positionCount = points.Count();
            lineRenderer.SetPositions(points.ToArray());
        }
    }

    /* ----------------------------------------------------------------
    This will loop through each gameOjects adding it to a lits until an
    gameObject does not contain a parent thenreturns the list. 
    -----------------------------------------------------------------*/
    private List<Vector3> GetAllParents(){
        List<Vector3> parents = new List<Vector3>();
        GameObject currentObj = this.gameObject;
        bool hasParent = true;
        while(hasParent){
            parents.Add(currentObj.transform.position);
            if(currentObj.transform.parent != null){
                currentObj = currentObj.transform.parent.gameObject;
            } else {
                hasParent = false;
            }
        }   

        return parents;     
    }

    /* ----------------------------------------------------------------
    Using the current point and parent this determines the direction
    the GrowthField will face
    -----------------------------------------------------------------*/
    private void RotateGrowthField(){        
        if(transform.parent){
            Vector3 diff = transform.parent.transform.position - transform.position;
            diff.Normalize();
            float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
        }
    }

    public RootNodeAttributes GetAttributes(){
        return nodeAttributes;
    }
}
