

/** BlueBack.SlackWebApi.IncomingWebhooks
*/
namespace BlueBack.SlackWebApi.IncomingWebhooks
{
	/** SendText
	*/
	public class SendText
	{
		/** Mode
		*/
		public enum Mode
		{
			Request,
			Wait,
			Result,
			Error,
		}

		/** url
		*/
		public string url;

		/** text
		*/
		public string text;

		/** errorstring
		*/
		public string errorstring;

		/** result
		*/
		public string result;

		/** mode
		*/
		public Mode mode;

		/** webrequest
		*/
		public UnityEngine.Networking.UnityWebRequest webrequest;

		/** uploadhandler
		*/
		public UnityEngine.Networking.UploadHandlerRaw uploadhandler;
		
		/** downloadhandler
		*/
		public UnityEngine.Networking.DownloadHandlerBuffer downloadhandler;

		/** constructor
		*/
		public SendText(string a_url,string a_text)
		{
			//url
			this.url = a_url; 

			//text
			this.text = a_text;

			//errorstring
			this.errorstring = null;

			//result
			this.result = null;

			//mode
			this.mode = Mode.Request;
		}

		/** DisposeWebRequest
		*/
		public void DisposeWebRequest()
		{
			if(this.downloadhandler != null){
				this.downloadhandler.Dispose();
				this.downloadhandler = null;
			}
			if(this.uploadhandler != null){
				this.uploadhandler.Dispose();
				this.uploadhandler = null;
			}
			if(this.webrequest != null){
				this.webrequest.Dispose();
				this.webrequest = null;
			}
		}

		/** Update
		*/
		public void Update()
		{
			switch(this.mode){
			case SendText.Mode.Request:
				{
					string t_jsonstring;
					{
						t_jsonstring = 
							"{" + 
								"\"text\":\"" + this.text + "\"" + 
							
								"," +

								"\"attachments\":[" +
									"{" +
										"\"type\":" + "\"section\"" + 
										
										"," +

										"\"text\":" + "{" +
											"\"type\":" + "\"mrkdwn\"" + 
											
											"," +

											"\"text\":" + "\"aaaaaaa\"" +
										"}" +
									"}" +

									"," +

									"{" +
										"\"type\":" + "\"section\"" + 
										
										"," +

										"\"text\":" + "{" +
											"\"type\":" + "\"mrkdwn\"" + 
											
											"," +

											"\"text\":" + "\"> bbbbbbb\"" +
										"}" +
									"}" +

								"]" +
							"}";
					}

					this.DisposeWebRequest();

					this.webrequest = new UnityEngine.Networking.UnityWebRequest(this.url,"POST");
					this.uploadhandler = new UnityEngine.Networking.UploadHandlerRaw(System.Text.Encoding.UTF8.GetBytes(t_jsonstring));
					this.downloadhandler = new UnityEngine.Networking.DownloadHandlerBuffer();

					this.webrequest.uploadHandler = this.uploadhandler;
					this.webrequest.downloadHandler = this.downloadhandler;
					this.webrequest.SetRequestHeader("Content-Type","application/json");
					this.webrequest.SendWebRequest();

					this.mode = SendText.Mode.Wait;
				}break;
			case SendText.Mode.Wait:
				{
					if(this.webrequest.result == UnityEngine.Networking.UnityWebRequest.Result.InProgress){
						//処理中。
					}else if(this.webrequest.result == UnityEngine.Networking.UnityWebRequest.Result.Success){
						//完了。

						this.result = this.webrequest.downloadHandler.text;
						this.errorstring = null;

						this.mode = SendText.Mode.Result;
						
						break;
					}else if(this.webrequest.error != null){
						//エラー。

						this.result = this.webrequest.downloadHandler.text;
						this.errorstring = null;

						this.mode = SendText.Mode.Error;
						break;
					}
				}break;
			}
		}

		/** Coroutine
		*/
		public System.Collections.IEnumerator Coroutine()
		{
			while((this.mode == Mode.Request)||(this.mode == Mode.Wait)){
				yield return null;
			}
			yield break;
		}
	}
}

