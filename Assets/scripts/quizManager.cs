using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Net;
using System.Collections.Specialized;

public class quizManager : MonoBehaviour
{
    // Start is called before the first frame update

    char[] answers = { '1', '2', '3', '2', '3' };
    char[] answersSecond = { '3', '3', '1', '1', '2' };
    public GameObject[] questions;
    public GameObject[] questions1;
    public GameObject Plus1;
    public GameObject plus;
    public GameObject finalBoard;

    public Text finalScore;

    public Text trackScore;


    public Text objectives;



    void Start()
    {

    }


    public void takeAnswer(string option)
    {
        //Debug.Log(option[0]);
        //Debug.Log(option[1]);
        char question = option[0];
        char answer = option[1];
        if (answers[question - '0'] == answer)
        {
            //Debug.Log("correct answer");
            if (question - '0' == 4)
            {
                Plus1.gameObject.SetActive(true);
                plus.GetComponent<healthScript>().reActive();
                Destroy(plus);
                arrowScript.instance.SetShea(Plus1.transform);
                objectives.text = "find second vaccine center";

            }
            else
            {
                questions[question - '0'].gameObject.SetActive(false);
                questions[question - '0' + 1].gameObject.SetActive(true);

            }

        }
        else
        {
            Debug.Log("wrong answer");

        }
    }

    public void takeAnswerSecond(string option)
    {
        //Debug.Log(option[0]);
        //Debug.Log(option[1]);

        char question = option[0];
        char answer = option[1];
        if (answersSecond[question - '0'] == answer)
        {
            //Debug.Log("correct answer");
            if (question - '0' == 4)
            {

                healthScript.instance.reActive();
                Destroy(Plus1.gameObject);
                finalBoard.gameObject.SetActive(true);
                finalScore.text=trackScore.text;
                sendScore("100");
                GameObject.Find("Player").transform.gameObject.GetComponent<playerMotor>().enabled=false;

            }
            else
            {
                questions1[question - '0'].gameObject.SetActive(false);
                questions1[question - '0' + 1].gameObject.SetActive(true);

            }

        }
        else
        {
            Debug.Log("wrong answer");

        }
    }

    public void onClickSend()
    {
        sendScore("10");
    }



    void sendScore(string score)
    {
        string URI = "http://localhost:3000/storeScore";
        string myParameters = "username=sanjay&score="+score;

        using (WebClient wc = new WebClient())
        {
            wc.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
            string HtmlResult = wc.UploadString(URI, myParameters);
            Debug.Log(HtmlResult);
        }
    }

}
