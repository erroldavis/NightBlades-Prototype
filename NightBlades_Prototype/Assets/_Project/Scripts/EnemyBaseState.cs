using UnityEngine;

namespace NBProtoype
{
    public abstract class EnemyBaseState : IState
    {
        protected readonly Enemy enemy;
        protected readonly Animator animator;

        protected static readonly int IdleHash = Animator.StringToHash("Cactus_IdleNormal");
        protected static readonly int RunHash = Animator.StringToHash("Cactus_RunFWD");
        protected static readonly int WalkHash = Animator.StringToHash("Cactus_WalkFWD");
        protected static readonly int AttackHash = Animator.StringToHash("Cactus_Attack01");
        protected static readonly int DieHash = Animator.StringToHash("Cactus_Die");

        protected const float crossFadeDuration = 0.1f;
        protected EnemyBaseState(Enemy enemy, Animator animator)
        {
            this.enemy = enemy;
            this.animator = animator;
        }

        public virtual void FixedUpdate()
        {
            //noop
        }

        public virtual void OnEnter()
        {
            //noop
        }

        public virtual void OnExit()
        {
            //noop
        }

        public virtual void Update()
        {
            //noop
        }
    }
}
