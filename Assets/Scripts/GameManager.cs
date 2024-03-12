using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public TMP_Text scoreText;
    public TMP_Text strikesText;

    private Sword sword;
    private Spawner spawner;

    private int score;
    private int strikes;

    private void Awake()
    {
        sword = FindObjectOfType<Sword>();
        spawner = FindObjectOfType<Spawner>();
    }

    private void Start()
    {
        NewGame();
    }

    private void NewGame()
    {
        score = 0;
        scoreText.text = score.ToString();
        strikes = 0;
        strikesText.text = "";

        sword.enabled = true;
        spawner.enabled = true;

        ClearScene();
    }

    private void ClearScene()
    {
        Vege[] veges = FindObjectsOfType<Vege>();
        foreach (Vege vege in veges) {
            Destroy(vege.gameObject);
        }

        Fruit[] fruits = FindObjectsOfType<Fruit>();
        foreach (Fruit fruit in fruits) {
            Destroy(fruit.gameObject);
        }
    }

    public void IncreaseScore()
    {
        score++;
        scoreText.text = score.ToString();
    }

    public void IncreaseStrikes()
    {
        strikes++;
        if (strikes == 1){
            strikesText.text = "X";
        } else if (strikes == 2) {
            strikesText.text = "XX";
        } else if (strikes >= 3) {
            strikesText.text = "XXX";
            EndGame();
        }
    }

    private void EndGame()
    {
        sword.enabled = false;
        spawner.enabled = false;

        StartCoroutine(PlayAgain());
    }

    private IEnumerator PlayAgain()
    {
        yield return new WaitForSeconds(2);
        NewGame();
    }

}
