using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SchoolManager : MonoBehaviour {

    public StudentAI[] Students;
    

    public GameObject GameOverOverlay;
	// Use this for initialization
	void Start () {
        if(MenuManager.DataType == 0)
        {
            Type = GameTimeType.Seconds;
        }
        if (MenuManager.DataType == 1)
        {

            Type = GameTimeType.Minutes;
        }
        if (MenuManager.DataType == 2)
        {
            Type = GameTimeType.Hours;

        }
        TimeVal = MenuManager.Timer;
    }

    float TimeSeconds;
    int Minutes;
    int Hours;
    bool gathered;
    public int TimeVal;
    public enum GameTimeType
    {
        Seconds,
        Minutes,
        Hours
    }

    public GameTimeType Type;

    public bool RoundFinish;
    public bool GameOver;
    public int PeopleInLove;
    public Text wintxt;
	// Update is called once per frame
	void LateUpdate ()
    {
        if(!gathered)
        {
            Students = GameObject.FindObjectsOfType<StudentAI>();
            gathered = true;
        }
        if (!RoundFinish)
        {
            TimeSeconds += Time.deltaTime;
            if (TimeSeconds >= 60 && Type != GameTimeType.Seconds)
            {
                TimeSeconds = 0;
                Minutes++;
            }
            if (Minutes >= 60 && Type != GameTimeType.Minutes)
            {
                Minutes = 0;
                Hours++;
            }

            if (Type == GameTimeType.Seconds && TimeSeconds >= TimeVal)
            {
                for(int i = 0; i < Students.Length; i++)
                {
                    if(Students[i].Partner != null)
                    {
                        PeopleInLove++;
                    }
                }
                RoundFinish = true;
            }
            if (Type == GameTimeType.Minutes && Minutes >= TimeVal)
            {
                for (int i = 0; i < Students.Length; i++)
                {
                    if (Students[i].Partner != null)
                    {
                        PeopleInLove++;
                    }
                }
                RoundFinish = true;
            }
            if (Type == GameTimeType.Hours && Hours >= TimeVal)
            {
                for (int i = 0; i < Students.Length; i++)
                {
                    if (Students[i].Partner != null)
                    {
                        PeopleInLove++;
                    }
                }
                RoundFinish = true;
            }
        }
        else
        {
            if(!GameOver)
            {
                wintxt.text = PeopleInLove.ToString();
                GameOverOverlay.SetActive(true);
                GameOver = true;
            }
        }
    }
}
