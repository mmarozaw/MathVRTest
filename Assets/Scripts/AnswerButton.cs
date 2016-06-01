using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AnswerButton : MonoBehaviour, IGvrGazeResponder {

	private Image image;

	private float timer;

	[SerializeField]
	private Text text;

	private bool pressed = false;

	//---------------------------------------------------
	void Awake(){
		image = GetComponent<Image> ();
	}

	//---------------------------------------------------
	public void Init(string text){
		Reset ();
		this.text.text = text;
		pressed = false;
	}
	//---------------------------------------------------
	public void ColorAsCorrect(){
		image.color = Color.green;
	}
	//---------------------------------------------------
	public void ColorAsWrong(){
		image.color = Color.red;
	}

	//---------------------------------------------------
	IEnumerator ConfirmationTimer(){
		timer = Time.time;
		while (Time.time - timer < GameConsts.TimeSubmitAnswer) {
			image.color = Color.blue + Color.white * (1f - (Time.time - timer) / GameConsts.TimeSubmitAnswer);
			yield return new WaitForSeconds (GameConsts.TimeSubmitAnswer/5f);
		}
		pressed = true;
		MathGameController.I.CheckAnswer (this);
	}

	//---------------------------------------------------
	private void Reset(){
		StopAllCoroutines ();
		image.color = Color.white;
	}

	//---------------------------------------------------
	public void OnGazeEnter() {
		StartCoroutine (ConfirmationTimer ());
	}

	//---------------------------------------------------
	public void OnGazeExit() {
		Reset ();
		if (pressed) {
			GameEvenetManager.TriggerNextExpression ();
		}
	}
		
	//---------------------------------------------------
	public void OnGazeTrigger() {

	}
}
