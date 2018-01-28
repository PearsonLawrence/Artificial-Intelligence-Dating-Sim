using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class StudentAI : MonoBehaviour {

    public GameObject[] Skins;

    public enum States
    {
        Roam,
        Chill,
        Socialize,
        Confess

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

    public AudioClip[] Giggles;

    public void playClickSound()
    {
        GetComponent<AudioSource>().PlayOneShot(Giggles[Random.Range(0, Giggles.Length - 1)]);
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

    void NewDestination()
    {
        Agent.SetDestination(RandomNavmeshLocation(10));
    }
    // Use this for initialization
    void Start()
    {
        int skin = Random.Range(0, Skins.Length - 1);
        for(int i = 0; i < Skins.Length; i++)
        {
            if(i == skin) { Skins[i].SetActive(true); }
            else { Skins[i].SetActive(false); }
        }
        RB = GetComponent<Rigidbody>();
        Agent = GetComponent<NavMeshAgent>();
        School = GameObject.FindObjectOfType<SchoolManager>();
        CurrentState = States.Roam;
        thought = GetComponent<IdleThoughtSystem>();
    }

    public float ChillTime;
    public void ForceInteract(StudentAI Target)
    {
        SocialTarget = Target;
        CurrentState = States.Socialize;
    }

    public void DoRoam()
    {
        if (Partner == null)
        {


            if (Agent.remainingDistance <= 10)
            {
                CurrentState = States.Chill;
                NewDestination();
                ChillTime = Random.Range(0, 10);

                int temp = Random.Range(0, 100);
                bool think = (temp > 75) ? true : false;
                if (think)
                {
                    thought.IdleGrow = true;
                }
            }
        }
        else
        {
            if(confessed)
            {
                if (Agent.remainingDistance <= 10)
                {
                    CurrentState = States.Chill;
                    NewDestination();
                    ChillTime = Random.Range(0, 10);

                    int temp = Random.Range(0, 100);
                    bool think = (temp > 75) ? true : false;
                    if (think)
                    {
                        thought.IdleGrow = true;
                    }
                }
            }
            else
            {
                Agent.SetDestination(Partner.transform.position);
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

    public GameObject PrefabPop;
    public float ChatLength;
    private float SetChatLength;
    bool enterChat;
    bool ChatBoxSpawned;
    public void DoSocial()
    {
        Agent.SetDestination(SocialTarget.transform.position);

        if (Agent.remainingDistance < 5 && enterChat == false)
        {
            Agent.SetDestination(transform.position);

            enterChat = true;
            if (!ChatBoxSpawned && !SocialTarget.ChatBoxSpawned)
            {
                ChatBoxSpawned = true;
                PersonTag targetTag = SocialTarget.gameObject.GetComponent<PersonTag>();
                PersonTag tag = GetComponent<PersonTag>();
                
                //Do conversation here!!!!
                bool result = PairWiseInteraction(tag, targetTag);
                ChatCooldown = 5;
                //Debug.Log("fuck has occurred");

                GameObject NewConvo = Instantiate(PrefabPop, transform.position, Quaternion.identity);
                ConversationPopup temp = NewConvo.GetComponent<ConversationPopup>();

                temp.Feeling = (result) ? 1 : 0;
                temp.One = this.gameObject;
                temp.Two = SocialTarget.gameObject;

                Destroy(NewConvo, ChatLength);
            }

            SetChatLength = ChatLength;
            ChatEngaged = false;
            //Agent.isStopped = true;
        }
        if (enterChat == true)
        {
            if(SetChatLength <= 0)
            {
                enterChat = false;
                ChatBoxSpawned = false;
               // Agent.isStopped = false;
                NewDestination();
                CurrentState = States.Roam;
            }
        }



        SetChatLength -= Time.deltaTime;

    }
    public StudentAI Partner;
    public ParticleSystem PSLove;
    public bool confessed;

    public void DoConfession()
    {
        Agent.SetDestination(SocialTarget.transform.position);

        if(PSLove.isPlaying == false)
        {
            PSLove.Play();

        }

        if (Agent.remainingDistance < 5 && enterChat == false)
        {
            Agent.SetDestination(transform.position);

            enterChat = true;
            if (!ChatBoxSpawned && !SocialTarget.ChatBoxSpawned)
            {
                ChatBoxSpawned = true;
                PersonTag targetTag = SocialTarget.gameObject.GetComponent<PersonTag>();
                PersonTag tag = GetComponent<PersonTag>();
                
                ChatCooldown = 5;
                //Debug.Log("fuck has occurred");

                GameObject NewConvo = Instantiate(PrefabPop, transform.position, Quaternion.identity);
                ConversationPopup temp = NewConvo.GetComponent<ConversationPopup>();

                Person rPerson = Kernal.instance.store.people[SocialTarget.GetComponent<PersonTag>().storeID]; 

                SocialRecord record = null;
                SocialRecord record2 = null;
                record = Kernal.instance.store.socialRecords[SocialTarget.GetComponent<PersonTag>().storeID][GetComponent<PersonTag>().storeID];
                record2 = Kernal.instance.store.socialRecords[GetComponent<PersonTag>().storeID][SocialTarget.GetComponent<PersonTag>().storeID];
               
                float AVG = record.desire + record2.desire / 2;

                bool result = (AVG > Random.Range(0, 100)) ? true : false;

                temp.Feeling = (result) ? 2 : 0;
                temp.One = this.gameObject;
                temp.Two = SocialTarget.gameObject;
                
                if(result)
                {
                    if(Partner != null)
                    {
                        Partner.Partner = null;
                    }
                    Partner = SocialTarget;
                    if(Partner.Partner != null)
                    {
                        Partner.Partner = null;
                    }
                    Partner.Partner = this;
                    record.RelationShipStatus = 1;
                    record2.RelationShipStatus = 1;
                    confessed = true;
                    Partner.confessed = false;
                }

                Destroy(NewConvo, ChatLength);
            }

            SetChatLength = ChatLength;
            ChatEngaged = false;
            //Agent.isStopped = true;
        }
        if (enterChat == true)
        {
            if (SetChatLength <= 0)
            {
                PSLove.Stop();
                enterChat = false;
                ChatBoxSpawned = false;
                // Agent.isStopped = false;
                NewDestination();
                CurrentState = States.Roam;
            }
        }
        SetChatLength -= Time.deltaTime;

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

        if(record.desire > 75)
        {
            CurrentState = States.Confess;
            enterChat = false;
            ChatBoxSpawned = false;
        }
        //Debug.Log(string.Format("{0}, {1}, {2}", record.familiarity, record.trust, record.eros));
        //Debug.Log(record.desire);
        bool result = roll < chance;
        if(result == true) { rPerson.attributes.popularity++;  }
        else { rPerson.attributes.popularity--; }
        return result;
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
            case States.Confess:
                DoConfession();
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


