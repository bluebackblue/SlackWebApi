

/** Editor.VersionManager
*/
#if(UNITY_EDITOR)
namespace Editor.VersionManager
{
	/** Window
	*/
	public class Window : UnityEditor.EditorWindow
	{
		/** s_window
		*/
		private static Window s_window = null;

		/** 開く。
		*/
		public static void OpenWindow()
		{
			Window t_window = (Window)UnityEditor.EditorWindow.GetWindow(typeof(Window));
			if(t_window != null){
				t_window.Show();
			}
		}

		/** 閉じる。
		*/
		public static void CloseWindow()
		{
			if(s_window != null){
				s_window.Close();
				s_window = null;
			}
		}

		/** serverjson
		*/
		private Creator_ServerJson serverjson;

		/** packagejson
		*/
		private Creator_PackageJson packagejson;

		/** readmemd
		*/
		private Creator_ReadmeMd readmemd;

		/** constructor
		*/
		public Window()
		{
			UnityEngine.Debug.Log("Window : constructor");

			s_window = this;

			//serverjson
			this.serverjson = null;

			//packagejson
			this.packagejson = null;

			//readmemd
			this.readmemd = null;

			UnityEditor.Compilation.CompilationPipeline.compilationFinished += OnCompilationFinished;

			//タイトル。
			this.titleContent.text = "VersionManager";
		}

		/** コンパイル完了。
		*/
		private void OnCompilationFinished(System.Object a_object)
		{
			UnityEngine.Debug.Log("Window : OnCompilationFinished");
		}

