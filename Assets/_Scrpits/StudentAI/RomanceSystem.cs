using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RomanceSystem : MonoBehaviour {

    public enum RelationshipStatus
    {
        Single,
        InRelationShip,
        Confessing,
        Questionable
    }

    public List<StudentAI> TopLoveIntrest;

    public StudentAI Senpai;

    private StudentAI Self;

    public float MaxDesireLevel;

    public RelationshipStatus CurrentRelationshipStatus;

    public StudentAI Partner;

	// Use this for initialization
	void Start () {
        CurrentRelationshipStatus = RelationshipStatus.Single;
        Self = GetComponent<StudentAI>();
	}


    public void SingleBehavior()
    {

    }

    public void RelationshipBehavior()
    {

    }

    public void ConfessingBehavior()
    {


    }

    public void QuestionableBehavior()
    {

    }


    // Update is called once per frame
    void Update () {
		switch(CurrentRelationshipStatus)
        {
            case RelationshipStatus.Single:
                SingleBehavior();
                break;
            case RelationshipStatus.InRelationShip:
                RelationshipBehavior();
                break;
            case RelationshipStatus.Confessing:
                ConfessingBehavior();
                break;
            case RelationshipStatus.Questionable:
                QuestionableBehavior();
                break;


        }
    }
}
