using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class StudentAI : MonoBehaviour {
    

    public enum States
    {
        Roam,
        Chill,
        Socialize

    }
    public enum SocialIntent
    {
        Friendly,
        Romantic,
        Hostile

    }
    public Sprite sprite;
    private Rigidbody RB;
    private NavMeshAgent Agent;

    public Vector3 Target;

    public SchoolManager School;


    public StudentAI SocialTarget;

    public States CurrentState;
    public SocialIntent CurrentIntent;
    public bool WorkTime;

    public IdleThoughtSystem thought;
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

    void NewDestination()
    {
        Agent.SetDestination(RandomNavmeshLocation(10));
    }
    // Use this for initialization
    void Start()
    {
        RB = GetComponent<Rigidbody>();
        Agent = GetComponent<NavMeshAgent>();
        School = GameObject.FindObjectOfType<SchoolManager>();
        CurrentState = States.Roam;
        thought = GetComponent<IdleThoughtSystem>();
    }

    public float ChillTime;
    public void DoRoam()
    {
        if (Agent.remainingDistance <= 10)
        {
            CurrentState = States.Chill;
            NewDestination();
            ChillTime = Random.Range(0, 10);

            int temp = Random.Range(0, 100);
            bool think = (temp > 75) ? true : false;
            if(think)
            {
                thought.IdleGrow = true;
            }
        }


    }
    
    public StudentAI FindClosetSocialStudent()
    {
        StudentAI CloseSocial = null;
        float Dist = 1000;
        for (int i = 0; i < School.Students.Length; i++)
        {
            float newDist = Vector3.Distance(transform.position, School.Students[i].transform.position);
            if (newDist < Dist && School.Students[i] != this)
            {
                Dist = newDist;
                CloseSocial = School.Students[i];
            }
        }
        return CloseSocial;
    }
    
    public bool ChatEngaged = false;

    public void DoChill()
    {
        ChillTime -= Time.deltaTime;
        if(ChatEngaged)
        {
            CurrentState = States.Socialize;
            thought.IdleGrow = false;
        }

        if(ChillTime <= 0)
        {
            //if(SocialTarget == null) { return; }
            //Agent.SetDestination(SocialTarget.transform.position);
            thought.IdleGrow = false;
            CurrentState = States.Roam;
            //SocialTarget = null;
        }
    }

    public void DoSocial()
    {
        Agent.SetDestination(SocialTarget.transform.position);

        if (Agent.remainingDistance < 5)
        {
            PersonTag targetTag = SocialTarget.gameObject.GetComponent<PersonTag>();
            PersonTag tag = GetComponent<PersonTag>();
            Agent.SetDestination(SocialTarget.transform.position);

            //Do conversation here!!!!
            PairWiseInteraction(tag, targetTag);
            CurrentState = States.Roam;
            ChatCooldown = 5;
            //Debug.Log("fuck has occurred");
        }
        else if(ChatEngaged == true)
        {
            
        }

        ChatEngaged = false;
    }

    public bool PairWiseInteraction(PersonTag initiator, PersonTag reciever)
    {
        Person sPerson = Kernal.instance.store.people[initiator.storeID];
        Person rPerson = Kernal.instance.store.people[reciever.storeID];

        int chance = (sPerson.attributes.engagement + rPerson.attributes.engagement) / 2;
        int roll = Random.Range(0, 10);

        // true if positive, false if negative
        int multi = (roll < chance) ? 1 : -1;

        // initiator record
        bool hasKey = Kernal.instance.store.socialRecords[initiator.storeID].ContainsKey(reciever.storeID);
        SocialRecord record = null;
        if(!hasKey)
        {
            Kernal.instance.store.socialRecords[initiator.storeID][reciever.storeID] = new SocialRecord();
        }
        record = Kernal.instance.store.socialRecords[initiator.storeID][reciever.storeID];

        // apply
        record.familiarity += multi * 5 * sPerson.attributes.aggression;
        record.trust       += multi * 5 * sPerson.attributes.popularity;
        record.eros        += multi * 5 * sPerson.attributes.charisma;

        //Debug.Log(string.Format("{0}, {1}, {2}", record.familiarity, record.trust, record.eros));
        //Debug.Log(record.desire);


        // reciever record
        hasKey = Kernal.instance.store.socialRecords[reciever.storeID].ContainsKey(initiator.storeID);
        if(!hasKey)
        {
            Kernal.instance.store.socialRecords[reciever.storeID][initiator.storeID] = new SocialRecord();
        }
        record = Kernal.instance.store.socialRecords[reciever.storeID][initiator.storeID];

        // apply
        record.familiarity += multi * 5 * rPerson.attributes.aggression;
        record.trust       += multi * 5 * rPerson.attributes.popularity;
        record.eros        += multi * 5 * rPerson.attributes.charisma;

        //Debug.Log(string.Format("{0}, {1}, {2}", record.familiarity, record.trust, record.eros));
        //Debug.Log(record.desire);

        return roll < chance;
    }

   
    // Update is called once per frame
    void Update()
    {
        transform.eulerAngles = new Vector3(0, 0, 0);
        switch (CurrentState)
        {
            case States.Roam:
                DoRoam();
                break;
            case States.Chill:
                DoChill();
                break;
            case States.Socialize:
                DoSocial();
                break;
        }

        if(ChatEngaged == true && CurrentState != States.Socialize && ChatCooldown <= 0)
        {
            CurrentState = States.Socialize;
        }

        ChatCooldown -= Time.deltaTime;

    }

    public float ChatCooldown;
}


