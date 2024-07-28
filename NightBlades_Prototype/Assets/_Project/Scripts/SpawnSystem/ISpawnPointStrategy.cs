using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NBProtoype
{
    public interface ISpawnPointStrategy
    {
        Transform NextSpawnPoint();
    }
}
