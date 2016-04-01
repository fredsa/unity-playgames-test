using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class UiTextLogController : MonoBehaviour
{
	static int MAX_ENTRIES = 20;

	LinkedList<string> buf = new LinkedList<string> ();
	Text text;

	void Awake ()
	{
		Application.stackTraceLogType = StackTraceLogType.ScriptOnly;
		text = GetComponent<Text> ();
	}

	void OnEnable ()
	{
		Application.logMessageReceivedThreaded += HandleLog;
	}

	void OnDisable ()
	{
		Application.logMessageReceivedThreaded -= HandleLog;
	}

	public void ClearLog ()
	{
		buf.Clear ();
		text.text = "";
	}

	void HandleLog (string msg, string stackTrace, LogType type)
	{
		string t = type == LogType.Log ? "" : type + " ";
		t += msg; 
		//t += stackTrace;
		ThreadSafeAppend (t);
	}

	void ThreadSafeAppend (string msg)
	{
		buf.AddLast (msg);
		if (buf.Count > MAX_ENTRIES) {
			buf.RemoveFirst ();
		}
		string t = "";
		foreach (string m in buf) {
			t += m + "\n";
		}
		text.text = t;
	}

}