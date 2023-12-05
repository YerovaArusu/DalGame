using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonHandler : MonoBehaviour
{
        [SerializeField] public Scene gameScene;
        public Button button;

        private void Start()
        {
                button = GetComponent<Button>();
                button.onClick.AddListener(onStartButtonPress);
        }

        void onStartButtonPress()
        {
                SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
        }
}
