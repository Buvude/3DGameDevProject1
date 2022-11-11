using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Inventory_Slash_ItemManager : MonoBehaviour
{
    private LayerMask mapItem;
    private LayerMask enemyItem;
    public TextMeshProUGUI Inventory;
    // Start is called before the first frame update
    void Start()
    {
        mapItem = LayerMask.NameToLayer("mapCollectable");
        enemyItem = LayerMask.NameToLayer("enemyCollectable"); //now those two layers can be refered to as a variable
    }
    

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == mapItem)
        {
            MapItemPickUp();   
        }
        else if (other.gameObject.layer == enemyItem)
        {
            EnemyItemPickUp();
        }
    }

    public void MapItemPickUp()
    {
        //update ui
        //destroy object
        //play sound effect?
        Debug.Log("test, you picked up item");

    }

    public void EnemyItemPickUp()
    {
        //update ui
        //destroy object
        //play sound effect?
        Debug.Log("test, you picked up item");

    }
}
