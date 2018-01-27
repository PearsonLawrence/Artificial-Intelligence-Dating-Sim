using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class LevelGenerator : MonoBehaviour {

    public MenuManager Manager;

    public bool spawn = true;

    public GameObject MalePrefab, FemalePrefab;
	// Use this for initialization
	void Start ()
    {
		
       
    }

    public Vector3 RandomNavmeshLocation(float radius)
    {
        Vector3 randomDirection = Random.insideUnitSphere * radius;
        randomDirection += transform.position;
        NavMeshHit hit;
        Vector3 finalPosition = Vector3.zero;
        if (NavMesh.SamplePosition(randomDirection, out hit, radius, 1))
        {
            finalPosition = hit.position;
        }
        return finalPosition;
    }
    // Update is called once per frame
    void Update () {
		if(spawn)
        {
            for (int i = 0; i < Manager.MaleSpawn; i++)
            {
                Instantiate(MalePrefab, RandomNavmeshLocation(12), Quaternion.identity);
            }
            for (int i = 0; i < Manager.FemaleSpawn; i++)
            {
                Instantiate(FemalePrefab, RandomNavmeshLocation(12), Quaternion.identity);
            }
            spawn = false;
        }
	}
}
