using UnityEngine;
using System.Collections;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;
using GooglePlayGames.BasicApi.Multiplayer;

public class GameManager : MonoBehaviour , RealTimeMultiplayerListener
{
	PlayGamesPlatform gamesPlatform;

	void OnEnable ()
	{
		Debug.Log ("***PlayGamesClientConfiguration.Builder().Build() …");
		PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder ()
		                                      // enables saving game progress.
		                                      //.EnableSavedGames ()
		                                      // registers a callback to handle game invitations received while the game is not running.
		                                      //.WithInvitationDelegate(<callback method>)
		                                      // registers a callback for turn based match notifications received while the
		                                      // game is not running.
		                                      //.WithMatchDelegate(<callback method>)
				.Build ();

		Debug.Log ("***PlayGamesPlatform.InitializeInstance() …");
		PlayGamesPlatform.InitializeInstance (config);

		PlayGamesPlatform.DebugLogEnabled = true;
			
		Debug.Log ("***PlayGamesPlatform.Activate() …");
		PlayGamesPlatform.Activate ();

		gamesPlatform = PlayGamesPlatform.Instance;
	}

	public void Do_Authenticate ()
	{
		Debug.Log ("***Do_Authenticate() -> …");
		gamesPlatform.Authenticate ((bool success) => {
			Debug.Log ("***Do_Authenticate() -> " + (success ? "SUCCESS" : "FAIL"));
		}, false);
	}

	public void Do_SignOut ()
	{
		Debug.Log ("***Do_SignOut()");
		gamesPlatform.SignOut ();
	}

	public void Do_CreateQuickGame ()
	{
		Debug.Log ("***Do_CreateQuickGame()");
		gamesPlatform.RealTime.CreateQuickGame (minOpponents: 1, maxOpponents : 1, variant : 0, listener: this);
	}

	void Update ()
	{
	
	}

	#region RealTimeMultiplayerListener implementation

	public void OnRoomSetupProgress (float percent)
	{
		Debug.Log ("***OnRoomSetupProgress (" + percent + ")");
	}

	public void OnRoomConnected (bool success)
	{
		Debug.Log ("***OnRoomConnected (" + success + ")");
	}

	public void OnLeftRoom ()
	{
		Debug.Log ("***OnLeftRoom ()");
	}

	public void OnParticipantLeft (Participant participant)
	{
		Debug.Log ("***OnParticipantLeft(" + participant + ")");
	}

	public void OnPeersConnected (string[] participantIds)
	{
		Debug.Log ("***OnPeersConnected(" + string.Join (",", participantIds) + ")");
	}

	public void OnPeersDisconnected (string[] participantIds)
	{
		Debug.Log ("***OnPeersDisconnected(" + string.Join (",", participantIds) + ")");
	}

	public void OnRealTimeMessageReceived (bool isReliable, string senderId, byte[] data)
	{
		Debug.Log ("***OnRealTimeMessageReceived(" + isReliable + "," + senderId + ",'" + (char)data [0] + "':" + data.Length + "bytes)");
	}

	#endregion
}
