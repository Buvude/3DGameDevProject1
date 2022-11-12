using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventoryManager : MonoBehaviour
{
    public TextMeshProUGUI inventorytxt;
    private LayerMask mapItem;
    private LayerMask enemyDrop;
    public int mapNum, eDropNum, mapNumMax, eDropNumMax;
    // Start is called before the first frame update
    void Start()
    {
        mapNum = 0;
        eDropNum = 0;
        UpdateInventoryText();
        mapItem = LayerMask.NameToLayer("MapItem");
        enemyDrop = LayerMask.NameToLayer("EnemyDropItem");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter (Collider other)
    {
        if (other.gameObject.layer == mapItem)
        {
            MapPickup();
        }
    }

    public void MapPickup()
    {

    }

    public void UpdateInventoryText()
    {
        inventorytxt.text = "Map collectables: " + mapNum + "/" + mapNumMax + "\n" +
            "Enemy drop collectables" + eDropNum + "/" + eDropNumMax;
    }
}
