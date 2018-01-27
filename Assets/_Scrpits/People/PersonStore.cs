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
    public int familiarity;
    public int trust;
    public int eros;

    public int desire
    {
        get{ return (familiarity + trust + eros) / 3;}
    }
}

[System.Serializable]
public class Person
{
    public CharacterAttributes attributes = new CharacterAttributes();
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