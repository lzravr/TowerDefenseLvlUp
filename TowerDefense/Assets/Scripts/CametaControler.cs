using UnityEngine;

public class CametaControler : MonoBehaviour {

    public Camera Kamera;
    public float panSpeed = 30f; //[pmeraj po X i Z
    public float panBordedThickness = 10f;
    public float scrollSpeed = 5f;
    public float rotationSpeed = 7f;
    public float minY, maxY;
    
    private bool doMovement = true;
    private Vector3 defaultPosition;
    private Quaternion defaultRotation;
    private Quaternion defaultCameraRotation;

    private void Start()
    {
        defaultPosition = transform.position;
        defaultRotation = transform.rotation;
        defaultCameraRotation = Kamera.transform.rotation;
    }

    // Update is called once per frame
    void Update ()
    {
        if(GameManager.gameIsOver)
        {
            this.enabled = false;
            return;
        }

        if (Input.GetKeyDown("space"))
            doMovement = !doMovement;


        if (doMovement == false)
        {
            return;
        }
       

        if (Input.GetMouseButtonUp(2) || Input.GetKeyUp("r"))
        {
            transform.position = defaultPosition;
            transform.rotation = defaultRotation;
            Kamera.transform.rotation = defaultCameraRotation;
        }

        Quaternion rot = Kamera.transform.rotation;
        Quaternion rotRig = transform.rotation;
        rot.z = Mathf.Clamp(rot.z, 0, 0);
        //if (Input.GetMouseButton(1) || Input.GetKey(KeyCode.LeftShift))
        //{
            if (Input.GetKey("c"))
            {
                Quaternion eu = new Quaternion(rotationSpeed * Time.deltaTime, 0, 0, 1);
                Kamera.transform.Rotate(eu.eulerAngles, Space.Self);
            }
            if (Input.GetKey("z"))
            {
                Quaternion eu = new Quaternion(-rotationSpeed * Time.deltaTime, 0, 0, 1);
                Kamera.transform.Rotate(eu.eulerAngles, Space.Self);
            }
            if (Input.GetKey("e") )
            {
                Quaternion eu = new Quaternion(0, rotationSpeed * Time.deltaTime, 0, 1);
                transform.Rotate(eu.eulerAngles, Space.World);          
            }
            if (Input.GetKey("q"))
            {
                Quaternion eu = new Quaternion(0, -rotationSpeed * Time.deltaTime, 0, 1);
                transform.Rotate(eu.eulerAngles, Space.World);
            }
        //}

        //donji levi 0 0 0
        if (Input.GetKey("w") || Input.mousePosition.y >= Screen.height - panBordedThickness)
        {
            transform.Translate(Vector3.forward * panSpeed * Time.deltaTime, Space.Self);
            //transform.Rotate((new Vector3(1,0,0)) * panSpeed * Time.deltaTime, Space.World);
            //space.world, gledano u globalnom sistemu, u lokalnom je kamera pod uglom
        }
        if (Input.GetKey("s") || Input.mousePosition.y <= panBordedThickness)
        {
            transform.Translate(Vector3.back * panSpeed * Time.deltaTime, Space.Self);
        }
        if (Input.GetKey("d") || Input.mousePosition.x >= Screen.width - panBordedThickness)
        {
            transform.Translate(Vector3.right * panSpeed * Time.deltaTime, Space.Self);
        }
        if (Input.GetKey("a") || Input.mousePosition.x <= panBordedThickness)
        {
            transform.Translate(Vector3.left * panSpeed * Time.deltaTime, Space.Self);
        }
        

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        Vector3 pos = transform.position;
        
      
        pos.y -= scroll * 1000 * scrollSpeed * Time.deltaTime;
        pos.y = Mathf.Clamp(pos.y, minY, maxY);

        transform.position = pos;
        
    }
}
