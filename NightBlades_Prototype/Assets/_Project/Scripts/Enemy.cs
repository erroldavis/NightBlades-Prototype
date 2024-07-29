using UnityEngine.AI;
using UnityEngine;
using KBCore.Refs;
using Utilities;

namespace NBProtoype
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class Enemy : Entity
    {
        [SerializeField, Self] NavMeshAgent agent;
        //[SerializeField, Self] PlayerDetector playerDetector;
        [SerializeField, Child] Animator animator;


        StateMachine stateMachine;

        void OnValidate() => this.ValidateRefs();

        void Start()
        {
            //attackTimer = new CountdownTimer(timeBetweenAttacks);

            stateMachine = new StateMachine();
        }

        void At(IState from, IState to, IPredicate condition) => stateMachine.AddTransition(from, to, condition);
        void Any(IState to, IPredicate condition) => stateMachine.AddAnyTransition(to, condition);

        void Update()
        {
            stateMachine.Update();
            //attackTimer.Tick(Time.deltaTime);
        }

        void FixedUpdate()
        {
            stateMachine.FixedUpdate();
        }
    }
}
