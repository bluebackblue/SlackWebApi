

/**
	Copyright (c) blueback
	Released under the MIT License
	@brief スラック。
*/


/** BlueBack.SlackWebApi.IncomingWebhooks
*/
namespace BlueBack.SlackWebApi.IncomingWebhooks
{
	/** SendText
	*/
	public sealed class SendText
	{
		/** Mode
		*/
		public enum Mode
		{
			/** リクエスト待ち。
			*/
			Request,

			/** 処理中。
			*/
			Work,

			/** 結果あり。
			*/
			Result,

			/** エラー。
			*/
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

			//webrequest
			this.webrequest = null;

			//uploadhandler
			this.uploadhandler = null;

			//downloadhandler
			this.downloadhandler = null;
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
			case Mode.Request:
				{
					string t_jsonstring;
					{
						string t_text = this.text.Replace("\r","").Replace('\"','\'');
						t_jsonstring = 
							"{" + 
								"\"text\"" + ":" + "\"" + t_text + "\"" + 
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

					this.mode = Mode.Work;
				}break;
			case Mode.Work:
				{
					if(this.webrequest.result == UnityEngine.Networking.UnityWebRequest.Result.InProgress){
						//処理中。
					}else if(this.webrequest.result == UnityEngine.Networking.UnityWebRequest.Result.Success){
						//完了。

						this.result = this.webrequest.downloadHandler.text;
						this.errorstring = null;

						this.mode = Mode.Result;

						break;
					}else if(this.webrequest.error != null){
						//エラー。

						this.result = this.webrequest.downloadHandler.text;
						this.errorstring = null;

						this.mode = Mode.Error;
						break;
					}
				}break;
			}
		}

		/** Coroutine
		*/
		public System.Collections.IEnumerator Coroutine()
		{
			while((this.mode == Mode.Request)||(this.mode == Mode.Work)){
				yield return null;
			}

			yield break;
		}
	}
}

