using UnityEngine;
using Utilities;

namespace NBProtoype
{
    public interface IDetectionStrategy 
    {
        bool Execute(Transform player, Transform detector, CountdownTimer timer);
    }
}

