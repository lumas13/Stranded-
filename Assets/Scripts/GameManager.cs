using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public Transform[] spawnPoints;
    public GameObject enemyPrefab;
    public GameObject timerText;

    public int numberOfSpawn;
    public float spawnInterval;

    int timeCountInt;
    float timerCount = 120;
    bool isStartCount;

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }

        isStartCount = true;

        for (int i = 0; i < numberOfSpawn; i++)
        {
            int randomIndex = Random.Range(0, spawnPoints.Length);
            Vector3 randomPos = spawnPoints[randomIndex].position;

            StartCoroutine(WaitAndSpawn(spawnInterval));
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isStartCount == true)
        {
            timerCountDown();
        }

        GameOver();
    }

    private void timerCountDown()
    {
        if (timerCount > 0)
        {
            timerCount -= Time.deltaTime;
            timeCountInt = Mathf.RoundToInt(timerCount);
            timerText.GetComponent<Text>().text = "Timer: " + timeCountInt;
        }

        else if (timerCount < 1)
        {
            timerCount = 120.0f;
            isStartCount = false;
        }
    }

    void GameOver()
    {
        if (timerCount <= 0)
        {
            SceneManager.LoadScene("LoseScene");
        }
    }
    private IEnumerator WaitAndSpawn(float waitTime)
    {
        while (true)
        {
            yield return new WaitForSeconds(waitTime);
            int randomIndex = Random.Range(0, spawnPoints.Length);
            Vector3 randomPos = spawnPoints[randomIndex].position;
            Instantiate(enemyPrefab, randomPos, Quaternion.identity);
        }
    }
}
