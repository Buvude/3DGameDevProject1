using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamagePopUp : MonoBehaviour
{

    private TextMeshPro textMesh;
    private float damageTotal;

    private float timer = 2.0f;

    private void Awake()
    {
        textMesh = GetComponent<TextMeshPro>();
    }
    public void Setup(int damageAmount)
    {
        damageTotal += damageAmount;
        textMesh.SetText(damageAmount.ToString());
    }

    public void addDamage(int damage)
    {
        damageTotal += damage;
        textMesh.SetText(damageTotal.ToString());
    }

    private void Update()
    {
        float moveYSpeed = 2.0f;
        transform.position += new Vector3(0, moveYSpeed) * Time.deltaTime;
        //transform.LookAt(GameObject.FindGameObjectWithTag("Player").transform,Vector3.up); // this doesnt work

        timer -= Time.deltaTime;
        if (timer <= 0) Destroy(this.gameObject);

    }

}
