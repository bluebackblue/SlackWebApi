# BlueBack.SlackWebApi
SlackWebApi����

## ���C�Z���X
MIT License
* https://github.com/bluebackblue/SlackWebApi/blob/main/LICENSE

## ����m�F
Unity 2020.2.4f1

## URL
### �ŐV
* https://github.com/bluebackblue/SlackWebApi.git?path=unity_SlackWebApi/Assets/UPM#0.0.1
### �J��
* https://github.com/bluebackblue/SlackWebApi.git?path=unity_SlackWebApi/Assets/UPM

## Unity�ւ̒ǉ����@
* Unity�N��
* ���j���[�I���F�uWindow->Package Manager�v
* �{�^���I���F�u����́{�{�^���v
* ���X�g�I���F�uAdd package from git URL...�v
* ��L��URL��ǉ��u https://github.com/�`�`/UPM#�o�[�W���� �v

### ��
Git�N���C�A���g���C���X�g�[������Ă���K�v������B
* https://docs.unity3d.com/ja/current/Manual/upm-git.html
* https://git-scm.com/

## �T���v��

```
{
	BlueBack.SlackWebApi.IncomingWebhooks.SendText t_sendtext = new BlueBack.SlackWebApi.IncomingWebhooks.SendText(
		"https://hooks.slack.com/services/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",
		"����������"
	);

	yield return t_sendtext.Coroutine();
}
```

