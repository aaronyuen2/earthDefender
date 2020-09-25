using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public GameObject gameUI;
    public GameObject gameOverUI;

    // GAME UI
    public Text timeElapsed;
    public Slider planetHealth;

    // Game Over UI
    public Text goDefendTime;
    public Text goHighscoreTime;

    // UI static variable
    public static UI ui;

    private void Awake()
    {
        ui = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        planetHealth.maxValue = Planet.p.health;
        planetHealth.value = Planet.p.health;
    }

    // Update is called once per frame
    void Update()
    {
        SetTimeElapsed();
    }

    void SetTimeElapsed()
    {
        timeElapsed.text = "TIME ELAPSED\n<size=55>" + GetTimeAsString(Game.g.gameTime) + "</size>";
    }

    string GetTimeAsString (float t)
    {
        string mins = Mathf.FloorToInt(t / 60).ToString();

        if (int.Parse(mins) < 10)
            mins = "0" + mins;

        string secs = ((int)(t % 60)).ToString();

        if (int.Parse(secs) < 10)
            secs = "0" + secs;

        return mins + ":" + secs;  // e.g. 02:35
    }

    // set the earth life value when the planet take damage
    public void SetPlanetHealthBarValue(int value)
    {
        planetHealth.value = value;
        StartCoroutine(PlanetHealthBarFlash());
    }

    IEnumerator PlanetHealthBarFlash()
    {
        Image fill = planetHealth.transform.Find("Fill Area/Fill").GetComponent<Image>();

        if (fill.color != Color.red)
        {
            Color dc = fill.color;
            fill.color = Color.red;

            yield return new WaitForSeconds(0.05f);

            fill.color = dc;
        }
    }
}
