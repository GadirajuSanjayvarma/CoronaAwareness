using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class canvasManager : MonoBehaviour
{

    public Joystick joystick;
    public Button jump;
    
    // Start is called before the first frame update
    public static canvasManager instance;
    void Awake()
    {
        instance = this;
    }

    void Start()
    {
           if (!Application.isMobilePlatform)
            {
                joystick.gameObject.SetActive(false);
                jump.gameObject.SetActive(false);
            }
       
    }

}
