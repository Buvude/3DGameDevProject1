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

    TextMeshProUGUI[] tMPArray = new TextMeshProUGUI[4];

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
   public void ResetVictory()
    {
        vicotry = false;
        totalScore = 0;
        jellyFishKilled = 0;
        hazmatDudesKilled = 0;
    }
    public void startFinalCalcMethod(TextMeshProUGUI Line1, TextMeshProUGUI Line2, TextMeshProUGUI Line3, TextMeshProUGUI Line4)
    {
        // TextMeshProUGUI[] tMPArray = new TextMeshProUGUI[4];
        tMPArray[0] = Line1;//  tMPArray.SetValue(Line1, 0);
        tMPArray[1] = Line2;//tMPArray.SetValue(Line2, 1);
        tMPArray[2] = Line3;//.SetValue(Line3, 2);
        tMPArray[3] = Line4;//tMPArray.SetValue(Line4, 3);
       StartCoroutine( "FinalScoreOutput"/*(tMPArray)*/);
       
    }
        IEnumerator  FinalScoreOutput(/*TextMeshProUGUI[] tMPArray*/)
    {
        tMPArray[0].text = "fish are friedns";
        string jellyfish, hazmatDudes, deadOrAlive, finalScore;
        jellyfish = "You killed " + jellyFishKilled + " jellyfish(*" + jellyFishWorth + ")\n " + (jellyFishKilled * jellyFishWorth) + " points";
        print("Test for Victory Image 1");
        hazmatDudes = "You killed " + hazmatDudesKilled + " hazmats(*" + hazmatDudesWorth + ")\n " + (hazmatDudesKilled * hazmatDudesWorth) + " points";
        print("Test for Victory Image 2");
        if (vicotry)
        {
            GameObject.FindGameObjectWithTag("VictoryImage").GetComponent<Image>().enabled = true;
            print("Test for Victory Image 3");
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
            deadOrAlive = "You survived! + " + VictoryWorth + " points!";
            totalScore += VictoryWorth;
        }
        else
        {
            GameObject.FindGameObjectWithTag("DeathImage").GetComponent<Image>().enabled = true;
            print("Test for Victory Image 4");
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

            deadOrAlive = "You died.";
        }
        finalScore = "Your final score was " + totalScore;
        print(tMPArray[0].text);
        tMPArray[0].text = jellyfish;
        yield return new WaitForSeconds(1f);
        tMPArray[1].text = hazmatDudes;
        yield return new WaitForSeconds(1f);
        tMPArray[2].text = deadOrAlive;
        yield return new WaitForSeconds(1f);
        tMPArray[3].text = finalScore;

    }
}
