

/** Editor.VersionManager
*/
#if(UNITY_EDITOR)
namespace Editor.VersionManager
{
	/** Creator_ServerJson
	*/
	public class Creator_ServerJson
	{
		/** Status
		*/
		public struct Status
		{
			/** lasttag
			*/
			public string lasttag;

			/** time
			*/
			public string time;
		}

		/** status
		*/
		public Status status;

		/** constructor
		*/
		public Creator_ServerJson()
		{
			try{
				if(BlueBack.AssetLib.Editor.ExistFile.IsExistFileFromAssetsPath("server.json") == true){
					//load
					string t_jsonstring = BlueBack.AssetLib.Editor.LoadText.LoadTextFromAssetsPath("server.json",null);
					this.status = BlueBack.JsonItem.Convert.JsonStringToObject<Status>(t_jsonstring);
				}else{
					//initialize save
					this.status.lasttag = "0.0.0";
					this.status.time = "---";
					string t_jsonstring = BlueBack.JsonItem.Convert.ObjectToJsonString<Status>(this.status);
					BlueBack.AssetLib.Editor.SaveText.SaveUtf8TextToAssetsPath(t_jsonstring,"server.json",false,BlueBack.AssetLib.LineFeedOption.CRLF);
				}
			}catch(System.Exception t_exception){
				UnityEngine.Debug.LogError(t_exception.Message);
			}
		}

		/** Download
		*/
		public void Download()
		{
			try{
				//download
				{
					string t_jsonstring = BlueBack.AssetLib.Editor.LoadText.TryLoadTextFromUrl("https://api.github.com/repos/bluebackblue/" + Setting.PACKAGE_NAME + "/releases/latest",null,System.Text.Encoding.GetEncoding("utf-8"));
					t_jsonstring = BlueBack.JsonItem.Normalize.Convert(t_jsonstring);
					BlueBack.JsonItem.JsonItem t_jsonitem = new BlueBack.JsonItem.JsonItem(t_jsonstring);
					this.status.lasttag = t_jsonitem.GetItem("name").GetStringData();
					this.status.time = System.DateTime.Now.ToString();
				}

				//save
				{
					string t_jsonstring = BlueBack.JsonItem.Convert.ObjectToJsonString<Status>(this.status);
					BlueBack.AssetLib.Editor.SaveText.SaveUtf8TextToAssetsPath(t_jsonstring,"server.json",false,BlueBack.AssetLib.LineFeedOption.CRLF);
				}
			}catch(System.Exception t_exception){
				UnityEngine.Debug.LogError(t_exception.Message);
			}
		}
	}
}
#endif

