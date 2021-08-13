using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Permanent : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Update()
    {
        if(SceneManager.GetActiveScene().name == "Title Scene")
        {
            Destroy(this.gameObject);
        }
    }
    public void OnLevelSelectTrigger(GameObject name)
    {
        Destroy(name.gameObject);
    }
}
