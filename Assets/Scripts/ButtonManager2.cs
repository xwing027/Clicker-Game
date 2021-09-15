using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager2 : MonoBehaviour
{
    #region Variables
    public static int moneyNo;

    public static int jobNo;
    public static int jobBonus;

    public Text purchase1T;
    public Image purchase1I;
    public Text purchase2T;
    public Image purchase2I;
    public Text purchase3T;
    public Image purchase3I;

    public Text endOfUp;

    [SerializeField]
    Text moneyUICounter;
    
    [SerializeField]
    Text jobUICounter;
    [SerializeField] //for some reason the line below wont show unless i directly serialize it :/
    Text jobBonusCounter;
    #endregion

    void Start()
    {
        moneyNo = PlayerPrefs.GetInt("dollars", 0); //sets up saving player's score, starts at 0

        jobNo = PlayerPrefs.GetInt("jobs", 0); //sets up saving player's job count, starts at 0

        endOfUp.enabled = false;

        UpdateMoney(); 
    }

    public void Update()
    {
        PlayerPrefs.SetInt("jobs", jobNo);
    }

    public void ButtonPressMoney() //the button that generates money when clicked
    {
        PlayerPrefs.SetInt("dollars", moneyNo); //begins saving money amount

        if (jobBonus == 0)
        {
            moneyNo++; //default generation is + $1 per click
            UpdateMoney();
        }

        if (jobBonus == 2) //if first upgrade is bought (+$2)
        {
            moneyNo += 2;
            UpdateMoney();
        }

        if (jobBonus == 4)//if second upgrade is bought (+$4)
        {
            moneyNo += 4;
            UpdateMoney();
        }
        
        if (jobBonus == 8)//if third upgrade is bought (+$8)
        {
            moneyNo += 8;
            UpdateMoney();
        }
    }

    void UpdateMoney() //updates the money count in the ui 
    {
            moneyUICounter.text = "$" + moneyNo;
    }

    #region Job Searcher Application
    public void JobSearcher()
    {
        jobUICounter.text = "Jobs taken: " + jobNo; //updates amount of jobs/upgrades bought in the ui

        jobBonusCounter.text = "+ $" + jobBonus; //updates how much money per click is being earnt with each upgrade
    }

    public void ToggleJobUpgrades(int num)
    {
        if (num == 0 && moneyNo >= 50) //if you have $50 you can then click the button
        {
            jobUICounter.enabled = true; //displays the job counter, keeps track of how many jobs(upgrades) taken
            jobBonusCounter.enabled = true; //displays theh bonus given with each job

            purchase1T.enabled = false; //hides the first upgrade button (buttons can only be hidden by the text and image elements)
            purchase1I.enabled = false;

            moneyNo -= 50; //cost $50 to enable job searcher
            jobNo++;
            jobBonus = 2;

            JobSearcher(); //run the job searcher application
            UpdateMoney();
        }
        else if (num == 1 && moneyNo >= 100) //if you have $100 and the previous upgrade you can then click
        {
            purchase2T.enabled = false;
            purchase2I.enabled = false;

            moneyNo -= 100;
            jobNo++;
            jobBonus = 4;

            JobSearcher();
            UpdateMoney();
        }
        else if (num == 2 && moneyNo >= 200)
        {
            purchase3T.enabled = false;
            purchase3I.enabled = false;

            moneyNo -= 200;
            jobNo++;
            jobBonus = 8;

            JobSearcher();
            UpdateMoney();

            endOfUp.enabled = true;
        }
        else
        {
            jobUICounter.enabled = false;
        }
    }

    #endregion
}
