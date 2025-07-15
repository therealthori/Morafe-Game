using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;



public class MasterInfo : MonoBehaviour
{
    /* ------------- Singleton (easy global access) ----------------- */
    public static MasterInfo Instance { get; private set; }

    /* ------------- UI References (drag in Inspector) -------------- */
    [Header("UI")]
    [SerializeField] TMP_Text coinText;       // “DIAMONDS: 0”
    [SerializeField] TMP_Text scoreText;      // “123”
    [SerializeField] TMP_Text bestText;       // “BEST 456”
    [SerializeField] TMP_Text multiplierText; // “x1”

    /* ------------- Scoring Setup ---------------------------------- */
    [Header("Scoring")]
    [SerializeField] Transform player;        // player Transform (for distance)
    [SerializeField] float     scorePerMeter = 1f;

    /* ------------- Runtime state ---------------------------------- */
    int   coins;
    int   multiplier = 1;
    float startZ;          // z‑position where run began
    float score;           // live (float) score

    /* ------------- Unity life‑cycle ------------------------------- */
    void Awake()
    {
        if (Instance != null) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);        // keep through scene loads
    }

    void Start()
    {
        startZ  = player.position.z;
        bestText.text = "BEST " + PlayerPrefs.GetInt("HighScore", 0) + "m";
        UpdateUI();
    }

    void Update()
    {
        // Distance‑based score
        float distance = player.position.z - startZ;
        score = distance * scorePerMeter * multiplier;

        UpdateUI();
    }

    void OnDestroy()        => SaveHighScore();
    void OnApplicationQuit() => SaveHighScore();

    /* ------------- Public API (called by CollectCoin, missions…) -- */
    public void AddCoin(int amount = 1)
    {
        coins += amount;

        // Example: every 100 coins increments multiplier
        if (coins % 100 == 0) IncreaseMultiplier();

        UpdateUI();
    }

    public void IncreaseMultiplier(int amount = 1)
    {
        multiplier += amount;
        UpdateUI();
    }

    /* ------------- Helpers --------------------------------------- */
    void UpdateUI()
    {
        coinText.text       = $"DIAMONDS: {coins}";
        multiplierText.text = $"x{multiplier}";

        int intScore = Mathf.FloorToInt(score);
        scoreText.text = intScore.ToString() + "m";

        int best = PlayerPrefs.GetInt("HighScore", 0);
        if (intScore > best) bestText.text = $"BEST {intScore}m";
    }

    void SaveHighScore()
    {
        int intScore = Mathf.FloorToInt(score);
        int best     = PlayerPrefs.GetInt("HighScore", 0);
        if (intScore > best)
        {
            PlayerPrefs.SetInt("HighScore", intScore);
            PlayerPrefs.Save();
        }
    }
}