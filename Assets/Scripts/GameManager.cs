using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager manager;
    public int targetNumber;
    public int points;
    public TextMeshProUGUI pointsText;
    public GameObject table;

    private void Awake()
    {
        // singleton
        // check if manager already exist
        if (manager == null)
        {
            // if not, this is it and it won't be destroyed when changing the scene
            DontDestroyOnLoad(gameObject);
            manager = this;
        }
        else
        {
            Destroy(gameObject);
        }

    }

    void Start()
    {
        this.targetNumber = 1;
        pointsText.text = "Points: " + this.points.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        pointsText.text = "Points: " + this.points.ToString();
    }

    public void setTargetArea(int area)
    {
        this.targetNumber += area;
    }

    public void resetTargetArea()
    {
        this.targetNumber = 1;
    }

    public int getTargetArea()
    {
        return this.targetNumber;
    }

    public void setPoints(int points)
    {
        this.points += points;
    }

    public void resetPoints()
    {
        this.points = 0;
        
    }

    public int getPoints()
    {
        return this.points;
    }

    public void newTable()
    {
        Destroy(GameObject.FindWithTag("Table"));
        Instantiate(table, new Vector3(-31.04f, 0.304f, -36.94f),Quaternion.identity);
    }
}
