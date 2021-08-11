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

    public int credits = 500;
    [SerializeField] private Text Credit_Holder;

    //Level
    public static int stage = 1;
    public static int wave = 1;
    public static bool boss_level = false;
    public static bool next = false;
    bool next_level = true;
    public static int enemy_increment = 1;

    //Backgrounds
    [SerializeField] GameObject Wave_1_BG;
    [SerializeField] GameObject Wave_2_BG;
    [SerializeField] GameObject Wave_3_BG;

    //Gestures
    private Touch trackedFinger1;
    float gestureTime = 0.0f;
    Vector2 startPoint;
    Vector2 endPoint;

    float minSwipeDistance = 2f;
    float swipeTime = 0.7f;

    // Start is called before the first frame update
    void Awake()
    {
        DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Update()
    {
        if (EnemySpawner.count <= 0 && next == false && next_level)
        {
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

                case 4: GameWin(); break;
            }
            next = true;
            wave++;
            //enemy_increment += 0;         
        }

        next_level = Swiped();
        UpdateUI();
        //GameOver();
        //GameWin();
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
            Debug.Log("Game Over");
            Time.timeScale = 0;
        }
    }

    void GameWin()
    {
        if (wave == 5)
        {
            Debug.Log("Game Win");
            Time.timeScale = 0;
        }
    }

    bool Swiped()
    {
        bool swiped = false;
        if(Input.touchCount > 0)
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
}
