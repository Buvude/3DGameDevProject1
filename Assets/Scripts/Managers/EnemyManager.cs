using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : Manager<EnemyManager>
{
    [Header("Base Variables")]
    public float maxEconomy;  //economy is the value that the manager has to spawn in creatures with 
    private float currentEconomy;  //economy is the value that the manager has to spawn in creatures with 
    public float timeInBetweenWaves = 10;//How long in seconds before managers spawns and scales



    [Header("SpawnControll variables")]
    public float initialRadius;//Radius of first spawn circle controlls mostly how far creatures spawn from you
    public float secondaryRadius;// adds some randomness to the spawnning so things dont show up in just a cirlce around you Should be atleast half as small as initialrad

    [Header("Scaling Variables")]
    public float EconomyIncreasePerWave = 5;// how much more money the Manager has to spawn creatures in with
    public float waveTimeScaling = 0;

    [Header("Statistic Variables")]
    public float waveCount;
    public float enemiesSlain;

    [Header("enemies Variables")]
    public List<GameObject> EnemyList;

    //random stuff
    private bool gameRunning = true;//continues loopy loop of spawns
    private Transform playerTransform;//used for finding position to spawn enemies

    private List<EnemyHolder> enemies = new List<EnemyHolder>();
    private List<EnemyHolder> nextWave = new List<EnemyHolder>();

    struct EnemyHolder
    {
        public Vector3 location;
        public GameObject enemy;
    }

    struct Point
    {
        public float x;
        public float y;
    }

    private void Start()
    {
        currentEconomy = maxEconomy;
        try { playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>(); }
        catch
        {
            print("No gameobject tagged Player in the scene");
            throw;
        }


        //testing

        startGame();


    }
    //calculate the waves enemies and positions
    //No check for an empty enemy list might be an issue
    public void buildWave()
    {


        List<GameObject> SpawnEnemyList = new List<GameObject>();
        // attempt to duplicate our enemy list so we can remove things from it when theyre too expensive
        EnemyList.ForEach((item) =>
        {
            SpawnEnemyList.Add(item);
        });

        //while we have spawn ecconomy to spend spend it to add monsters to the next wave
        while (currentEconomy > 0)
        {
            EnemyHolder waveMonster;
            //find an enemy that fits in our budget
            int rand = Random.Range(0, SpawnEnemyList.Count);//snag a random index of an enemy

            int Cost = spawnCost(SpawnEnemyList, rand);
            //see if the enemy at that index fits in our budget if it does not remove it from the list
            if (currentEconomy - Cost < 0)
            {
                //remove it from possible enemies to spawn
                SpawnEnemyList.RemoveAt(rand);
                if (SpawnEnemyList.Count <= 0)//if there are no more enemies left in the enemies availiable break
                    break;
                continue;
            }
            else
            {
                //add one of those suckers to the god damn next wave YEaaaaah
                waveMonster.enemy = SpawnEnemyList[rand];
                //take the cost of the enemy out of the budget
                currentEconomy -= Cost;
            }

            //generate first random point on a circle
            Vector3 spawnPoint = findSpawnPoint();

            //assign or legal spawn location to the monster being added to the wave
            waveMonster.location = spawnPoint;

            //add an enemy with location to next wave
            nextWave.Add(waveMonster);

            //empty that baby out 
            waveMonster.enemy = null;
            waveMonster.location = Vector3.zero;
        }

    }
    public int spawnCost(List<GameObject> li, int i)
    {
        if (li[i].GetComponent<Enemy>() != null)
        {
            //condition for most non flying enemy
            return (int)li[i].GetComponent<Enemy>().SpawnCost;
        }
        else
        {
            //condition for the jellies because fuck me I made them weird
            return (int)li[i].GetComponentInChildren<Enemy>().SpawnCost;
        }
    }

    // spawn in a wave
    public void spawnWave()
    {
        //loop through all the enemies and spawn the wave/ could add a little delay for some fun style
        foreach (EnemyHolder e in nextWave)
        {
            //spawn an enemy from e.enemy at the location e.location
            Instantiate(e.enemy, e.location, Quaternion.identity);
        }
    }

    //Loopy pooy for things



    //finds a valid spawn point
    public Vector3 findSpawnPoint()
    {


        float angle = Random.Range(0.0f, 1.0f) * Mathf.PI * 2;
        float z = Mathf.Cos(angle) * initialRadius;
        float x = Mathf.Sin(angle) * initialRadius;

        Vector3 firstSpawnPoint = new Vector3(playerTransform.position.x + x, 0, playerTransform.position.z + z);

        float angle2 = Random.Range(0.0f, 1.0f) * Mathf.PI * 2;
        float z2 = Mathf.Cos(angle2) * secondaryRadius;
        float x2 = Mathf.Sin(angle2) * secondaryRadius;

        Vector3 finalSpawnPoint = new Vector3(firstSpawnPoint.x + x2, 0, firstSpawnPoint.z + z2);


        //check if the point is a safe spot to spawn by checking if a raycast from the sky will hit the ground
        Vector3 offset = new Vector3(0, 40, 0);//this offset is how high the raycast will shoot down above the player

        RaycastHit validityCheck;//container for raycast info
        bool hit = Physics.Raycast(finalSpawnPoint + offset, Vector3.down, out validityCheck, 100);//from our potential spawn

        //can explode if the raycast doesnt hit anything, just extend terrarin far outside of outofBounds
        if (validityCheck.collider.gameObject.CompareTag("Ground"))
        {
            Vector3 smallOffsetY = new Vector3(0, .5f, 0);//add a small y offset so things dont show up in the ground
            //if we hit a ground object spawn there
            return validityCheck.point + smallOffsetY;
        }
        else
        {
            // if we hit something else find a new spawn point
            finalSpawnPoint = findSpawnPoint();//thats recursion boiiiiii if we have a stack overflow this went infinite and needs a limit
        }


        return finalSpawnPoint;
    }



    public void startGame()
    {
        StartCoroutine(SpawnLoop());
    }

    IEnumerator SpawnLoop()
    {
        while (gameRunning)
        {
            yield return new WaitForSecondsRealtime(timeInBetweenWaves);//give the playersome time to play the game
            buildWave();//build our next wave and get that baby in next wave
            spawnWave();// spawn in the next wave
            applyScaling();// all our wave/ enemy scaling happens heereere
            currentEconomy = maxEconomy; //reset the managers monies
            nextWave.Clear();// clear out nextwave so we have an empty list to build
            waveCount++;
        }
    }

    //handles all the scaling 
    public void applyScaling()
    {
        maxEconomy += EconomyIncreasePerWave;
        timeInBetweenWaves -= waveTimeScaling;
    }


}