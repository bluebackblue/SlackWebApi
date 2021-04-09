

/** Editor.VersionManager
*/
#if(UNITY_EDITOR)
namespace Editor.VersionManager
{
	/** Creator_PackageJson
	*/
	public class Creator_PackageJson
	{
		/** constructor
		*/
		public Creator_PackageJson()
		{
		}

		/** Item
		*/
		private struct Item
		{
			/** Author
			*/
			public struct Author
			{
				public string name;
				public string url;
			};

			/** Samples
			*/
			public struct Samples
			{
				public string displayName;
				public string path;
			}

			/** 名前。
			*/
			public string name;

			/** 表示名。
			*/
			public string displayName;

			/** version
			*/
			public string version;

			/** unity
			*/
			public string unity;

			/** 説明。
			*/
			public string discription;

			/** 作者。
			*/
			public Author author;

			/** キーワード。
			*/
			public string[] keyword;

			/** 依存関係。
			*/
			public System.Collections.Generic.Dictionary<string,string> dependencies;

			/** サンプル。
			*/
			public System.Collections.Generic.List<Samples> samples;
		}

		/** SaveVersionCs
		*/
		public void SaveVersionCs(string a_version)
		{
			UnityEngine.Debug.Log("SaveVersionCs : " + a_version);

			string[] t_headr = new string[]{
				"",
				"",
				"/**",
				" * Copyright (c) blueback",
				" * Released under the MIT License",
				" * @brief バージョン。",
				"*/",
				"",
				"",
				"/** <<namespace_comment>>",
				"*/",
				"namespace <<namespace_name>>",
				"{",
				"	/** Version",
				"	*/",
				"	public class Version",
				"	{",
				"		/** version",
				"		*/",
				"		public const string packageversion = \"<<version>>\";",
				"	}",
				"}",
				"",
			};

			System.Collections.Generic.Dictionary<string,string> t_replace_list = new System.Collections.Generic.Dictionary<string,string>();
			{
				t_replace_list.Add("<<namespace_name>>",Setting.AUTHOR_NAME + "." + Setting.PACKAGE_NAME);
				t_replace_list.Add("<<namespace_comment>>",Setting.AUTHOR_NAME + "." + Setting.PACKAGE_NAME);
				t_replace_list.Add("<<version>>",a_version);
			}

			string t_path = "UPM/Runtime/" + Setting.AUTHOR_NAME + "/" + Setting.PACKAGE_NAME + "/Version.cs";
			BlueBack.AssetLib.Editor.SaveScript.SaveScriptToAssetsPath(null,t_path,t_headr,null,null,null,t_replace_list,false,BlueBack.AssetLib.LineFeedOption.CRLF);
			BlueBack.AssetLib.Editor.RefreshAsset.Refresh();
		}

		/** SavePackageJson
		*/
		public void SavePackageJson(string a_version)
		{
			UnityEngine.Debug.Log("SavePackageJson : " + a_version);

			Item t_item;
			{
				//name
				t_item.name = (Setting.AUTHOR_NAME + "." + Setting.PACKAGE_NAME).ToLower();

				//displayName
				t_item.displayName = Setting.PACKAGE_NAME;

				//version
				t_item.version = a_version;

				//unity
				t_item.unity = Setting.PACKAGEJSON_UNITY;

				//discription
				t_item.discription = Setting.PACKAGEJSON_DISCRIPTION;

				//author
				t_item.author.name = Setting.AUTHOR_NAME;
				t_item.author.url = "https://github.com/bluebackblue";
				
				//keyword
				t_item.keyword = Setting.PACKAGEJSON_KEYWORD;
				
				//dependencies
				t_item.dependencies = new System.Collections.Generic.Dictionary<string,string>();
				{
				}

				//samples
				t_item.samples = new System.Collections.Generic.List<Item.Samples>();
				{
					string t_path = "Samples/" + Setting.PACKAGE_NAME + "/000";
					System.Collections.Generic.List<string> t_sample_directory_list = BlueBack.AssetLib.Editor.DirectoryNameList.CreateOnlyTopDirectoryNameListFromAssetsPath(t_path);
					for(int ii=0;ii<t_sample_directory_list.Count;ii++){
						Item.Samples t_sample_item = new Item.Samples();
						{
							t_sample_item.path = "Samples~/" + t_sample_directory_list[ii];
							t_sample_item.displayName = t_sample_directory_list[ii];
						}
						t_item.samples.Add(t_sample_item);
					}
				}
			}

			string t_jsonstring = BlueBack.JsonItem.Convert.ObjectToJsonString<Item>(t_item);

			BlueBack.AssetLib.Editor.SaveText.SaveUtf8TextToAssetsPath(t_jsonstring,"UPM/package.json",false,BlueBack.AssetLib.LineFeedOption.CRLF);
			BlueBack.AssetLib.Editor.RefreshAsset.Refresh();
		}
	}
}
#endif

