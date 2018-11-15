using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HudManager : MonoBehaviour
{
    [SerializeField]
    private float HoldStartToGoBackTime;
    private float holdingStartTimer;
    public static bool IsGamePaused { get {return isGamePaused;} private set { isGamePaused = value; } }

    private const double V = 0.1;
    private static Slider[] scoreSliders;
    private static bool HUD_MANAGER_DEBUG = false;
    private static bool isGamePaused;
    private static GameObject pauseTextGO;

    // Use this for initialization
    void Start()
    {
        if(HUD_MANAGER_DEBUG)
        {
            GameObject sliderSanityCheck = GameObject.Find("PlayersBlackCountSoul");
            Debug.Log("Finding object : "+ sliderSanityCheck.name);
            Debug.Log("Finding its Slider : " + sliderSanityCheck.GetComponent<UnityEngine.UI.Slider>());
        }

        scoreSliders = new Slider[3];
        scoreSliders[0] = GameObject.Find("PlayersBlackCountSoul").GetComponent<Slider>();
        scoreSliders[0].value = 0;
        scoreSliders[1] = GameObject.Find("PlayersRedCountSoul").GetComponent<Slider>();
        scoreSliders[1].value = 0;
        scoreSliders[2] = GameObject.Find("PlayersYellowCountSoul").GetComponent<Slider>();
        scoreSliders[2].value = 0;

        pauseTextGO = GameObject.Find("PauseText");
        pauseTextGO.SetActive(false);

        IsGamePaused = false;
        holdingStartTimer = 0.0f;
    }

    public static void IncrementScoreSliderValue(int player_id, float value_)
    {
        if (scoreSliders[player_id].value + value_ > 1)
            scoreSliders[player_id].value = 1.0f;
        else if (scoreSliders[player_id].value + value_ < 0)
            scoreSliders[player_id].value = 0.0f;
        else
            scoreSliders[player_id].value += value_; 
    }

    private void Update()
    {
        if (!IsGamePaused && InputsManager.AnyPauseButtonDown() )
        {
            Time.timeScale = 0.0f;
            pauseTextGO.SetActive(true);
            IsGamePaused = true;
        }

        if (IsGamePaused)
        {
            if (InputsManager.AnyPauseButtonDown())
            {
                holdingStartTimer += Time.unscaledDeltaTime;
                if (holdingStartTimer >= HoldStartToGoBackTime)
                {
                    Time.timeScale = 1.0f;
                    SceneManager.LoadScene("Menus");
                }
            }
            else
            {
                holdingStartTimer = 0.0f;
                Time.timeScale = 1.0f;
                pauseTextGO.SetActive(false);
                IsGamePaused = false;
            }
        }
    }
}

