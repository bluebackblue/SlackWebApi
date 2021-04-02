# BlueBack.SlackWebApi
SlackWebApi操作
* IncomingWebhooksで投稿。
* OAuthTokenで画像の投稿。

## ライセンス
MIT License
* https://github.com/bluebackblue/SlackWebApi/blob/main/LICENSE

## 動作確認
Unity 2020.2.4f1

## URL
### 最新
* https://github.com/bluebackblue/SlackWebApi.git?path=unity_SlackWebApi/Assets/UPM#0.0.2
### 開発
* https://github.com/bluebackblue/SlackWebApi.git?path=unity_SlackWebApi/Assets/UPM

## Unityへの追加方法
* Unity起動
* メニュー選択：「Window->Package Manager」
* ボタン選択：「左上の＋ボタン」
* リスト選択：「Add package from git URL...」
* 上記のURLを追加「 https://github.com/〜〜/UPM#バージョン 」

### 注
Gitクライアントがインストールされている必要がある。
* https://docs.unity3d.com/ja/current/Manual/upm-git.html
* https://git-scm.com/

## サンプル

```
{
	BlueBack.SlackWebApi.IncomingWebhooks.SendText t_sendtext = new BlueBack.SlackWebApi.IncomingWebhooks.SendText(
		"https://hooks.slack.com/services/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",
		"あいうえお"
	);

	yield return t_sendtext.Coroutine();
}
```

