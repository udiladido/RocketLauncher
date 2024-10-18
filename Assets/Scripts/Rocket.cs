using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class Rocket : MonoBehaviour
{

    [SerializeField]
    private Rigidbody2D _rb2d;
    private float fuel = 100f;
    
    private readonly float SPEED = 5f;
    private readonly float FUELPERSHOOT = 10f;


    [SerializeField] private TextMeshProUGUI CurrentScoreTxt;
    [SerializeField] private TextMeshProUGUI HighScoreTxt;
    [SerializeField] private Button RestartButton;


    public int score = 0;
    public int highScore = 0;
  
    public float stopSpeed = 0.1f;

    public event EventHandler isDead;



    void Awake()
    {
      

        _rb2d = GetComponent<Rigidbody2D>();


        CurrentScoreTxt = GameObject.Find("NowScore").GetComponent<TextMeshProUGUI>();
        HighScoreTxt = GameObject.Find("HighScore").GetComponent<TextMeshProUGUI>();
        RestartButton = GameObject.Find("RestartButton").GetComponent<Button>();
        

        
        RestartButton.onClick.AddListener(Restart);


        if (PlayerPrefs.HasKey("HighScore")) 
        highScore = PlayerPrefs.GetInt("HighScore");

    }

    // 액션 이벤트로 isGameOver일때 Gameover 불러오도록 처리?


    public void Shoot()
    {

        if (fuel > 0)
        {
            _rb2d.AddForce(Vector2.up * SPEED, ForceMode2D.Impulse);
            fuel -= 10;

            Debug.Log(fuel);

            Debug.Log(_rb2d.velocity.magnitude);
        }
        else
        {
         

            //움직임이 멈추면 GameOver 불러오기 - 코루틴과 Invoke()?
            StartCoroutine(PlayerDead());


        }
          
    }

    IEnumerator PlayerDead()
    {

        while (_rb2d.velocity.magnitude > stopSpeed)
        {
  
            yield return null;

        }


        yield return new WaitForSeconds(1f);
        GameOver();

    }


    public void Update()
    {
        ScoreUpdate();

    }

    private void ScoreUpdate()
    {

        score = (int)transform.position.y;
        CurrentScoreTxt.text = $"{score} M";

        if (score > highScore)
        {
            highScore = score;
            HighScoreTxt.text = $"HIGH : {highScore} M";
        }
        else
            HighScoreTxt.text = $"HIGH : {highScore} M";

    }

    
    private void GameOver()
    {

        if (score >= highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("HighScore", highScore);
    
        }
          

    }

    private void Restart()
    {
        SceneManager.LoadScene("RocketLauncher");
      
    }



}
