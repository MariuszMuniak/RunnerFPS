using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace FPS.SceneManagement
{
    public class Scene : MonoBehaviour
    {
        public void LoadScene(int sceneToLoad)
        {
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
