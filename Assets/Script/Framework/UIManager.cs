using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class UIManager: MonoBehaviour
{
	static UIManager _instance;
	public static UIManager Instance
	{
		get
		{
			if(_instance == null)
			{
				_instance = FindObjectOfType<UIManager>();
				if(_instance == null)
				{
					_instance = new GameObject("UIManager Singleton").AddComponent<UIManager>().GetComponent<UIManager>();
				}
			}
			return _instance;
		}
	}

    public GameObject playCanvas;
    public GameObject endgameCanvas;

    Coroutine _scoreCorr;

    public Text txt;
    float _score;
    public float scoreByMinute;

    void Start()
    {
        _scoreCorr = StartCoroutine(ScoreCoroutine());
    }

    IEnumerator ScoreCoroutine()
    {
        var tick = scoreByMinute / 60;

        var ins = new WaitForSeconds(tick);

        while (true)
        {
            yield return ins;
            _score++;
            txt.text = "Score: " + _score.ToString();
        }
    }

    public void Endgame()
    {
        StopCoroutine(_scoreCorr);
        playCanvas.SetActive(false);
        GameObject.FindObjectOfType<HideInstructions>().inst.SetActive(false);
        endgameCanvas.SetActive(true);
        SetFinalScore();
    }

    void SetFinalScore()
    {
        var txt = endgameCanvas.GetComponentsInChildren<Text>().Where(x => x.gameObject.name == "SCORE").First();
        txt.text = txt.text + " " + _score.ToString();
    }
}
