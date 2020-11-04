using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Canvas MenuCanvas;
    public Canvas InstructionCanvas;
    public Canvas CreditCanvas;

    public Text Premise;
    public Text Weapons;
    public Text Enemy;
    public Text Score;

    private bool onInstruct;
    private bool onPremise;
    private bool onWeapons;
    private bool onEnemies;

    public static int score = 0;
    public static int initScore = 0;

    public AudioSource backgroundMusic;

    static private GameManager G;

    // Start is called before the first frame update
    void Start()
    {
        G = this;

        CreditCanvas.enabled = false;
        MenuCanvas.enabled = true;
        InstructionCanvas.enabled = false;
        onInstruct = false;
        onPremise = true;
        onWeapons = false;
        onEnemies = false;
    }

    public void Back()
    {
        if(onInstruct)
        {
            Premise.enabled = true;
            Weapons.enabled = false;
            Enemy.enabled = false;
            onInstruct = false;
            onPremise = true;
            onWeapons = false;
            onEnemies = false;

            MenuCanvas.enabled = true;
            InstructionCanvas.enabled = false;
        }
        else
        {
            CreditCanvas.enabled = false;
            MenuCanvas.enabled = true;
        }
    }

    public void Quit()
    {
        Debug.Log("Quit");
        Application.Quit();
    }

    public void StartGame()
    {
        Debug.Log("StartIt");
        score = 0;
        initScore = score;
        SceneManager.LoadScene(1);
    }

    public void Credit()
    {
        CreditCanvas.enabled = true;
        MenuCanvas.enabled = false;
    }

    public void Instruct()
    {
        MenuCanvas.enabled = false;
        InstructionCanvas.enabled = true;
        onInstruct = true;

        Weapons.enabled = false;
        Enemy.enabled = false;
    }

    public void showPremise()
    {
        if(onWeapons)
        {
            Weapons.enabled = false;
            onWeapons = false;
            Premise.enabled = true;
            onPremise = true;
        }
        else if(onEnemies)
        {
            Enemy.enabled = false;
            onEnemies = false;
            Premise.enabled = true;
            onPremise = true;
        }
    }

    public void showWeapons()
    {
        if (onPremise)
        {
            Premise.enabled = false;
            onPremise = false;
            Weapons.enabled = true;
            onWeapons = true;
        }
        else if (onEnemies)
        {
            Enemy.enabled = false;
            onEnemies = false;
            Weapons.enabled = true;
            onWeapons = true;
        }
    }

    public void showEnemies()
    {
        if (onWeapons)
        {
            Weapons.enabled = false;
            onWeapons = false;
            Enemy.enabled = true;
            onEnemies = true;
        }
        else if (onPremise)
        {
            Premise.enabled = false;
            onPremise = false;
            Enemy.enabled = true;
            onEnemies = true;
        }
    }

    public static void NextLevel()
    {
        Debug.Log("Go To Next");
        initScore = score;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public static void points(int add)
    {
        score += add;
    }
    
    void Update()
    {
        if(SceneManager.GetActiveScene().buildIndex > 0)
        {
            G.Score.text = "Score: " + score;
        }
        else
        {
            Debug.Log("Point 0");
        }
        

        if (Input.GetKeyDown(KeyCode.R))
        {
            Reset(false);
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Quit();
        }
    }

    public static void Reset(bool damage)
    {
        if (damage)
        {
            initScore -= 50;
            if (initScore < 0) { initScore = 0; }
        }
        score = initScore;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
