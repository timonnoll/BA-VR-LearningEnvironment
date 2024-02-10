using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TN
{
    /// <summary>
    /// Handle button interaction in the start menu.
    /// </summary>
    public class GameStartMenu : MonoBehaviour
    {
        [Header("UI Pages")]
        public GameObject mainMenu;
        public GameObject about;

        [Header("Main Menu Buttons")]
        public Button startButton;
        public Button aboutButton;
        public Button quitButton;

        public Button returnButton;

        // Start with the main menu and add listener to main menu buttons.
        private void Start()
        {
            EnableMainMenu();

            startButton.onClick.AddListener(StartGame);
            aboutButton.onClick.AddListener(EnableAbout);
            quitButton.onClick.AddListener(QuitGame);
            returnButton.onClick.AddListener(EnableMainMenu);
        }

        // Close the application.
        public void QuitGame()
        {
            Application.Quit();
        }

        // Switch to game scene with fade transition.
        public void StartGame()
        {
            HideAll();
            SceneTransitionManager.instance.GoToScene(1);
        }

        // Hide start menu UI.
        public void HideAll()
        {
            mainMenu.SetActive(false);
            about.SetActive(false);
        }

        // Show main menu UI and hide about UI.
        public void EnableMainMenu()
        {
            mainMenu.SetActive(true);
            about.SetActive(false);
        }

        // Hide main menu UI and show about UI.
        public void EnableAbout()
        {
            mainMenu.SetActive(false);
            about.SetActive(true);
        }
    }

}

