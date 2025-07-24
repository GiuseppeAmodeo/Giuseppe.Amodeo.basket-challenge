using UnityEngine;


[CreateAssetMenu(fileName = "GameSettings", menuName = "GameConfig/GameSettings")]
public class GameSettings : ScriptableObject
{
    [Min(2)]
    public int MatchTime = 2;

}
