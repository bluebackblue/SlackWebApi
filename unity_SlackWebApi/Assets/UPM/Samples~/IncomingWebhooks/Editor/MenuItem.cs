

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
			/** item
			*/
			private BlueBack.SlackWebApi.IncomingWebhooks.SendText item;

			/** constructor
			*/
			public IncomingWebhooks_SendText()
			{
				UnityEngine.Debug.Log("Start");

				this.item = new BlueBack.SlackWebApi.IncomingWebhooks.SendText(
					"https://hooks.slack.com/services/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",
					"あいうえお"
				);
			}

			/** Update
			*/
			public void Update()
			{
				if(this.item != null){
					this.item.Update();
					switch(this.item.mode){
					case BlueBack.SlackWebApi.IncomingWebhooks.SendText.Mode.Request:
					case BlueBack.SlackWebApi.IncomingWebhooks.SendText.Mode.Wait:
						{
						}return;
					}
				}

				//終了。
				UnityEditor.EditorApplication.update -= Update;
				
				{
					UnityEngine.Debug.Log("mode : " + this.item.mode.ToString());

					if(this.item.result != null){
						UnityEngine.Debug.Log("result : " + this.item.result);
					}

					if(this.item.errorstring != null){
						UnityEngine.Debug.Log("errorstring : " + this.item.errorstring);
					}

					UnityEngine.Debug.Log(this.item.webrequest.responseCode.ToString());
					foreach(var t_pair in this.item.webrequest.GetResponseHeaders()){
						UnityEngine.Debug.Log(t_pair.Key + " : " + t_pair.Value);
					}
				}

				UnityEngine.Debug.Log("End");
				this.item.DisposeWebRequest();
				this.item = null;
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

