using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class CharacterAttributes
{
    public int aggression;
    public int charisma;
    public int popularity;
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
    [SerializeField]
    private CharacterAttributes _attributes;
    public CharacterAttributes attributes
    {
        get
        {
            return _attributes;
        }
        private set
        {
            attributes = value;
        }
    }

    [SerializeField]
    private SocialRecord _social;
    public SocialRecord social
    {
        get
        {
            return _social;
        }
        private set
        {
            _social = value;
        }
    }
}

[System.Serializable]
public class PersonStore
{
    // Lookup table for people by ID (e.g. index)
    [SerializeField]
    private List<Person> _people;
    public List<Person> people
    {
        get
        {
            return _people;
        }
        private set
        {
            _people = value;
        }
    }

    // Lookup table for relationships by POV ID and TARGET ID
    public List<Dictionary<int,SocialRecord>> _socialRecords;
    public List<Dictionary<int,SocialRecord>> socialRecords
    {
        get
        {
            return _socialRecords;
        }
        set
        {
            _socialRecords = value;
        }
    }

    public PersonStore()
    {
        _people = new List<Person>();
        _socialRecords = new List<Dictionary<int,SocialRecord>>();
    }
}