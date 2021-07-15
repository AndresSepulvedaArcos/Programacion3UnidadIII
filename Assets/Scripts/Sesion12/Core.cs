using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Core : Tower
{

    public Transform otherCore;
    public GameObject minionPrefabA;


    private void Start()
    {
        StartCoroutine(MinionSpawn());
    }
    IEnumerator MinionSpawn()
    {
        while(gameObject.activeInHierarchy)
        {
            GenerateMinions();
            yield return new WaitForSeconds(30);
        }
    }
    void GenerateMinions()
    {
        GameObject minion;
        Vector3 spawnPosition = Vector3.zero;
        for (int i = 0; i < 5; i++)
        {

            spawnPosition = transform.position + transform.forward * 8 + (Vector3)Random.insideUnitCircle * 4f;
            spawnPosition.y = 5.1f;
            minion = Instantiate(minionPrefabA, spawnPosition, Quaternion.identity);
            minion.GetComponent<Minion>().InitializeMinion(team, otherCore);
        }
    }
}
