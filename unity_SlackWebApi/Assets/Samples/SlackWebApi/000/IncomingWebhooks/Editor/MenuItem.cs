

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


				#if(true)
				{


				}
				#endif


				this.sendtext = new BlueBack.SlackWebApi.IncomingWebhooks.SendText(
					"https://hooks.slack.com/services/T00000000/B0000000000/000000000000000000000000",
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
					case BlueBack.SlackWebApi.IncomingWebhooks.SendText.Mode.Work:
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
		*/
		[UnityEditor.MenuItem("サンプル/SlackWebApi/IncomingWebhooks/Test")]
		private static void MenuItem_Sample_SlackWebApi_IncomingWebhooks_Test()
		{
			UnityEditor.EditorApplication.update += new IncomingWebhooks_SendText().Update;
		}
	}
	#endif
}

