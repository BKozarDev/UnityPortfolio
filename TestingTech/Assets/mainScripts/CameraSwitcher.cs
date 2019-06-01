using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    public Camera[] cam;
    public int i = 0;

    // Start is called before the first frame update
    void Start()
    {
        cam = FindObjectsOfType<Camera>();

        foreach(Camera camera in cam)
        {
            camera.GetComponent<AudioListener>().enabled = false;
        }
        cam[0].GetComponent<AudioListener>().enabled = true;
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            cam[i].enabled = false;
            cam[i].GetComponent<AudioListener>().enabled = false;

            if (i != 0)
            {
                cam[i - 1].enabled = true;
                cam[i - 1].GetComponent<AudioListener>().enabled = true;
                i--;
            } else
            {
                i = cam.Length-1;
                cam[i].enabled = true;
            }
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            cam[i].enabled = false;
            cam[i].GetComponent<AudioListener>().enabled = false;

            if (i != cam.Length-1)
            {
                cam[i + 1].enabled = true;
                cam[i + 1].GetComponent<AudioListener>().enabled = true;
                
                i++;
            }
            else
            {
                i = 0;
                cam[i].enabled = true;
            }
        }
    }
}
