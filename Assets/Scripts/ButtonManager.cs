using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    #region Variables
    public static int moneyNo;
    
    public static int jobNo;
    public static int jobRate = 1;
    public static int jobSec = 5;
    public static int jobBank;

    public Text purchaseText;
    public Image purchase;
    public Text jobPerSec;

    [SerializeField]
    Text moneyUICounter;
    [SerializeField]
    Text jobUICounter;
    public GameObject jobSearcher;

    public GameObject socialz;
    #endregion

    void Start()
    {
        moneyNo = PlayerPrefs.GetInt("dollars",0); //sets up saving player's score
        
        jobNo = PlayerPrefs.GetInt("jobs", 0);
        jobSearcher = GameObject.Find("Canvas/Gameplay/Job Searcher"); //finds jobsearcher to prevent it from being lost so it can be activated and deactivated
        jobSearcher.SetActive(false); //hides job searcher

        socialz = GameObject.Find("Canvas/Gameplay/Socialz");
        socialz.SetActive(false);

        UpdateMoney();

    }

    public void Update()
    {
        if (moneyNo >= 10) //change to 25 - Activate Job searcher
        {
            jobSearcher.SetActive(true);

            PlayerPrefs.SetInt("jobs", jobNo);
        }

        if (moneyNo == 30 && jobNo == 25) //change to $300 - activate social media
        {
            //show social media
            
        }
    }

    public void ButtonPressFB()
    {
        moneyNo++;

        UpdateMoney();

        PlayerPrefs.SetInt("dollars", moneyNo);
    }

    void UpdateMoney()
    {
        if (moneyNo != 1) //this is purely for grammar - just annouces how many fanbots have been generated
        {
            moneyUICounter.text = moneyNo + " dollars";
        }
        else
        {
            moneyUICounter.text = moneyNo + " dollar";
        }
    }

    #region Job Searcher Application
    public void JobSearcher()
    {
        InvokeRepeating("JobSearcher", 5.0f, 5.0f);
        //add to the job count every 5 seconds

        jobNo++; //add to job count
        jobUICounter.text = "No. of Jobs taken: " + jobNo; //shows job count in ui
        jobPerSec.text = jobRate + " jobs per " + jobSec + " seconds + $10";
        UpdateMoney(); //updates money count in ui

        JobPayment();
    }

    private void JobPayment()
    {
        ///moneyNo = moneyNo + 10;
        if (jobBank >= 50) //if first upgrade has been bought
        {
            InvokeRepeating("JobPayment", 5.0f, 5.0f);
            //give 10$ every 5 seconds
            moneyNo += 10;
        }
    }

    public void ToggleJob1()
    {
        if (jobUICounter.enabled == false && moneyNo >= 50) //if you have $50 you can then:
        {
            jobUICounter.enabled = true; //enables jobcounter to be viewed
            purchase.enabled = false; //disables the purchase button, so does the line below
            purchaseText.enabled = false;
            
            jobPerSec.enabled = true;

            moneyNo -= 50; //cost $50 to enable job searcher, you earn $10 for the first job
            jobBank += 50; //used to keep track of how many upgrades have been bought

            //JobPayment();
            //JobSearcher(); //run the job searcher application
        }
        else
        {
            jobUICounter.enabled = false;
        }
    }

    #endregion

    public void Socialz()
    {

    }
}
