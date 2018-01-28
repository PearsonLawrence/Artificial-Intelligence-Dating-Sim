using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class CharacterAttributes
{
    public int aggression;
    public int charisma;
    public int popularity;

    public int engagement
    {
        get
        {
            return aggression + charisma + popularity;
        }
    }
}

[System.Serializable]
public class SocialRecord
{
    public int RelationShipStatus; // 0 = single, 1 = Relationship, 3 = questionable, 4 = confessing
        
    

    public int familiarity;
    public int trust;
    public int eros;

    public bool DesireOverride;

    public float desire
    {
        get
        {
            float Damp = 0;
            switch (RelationShipStatus)
            {
                case 0:
                    Damp = 1;
                    break;
                case 1:
                    Damp = .25f;
                    break;
                case 2:
                    Damp = .75f;
                    break;
                case 3:
                    Damp = 1.25f;
                    break;

            }
            float FinalDamp = (3 * Damp);
            if (!DesireOverride)
            {
                return (familiarity + trust + eros) / FinalDamp;
            }
            else
            {
                return 100;
            }
        }
    }
}

[System.Serializable]
public class Person
{
    public string name;
    public Gender gender;
    public string genderDescriptor; // only applicable if gender is other
    public CharacterAttributes attributes = new CharacterAttributes();

    public StudentAI aiController;

    // oh no
    // i regret everything
}

[System.Serializable]
public class PersonStore
{
    // Lookup table for people by ID (e.g. index)
    public List<Person> people;

    // Lookup table for relationships by POV ID and TARGET ID
    public List<Dictionary<int,SocialRecord>> socialRecords;

    public PersonStore()
    {
        people = new List<Person>();
        socialRecords = new List<Dictionary<int,SocialRecord>>();
    }
}