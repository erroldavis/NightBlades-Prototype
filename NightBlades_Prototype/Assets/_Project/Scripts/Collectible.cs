using UnityEngine;

namespace NBProtoype
{
    public class Collectible : Entity
    {
        [SerializeField] int score = 10; // FIXME set using Factory

        void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                //scoreChannel.Invoke(score);
                Destroy(gameObject);
            }
        }
    }
}
