using UnityEngine;
using System.Collections;

public static class Variables {
	public static readonly float EnemySpawnPerc = 0.3f;
	public static readonly float BossSpawnPerc = 0.1f;
	public static readonly float ItemSpawnPerc = 0.15f;
	public static readonly float StageOneSpawnPerc = 0.35f;
	public static readonly float StageTwoSpawnPerc = 0.45f;
	public static readonly float StageThreeSpawnPerc = 0.55f;
	public static readonly float[] StageSpawnPerc = new float[] {0.35f, 0.5f, 0.65f, 0.8f};
	
	public static readonly int BackgroundMaxCount = 8;
	public static readonly int LevelChunkMaxCount = 3;
	public static readonly int LiquidMaxCount = 3;
	public static readonly float BackgroundSeperation = 2.24f;
	public static readonly float TreeLineSeperation = 12f;
	public static readonly float LiquidSeperation = 4.0f;
	public static readonly string[] EnemySprites = new string[] { "Crab", "Star", "Worm" };
	public static readonly string[] WormClusters = new string[] { "WormCluster1", "WormCluster2", "WormCluster3", "WormCluster4" };
	public static readonly string[] WormSprites = new string[] { "Worm1", "Worm2", "Worm3", "Worm4" };
	public static readonly string[] PoopSprites = new string[] { "Poop1", "Poop2", "Poop3" };
	public static readonly string[] LevelChunks = new string[] { "spawn_chunk1" };
	public static readonly string[] GameMusic = new string[] { "loop1", "loop2", "loop3" };
	public static readonly string[] Farts = new string[] { "fart1", "fart2", "fart3", "fart4" };
	public static readonly string BossRoar = "bossroar" ;
	public static readonly Vector3 FirstMapVector = new Vector3 (0, 3.5f, 0);
	public static readonly float LevelChunkSeperation = 5.3f;
	public static readonly float ChunkRestartSeperation = 7.7f;
	public static readonly Quaternion DefaultQuaternion = new Quaternion (0, 0, 0, 0);

	public static readonly string[] BackgroundChunks = new string[] { "BgSprite1", "BgSprite2", "BgSprite3", "BgSprite4", "BgSprite5", "BgSprite6" };
	public static readonly string[] LiquidChunks = new string[] { "Liquid1", "Liquid2", "Liquid3", "Liquid4", "Liquid5" };
	public static readonly Vector3 BackgroundStartPosition = new Vector3(0, -4.2f, 10f);
	public static readonly Vector3 LiquidStartPosition = new Vector3(0, 8.5f, -1f);
	public static readonly Vector3 TreeLineStartPos = new Vector3(0, 0, 0);
	public static readonly float LiquidSpeed = -0.09f;

	public static readonly int MaxSize = 6;
	public static readonly int MinSize = 1;
	public static readonly int BossMinLength = 20;
	public static readonly int BossMaxLength = 60;
	public static readonly int StrafeSpeed = 10;
	public static readonly float RunSpeed = 0.03f;
	public static readonly float RunSpeedIncrement = 0.005f;
	public static readonly float MaxRunSpeed = 0.09f;
	public static readonly float BossRunSpeed = -0.015f;
	public static readonly float BossStrafeSpeed = 0.025f;
	public static readonly float BossDirectionChance = 0.01f;

	public static readonly Vector3 CrabSpeed = new Vector3 (0, 0.005f, 0);
	public static readonly float WormMinSpeed = -0.001f;
	public static readonly float WormMaxSpeed = -0.005f;
	public static readonly float StarMaxSpeed = 0.009f;
	public static readonly float SmallBorderX = 1.71f;
	public static readonly float SmallMediumBorderX = 1.7f;
	public static readonly float MediumBorderX = 1.65f;
	public static readonly float MediumLargeBorderX = 1.55f;
	public static readonly float LargeBorderX = 1.46f;
	public static readonly float MaxBorderX = 1.38f;
	public static readonly float StarMaxRadius = 0.3f;
	public static readonly float BossBorderX = 1.64f;
	
	public static readonly int MowerMaxHealth = 5;
	public static readonly int MapRestartPoint = 1000;
	
	public static readonly Rect HealthBarBackground = new Rect (Screen.width - 130f, Screen.height - 35f, 106f, 20f);
	public static readonly float HealthBarLeft = Screen.width - 127f;
	public static readonly float HealthBarTop = Screen.height - 35f;
	public static readonly float HealthBarMaxWidth = 100f;
	public static readonly float HealthBarHeight = 20f;
	
	public static readonly string GameTitle = "Title";
	public static readonly string ReplayButton = "Replay";
	public static readonly string HighscoreLabel = "HighscoreLabel";
	public static readonly string HighscoreInGame = "HighScoreInGame";
	public static readonly string Highscore = "HighScore";
	public static readonly string BackgroundDimmer = "BackgroundDimmer";
	public static readonly string PlayerPrefabName = "Player";
	public static readonly string BossHeadName = "BossHead";
	public static readonly string BossBodyName = "BossBody";
	public static readonly string BossTailName = "BossTail";
	public static readonly string BackgroundControllerName = "BackgroundController";
	public static readonly string ChunkFactoryName = "LevelChunkFactory";
	public static readonly string TitleAnimationName = "TitleAnimation";

    public static readonly Vector3 TitlePos = new Vector3 (-10, 2.3f, 0);
	public static readonly Vector3 PlayerPos = new Vector3 (0, -2.2f, 0);
	public static readonly Vector3 DefaultPos = new Vector3 (0, 0f, 0);
	public static readonly Vector3 highscoreLabelPos = new Vector3 (-11.14f, -2.3f, 0);
	public static readonly Vector3 highscorePos = new Vector3 (-10.05f, -1.97f, -1);
	/*public static readonly Rect buttonInset = new Rect (0, 0, 0, 0);
	public static readonly Vector3 leftButtonPos = new Vector3 (0.3f, 0.5f, 0);
	public static readonly Vector3 rightButtonPos = new Vector3 (0.7f, 0.5f, 0);
	public static readonly Vector3 startPromptPos = new Vector3 (0.52f, 0.5f, 0);
	public static readonly Vector3 buttonScale = new Vector3 (0.3f, 0.1f, 1);
	public static readonly Vector3 startPromptScale = new Vector3 (0.7f, 0.2f, 1);*/
	
	public static readonly int EnemyPoolSize = 130;
	public static readonly int WormPoolSize = 70;
	public static readonly int ItemPoolSize = 12;

	public static readonly float DefaultAlpha = 0f;

	public static readonly float HighScoreRatio = 180f / 723f;

    // Add tag names

	public enum GameState {
		LoadGame,
		TitleScreen,
		GameStart,
		GameEnd
	}
}
