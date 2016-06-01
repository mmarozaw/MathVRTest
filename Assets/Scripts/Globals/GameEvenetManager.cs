using System.Collections;

public class GameEvenetManager  {

	public delegate void GameEvent();
	public static event GameEvent CorrectAnswer, NextExpression;

	public static void TriggerCorrectAnswer(){
		if (CorrectAnswer != null){
			CorrectAnswer();
		}
	}

	public static void TriggerNextExpression(){
		if (NextExpression != null){
			NextExpression();
		}
	}

}
