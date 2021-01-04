using UnityEngine;

using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class playerController : MonoBehaviour
{
    [SerializeField]
    private float speed = 5f;

    public  float lookSensitivity;

    [SerializeField]
    private float thrusterForce = 1000f;



    public playerMotor motor;
    int rightFingerId;
    float halfScreenWidth;
    Vector2 lookInput;
    Vector2 lookInputStopping;
    float cameraPitch;
 
    public static playerController instance;
     private void Awake() {
        if (instance != null) {
            Destroy(gameObject);
        }else{
            instance = this;
           
        }
    }
   


    void Start()
    {
        /*   Debug.Log(canvasManager.instance.text.text);
         if(Application.isMobilePlatform)
         lookSensitivity=float.Parse(canvasManager.instance.text.text);
   */
        lookSensitivity=3.0f;
        lookInputStopping = new Vector2(1, 1);
        motor = gameObject.GetComponent<playerMotor>();

        rightFingerId = -1;

        // only calculate once
        halfScreenWidth = Screen.width / 2;

        //PlayerController.lookSensitivity=sens;

        // calculate the movement input dead zone
        //moveInputDeadZone = Mathf.Pow(Screen.height / moveInputDeadZone, 2);



    }



    void FixedUpdate()
    {
         
        if (Application.isMobilePlatform)
        {
            mobileInput();
        }
        else
        {
            pcInput();
        }
        //mobileInput();
    }

    public void mobileInput()
    {
        //Debug.Log(canvasManager.instance);
        float _xMov = canvasManager.instance.joystick.Horizontal;
        float _zMov = canvasManager.instance.joystick.Vertical;
        //Debug.Log(_xMov);
        //Debug.Log(_zMov);
        Vector3 _movHorizontal = transform.right * _xMov;
        Vector3 _movVertical = transform.forward * _zMov;

        Vector3 _velocity = (_movHorizontal + _movVertical).normalized * speed;


        //apply movement
        motor.Move(_velocity);

        //apply the thruster force
        Vector3 _thrusterForce = Vector3.zero;


        if (jumpScript.buttonPressed)
        {

            if (this.transform.position.y <= 2)
                _thrusterForce = Vector3.up * thrusterForce;

        }
        motor.Applythruster(_thrusterForce);


        GetTouchInput();
        Debug.Log(lookInput);

        if (rightFingerId != -1)
        {

            motor.RotateCamera(lookInput.y);
            Vector3 _rotation = new Vector3(0f, lookInput.x, 0f);

            motor.Rotate(_rotation);


        }
        else
        {
            if (lookInputStopping == Vector2.zero)
            {

                motor.RotateCamera(lookInput.y);
                Vector3 _rotation = new Vector3(0f, lookInput.x, 0f);

                motor.Rotate(_rotation);
                lookInputStopping = new Vector2(1, 1);

            }


        }






    }

    void GetTouchInput()
    {
        // Iterate through all the detected touches
        //Debug.Log(Input.touchCount);
        for (int i = 0; i < Input.touchCount; i++)
        {

            Touch t = Input.GetTouch(i);

            // Check each touch's phase
            switch (t.phase)
            {
                case TouchPhase.Began:

                    if (t.position.x > halfScreenWidth && rightFingerId == -1)
                    {
                        // Start tracking the rightfinger if it was not previously being tracked
                        rightFingerId = t.fingerId;
                    }
                    //lookInputStopping=new Vector2(1,1);

                    break;
                case TouchPhase.Ended:
                case TouchPhase.Canceled:

                    if (t.fingerId == rightFingerId)
                    {
                        // Stop tracking the right finger
                        //rightFingerId = -1;
                        lookInputStopping = Vector2.zero;
                        rightFingerId = -1;
                        lookInput = Vector2.zero;
                        //lookInput = -(t.deltaPosition * lookSensitivity * Time.deltaTime);
                        //Debug.Log("Stopped tracking right finger");
                    }

                    break;
                case TouchPhase.Moved:

                    // Get input for looking around
                    if (t.fingerId == rightFingerId)
                    {
                        lookInput = t.deltaPosition * lookSensitivity * Time.deltaTime;
                    }
                    //lookInputStopping=new Vector2(1,1);

                    break;
                case TouchPhase.Stationary:
                    // Set the look input to zero if the finger is still
                    if (t.fingerId == rightFingerId)
                    {
                        lookInput = Vector2.zero;
                    }
                    break;
            }
        }
    }






    void pcInput()
    {
        //Debug.Log("hello world");
        float _xMov = Input.GetAxisRaw("Horizontal");
        float _zMov = Input.GetAxisRaw("Vertical");
        //Debug.Log(Input.GetAxisRaw("Horizontal"));
        //Debug.Log(_zMov);
        Vector3 _movHorizontal = transform.right * _xMov;
        Vector3 _movVertical = transform.forward * _zMov;

        Vector3 _velocity = (_movHorizontal + _movVertical).normalized * speed;

        //apply movement
        motor.Move(_velocity);

        //calculate rotation as a 3d vector (turning around)
        float _yRot = Input.GetAxisRaw("Mouse X");
        //Debug.Log("sensitivity is"+lookSensitivity);
        Vector3 _rotation = new Vector3(0f, _yRot, 0f) * lookSensitivity;

        motor.Rotate(_rotation);

        //calculate camera rotation as a 3d vector (turning around)
        float _xRot = Input.GetAxisRaw("Mouse Y");

        float _Camerarotation = _xRot * lookSensitivity;

        motor.RotateCamera(_Camerarotation);

        //apply the thruster force
        Vector3 _thrusterForce = Vector3.zero;


        if (Input.GetButton("Jump"))
        {

            if (this.transform.position.y <= 2)
            {
                _thrusterForce = Vector3.up * thrusterForce;

            }

        }
        motor.Applythruster(_thrusterForce);

    }

}
