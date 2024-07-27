using Cinemachine;
using KBCore.Refs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
using Utilities;

namespace NBProtoype
{
    public class SwitchPersonality : MonoBehaviour
    {
        // referenses to controlled game object
        [SerializeField, Anywhere] CinemachineFreeLook freeLookVCam;
        [SerializeField, Anywhere] InputReader input;

        [Header("Personality Switch Settings")]
        public GameObject persona1, persona2, persona3;

        // variable contains which avatar is on active
        public int whichPersonaIsOn = 1;



        // Use this for initialization
        void Start()
        {
            //enable first personality and disable another personality
            persona1.gameObject.SetActive(true);
            persona2.gameObject.SetActive(false);
            persona3.gameObject.SetActive(false);
        }
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                SwitchCharacter();
            }
        }


        // Switch personality 
        public void SwitchCharacter()
        {
            //personality[currentPersonalityIndex].SetActive(false);
            //currentPersonalityIndex = (currentPersonalityIndex + 1) % personality.Length;
            //personality[currentPersonalityIndex].SetActive(true);

            // processing whichAvatarIsOn variable
            switch (whichPersonaIsOn)
            {

                // if the first avatar is on
                case 1:

                    // then the second avatar is on now
                    whichPersonaIsOn = 2;

                    // disable the first one and anable the second one
                    persona1.gameObject.SetActive(false);
                    persona2.gameObject.SetActive(true);
                    persona3.gameObject.SetActive(false);
                    break;

                // if the second avatar is on
                case 2:

                    // then the first avatar is on now
                    whichPersonaIsOn = 3;

                    // disable the second one and anable the first one
                    persona1.gameObject.SetActive(false);
                    persona2.gameObject.SetActive(false);
                    persona3.gameObject.SetActive(true);
                    break;


                case 3:

                    // then the first avatar is on now
                    whichPersonaIsOn = 1;

                    // disable the second one and anable the first one
                    persona1.gameObject.SetActive(true);
                    persona2.gameObject.SetActive(false);
                    persona3.gameObject.SetActive(false);
                    break;

            }

        }
    }
}
