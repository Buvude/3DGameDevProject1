using TMPro;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public AudioSource collectable;
    private bool triggeredEndingCutscene=false;
    internal EventAnimManager eAM;
    public TextMeshProUGUI inventorytxt;
    private LayerMask mapItem;
    private LayerMask enemyDrop;
    public int mapNum, eDropNum, mapNumMax, eDropNumMax;
    // Start is called before the first frame update
    void Start()
    {
        /*Debug.Log(Time.deltaTime.ToString());*/
        eAM = GameObject.FindGameObjectWithTag("EventManeger").GetComponentInChildren<EventAnimManager>();
        mapNum = 0;
        eDropNum = 0;
        UpdateInventoryText();
        mapItem = LayerMask.NameToLayer("MapItem");
        enemyDrop = LayerMask.NameToLayer("EnemyDropItem");
    }

    // Update is called once per frame
    void Update()
    {
        if (mapNum == mapNumMax && eDropNum == eDropNumMax && triggeredEndingCutscene == false)
        {
            triggeredEndingCutscene = true;
            eAM.exitOpen();

        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == mapItem)
        {
            MapPickup();
            Destroy(other.gameObject);
            collectable.Play();
        }
        else if (other.gameObject.layer == enemyDrop)
        {
            EnemyDropPickup();
            Destroy(other.gameObject);
        }
    }

    public void MapPickup()
    {
        mapNum++;
        UpdateInventoryText();
    }
    public void EnemyDropPickup()
    {
        eDropNum++;
        UpdateInventoryText();
    }

    public void UpdateInventoryText()
    {
        inventorytxt.text = "Map collectables: " + mapNum + "/" + mapNumMax + "\n" +
            "Enemy drop collectables" + eDropNum + "/" + eDropNumMax;
    }
}
