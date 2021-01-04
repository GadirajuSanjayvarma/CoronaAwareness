using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.AI;

public class peopleScript : MonoBehaviour
{
    // Start is called before the first frame update
    private NavMeshAgent agent;
    public Transform startingPosition;
    public Transform finalPosition;

    public Transform currentDestination;
    public Transform optionalDestination;



    void Start()
    {

        agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(finalPosition.position);
        currentDestination = finalPosition;
        optionalDestination = startingPosition;

    }

    // Update is called once per frame

    void Update()
    {
        //Debug.Log(Vector3.Distance(currentDestination.position, this.transform.position));
        if (Vector3.Distance(currentDestination.position, this.transform.position) <= 3.0f)
        {
            Transform temp = optionalDestination;
            optionalDestination = currentDestination;
            currentDestination = temp;

            agent.SetDestination(currentDestination.position);
        }

    }





}
