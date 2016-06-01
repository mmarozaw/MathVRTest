using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static partial class GameConsts {

	public static readonly int AnswGenerRange = 20; // Range in wich incorrect answers will be generated from correct one

	private static readonly Dictionary<DifficultyLevels, MathDifficulty> mathDifficultyValues = new Dictionary<DifficultyLevels, MathDifficulty> {
		{DifficultyLevels.Easy1,			new MathDifficulty(1, 9, "+-")},
		{DifficultyLevels.Easy2,			new MathDifficulty(2, 20, "+-*")},
		{DifficultyLevels.Normal1,			new MathDifficulty(3, 500, "+-*", 5)},
		{DifficultyLevels.Normal2,			new MathDifficulty(3, 500, "+-*/", 5)},
		{DifficultyLevels.Hard1,			new MathDifficulty(11, 999, "-**///", 6)},
		{DifficultyLevels.Hard2,			new MathDifficulty(11, 999, "-***////", 6)}
	};

	public static MathDifficulty GetMathDifficulty(DifficultyLevels diffLevel){
		if (mathDifficultyValues.ContainsKey(diffLevel)) {
			return mathDifficultyValues[diffLevel];
		}

		return null;
	}

}

public class MathDifficulty{
	public int MinValue {
		get;
		private set;
	}

	public int MaxValue {
		get;
		private set;
	}

	public string Signs {
		get;
		private set;
	}

	public int NumOfAnswers{
		get;
		private set;
	}

	public MathDifficulty(int minValue, int maxValue, string signs, int numOfAnswers = 4){
		this.MinValue = minValue;
		this.MaxValue = maxValue;
		this.Signs = signs;

		if (numOfAnswers < 4) {
			Debug.LogWarning ("NumOfAnswers less than 4");
			this.NumOfAnswers = 4;
		}else if(numOfAnswers > 6){
			Debug.LogWarning ("NumOfAnswers greater than 6");
			this.NumOfAnswers = 6;
		}else{
			this.NumOfAnswers = numOfAnswers;
		}
	}
}

public enum DifficultyLevels{
	Easy1 = 0,
	Easy2,
	Normal1,
	Normal2,
	Hard1,
	Hard2,
	LastValue
}