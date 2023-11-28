using UnityEngine;

[CreateAssetMenu(fileName = "Damage Config", menuName = "Guns/Damage Config", order = 1)]
public class DamageConfigScriptableObject : ScriptableObject
{
    public int Damage;

    public float BulletsAmount;
    public float ProjectileSpeed;
    
    public float VerticalBulletsSpread;
    public float HorizontalBulletsSpread;
}