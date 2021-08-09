using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameSystem : MonoBehaviour
{
    protected UICallbackScript UICallback;

    //Player
    public static float Health = 100;
    [SerializeField] private GameObject healthBar;
    
    public static int score = 0;
    [SerializeField] private GameObject scoreHolder;

    public int credits = 500;
    [SerializeField] private Text creditHolder;

    //Level
    public static int stage = 1;
    public static int wave = 1;
    public static bool next = false;

    //Backgrounds
    [SerializeField] GameObject Wave1;
    [SerializeField] GameObject Wave2;

    // Start is called before the first frame update
    void Awake()
    {
        DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Update()
    {
        if (EnemySpawner.count <= 0 && next == false)
        {
            switch (wave)
            {
                case 1:
                    Wave1.SetActive(true);
                    break;                    

                case 4:
                    Wave1.SetActive(false);
                    Wave2.SetActive(true);
                    break;
            }
            next = true;
            wave++;
        }

        UpdateUI();
    }

    void UpdateUI()
    {
        healthBar.GetComponent<Image>().fillAmount = Health / 100;
        scoreHolder.GetComponent<Text>().text = score.ToString();
        creditHolder.text = credits.ToString();
    }
}
