using System.Collections.Generic;

using Unity.VisualScripting;

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

    public int CharRegister(VitalitySystem vitalitySystem)
    {
        CharacterData characterData = new CharacterData { _id = _currentID, _vitalitySystem = vitalitySystem };
        _registeredCharacters.Add(characterData._id, characterData);
        EventManager.CallOnCharRegister();
        _currentID++;
        return characterData._id;
    }
    public void CharDelete(int id)
    {
        if (_registeredCharacters.ContainsKey(id))
        {
            CharacterData characterData = _registeredCharacters[id];
            _registeredCharacters.Remove(characterData._id);
            EventManager.CallOnCharDelete();
        }
    }
}

public class CharacterData
{
    public int _id;
    public VitalitySystem _vitalitySystem;
}