using UnityEngine;

namespace FeelGoodOpgUtils.Debug
{
	// Add this to any game object in a scene in order to bring up the console
	public class ConsoleToGUI : MonoBehaviour
	{
		//#if !UNITY_EDITOR
		static string MyLog = "";

		void OnEnable()
		{
			Application.logMessageReceived += Log;
		}

		void OnDisable()
		{
			Application.logMessageReceived -= Log;
		}

		public void Log(string logString, string stackTrace, LogType type)
		{
			MyLog = logString + "\n" + stackTrace + "\n" + MyLog;
			if (MyLog.Length > 5000)
			{
				MyLog = MyLog.Substring(0, 4000);
			}
		}

		void OnGUI()
		{
			//if (!Application.isEditor) //Do not display in editor ( or you can use the UNITY_EDITOR macro to also disable the rest)
			{
				MyLog = GUI.TextArea(new Rect(10, 10, Screen.width - 10, Screen.height - 10), MyLog);
			}
		}
		//#endif
	}
}