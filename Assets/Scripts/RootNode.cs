using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class RootNode : MonoBehaviour
{    
    [SerializeField] private int generation;
    [SerializeField] private float nodeFacing;
    [SerializeField] private SpriteRenderer nodeSprite;
    private List<Vector3> family = new List<Vector3>();
    private LineRenderer lineRenderer;
    private float rotation;

    public void Initialize(int lastGen, RootNode activeRoot = null){

        if(activeRoot != null){
            this.transform.parent = activeRoot.gameObject.transform;
        } 
        generation = lastGen + 1;

        RotateGrowthField();

        family.Add(this.transform.position);
        this.name = "Node" + generation;
        // print(this.name);
        // if(this.transform.parent != null){
        //     print(this.transform.parent.name);
        //     family.Add(this.transform.parent.transform.position);
        // }       
        DrawLine(GetAllParents());
        Root.activeNode = this;
        ChangeColor(Color.red)        ;
    }

    void DrawLine(List<Vector3> points){
        if(this.transform.parent){
            Destroy(this.transform.parent.GetComponent<LineRenderer>());
        }
        // Vector3[] curve = Curver.MakeSmoothCurve(points.ToArray(), 10.0f);
        // List<Vector3> curveToDraw = new List<Vector3>();                
        // foreach(Vector3 c in curve){
        //     curveToDraw.Add(c);
        // }        
        List<Vector3> lineToDraw = points;
        if(lineToDraw.Count() > 1){
            lineRenderer = GetComponent<LineRenderer>();
            lineRenderer.positionCount = lineToDraw.Count();
            lineRenderer.SetPositions(lineToDraw.ToArray());
        }
    }
    
    public int GetGeneration(){
        return generation;
    }

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

    private void RotateGrowthField(){        
        if(transform.parent){
            Vector3 diff = transform.parent.transform.position - transform.position;
            diff.Normalize();
            float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
        }
    }

    public void ChangeColor(Color color){
        nodeSprite.color = color;
    }
}
