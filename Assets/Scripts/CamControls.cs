using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamControls : MonoBehaviour
{
    Camera myCamera;
    float cameraDepth;
    // Start is called before the first frame update
    void Start()
    {
        myCamera = GetComponent<Camera>();
        cameraDepth = myCamera.transform.position.z;
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

    public void WidenField(){

    }
}