		/** OnEnable
		*/
		private void OnEnable()
		{
			UnityEngine.Debug.Log("Window : OnEnable");

			//serverjson
			this.serverjson = new Creator_ServerJson();

			//packagejson
			this.packagejson = new Creator_PackageJson();

			//readmemd
			this.readmemd = new Creator_ReadmeMd();

			//ルート。
			UnityEngine.UIElements.VisualElement t_root = this.rootVisualElement;
			t_root.Clear();

			//UXMLのロード。
			UnityEngine.UIElements.VisualTreeAsset t_visualtree = BlueBack.AssetLib.Editor.LoadAsset.LoadAssetFromAssetsPath<UnityEngine.UIElements.VisualTreeAsset>("Editor/VersionManager/window.uxml");
			if(t_visualtree == null){
				return;
			}
			UnityEngine.UIElements.VisualElement t_root_element = t_visualtree.CloneTree();
			t_root.Add(t_root_element);

			//リロードボタン。
			{
				UnityEngine.UIElements.Button t_button = UnityEngine.UIElements.UQueryExtensions.Q<UnityEngine.UIElements.Button>(t_root,"reload");
				if(t_button != null){
					t_button.clickable.clicked += () => {
						UnityEngine.Debug.Log("Reload");
						this.OnEnable();
					};
				}
			}

			//サンプルコピー。
			{
				UnityEngine.UIElements.Button t_button = UnityEngine.UIElements.UQueryExtensions.Q<UnityEngine.UIElements.Button>(t_root,"samplecopy");
				if(t_button != null){
					t_button.clickable.clicked += () => {
						UnityEngine.Debug.Log("SampleCopy");
						SampleCopy.Copy();
						this.OnEnable();
					};
				}
			}

			//ＵＴＦ８にコンバート。
			{
				UnityEngine.UIElements.Button t_button = UnityEngine.UIElements.UQueryExtensions.Q<UnityEngine.UIElements.Button>(t_root,"converttoutf8");
				if(t_button != null){
					t_button.clickable.clicked += () => {
						UnityEngine.Debug.Log("ConvertToUtf8");
						BlueBack.AssetLib.Editor.ConvertText.ConvertTextToUtf8FromAssetsPath("",".*","^.*\\.cs$",false,BlueBack.AssetLib.LineFeedOption.CRLF);
						this.OnEnable();
					};
				}
			}

			//「server.json」。
			{
				UnityEngine.Debug.Log("server.json : " + this.serverjson.status.lasttag);

				{
					UnityEngine.UIElements.Label t_label = UnityEngine.UIElements.UQueryExtensions.Q<UnityEngine.UIElements.Label>(t_root,"label_server");
					if(t_label != null){
						t_label.text = this.serverjson.status.time;
					}

				}

				{
					UnityEngine.UIElements.Button t_button = UnityEngine.UIElements.UQueryExtensions.Q<UnityEngine.UIElements.Button>(t_root,"button_server");
					if(t_button != null){
						t_button.text = this.serverjson.status.lasttag;
						t_button.AddToClassList("red");

						t_button.clickable.clicked += () => {
							UnityEngine.Debug.Log("Download");
							this.serverjson.Download();
							this.OnEnable();
						};

					}
				}
			}

			//「readme.md」。
			{
				{
					UnityEngine.UIElements.Label t_label = UnityEngine.UIElements.UQueryExtensions.Q<UnityEngine.UIElements.Label>(t_root,"label_readme");
					if(t_label != null){
						t_label.text = this.readmemd.version;
					}
				}

				for(int ii=0;ii<3;ii++){
					UnityEngine.UIElements.Button t_button = UnityEngine.UIElements.UQueryExtensions.Q<UnityEngine.UIElements.Button>(t_root,"button_readme_" + ii.ToString());
					if(t_button != null){

						string[] t_version_split = this.serverjson.status.lasttag.Split('.');
						int t_version_split_item2 = int.Parse(t_version_split[2]);

						string t_version;

						switch(ii){
						case 0:
							{
								t_version = string.Format("{0}.{1}.{2}",t_version_split[0],t_version_split[1],t_version_split_item2 - 1);
							}break;
						case 1:
							{
								t_version = string.Format("{0}.{1}.{2}",t_version_split[0],t_version_split[1],t_version_split_item2);
							}break;
						case 2:
							{
								t_version = string.Format("{0}.{1}.{2}",t_version_split[0],t_version_split[1],t_version_split_item2 + 1);
							}break;
						default:
							{
								t_version = "";
								UnityEngine.Debug.Assert(false);
							}break;
						}

						t_button.text = t_version;
						if(t_version == this.readmemd.version){
							t_button.AddToClassList("red");
						}

						//「package.json」作成。
						t_button.clickable.clicked += () => {
							this.readmemd.SaveReadmeMd(t_version);
							this.OnEnable();
						};
					}
				}
			}

			//「package.json」。
			{
				UnityEngine.Debug.Log("package.json : " + Setting.GetPackageVersion());

				{
					UnityEngine.UIElements.Label t_label = UnityEngine.UIElements.UQueryExtensions.Q<UnityEngine.UIElements.Label>(t_root,"label_package");
					if(t_label != null){
						t_label.text = Setting.GetPackageVersion();
					}
				}

				for(int ii=0;ii<3;ii++){
					UnityEngine.UIElements.Button t_button = UnityEngine.UIElements.UQueryExtensions.Q<UnityEngine.UIElements.Button>(t_root,"button_package_" + ii.ToString());
					if(t_button != null){

						string[] t_version_split = this.serverjson.status.lasttag.Split('.');
						int t_version_split_item2 = int.Parse(t_version_split[2]);

						string t_version;

						switch(ii){
						case 0:
							{
								t_version = string.Format("{0}.{1}.{2}",t_version_split[0],t_version_split[1],t_version_split_item2 - 1);
							}break;
						case 1:
							{
								t_version = string.Format("{0}.{1}.{2}",t_version_split[0],t_version_split[1],t_version_split_item2);
							}break;
						case 2:
							{
								t_version = string.Format("{0}.{1}.{2}",t_version_split[0],t_version_split[1],t_version_split_item2 + 1);
							}break;
						default:
							{
								t_version = "";
								UnityEngine.Debug.Assert(false);
							}break;
						}

						t_button.text = t_version;
						if(t_version == Setting.GetPackageVersion()){
							t_button.AddToClassList("red");
						}

						//「package.json」作成。
						t_button.clickable.clicked += () => {
							this.packagejson.SavePackageJson(t_version);
							this.packagejson.SaveVersionCs(t_version);
							this.OnEnable();
						};
					}
				}
			}
		}
	}
}
#endif

