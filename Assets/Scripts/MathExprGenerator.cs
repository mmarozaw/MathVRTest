using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MathExprGenerator: wqLocalSingletonBehaviour<MathExprGenerator> {

	public string Expression {
		get;
		private set;
	}

	public int NumOfAnswers {
		get;
		private set;
	}

	private int[] answers = new int[6]; //answer[0] is a correct answer

	public int GetAnswer(int num){
		return answers [num];
	}


	//---------------------------------------------------
	private MathDifficulty mathDifficulty;
	private List<int> randomAnswers = new List<int> (20);
	public void GenerateExpression(DifficultyLevels diffLevel){

		mathDifficulty = GameConsts.GetMathDifficulty (diffLevel);

		int firstNum = Random.Range (mathDifficulty.MinValue, mathDifficulty.MaxValue);
		int secondNum = 1;

		char sign = mathDifficulty.Signs [Random.Range (0, mathDifficulty.Signs.Length)];

		switch (sign) {
		case '+':
			secondNum = Random.Range (mathDifficulty.MinValue, mathDifficulty.MaxValue);
			answers [0] = firstNum + secondNum;
			break;
		case '-':
			secondNum = Random.Range (mathDifficulty.MinValue, mathDifficulty.MaxValue);
			if (firstNum < secondNum) {
				firstNum += secondNum;
				secondNum = firstNum - secondNum;
				firstNum -= secondNum;
			}
			answers [0] = firstNum - secondNum;
			break;
		case '*':
			secondNum = Random.Range (mathDifficulty.MinValue, mathDifficulty.MaxValue);
			answers [0] = firstNum * secondNum;
			break;
		case '/':
			secondNum = Random.Range (mathDifficulty.MinValue, mathDifficulty.MaxValue / mathDifficulty.MinValue);
			answers [0] = Random.Range (2, mathDifficulty.MaxValue / secondNum);
			firstNum = secondNum * answers [0];
			break;
		}

		//GENERATE WRONG ANSWERS
		randomAnswers.Clear ();
		int minRandomAnswer = 0 - Random.Range (0, GameConsts.AnswGenerRange < answers [0] ? GameConsts.AnswGenerRange : answers [0]);

		for (int i = 0; i < GameConsts.AnswGenerRange; i++) {
			if (minRandomAnswer + i != 0) {
				randomAnswers.Add (answers [0] + (minRandomAnswer + i));
			}
		}

		NumOfAnswers = mathDifficulty.NumOfAnswers;

		for (int i = 1; i < NumOfAnswers; i++) {
			int rnd = Random.Range (0, randomAnswers.Count);
			answers [i] = randomAnswers [rnd];
			randomAnswers.RemoveAt (rnd);
		}

		Expression = string.Format ("{0} {1} {2}", firstNum, sign, secondNum);
			
	}
}
