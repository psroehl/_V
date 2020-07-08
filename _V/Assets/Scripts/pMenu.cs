using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class pMenu : MonoBehaviour {

    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;

    public GameObject LosePanel;

    public FPSController fpsController;

    // Update is called once per frame
    void Update() {


        //if (PlayerUI.instance.isDead)

        //{
        //    Time.timeScale = 0f;
        //    GameIsPaused = true;
        //    Cursor.lockState = CursorLockMode.None;
        //    Cursor.visible = true;
        //    fpsController.mouseShow = true;
        //    fpsController.enabled = false;

        //    pauseMenuUI.SetActive(false);
        //    LosePanel.SetActive(true);

        //    this.enabled = false;
        //    return;
        //}

        if (GameIsPaused == false){
            pauseMenuUI.SetActive(false);
        }
       
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Cursor.lockState = CursorLockMode.None;
                Resume();
                Cursor.visible = false;
            } 
            else
            {
                Pause();
            }
      
        }
    }

    private bool isPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        fpsController.enabled = true;
        fpsController.mouseShow = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        fpsController.mouseShow = true;
        fpsController.enabled = false;
        Debug.Log("pause button is pushed!");
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        GameIsPaused = false;

        SceneManager.LoadScene("StartMenuA");
    }

    public void QuitGame()
    {
        Application.Quit();
        SceneManager.LoadScene("StartMenuA");
    }
}