using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NBProtoype
{
    public interface IState
    {
        void OnEnter();
        void Update();
        void FixedUpdate();
        void OnExit();
    }
}
