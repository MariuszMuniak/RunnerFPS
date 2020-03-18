using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace FPS.Settings
{
    public class Settings : MonoBehaviour
    {
        static Settings settings = null;

        string filePath;

        float sensitivity = 12;
        float masterVolume = 1;
        float musicVolume = 1;

        public float Sensitivity
        {
            get { return sensitivity; }
            set { sensitivity = value; }
        }

        public float MasterVolume
        {
            get { return masterVolume; }
            set { masterVolume = value; }
        }

        public float MusicVolume
        {
            get { return musicVolume; }
            set { musicVolume = value; }
        }

        private void Awake()
        {
            if (settings == null)
            {
                settings = this;
                DontDestroyOnLoad(this);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        void Start()
        {
            filePath = Application.persistentDataPath;

            if (File.Exists(FullFilePath()))
            {
                LoadSettings();
            }
            else
            {
                InitialSettings();
            }

            SceneManager.sceneLoaded += OnSceneLoaded;
            SceneManager.sceneUnloaded += OnSceneUnLoaded;
        }

        private string FullFilePath()
        {
            return filePath + "\\GameSettings.txt";
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            LoadSettings();
        }

        private void OnSceneUnLoaded(Scene scene)
        {
            SaveSettings();
        }

        private void SaveSettings()
        {
            List<string> lines = new List<string>();

            lines.Add($"{SettingsIndex.Sensitivity}|{sensitivity}");
            lines.Add($"{SettingsIndex.MasterVolume}|{masterVolume}");
            lines.Add($"{SettingsIndex.MusicVolume}|{musicVolume}");


            File.WriteAllLines(FullFilePath(), lines);
        }

        private void LoadSettings()
        {
            List<string> lines = new List<string>();

            lines = File.ReadAllLines(FullFilePath()).ToList();

            foreach (string line in lines)
            {
                string[] setting = line.Split('|');

                if (setting[0] == SettingsIndex.Sensitivity.ToString())
                {
                    sensitivity = float.TryParse(setting[1], out float result) ? result : sensitivity;
                }
                else if (setting[1] == SettingsIndex.MasterVolume.ToString())
                {
                    masterVolume = float.TryParse(setting[1], out float result) ? result : masterVolume;
                }
                else if (setting[1] == SettingsIndex.MusicVolume.ToString())
                {
                    musicVolume = float.TryParse(setting[1], out float result) ? result : musicVolume;
                }
            }
        }

        private void InitialSettings()
        {
            FileStream file = File.Create(FullFilePath());
            file.Close();

            List<string> lines = new List<string>();

            lines.Add($"{SettingsIndex.Sensitivity}|{sensitivity}");
            lines.Add($"{SettingsIndex.MasterVolume}|{masterVolume}");
            lines.Add($"{SettingsIndex.MusicVolume}|{musicVolume}");


            File.WriteAllLines(FullFilePath(), lines);
        }
    }
}
