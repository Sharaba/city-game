using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Rank : MonoBehaviour
{
    public StealMode steal;
    public int plusXP;
    public int ExperiencePoints = 0;
    public string CurrentRank;
    public string[] Ranks = new string[4];
    public bool oncehas=false;
    public Text RankK;
	public Image xpBar;
    void Start()
    {
        Ranks[0] = "Bandit";
        Ranks[1] = "Prowler";
        Ranks[2] = "Shadow";
        Ranks[3] = "Master";
        CurrentRank = Ranks[0];
		xpBar.fillAmount = 0f;
        
    }
    public void Update()
    {
        RankK.text = CurrentRank;
		xpBar.fillAmount = XpCalculator();
        PlusXPP();
        CheckRankUP();
    }

	public float XpCalculator()
	{
		if (CurrentRank == Ranks[0]) {
			
			return ExperiencePoints / 30f;

		}else if (CurrentRank == Ranks[1]) {
			
			return ExperiencePoints / 50f;

		}else if (CurrentRank == Ranks[2]) {
			
			return ExperiencePoints / 80f;

		}else	{ return ExperiencePoints / 100f; }

	}

    void PlusXPP()
    {
        if (plusXP>0&&!oncehas)
        {
            plusXP = 0;
            ExperiencePoints += 5;
            oncehas = true;
        }
        if (plusXP >= 20 && !oncehas)
        {
            plusXP = 0;
            ExperiencePoints += 10;
            oncehas = true;
        }
        if (plusXP >= 40 && !oncehas)
        {
            plusXP = 0;
            ExperiencePoints += 20;
            oncehas = true;
        }
        if (plusXP >= 60 && !oncehas)
        {
            plusXP = 0;
            ExperiencePoints += 40;
            oncehas = true;
        }
        if (steal.Stealed)
            oncehas = false;
    }
    void CheckRankUP()
    {
        if(ExperiencePoints>=30 && ExperiencePoints<49)
        {
            CurrentRank = Ranks[1];
			ExperiencePoints = 0;
        }
        if(ExperiencePoints>=50 && ExperiencePoints<79)
        {
            CurrentRank = Ranks[2];
			ExperiencePoints = 0;
        }
        if(ExperiencePoints>=80 && ExperiencePoints<99)
        {
            CurrentRank = Ranks[3];
			ExperiencePoints = 0;
        }
    }
}