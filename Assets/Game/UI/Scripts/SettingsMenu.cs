using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

    public class SettingsMenu : MonoBehaviour
    {
        Animator animator;
        private bool fullscreen;
        public AudioMixer audioMixer;
        public Text fullScreenText;

        public void Start()
        {
            animator = GameObject.Find("UI_Manager").GetComponent<Animator>();
        }

        public void Resolution()
        {
            
        }

        public void Graphics()
        {
            //:P
        }

        public void SetFullscreen()
        {
            if (!fullscreen)
            {
                fullScreenText.text = "FullScreen";
                Screen.fullScreen = true;
                fullscreen = true;
            }
            else
            {
                fullScreenText.text = "Windowed Bordeless";
                Screen.fullScreen = false;
                fullscreen = false;
            }
        }

        public void SetBrightness(float brightness)
        {
            Screen.brightness = brightness;
        }

        public void SetVolume(float volume)
        {
        audioMixer.SetFloat("volume", volume);
        }

        public void Back()
        {
                animator.SetTrigger("Back");
        }
    }
