# mandelbrot_by_azure_func

🐧🐧🐧 `Azure Functions`でマンデルブロ集合を描写するAPIを実装する。  

![成果物](.development/img/fruit.gif)  

指定可能なパラメタは以下の通り。  

| パラメタ | 説明 |
| ---- | ---- |
| width | 画像サイズ(縦幅) |
| height | 画像サイズ(横幅) |
| x0 | 実軸の最小座標 |
| y0 | 虚軸の最小座標 |
| x1 | 実軸の最大座標 |
| y1 | 虚軸の最大座標 |
| hue | 色相 |
| maxIter | 最大反復回数 |

パラメタはクエリパラメタとボディ部のJSONデータのどちらでも指定可能。  

`Azure Functions`の仕様上、widthに1億など巨大な数字を入力されてもタイムアウトが発生するため、パラメタのチェックはしていない。  
リクエストの回数のみが課金対象となっているため、一回の要求で大きな処理をしてもお財布は痛まない。  

## Azure Functionsへのデプロイ方法

[公式の拡張機能(VSCode)](https://marketplace.visualstudio.com/items?itemName=ms-azuretools.vscode-azurefunctions)を用いて簡単にデプロイ可能。  

資源グループを作成していない場合には、Azureポータルサイトから資源グループを作成し、「関数アプリ」を作成する。  
その中に、適当な名前の関数(ここでは"mandelbrot_by_azure_func")を作成し、このプロジェクトをアップロードする。  

これより下は、インストールした公式の拡張機能で実行可能。  
「A」のマークのメニュータブが左に表示されるので、これをクリック、AzureにサインインしてWORKSPACEの中にある雲のアイコンのデプロイボタンを押せばデプロイ完了。  

とっても簡単🐙🐙🐙  

## 補足

GitHub Actionsが使えない事件。  
GitHub Actionsでデプロイしようとすると、以下のエラーが、、、  

```console
Error: Execution Exception (state: PublishContent) (step: Invocation)
Error:   When request Azure resource at PublishContent, zipDepoy : WEBSITE_RUN_FROM_PACKAGE in your function app is set to an URL. Please remove WEBSITE_RUN_FROM_PACKAGE app setting from your function app.
Error: Deployment Failed!
```

アクションの内容は<https://learn.microsoft.com/ja-jp/azure/azure-functions/functions-how-to-github-actions>通り。  

で、`WEBSITE_RUN_FROM_PACKAGE`の値を変更しようと調べてみると、

|ホスティング プラン | Windows | Linux |
| ---- | ---- | ---- |
| 従量課金プラン | 1 が強く推奨されます。 | サポートされるのは \<URL\> のみです。 |
| Premium | 1 が推奨されます。 | 1 が推奨されます。 |
| 専用 | 1 が推奨されます。 | 1 が推奨されます。 |

との記載が、、、  
<https://learn.microsoft.com/ja-jp/azure/azure-functions/run-functions-from-deployment-package>より。  

いわゆる無料プランは従量課金のみ、、、  
無料プランはGitHub Actions使えないの！？  

そういえば、Visual Studioも少し前まで、F12による定義へのジャンプは無料プランでは使えなったような、、、  
まあ、VSCodeの拡張機能でも問題ないか、、、  

## 参考資料

- [Getting started with Azure Functions](https://learn.microsoft.com/en-us/azure/azure-functions/functions-get-started?pivots=programming-language-csharp)
- [Quickstart: Create a C# function in Azure using Visual Studio Code](https://learn.microsoft.com/en-us/azure/azure-functions/create-first-function-vs-code-csharp)
