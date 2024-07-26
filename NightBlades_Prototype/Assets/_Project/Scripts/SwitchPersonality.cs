using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
using Utilities;

namespace NBProtoype
{
    public class SwitchPersonality : MonoBehaviour
    {
        // referenses to controlled game objects
        [Header("Personality Switch Settings")]
        public GameObject[] personality; 
        private int currentPersonalityIndex = 0;
        [SerializeField] float switchDuration = 0.5f;
        [SerializeField] float switchCooldown = 0f;

        // variable contains which avatar is on active
        //int whichAvatarIsOn = 1;


       
        // Use this for initialization
        void Update()
        {
            
            // enable first personality and disable another personality
            //personality1.gameObject.SetActive(true);
            //personality2.gameObject.SetActive(false);
            //personality3.gameObject.SetActive(false);

            if (Input.GetKeyDown(KeyCode.Tab))
            {
                SwitchCharacter();
            }
        }

       
        // Switch personality 
        public void SwitchCharacter ()
        {
            personality[currentPersonalityIndex].SetActive(false);
            currentPersonalityIndex = (currentPersonalityIndex + 1) % personality.Length;
            personality[currentPersonalityIndex].SetActive(true);
        }
    }
}
