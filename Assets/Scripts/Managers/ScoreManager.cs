using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public int jellyFishKilled, hazmatDudesKilled, //tracks how many of each were killed
        jellyFishWorth, hazmatDudesWorth, //tracks the point value for each kill
        totalScore;//total score
    public bool vicotry = false;
    public enum EnemyType { JellyFish, HazmatDude}
    // Start is called before the first frame update
    
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
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
}
