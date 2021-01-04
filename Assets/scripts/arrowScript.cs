using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arrowScript : MonoBehaviour
{

    public Transform shae;
    public static arrowScript instance;
    
    void Awake()
    {
        instance=this;
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        this.transform.LookAt(shae);
    }


    public void SetShea(Transform dest)
    {
    shae=dest;
    }
}
