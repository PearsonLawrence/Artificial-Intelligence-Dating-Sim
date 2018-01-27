﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class LevelGenerator : MonoBehaviour {

    public MenuManager Manager;

    public bool spawn = true;

    public GameObject MalePrefab, FemalePrefab;

    public Kernal kernal;
    // Use this for initialization
    void Start ()
    {
        kernal = GameObject.FindObjectOfType<Kernal>();
       GatherPreplacedCharacters();
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

    void GatherPreplacedCharacters()
    {
        var characters = GameObject.FindObjectsOfType(typeof(PersonTag));

        for(int i = 0; i < characters.Length; ++i)
        {
            PersonTag tag = (characters[i] as PersonTag);
            Person temp = new Person();
            temp.attributes.aggression = Random.RandomRange(0, 5);
            temp.attributes.charisma = Random.RandomRange(0, 5);
            temp.attributes.popularity = Random.RandomRange(0, 5);

            kernal.store.people.Add(temp);
            kernal.store.socialRecords.Add(new Dictionary<int, SocialRecord>());
        }
    }

    int numSpawned = 0;
    GameObject SpawnPerson(GameObject prefab, Vector3 spawnLocation, Quaternion spawnRotation)
    {
        GameObject temp = Instantiate(prefab, RandomNavmeshLocation(12), Quaternion.identity);
        temp.GetComponent<PersonTag>().storeID = numSpawned;
        numSpawned++;
        Person TempPerson = new Person();

        kernal.store.people.Add(TempPerson);
        kernal.store.socialRecords.Add(new Dictionary<int, SocialRecord>());

        return temp;
    }

    // Update is called once per frame
    void Update () {
		if(spawn)
        {
            

            int Majority = (Manager.MaleSpawn > Manager.FemaleSpawn) ? Manager.MaleSpawn : Manager.FemaleSpawn;

            for (int i = 0; i < Majority; i++)
            {
                if (i < Manager.MaleSpawn)
                {
                    SpawnPerson(MalePrefab, RandomNavmeshLocation(12), Quaternion.identity);
                }
                if (i < Manager.FemaleSpawn)
                {
                    SpawnPerson(FemalePrefab, RandomNavmeshLocation(12), Quaternion.identity);
                }
            }
            spawn = false;
        }
	}
}
