using System.Collections.Generic;

public class CharacterAttributes
{
    public int aggression;
    public int charisma;
    public int familiarity;
}

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
    public CharacterAttributes attributes {get; private set;}
    public SocialRecord social {get; private set;}
}

[System.Serializable]
public class PersonStore
{
    // Lookup table for people by ID (e.g. index)
    public List<Person> people {get; private set;}

    // Lookup table for relationships by POV ID and TARGET ID
    public List<Dictionary<int,SocialRecord>> socialRecords {get; private set;}

    public PersonStore()
    {
        people = new List<Person>();
        socialRecords = new List<Dictionary<int,SocialRecord>>();
    }
}