using UnityEngine;

namespace NBProtoype
{
    public interface IEntityFactory<T> where T : Entity
    {
        T Create(Transform spawnPoint);
    }
}
