using UnityEngine;
using UnityEngine.EventSystems;

using System.Collections;
using System.Collections.Generic;
[RequireComponent(typeof(Rigidbody))]
public class playerMotor : MonoBehaviour
{
    [SerializeField]
    private Camera cam;
    [SerializeField]
    
    private float cameraRotataionLimit =85;
     
    private Vector3 velocity = Vector3.zero;
     
    private Vector3 rotation = Vector3.zero;
     
    private float CamerarotationX = 0f;
     
    private float currentCamerarotationX = 0f;
     
    private Vector3 ThrusterForce = Vector3.zero;
    public GameObject followTarget;
    public bool idle;

    public Animator anim;
  
    public Rigidbody rb;
    void Start()
      

    {
        //this.GetComponent<NetworkIdentity>().AssignClientAuthority(gun);
        idle=true;
        rb = GetComponent<Rigidbody>();
        //position=gun.transform.position.z;

    }

    public void Move(Vector3 _velocity)
    {
        velocity = _velocity;



    }
    public void Rotate(Vector3 _rotation)
    {
        rotation = _rotation;



    }
 
    
    public void RotateCamera(float _rotation)
    {
        CamerarotationX = _rotation;



    }
     public void Applythruster(Vector3 _thrusterForce)
    {
        ThrusterForce = _thrusterForce;



    }
    // run every physics iteration
    void FixedUpdate()
    {
        //Debug.Log(velocity);
        //cam.transform.position=new Vector3(bodyPosition.transform.position.x,cam.transform.position.y,bodyPosition.transform.position.z);
        PerformMovement();
        PerformRotation();
        PerformCameraRotation();

    
    }

   void PerformMovement()
    {
        //Debug.Log(velocity);
          if (velocity != Vector3.zero)
        {
           
            rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);

            anim.Play("running");
            
        }
        else
        {
              if (ThrusterForce != Vector3.zero)
                {
                    //Debug.Log(ThrusterForce);
                    
                        anim.Play("jump");
                        idle=false;
                        StartCoroutine(wait());
                    //rb.MovePosition(rb.position + ThrusterForce * Time.fixedDeltaTime);

                }
                else
                {
                      
                       if(idle==true)
                       {
                            anim.Play("idle");
                        
                       }
                           
            }
        }
          if (ThrusterForce != Vector3.zero)
                {
                    //Debug.Log(ThrusterForce);
                    
                    rb.AddForce(ThrusterForce * Time.fixedDeltaTime,ForceMode.Acceleration);


                }
    

    }
   


      void PerformRotation()
    {
        //Debug.Log("haha got you");
        if (rotation != Vector3.zero)
        {
            rb.MoveRotation(rb.rotation*Quaternion.Euler(rotation));


        }


    }


    private IEnumerator wait()
    {
            yield return new WaitForSeconds(2f);
            idle=true;


    }
    void PerformCameraRotation()
    {
        if (followTarget != null)
        {
           currentCamerarotationX -=CamerarotationX;
           currentCamerarotationX=Mathf.Clamp(currentCamerarotationX,-cameraRotataionLimit,cameraRotataionLimit);
           //cam.transform.localEulerAngles=new Vector3(currentCamerarotationX,0f,0f);
           followTarget.transform.localEulerAngles=new Vector3(currentCamerarotationX,0f,0f);

          //leftHand.transform.localEulerAngles=cam.transform.localEulerAngles;
          //rightHand.transform.localEulerAngles=cam.transform.localEulerAngles;
        }



    }


}
