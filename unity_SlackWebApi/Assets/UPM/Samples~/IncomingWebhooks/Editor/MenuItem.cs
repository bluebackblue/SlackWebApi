

/** Samples.SlackWebApi.IncomingWebhooks.Editor
*/
namespace Samples.SlackWebApi.IncomingWebhooks.Editor
{
	/** MenuItem
	*/
	#if(UNITY_EDITOR)
	public class MenuItem
	{
		/** IncomingWebhooks_SendText
		*/
		public class IncomingWebhooks_SendText
		{
			/** sendtext
			*/
			private BlueBack.SlackWebApi.IncomingWebhooks.SendText sendtext;

			/** constructor
			*/
			public IncomingWebhooks_SendText()
			{
				UnityEngine.Debug.Log("Start");

				this.sendtext = new BlueBack.SlackWebApi.IncomingWebhooks.SendText(
					"https://hooks.slack.com/services/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",
					"あいうえお"
				);
			}

			/** Update
			*/
			public void Update()
			{
				if(this.sendtext != null){
					this.sendtext.Update();
					switch(this.sendtext.mode){
					case BlueBack.SlackWebApi.IncomingWebhooks.SendText.Mode.Request:
					case BlueBack.SlackWebApi.IncomingWebhooks.SendText.Mode.Wait:
						{
						}return;
					}
				}

				//終了。
				UnityEditor.EditorApplication.update -= Update;
				
				{
					UnityEngine.Debug.Log("mode : " + this.sendtext.mode.ToString());

					if(this.sendtext.result != null){
						UnityEngine.Debug.Log("result : " + this.sendtext.result);
					}

					if(this.sendtext.errorstring != null){
						UnityEngine.Debug.Log("errorstring : " + this.sendtext.errorstring);
					}

					UnityEngine.Debug.Log(this.sendtext.webrequest.responseCode.ToString());
					foreach(var t_pair in this.sendtext.webrequest.GetResponseHeaders()){
						UnityEngine.Debug.Log(t_pair.Key + " : " + t_pair.Value);
					}
				}

				UnityEngine.Debug.Log("End");
				this.sendtext.DisposeWebRequest();
				this.sendtext = null;
			}
		}

		/** テスト。

			テスト用Slackへの招待リンク		: https://join.slack.com/t/bluebacktest/shared_invite/zt-ouhjkdsw-mVvcRoYCOBXpUndDqxW4TA
			テスト用Slack					: https://bluebacktest.slack.com/
			IncomingWebhooks					: https://hooks.slack.com/services/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

		*/
		[UnityEditor.MenuItem("サンプル/SlackWebApi/IncomingWebhooks/Test")]
		private static void MenuItem_Sample_SlackWebApi_IncomingWebhooks_Test()
		{
			UnityEditor.EditorApplication.update += new IncomingWebhooks_SendText().Update;
		}
	}
	#endif
}

