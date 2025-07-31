using UnityEngine;


[CreateAssetMenu(menuName = "AI/Difficulty", fileName = "DifficultySettings")]
public class DifficultySettings : ScriptableObject
{
    public float MinShootForce;
    public float MaxShootForce;
    public float MinShootSpread;
    public float MaxShootSpread;
}
