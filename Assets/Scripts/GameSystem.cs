using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameSystem : MonoBehaviour
{
    //Player
    public static float health = 100;
    [SerializeField] private GameObject Health_Bar;
    [SerializeField] private Text Health_Holder;

    public static int score = 0;
    [SerializeField] private GameObject Score_Holder;

    public static int credits = 500;
    [SerializeField] private Text Credit_Holder;

    //Level
    public static int stage = 1;
    public static int wave = 1;
    public static bool boss_level = true;
    public static bool next = false;
    bool going_next_level = false;
    bool swiped = false;
    public static int enemy_increment = 0;
    int wave_increment = 0;
    int game_result = 0; //1 - win, 2 - lose

    //Backgrounds
    [SerializeField] GameObject Wave_1_BG;
    [SerializeField] GameObject Wave_2_BG;
    [SerializeField] GameObject Wave_3_BG;

    [SerializeField] public GameObject Instructions;
    [SerializeField] GameObject GameResult;

    //Gestures
    private Touch trackedFinger1;
    float gestureTime = 0.0f;
    Vector2 startPoint;
    Vector2 endPoint;

    float minSwipeDistance = 2f;
    float swipeTime = 0.7f;

    float tapTime = 0.7f;
    float tapDistance = 0.1f;

    public static bool activated = true;

    // Start is called before the first frame update
    void Awake()
    {
        SoundManagerScript.PlaySound("BGM_Default");

        health = 100;
        credits = 500;
        score = 0;

        stage = 1;
        wave = 1;
        boss_level = false;
        next = false;
        going_next_level = false;
        swiped = false;
        enemy_increment = 0;
        wave_increment = 0;
        game_result = 0;
}

    // Update is called once per frame
    void Update()
    {
        UpdateUI();

        if (health <= 0)
        {
            game_result = 2;
        }

        if (game_result == 0)
        {
            if (EnemySpawner.count <= 0 && next == false)
            {
                if (wave >= 4)
                {
                    game_result = 1;
                }

                if (!going_next_level)
                {
                    next = true;                    

                    switch (wave)
                    {
                        case 1:
                            Wave_1_BG.SetActive(true);
                            break;

                        case 2:
                            Wave_1_BG.SetActive(false);
                            Wave_2_BG.SetActive(true);
                            break;

                        case 3:
                            Wave_1_BG.SetActive(false);
                            Wave_2_BG.SetActive(false);
                            Wave_3_BG.SetActive(true);
                            boss_level = true;
                            break;
                    }

                    //Total of 3 waves each (> 1);
                    if (wave_increment > -1)
                    {
                        wave++;
                        wave_increment = 0;
                        going_next_level = true;
                    }

                    else
                    {
                        wave_increment++;
                    }
                }

                else
                {
                    Debug.Log("Activate Guide");
                    Instructions.SetActive(true);
                    swiped = Swiped();
                    if (swiped)
                    {
                        going_next_level = false;
                        enemy_increment += 2;
                    }
                }
            }            
        }

        else if(game_result == 1)
        {
            GameWin();
        }

        else { GameOver(); }

        if ((game_result == 1 || game_result == 2) && Tapped())
        {
            SceneManager.LoadScene("Title Scene");
        }
    }

    void UpdateUI()
    {
        Health_Bar.GetComponent<Image>().fillAmount = health / 100;
        Health_Holder.GetComponent<Text>().text = health.ToString() + " / " + 100;
        Score_Holder.GetComponent<Text>().text = score.ToString();
        Credit_Holder.text = credits.ToString();
    }

    void GameOver()
    {
        if(health <= 0)
        {
            Time.timeScale = 0;
            Debug.Log("Game Over");
            GameResult.gameObject.SetActive(true);
            GameResult.GetComponentInChildren<Text>().text = "GAME OVER";
            Instructions.gameObject.SetActive(true);
            Instructions.GetComponentInChildren<Text>().text = "TAP TO CONTINUE";
        }
    }

    void GameWin()
    {
        Debug.Log("Game Win");
        Time.timeScale = 0;
        GameResult.gameObject.SetActive(true);
        GameResult.GetComponentInChildren<Text>().text = "YOU SURVIVED";
        Instructions.gameObject.SetActive(true);
        Instructions.GetComponentInChildren<Text>().text = "TAP TO CONTINUE";
    }

    bool Swiped()
    {
        bool swiped = false;
        if (Input.touchCount > 0)
        {
            trackedFinger1 = Input.GetTouch(0);

            if(trackedFinger1.phase == TouchPhase.Began)
            {
                gestureTime = 0;
                startPoint = trackedFinger1.position;
            }
            else if(trackedFinger1.phase == TouchPhase.Ended)
            {
                endPoint = trackedFinger1.position;
                if(gestureTime <= swipeTime && Vector2.Distance(startPoint, endPoint) >= Screen.dpi * +minSwipeDistance)
                {
                    Debug.Log("Swipe!");
                    Instructions.SetActive(false);
                    swiped = true;
                }
            }
            else
            {
                gestureTime += Time.deltaTime;
            }
        }
        return swiped;
    }

    bool Tapped()
    {
        bool tapped = false;
        if (Input.touchCount > 0)
        {
            trackedFinger1 = Input.GetTouch(0);

            if (trackedFinger1.phase == TouchPhase.Began)
            {
                gestureTime = 0;
                startPoint = trackedFinger1.position;
            }
            else if (trackedFinger1.phase == TouchPhase.Ended)
            {
                endPoint = trackedFinger1.position;
                if (gestureTime <= tapTime && Vector2.Distance(startPoint, endPoint) < Screen.dpi * tapDistance)
                {
                    Debug.Log("Tap!");
                    Instructions.SetActive(false);
                    tapped = true;
                }
            }
            else
            {
                gestureTime += Time.deltaTime;
            }
        }
        return tapped;
    }
}
