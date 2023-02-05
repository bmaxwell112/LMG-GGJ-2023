using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamControls : MonoBehaviour
{
    Camera myCamera;
    float cameraDepth;
    [SerializeField] float insetPercent = 0;
    [SerializeField] float camMoveTime = 0.5f;
    Vector2[] edgePoints = new Vector2[4];   
    bool expanding = false; 

    // Start is called before the first frame update
    void Start()
    {
        myCamera = GetComponent<Camera>();
        cameraDepth = myCamera.transform.position.z;
        SetCameraBoundries();
    }

    /*==========================================================
	Formula Functions
	The below function are used to check for things such as 
	mouse location, and use avalible data to calculate players
	movement based on the state.
	==========================================================*/
    public Vector2 CalculateWorldPointOfMouseClick()
    {
        float mouseX = Input.mousePosition.x;
        float mouseY = Input.mousePosition.y;

        Vector3 weirdTriplet = new Vector3(mouseX, mouseY, cameraDepth);
        Vector2 worldPos = myCamera.ScreenToWorldPoint(weirdTriplet);

        return worldPos;
    }
    public void SetCameraBoundries(){

        // Making camera bounds
        var leftBottom = (Vector2) myCamera.ScreenToWorldPoint(new Vector3(0, 0, myCamera.nearClipPlane));
        leftBottom = new Vector2(leftBottom.x - (leftBottom.x * insetPercent), leftBottom.y - (leftBottom.y * insetPercent));        

        var leftTop = (Vector2) myCamera.ScreenToWorldPoint(new Vector3(0, myCamera.pixelHeight, myCamera.nearClipPlane));
        leftTop = new Vector2(leftTop.x - (leftTop.x * insetPercent), leftTop.y - (leftTop.y * insetPercent));

        var rightTop = (Vector2) myCamera.ScreenToWorldPoint(new Vector3(myCamera.pixelWidth, myCamera.pixelHeight, myCamera.nearClipPlane));
        rightTop = new Vector2(rightTop.x - (rightTop.x * insetPercent), rightTop.y - (rightTop.y * insetPercent));
        
        var rightBottom = (Vector2) myCamera.ScreenToWorldPoint(new Vector3(myCamera.pixelWidth, 0, myCamera.nearClipPlane));        
        rightBottom = new Vector2(rightBottom.x - (rightBottom.x * insetPercent), rightBottom.y - (rightBottom.y * insetPercent) + 1);

        edgePoints = new [] {leftBottom, leftTop, rightTop, rightBottom };
    }

    public void WidenField(int size){
            StartCoroutine(resizeRoutine(myCamera.orthographicSize, size, camMoveTime));
    }

    private IEnumerator resizeRoutine(float oldSize, float size, float time)
    {        
        expanding = true;
        float newSize = myCamera.orthographicSize + size;
        float elapsed = 0;
        while (elapsed <= time)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / time);
               myCamera.orthographicSize = Mathf.Lerp(oldSize, newSize, t);
            yield return null;
        }
        SetCameraBoundries();
        expanding = false;
    }
}
