using UnityEngine.AI;
using UnityEngine;

namespace NBProtoype
{
    public class EnemyChaseState: EnemyBaseState 
    {
        readonly NavMeshAgent agent;
        readonly Transform player;

        public EnemyChaseState(Enemy enemy, Animator animator, NavMeshAgent agent, Transform player) : base(enemy, animator)
        {
            this.agent = agent;
            this.player = player;
        }

        public override void OnEnter()
        {
            Debug.Log("Chase");
            animator.CrossFade(RunHash, crossFadeDuration);
        }

        public override void Update()
        {
            agent.SetDestination(player.position);
        }
    }
}

