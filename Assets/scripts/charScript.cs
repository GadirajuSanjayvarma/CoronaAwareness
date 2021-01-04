using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class charScript : MonoBehaviour
{
    public Animator anim;
    private NavMeshAgent agent;
    public Text messageText;
    public GameObject Conversation;
    
    public float time;

    private string message="Hello sanjay how are you.\nVaccination is very important.\nGo an get vaccine at nearby centers";

    public GameObject health1;
    public GameObject health2;
    public Text objectives;


    void Start()
    {
        Conversation.gameObject.SetActive(false);
        agent = GetComponent<NavMeshAgent>();
        messageText.text="";
        time=0.5f;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(this.enabled==false)
        {
            return;
        }
        Conversation.gameObject.SetActive(true);
        //we are taking name of player collided with gun
        agent.speed=0.0f;
        string name=other.transform.name;
        
       if(name.Contains("Player"))
       {
         anim.Play("talking");
         //Debug.Log("executing");
         displayMessage();
          //Debug.Log(singleGameManager.instance.player.GetComponent<singlePlayer>());
       }
       
       
    }
    

    void displayMessage()
    {
       

        for(int i=0;i<message.Length;i++)
        {
               //Debug.Log(i);
            StartCoroutine(wait(i,time));
            time+=0.1f;
            //Debug.Log(message[i]);
            //messageText.text+=message[i];
        }
     



    }
    private IEnumerator wait(int i,float time)
    {
        
            yield return new WaitForSeconds(time);
            //Debug.Log(i);
            messageText.text+=message[i];
            if(i==message.Length-1)
            {
                anim.Play("Walking");
                agent.speed=3.0f;
                Conversation.gameObject.SetActive(false);
                this.enabled=false;
                health1.gameObject.SetActive(true);
                //arrowScript.instance.gameObject.transform.LookAt(health1.transform);
                arrowScript.instance.SetShea(health1.transform);
                objectives.text="find first vaccine center";
                //health2.gameObject.SetActive(true);
            }


    }

}
