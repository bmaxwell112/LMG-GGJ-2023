using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class RootNode : MonoBehaviour
{
    [SerializeField] private int generation;
    [SerializeField] private float nodeFacing;
    [SerializeField] private List<RootNode> family;
    private LineRenderer lineRenderer;

    public void Initialize(int lastGen, List<RootNode> lastFamily = null){
        if(lastFamily != null){
            this.transform.parent = lastFamily.Last().gameObject.transform;
            family = lastFamily;
        }
        family.Add(this);
        generation = lastGen + 1;
        DrawLine(family);
        Root.SetActiveNode(this);
    }

    void DrawLine(List<RootNode> points){
        List<Vector3> pointPos = new List<Vector3>();
        Vector3 parentTransPos = family.Last().gameObject.transform.position;
        Vector3 transPos = this.transform.position;
        foreach(RootNode point in points){
            pointPos.Add(point.gameObject.transform.position);
        }
        Vector3[] curve = Curver.MakeSmoothCurve(pointPos.ToArray(), 3.0f);
        List<Vector3> curveToDraw = new List<Vector3>();
        foreach(Vector3 c in curve){
            curveToDraw.Add(c);
            print("Curve Point " + c);
            if(c == parentTransPos){
                break;
            }
        }        
        if(curveToDraw.Count() > 1){
            lineRenderer = GetComponent<LineRenderer>();
            lineRenderer.positionCount = curveToDraw.Count();
            lineRenderer.SetPositions(curveToDraw.ToArray());
        }
    }

    public List<RootNode> GetFamily(){
        return family;
    }

    public int GetGeneration(){
        return generation;
    }
}
