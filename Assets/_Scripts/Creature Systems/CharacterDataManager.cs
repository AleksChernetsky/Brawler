using System.Collections.Generic;

using UnityEngine;

public class CharacterDataManager : MonoBehaviour
{
    private int _currentID;
    private Dictionary<int, CharacterData> _registeredCharacters = new Dictionary<int, CharacterData>();

    public static CharacterDataManager instance;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public int CharRegister(string name, VitalitySystem vitalitySystem)
    {
        CharacterData characterData = new CharacterData { _id = _currentID, _name = name, _vitalitySystem = vitalitySystem };
        _registeredCharacters.Add(characterData._id, characterData);
        _currentID++;
        return characterData._id;
    }
}

public class CharacterData
{
    public int _id;
    public string _name;
    public int _killsCount;
    public VitalitySystem _vitalitySystem;
}