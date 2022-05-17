

/** Editor
*/
namespace Editor
{
	//<<COMMENT>>## 例

	//<<CS_BLOCK_START>>
	/** 文字列送信。
	*/
	public sealed class Exsample : UnityEngine.MonoBehaviour
	{
		/** sendtext
		*/
		private BlueBack.SlackWebApi.IncomingWebhooks.SendText sendtext;

		/** Start
		*/
		private void Start()
		{
			//サンプル用。
			//「https://api.slack.com/apps」でWebhookURLを取得して下記のＵＲＬを差し替える。
			string t_webhookurl = "https://hooks.slack.com/services/T00000000/B0000000000/000000000000000000000000";

			//処理開始。
			this.sendtext = new BlueBack.SlackWebApi.IncomingWebhooks.SendText(t_webhookurl	,"あいうえお");
		}

		/** Update
		*/
		private void Update()
		{
			if(this.sendtext != null){

				//処理更新。
				this.sendtext.Update();

				switch(this.sendtext.mode){
				case BlueBack.SlackWebApi.IncomingWebhooks.SendText.Mode.Error:
				case BlueBack.SlackWebApi.IncomingWebhooks.SendText.Mode.Result:
					{
						if(this.sendtext.result != null){
							UnityEngine.Debug.Log("result : " + this.sendtext.result);
						}
						if(this.sendtext.errorstring != null){
							UnityEngine.Debug.Log("errorstring : " + this.sendtext.errorstring);
						}

						this.sendtext = null;
					}break;
				}
			}
		}
	}
	//<<CS_BLOCK_END>>
}

