using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MathGameController : wqLocalSingletonBehaviour<MathGameController> {

	[SerializeField]
	private Text expressionText, statText;

	[SerializeField]
	private AnswerButton[] answerBtns;

	[SerializeField]
	private Transform[] answer4Transforms;

	[SerializeField]
	private Transform[] answer5Transforms;

	[SerializeField]
	private Transform[] answer6Transforms;

	private Transform[] currentAnswerTransforms;

	//---------------------------------------------------
	void Start () {
		UpdateLabels ();
		NextExpression ();
	}
	//---------------------------------------------------
	void OnEnable(){
		GameEvenetManager.NextExpression += NextExpression;
	}

	void OnDisable(){
		GameEvenetManager.NextExpression -= NextExpression;
	}
	//---------------------------------------------------

	private void NextExpression(){
		Player.I.PlayedExpressions++;
		MathExprGenerator.I.GenerateExpression (Player.I.Difficulty);

		expressionText.text = MathExprGenerator.I.Expression;

		switch (MathExprGenerator.I.NumOfAnswers) {
		case 4:
			currentAnswerTransforms = answer4Transforms;
			break;
		case 5:
			currentAnswerTransforms = answer5Transforms;
			break;
		case 6:
			currentAnswerTransforms = answer6Transforms;
			break;
		}

		for (int i = 0; i < currentAnswerTransforms.Length * 3; i++){
			int a = Random.Range (0, currentAnswerTransforms.Length);
			int b = Random.Range (0, currentAnswerTransforms.Length);
			Transform t = currentAnswerTransforms [a];
			currentAnswerTransforms [a] = currentAnswerTransforms [b];
			currentAnswerTransforms [b] = t;
		}

		for (int i = 0; i < answerBtns.Length; i++) {
			if (i < MathExprGenerator.I.NumOfAnswers) {
				answerBtns [i].Init (MathExprGenerator.I.GetAnswer (i).ToString ());
				answerBtns [i].transform.localPosition = currentAnswerTransforms [i].localPosition;
				answerBtns [i].gameObject.SetActive (true);
			} else {
				answerBtns [i].gameObject.SetActive (false);
			}
		}
			
	}
	//---------------------------------------------------
	public void CheckAnswer(AnswerButton btn){
		if (btn != answerBtns [0]) {
			btn.ColorAsWrong ();
		} else {
			GameEvenetManager.TriggerCorrectAnswer ();
		}
		answerBtns [0].ColorAsCorrect ();
		UpdateLabels ();
		expressionText.text = string.Format ("{0} = {1}", expressionText.text, MathExprGenerator.I.GetAnswer (0));
	}

	//---------------------------------------------------
	private void UpdateLabels(){
		statText.text = string.Format ("<color=#00ff00ff>{0}</color>:<color=#ff0000ff>{1}</color>", Player.I.NumOfCorrect, Player.I.NumOfWrong);
	}
}
