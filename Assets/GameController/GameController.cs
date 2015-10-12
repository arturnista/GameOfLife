using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameController : MonoBehaviour {

	private float stepTime;
	private bool running;
	private float timeCounter;
	private Slider stepTimeSlider;
	private Text buttonStartStopText;
	Cell[] cellsInGame;

	void Start(){
		cellsInGame = GameObject.FindObjectsOfType<Cell> ();
		Clear ();
		stepTimeSlider = GameObject.Find ("StepTime Slider").GetComponent<Slider>();
		stepTimeSlider.value = stepTimeSlider.maxValue / 2;
		buttonStartStopText = GameObject.Find ("Start/Stop Button").GetComponentInChildren<Text> ();
		buttonStartStopText.text = "Run";
		running = false;
	}

	public void ChangeState(){
		running = !running;
		if (running) {
			buttonStartStopText.text = "Stop";
		} else {
			buttonStartStopText.text = "Run";
		}
	}

	public void ChangeStepTime(){
		stepTime = 10f / stepTimeSlider.value;
	}

	public void Clear(){
		foreach (Cell cell in cellsInGame) {
			cell.KillCell();
		}
	}

	public void Fill(){
		foreach (Cell cell in cellsInGame) {
			cell.ReviveCell();
		}
	}

	public void RandomPad(){
		foreach (Cell cell in cellsInGame) {
			if(Random.Range(0f, 10f) > 7){
				cell.ReviveCell();
			} else {
				cell.KillCell();
			}
		}
	}

	void Update () {
		if (!running) {
			return;
		}
		timeCounter += Time.deltaTime;
		if (timeCounter > stepTime) {
			timeCounter = 0;
			foreach (Cell cell in cellsInGame) {
				cell.PrepareStep();
			}
			foreach (Cell cell in cellsInGame) {
				cell.Step();
			}
		}
	}
}
