using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class scoreManager : MonoBehaviour
{
    // Start is called before the first frame update
    public Text score;
    float currentTime;
    public float gap;
    int value;
    void Start()
    {
        value=100;
        currentTime=Time.time;
        score.text="your score is "+value;
    }

    // Update is called once per frame
    void Update()
    {

        if(Time.time-currentTime>gap)
        {
            value-=5;
            score.text="Score: "+value;
            currentTime=Time.time;

        }

        
    }

    public void onClickRestart()
    {
        SceneManager.LoadScene("level1", LoadSceneMode.Single);

    }
    public void onClickHome()
    {
        SceneManager.LoadScene("intro", LoadSceneMode.Single);

    }
    public void onClickLeaderboard()
    {
       

    }

    
}
