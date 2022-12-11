using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    public int jellyFishKilled, hazmatDudesKilled, //tracks how many of each were killed
        jellyFishWorth, hazmatDudesWorth, //tracks the point value for each kill
        totalScore, VictoryWorth;//total score
    public bool vicotry = false;
    public enum EnemyType { JellyFish, HazmatDude}
    // Start is called before the first frame update
    
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }
    public void EndRound()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Kill(EnemyType whoDied)
    {
        switch (whoDied)
        {
            case EnemyType.JellyFish:
                jellyFishKilled++;
                totalScore += jellyFishWorth;
                break;
            case EnemyType.HazmatDude:
                hazmatDudesKilled++;
                totalScore += hazmatDudesWorth;
                break;
            default:
                break;
        }
    }
   
    public void startFinalCalcMethod(TextMeshProUGUI Line1, TextMeshProUGUI Line2, TextMeshProUGUI Line3, TextMeshProUGUI Line4)
    {
        TextMeshProUGUI[] tMPArray = new TextMeshProUGUI[4];
        tMPArray.SetValue(Line1, 0);
        tMPArray.SetValue(Line2, 1);
        tMPArray.SetValue(Line3, 2);
        tMPArray.SetValue(Line4, 3);
        StartCoroutine("FinalScorOutput", tMPArray);
    }
    IEnumerator FinalScorOutput(TextMeshProUGUI[] ArrayStuff)
    {
        string jellyfish, hazmatDudes, deadOrAlive, finalScore;
        jellyfish = "You have killed " + jellyFishKilled + "of those jelly fish creatures(*" + jellyFishWorth + ")\t " + (jellyFishKilled * jellyFishWorth) + " points";
        hazmatDudes = "You have killed " + hazmatDudesKilled + "of those Hazmat dudes(*" + hazmatDudesWorth + ")\t " + (hazmatDudesKilled * hazmatDudesWorth) + " points";
        if (vicotry)
        {
            /* Image[] temp;
             temp= GameObject.FindGameObjectWithTag("UI").GetComponentsInChildren<Image>();*/
            /*foreach(Image tempImage in temp)
            {
                if (tempImage.gameObject.CompareTag("VictoryImage"))
                {
                    print("Got a vicotry Image");
                    tempImage.gameObject.SetActive(true);
                    break;
                }
            }*/
            deadOrAlive = "You have manged to survie! You get " + VictoryWorth + " points as a reward!";
            totalScore += VictoryWorth;
        }
        else
        {
            /*  Image[] temp;*/
            /*temp = GameObject.FindGameObjectWithTag("UI").GetComponentsInChildren<Image>();
            foreach (Image tempImage in temp)
            {
                if (tempImage.gameObject.CompareTag("DeathImage"))
                {
                    print("got a death image");
                    tempImage.gameObject.SetActive(true);
                    break;
                }
            }*/
            print(transform.Find("UI").Find("Death"));
            deadOrAlive = "Unfortunatly you died trying to escape... You do not get any points for this";
        }
        finalScore = "Your final score is...\t" + totalScore;
        ArrayStuff[0].text = jellyfish;
        yield return new WaitForSeconds(1f);
        ArrayStuff[1].text = hazmatDudes;
        yield return new WaitForSeconds(1f);
        ArrayStuff[2].text = deadOrAlive;
        yield return new WaitForSeconds(1f);
        ArrayStuff[3].text = finalScore;

    }
}
