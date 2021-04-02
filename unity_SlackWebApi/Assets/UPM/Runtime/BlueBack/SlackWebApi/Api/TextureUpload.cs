

/** BlueBack.SlackWebApi.Api
*/
namespace BlueBack.SlackWebApi.Api
{
	/** TextureUpload
	*/
	public class TextureUpload
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

		/** token
		*/
		public string token;

		/** channel
		*/
		public string channel;

		/** texture
		*/
		public UnityEngine.Texture2D texture;

		/** title
		*/
		public string title;

		/** comment
		*/
		public string comment;

		/** name
		*/
		public string name;

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

		/** constructor
		*/
		public TextureUpload(string a_token,string a_channel,UnityEngine.Texture2D a_texture,string a_title,string a_comment,string a_name)
		{
			//token
			this.token = a_token; 

			//channel
			this.channel = a_channel;

			//texture
			this.texture = a_texture;

			//title
			this.title = a_title;

			//comment
			this.comment = a_comment;

			//name
			this.name = a_name;

			//errorstring
			this.errorstring = null;

			//result
			this.result = null;

			//mode
			this.mode = Mode.Request;

			//webrequest
			this.webrequest = null;
		}

		/** DisposeWebRequest
		*/
		public void DisposeWebRequest()
		{
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
					System.Collections.Generic.List<UnityEngine.Networking.IMultipartFormSection> t_postdata = new System.Collections.Generic.List<UnityEngine.Networking.IMultipartFormSection>();
					{
						byte[] t_texture_byte = UnityEngine.ImageConversion.EncodeToPNG(this.texture);

						t_postdata.Add(new UnityEngine.Networking.MultipartFormDataSection("token",this.token));
						t_postdata.Add(new UnityEngine.Networking.MultipartFormDataSection("channels",this.channel));
						t_postdata.Add(new UnityEngine.Networking.MultipartFormDataSection("filename",this.name + ".png"));
						t_postdata.Add(new UnityEngine.Networking.MultipartFormDataSection("filetype","png"));
						t_postdata.Add(new UnityEngine.Networking.MultipartFormDataSection("initial_comment",this.comment));
						t_postdata.Add(new UnityEngine.Networking.MultipartFormDataSection("title",this.title));
						t_postdata.Add(new UnityEngine.Networking.MultipartFormFileSection("file",t_texture_byte,this.name + ".png","image/png"));
					}

					this.DisposeWebRequest();

					this.webrequest = UnityEngine.Networking.UnityWebRequest.Post("https://slack.com/api/files.upload?",t_postdata);
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

