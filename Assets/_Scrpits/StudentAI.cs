using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class StudentAI : MonoBehaviour {
    

    public enum States
    {
        Roam,
        Work,
        Home

    }

    public Sprite sprite;
    private Rigidbody RB;
    private NavMeshAgent Agent;

    public Vector3 Target;

   

    [System.Serializable]
    public class Traits
    {
        public bool Introvert;

        public int BloodType; // 0 = NA, 1 = A, 2 = B, 3 = O
    }

    public Traits traits;

    public States CurrentState;



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
        Agent.SetDestination(RandomNavmeshLocation(1000));
    }
    // Use this for initialization
    void Start()
    {
        RB = GetComponent<Rigidbody>();
        Agent = GetComponent<NavMeshAgent>();
        CurrentState = States.Roam;
    }


    public void DoRoam()
    {
        if (Agent.remainingDistance <= 5) { NewDestination(); }


    }
    public void DoWork()
    {
    }
    public void DoHome()
    {

    }

    public bool WorkTime;

    // Update is called once per frame
    void Update()
    {
        transform.eulerAngles = new Vector3(0, 0, 0);
        switch (CurrentState)
        {
            case States.Roam:
                DoRoam();
                break;
            case States.Work:
                DoWork();
                break;
            case States.Home:
                DoHome();
                break;
        }

        if (WorkTime && CurrentState != States.Work)
        {
            CurrentState = States.Work;
        }

        
    }
}


