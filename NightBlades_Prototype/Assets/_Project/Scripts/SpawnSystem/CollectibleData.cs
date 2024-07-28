using UnityEngine;

namespace NBProtoype
{
    [CreateAssetMenu(fileName = "CollectibleData", menuName = "NBPrototype/Collectible Data")]
    public class CollectibleData : EntityData
    {
        public int score;
        // additional properties specific to collectibles
    }
}

