using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    // Start is called before the first frame update

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onStart()
    {
        SceneManager.LoadScene("Level 1");
        Time.timeScale = 1;
    }

   
    public void onExit()
    {
        Debug.Log("Exit Game");
        Application.Quit();
    }

    public void onCloseWindow(GameObject name)
    {
        name.SetActive(false);
    }

    public void onOpenWindow(GameObject name)
    {
        name.SetActive(true);
    }
}
