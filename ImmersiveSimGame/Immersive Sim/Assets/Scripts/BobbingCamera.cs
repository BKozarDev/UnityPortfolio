using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BobbingCamera : MonoBehaviour
{
    public float bobbingSpeed = 0.18f;
    public float bobbingAmount = 0.2f;
    public float midPointY = 2.0f;
    public float midPointX = 0.0f;

    public Vector3 newPos;
    private float timer = 0.0f;
    private float waveslice;

    GameObject audio;

    private void Start()
    {
        audio = GameObject.FindGameObjectWithTag("Audio");
    }

    public bool step = false;

    // Update is called once per frame
    void Update()
    {

        float hor = Input.GetAxis("Horizontal");
        float ver = Input.GetAxis("Vertical");

        waveslice = 0.0f;
        if (Mathf.Abs(hor) == 0 && Mathf.Abs(ver) == 0)
        {
            timer = 0.0f;
        }
        else
        {
            waveslice = Mathf.Sin(timer);
            timer += bobbingSpeed;
            if (timer > Mathf.PI * 2)
            {
                timer -= Mathf.PI * 2;
            }
        }
        if(waveslice <= -0.7)
        {
            step = true;
            audio.GetComponent<AudioControl>().FootStep();
        } else if (waveslice > -0.5)
        {
            step = false;
        }

        if (waveslice != 0)
        {
            var translateChange = waveslice * bobbingAmount;
            var totalAxes = Mathf.Abs(hor) + Mathf.Abs(ver);
            totalAxes = Mathf.Clamp(totalAxes, 0.0f, 1.0f);
            var totalAxes_X = Mathf.Clamp(totalAxes, 0.0f, 0.3f);
            translateChange *= totalAxes;
            newPos = transform.localPosition;
            newPos.y = midPointY + translateChange;
            Debug.Log(translateChange);
            transform.localPosition = newPos;
        }
        else
        {
            newPos = transform.localPosition;
            newPos.y = midPointY;
            newPos.x = midPointX;
            transform.localPosition = newPos;
        }
    }
}
