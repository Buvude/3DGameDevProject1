using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    //Player Stats
    public PlayerStats stats;
    //UI Text GameObject
    public GameObject hpTextObj;
    //Text Component
    TextMeshProUGUI tmpHpText;
    // Start is called before the first frame update
    void Start()
    {
        tmpHpText = hpTextObj.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        tmpHpText.text = "HP:" + stats.CurHealth;
    }
}
