using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class healthScript : MonoBehaviour
{
    // Start is called before the first frame update
    public ParticleSystem ps;
    public GameObject Plus1;
    public Text objectives;
    public playerMotor pm;
    public GameObject quiz;

    public static healthScript instance;

  void Awake()
  {
      instance=this;
  }

    void Start()
    {
        ps.Play();    
    }

      private void OnTriggerEnter(Collider other)
    {
        if(other.name=="Player")
        {
            //pm.enabled=false;
            if(this.transform.name=="Plus")
            {
                pm.enabled=false;
                quiz.gameObject.SetActive(true);
                 

            }
            else
            {
                    pm.enabled=false;
                    quiz.gameObject.SetActive(true);
            }

        }
     
    }


    public void reActive()
    {
                pm.enabled=true;
                quiz.gameObject.SetActive(false);
    }

    // Update is called once per frame
 
}
