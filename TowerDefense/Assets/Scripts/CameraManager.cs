using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour {

    public Camera mainCamera;
    public GameObject cameraRig;
    public static Camera MainCamera;
    public static GameObject CameraRig;

    public static Camera ActiveCamera;

    private BuildManager buildManager;
    

	// Use this for initialization
	void Start () {
        MainCamera = mainCamera;
        ActiveCamera = mainCamera;
        CameraRig = cameraRig;
        buildManager = BuildManager.instance;
        mainCamera.enabled = true;
        //secondary.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		
        if(Input.GetKeyDown("v"))
        {
            ChangeCamera();           
        }
	}

    public void ChangeCamera()
    {
       
        Node n = buildManager.GetSelectedNode();
        if (n != null)
        {
            Turret t = n.turret.GetComponent<Turret>();
            //mainCamera.enabled = !mainCamera.enabled;
            t.secondaryCamera.enabled = !t.secondaryCamera.enabled;
            ActiveCamera = t.secondaryCamera;
        }    
        else
        {
            mainCamera.enabled = true;
            Debug.Log("Turret NOT selected");
            ActiveCamera = mainCamera;
        }

        //Debug.Log("AKTIVNA KAMERA: " + ActiveCamera);
    }

}
