using UnityEngine;
using System.Collections;

public class Player {
	
	private static Player instance = new Player();
	public static Player I { get { return instance; } }
	
	private Player() {
		NumOfCorrect = 0;
		playedExpressions = 0;
		Difficulty = DifficultyLevels.Easy1;
		GameEvenetManager.CorrectAnswer += AddCorrect;
	}
			
	public int NumOfCorrect{
		get;
		private set;
	}
	private void AddCorrect(){
		NumOfCorrect++;
	}
		
	public int NumOfWrong {
		get{ return playedExpressions - NumOfCorrect; }
	}


	private int playedExpressions;
	public int PlayedExpressions {
		get{ return playedExpressions; }
		set{
			playedExpressions = value;
			if (playedExpressions % GameConsts.ExprRiseDifficulty == 0) {
				RiseDifficulty ();
			}
		}
	}

	public DifficultyLevels Difficulty {
		get;
		private set;
	}

	private void RiseDifficulty(){
		if ((int)Difficulty < (int)DifficultyLevels.LastValue - 1){
			Difficulty = (DifficultyLevels)((int)Difficulty + 1);
		}
	}
}
