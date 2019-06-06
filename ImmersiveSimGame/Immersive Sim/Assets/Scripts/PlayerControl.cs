using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{

    public float speed = 1.0f;
    float horizontal;
    float vertical;

    public float jumpSpeed = 1.0f;

    Rigidbody rb;
    private bool isGround = true;
    private bool slow = false;

    public GameObject _camera;

    public GameObject audioController;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        audioController = GameObject.FindGameObjectWithTag("Audio");
        _camera = GameObject.FindGameObjectWithTag("MainCamera");
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == ("Ground") || collision.gameObject.tag == ("Items") && isGround == false)
        {
            _camera.GetComponent<BobbingCamera>().enabled = true;
            isGround = true;
            if (slow)
            {
                speed *= 2;
                slow = false;
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        UpdateInput();
        UpdateAction();
    }

    public GameObject light;

    private bool jump = false;
    private bool haveLight = true;
    private bool isLight = false;

    private void UpdateInput()
    {
        vertical = Input.GetAxis("Vertical") * speed;
        if (isGround)
            horizontal = Input.GetAxis("Horizontal") * speed;

        Vector3 movement = new Vector3(horizontal * Time.deltaTime, 0, vertical * Time.deltaTime);

        transform.Translate(movement);

        if (Input.GetKeyDown(KeyCode.Space) && isGround)
        {
            jump = true;
        } else
        {
            jump = false;
        }

        if (Input.GetKeyDown(KeyCode.F) && haveLight)
        {
            if (isLight)
            {
                audioController.GetComponent<AudioControl>().FlashLight_turn_Off();

                isLight = false;
            } else
            {
                audioController.GetComponent<AudioControl>().FlashLight_turn_On();

                isLight = true;
            }
        }
    }

    private void UpdateAction()
    {
        if (jump)
        {
            _camera.GetComponent<BobbingCamera>().enabled = false;
            isGround = false;
            if (!slow)
            {
                speed = speed / 2;
                slow = true;
            }
            rb.AddForce(new Vector3(0, 4, 0) * jumpSpeed, ForceMode.Impulse);
        }

        if (isLight)
        {

            light.SetActive(true);
        } else
        {

            light.SetActive(false);
        }
    }
}
