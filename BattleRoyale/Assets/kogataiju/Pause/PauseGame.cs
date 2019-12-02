using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour
{
    [SerializeField]
    private GameObject pausePrefab;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("q"))
        {
            //アクティブ、非アクティブの切り替え
            pausePrefab.SetActive(!pausePrefab.activeSelf);

            if (pausePrefab.activeSelf)
            {
                Time.timeScale = 0f;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            else
            {
                Time.timeScale = 1.0f;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
    }
}
