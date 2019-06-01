using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImmersiveObjects : MonoBehaviour
{
    public Transform center;

    public GameObject camera;
    public GameObject player;

    private void Start()
    {
        camera = GameObject.FindGameObjectWithTag("MainCamera");
        player = GameObject.FindGameObjectWithTag("Player");
        center = camera.transform;
    }

    public bool canGrab;
    public bool canRotate;
    public float reach = 1f;
    public float rate = .45f;
    public LayerMask layerMask;


    private bool wantGrab;
    private bool wantRotate;
    private GameObject grabbed = null;
    private float wantedPosition = 3f;

    public bool isRotating { get; private set; }

    // Update is called once per frame
    void Update()
    {
        UpdateInput();
        UpdateLogic();
    }

    private void UpdateInput()
    {
        if(Input.GetKey(KeyCode.Mouse0) && canGrab)
        {
            wantGrab = true;
        } else if(Input.GetKeyUp(KeyCode.Mouse0))
        {
            wantGrab = false;
        }
        
        if(Input.GetKey(KeyCode.Mouse1) && grabbed != null && canRotate)
        {
            wantRotate = true;
        } else if(Input.GetKeyUp(KeyCode.Mouse1))
        {
            wantRotate = false;
        }
        
    }

    private void UpdateLogic()
    {
        if (wantGrab)
        {
            if(grabbed == null)
            {
                Ray ray = new Ray(center.position, center.forward);
                RaycastHit hit;
                if(Physics.Raycast(ray, out hit, reach, layerMask))
                {
                    if(hit.collider.tag == "Items")
                    {
                        grabbed = hit.collider.gameObject;
                    }
                }
            } else
            {
                if (grabbed.GetComponent<Rigidbody>())
                {
                    Rigidbody rigidB = grabbed.GetComponent<Rigidbody>();
                    rigidB.velocity = ((center.position + (center.forward * wantedPosition)) - grabbed.transform.position) * rate;
                } else
                {
                    grabbed = null;
                }
            }
        } else
        {
            if(grabbed != null)
            {
                grabbed = null;
            }
        }

        if (wantRotate)
        {
            if(grabbed != null)
            {
                player.GetComponent<CameraControl>().enabled = false;
                camera.GetComponent<CameraControl>().enabled = false;

                float xa = Input.GetAxis("Mouse X") * 10;
                float ya = Input.GetAxis("Mouse Y") * 10;
                grabbed.transform.Rotate(new Vector3(ya, -xa, 0), Space.World);
                isRotating = true;
            } else
            {
                isRotating = false;
            }
        } else
        {
            player.GetComponent<CameraControl>().enabled = true;
            camera.GetComponent<CameraControl>().enabled = true;
            isRotating = false;
        }
    }
}
