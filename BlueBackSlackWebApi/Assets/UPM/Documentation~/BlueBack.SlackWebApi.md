# BlueBack.SlackWebApi
SlackWebApi操作
* IncomingWebhooksで投稿
* OAuthTokenで画像投稿

## ライセンス
MIT License
* https://github.com/bluebackblue/UpmSlackWebApi/blob/main/LICENSE

## 依存 / 使用ライセンス等
### ランタイム
### エディター
* https://github.com/bluebackblue/UpmSlackWebApi
### サンプル
* https://github.com/bluebackblue/UpmSlackWebApi

## 動作確認
Unity 2022.1.0b8

## UPM
### 最新
* https://github.com/bluebackblue/UpmSlackWebApi.git?path=BlueBackSlackWebApi/Assets/UPM#0.0.22
### 開発
* https://github.com/bluebackblue/UpmSlackWebApi.git?path=BlueBackSlackWebApi/Assets/UPM

## Unityへの追加方法
* Unity起動
* メニュー選択：「Window->Package Manager」
* ボタン選択：「左上の＋ボタン」
* リスト選択：「Add package from git URL...」
* 上記UPMのURLを追加「 https://github.com/～～/UPM#バージョン 」
### 注
Gitクライアントがインストールされている必要がある。
* https://docs.unity3d.com/ja/current/Manual/upm-git.html
* https://git-scm.com/

## 例
```cs
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
```

