using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager :Manager<EnemyManager>
{
    [Header ("Base Variables")]
    private float maxEconomy = 10;  //economy is the value that the manager has to spawn in creatures with 
    private float currentEconomy = 10;  //economy is the value that the manager has to spawn in creatures with 
    public float timeInBetweenWaves = 10;//How long in seconds before managers spawns and scales
    public float radiusOfSpawning = 10; //radius of how far away the manager will spawn creatures from you
    public float lowerNoiseBound, upperNoiseBound;// randomness for spawning creatures 
    private bool gameRunning = true;

    [Header("SpawnControll variables")]
    public float initialRadius;//Radius of first spawn circle controlls mostly how far creatures spawn from you
    public float secondaryRadius;// adds some randomness to the spawnning so things dont show up in just a cirlce around you Should be atleast half as small as initialrad

    [Header("Scaling Variables")]
    private float EconomyIncreasePerWave = 5;// how much more money the Manager has to spawn creatures in with
    private float waveTimeScaling = 0;

    [Header("Statistic Variables")]
    public float waveNum;
    public float enemiesSlain;

    [Header("enemies Variables")]
    public List<GameObject> EnemyList;

    
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
        print(EnemyList.Count);
    }
    //calculate the waves enemies and positions
    public void buildWave(){

        
        List<GameObject> SpawnEnemyList = new List<GameObject>();
        // attempt to duplicate our enemy list so we can remove things from it when theyre too expensive
        EnemyList.ForEach((item) =>
        {
            SpawnEnemyList.Add(item);
        });

        //while we have spawn ecconomy to spend spend it to add monsters to the next wave
        while (currentEconomy > 0)
        {
            EnemyHolder temp;
            //find an enemy that fits in our budget
            int rand = Random.Range(1, EnemyList.Count);//snag a random index of an enemy
            //see if the enemy at that index fits in our budget if it does not remove it from the list
            if(currentEconomy - SpawnEnemyList[rand].GetComponent<Enemy>().SpawnCost < 0)
            {
                //remove it from possible enemies to spawn
                SpawnEnemyList.RemoveAt(rand);
                continue;
            }
            else
            {
                //add one of those suckers to the god damn next wave YEaaaaah
                temp.enemy = SpawnEnemyList[rand];
                //take the cost of the enemy out of the budget
                currentEconomy -= SpawnEnemyList[rand].GetComponent<Enemy>().SpawnCost;
            }

            //generate first random point on a circle
            float angle = Random.Range(0,1) * Mathf.PI * 2;
            float x = Mathf.Cos(angle) * initialRadius;
            float y = Mathf.Sin(angle) * initialRadius;

            Point p;
            p.x = x;
            p.y = y;

            // we have our initial point it has no reference of the players location though
            

            //find a location for the enemy 
            //assign the location to the location field of our holder

            //add the temp holder to next wave
            temp.location = Vector3.zero;

            


            //add an enemy to the next wave as a test
            nextWave.Add(temp);

            //empty that baby out 
            temp.enemy = null;
            temp.location = Vector3.zero;
        }

    }


    // spawn in a wave
    public void spawnWave() 
    {
        //loop through all the enemies and spawn the wave/ could add a little delay for some fun style
        foreach(EnemyHolder e in nextWave)
        {
            //spawn an enemy from e.enemy at the location e.location
            
        }
    }

    //Loopy pooy for things
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            startGame();//start the gameplay spawning and any other intro stuff we want to add
        }
    }

    public void startGame()
    {
        StartCoroutine(SpawnLoop());
    }

    IEnumerator SpawnLoop()
    {
        while (gameRunning)
        {
            buildWave();//build our next wave and get that baby in next wave
            yield return new WaitForSecondsRealtime(timeInBetweenWaves);//give the playersome time to play the game
            spawnWave();// spawn in the next wave
            applyScaling();// all our wave/ enemy scaling happens heereere
            currentEconomy = maxEconomy; //reset the managers monies
            nextWave.Clear();// clear out nextwave so we have an empty list to build
        }
    }

    //handles all the scaling 
    public void applyScaling()
    {

    }


}