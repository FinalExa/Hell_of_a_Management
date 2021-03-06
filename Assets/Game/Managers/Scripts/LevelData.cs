using UnityEngine;


[CreateAssetMenu(fileName = "New Levels Data", menuName = "ScriptableObjects/Levels Data", order = 6)]
public sealed class LevelData : ScriptableObject
{
    [System.Serializable]
    public struct Level
    {
        public string levelName;
        public string levelGoalDescription;
        public string LevelLogoPath;

        public uint firstStarScore;
        public uint secondStarScore;
        public uint thirdStarScore;

        public float levelTimer;
    }

    [SerializeField] Level[] levels;

    public Level GetLevel(uint id) => levels[id];
}
