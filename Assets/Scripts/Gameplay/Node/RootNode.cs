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
    [SerializeField] int textureVal = -1;
    [SerializeField] private RootNodeAttributes nodeAttributes;
    [SerializeField] private GameObject indicator;
    private List<Vector3> family = new List<Vector3>();
    private LineRenderer lineRenderer;
    private float rotation;    
    private List<Vector3> linePoints = new List<Vector3>();    

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
        
        linePoints.Add(this.transform.position);
        if(this.transform.parent != null){
            linePoints.Add(transform.parent.position);
            float dist = GetNodeDistance();
            nodeAttributes.DetermineNutrientValue(dist);
        } else{
            nodeAttributes.thickness = 2;
        }        
        float width = (nodeAttributes.thickness / 8f);
        DrawLine(width);
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
    public void ChangeNode(bool active, bool isEnabled){
        indicator.SetActive(active);
        nodeClick.SetActive((isEnabled));
    }    

    /* ----------------------------------------------------------------
    Draws a line based on given list of points. 
    -----------------------------------------------------------------*/
    public void DrawLine(float lineWidth){
        if(linePoints.Count() > 1){
            lineRenderer = GetComponent<LineRenderer>();
            if(textureVal < 0){
                textureVal = Random.Range(0, textures.Length-1);
            }
            lineRenderer.material.SetTexture("_MainTex", textures[textureVal]);
            lineRenderer.startWidth = lineWidth;
            lineRenderer.endWidth = lineWidth;
            lineRenderer.positionCount = linePoints.Count();
            lineRenderer.SetPositions(linePoints.ToArray());
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

    private float GetNodeDistance(){
        return Vector2.Distance(transform.position, transform.parent.position);
    }
}
