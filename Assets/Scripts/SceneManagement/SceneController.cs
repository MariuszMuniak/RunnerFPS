using FPS.Control;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Characters.FirstPerson;

namespace FPS.SceneManagement
{
    public class SceneController : MonoBehaviour
    {
        [SerializeField] GameObject hud;
        [SerializeField] GameObject gui;

        GameObject player;

        private void Awake()
        {
            player = GameObject.FindWithTag("Player");
        }

        private void Start()
        {
            PauseOnLevelBeginning();
        }

        public void PauseGame()
        {
            FreezeScene();
            ShowCursor();
            DisableControllersInPlayer();

            hud.SetActive(false);
            gui.SetActive(true);
        }

        public void UnPauseGame()
        {
            UnFreezeScene();
            HideCursor();
            EnableControllersInPlayer();

            hud.SetActive(true);
            gui.SetActive(false);
        }

        private void PauseOnLevelBeginning()
        {
            UnityEngine.SceneManagement.Scene scene = SceneManager.GetActiveScene();

            string[] sceneName = scene.name.Split('_');

            if (sceneName[0] == "Level")
            {
                FreezeScene();
                ShowCursor();
                DisableControllersInPlayer();

                hud.SetActive(false);
            }
        }

        private void DisableControllersInPlayer()
        {
            player.GetComponent<FirstPersonController>().enabled = false;
            player.GetComponent<PlayerController>().enabled = false;
        }

        private void EnableControllersInPlayer()
        {
            player.GetComponent<FirstPersonController>().enabled = true;
            player.GetComponent<PlayerController>().enabled = true;
        }

        private void ShowCursor()
        {
            player.GetComponent<FirstPersonController>().GetMouseLook().SetCursorLock(false);
        }

        private void HideCursor()
        {
            player.GetComponent<FirstPersonController>().GetMouseLook().SetCursorLock(true);
        }

        private void FreezeScene()
        {
            Time.timeScale = 0;
        }

        private void UnFreezeScene()
        {
            Time.timeScale = 1;
        }
    }
}
