# Blazor1 プロジェクト ドキュメント

## 目次

1. [.NET Blazor について](#net-blazor-について)
2. [プロジェクト概要](#プロジェクト概要)
3. [プロジェクト構造](#プロジェクト構造)
4. [主要コンポーネント](#主要コンポーネント)
5. [レンダリングモード](#レンダリングモード)
6. [実行方法](#実行方法)
7. [技術スタック](#技術スタック)

---

## .NET Blazor について

### Blazorとは

Blazorは、Microsoftが開発したオープンソースのWebフレームワークで、C#とRazor構文を使用してインタラクティブなWebアプリケーションを構築できます。JavaScriptを使わずに、C#だけでフロントエンドとバックエンドの両方を開発できることが最大の特徴です。

### Blazorの主な特徴

#### 1. **C#による統一開発**
   - フロントエンドとバックエンドを同じ言語（C#）で開発
   - JavaScriptの知識が不要
   - .NETエコシステム全体を活用可能

#### 2. **コンポーネントベースのアーキテクチャ**
   - 再利用可能なUIコンポーネント
   - コンポーネント間でのデータバインディング
   - イベントハンドリング

#### 3. **複数のレンダリングモード**
   - **Server**: サーバー側でレンダリング（SignalR使用）
   - **WebAssembly**: クライアント側でレンダリング（ブラウザ内で.NET実行）
   - **Auto**: 初回はサーバー、以降はWebAssembly
   - **Static**: 静的レンダリング（SSR）

#### 4. **パフォーマンス**
   - サーバー側レンダリングによる高速な初期表示
   - WebAssemblyによるクライアント側の高速実行
   - ストリーミングレンダリングによる段階的なコンテンツ表示

#### 5. **.NETエコシステムの活用**
   - NuGetパッケージの利用
   - Entity Framework Coreとの統合
   - ASP.NET Coreの全機能を利用可能

### Blazorでできること

#### 1. **Webアプリケーション開発**
   - シングルページアプリケーション（SPA）
   - プログレッシブWebアプリ（PWA）
   - サーバーサイドレンダリング（SSR）アプリケーション

#### 2. **リアルタイムアプリケーション**
   - SignalRを使用したリアルタイム通信
   - チャットアプリケーション
   - ダッシュボードやモニタリングシステム

#### 3. **データ駆動型アプリケーション**
   - CRUD操作を含むデータ管理アプリ
   - フォーム処理とバリデーション
   - データビジュアライゼーション

#### 4. **エンタープライズアプリケーション**
   - 業務アプリケーション
   - 管理画面
   - レポート生成システム

#### 5. **モバイルアプリケーション**
   - .NET MAUIとの統合により、モバイルアプリも開発可能

#### 6. **マイクロサービス**
   - バックエンドAPIとの統合
   - gRPC、RESTful APIとの連携

---

## プロジェクト概要

このプロジェクトは、**Blazor Web App**（.NET 10.0）を使用したサンプルアプリケーションです。サーバー側レンダリングとWebAssemblyの両方をサポートするハイブリッド構成になっています。

### プロジェクト構成

- **Blazor1**: サーバー側プロジェクト（ホストアプリケーション）
- **Blazor1.Client**: クライアント側プロジェクト（WebAssembly）

### 主な機能

1. **ホームページ**: シンプルなウェルカムページ
2. **カウンター**: インタラクティブなカウンターコンポーネント
3. **天気予報**: ストリーミングレンダリングのデモ

---

## プロジェクト構造

```
Blazor1/
├── Blazor1/                          # サーバー側プロジェクト
│   ├── Components/                   # Razorコンポーネント
│   │   ├── Layout/                   # レイアウトコンポーネント
│   │   │   ├── MainLayout.razor      # メインレイアウト
│   │   │   ├── NavMenu.razor         # ナビゲーションメニュー
│   │   │   └── ReconnectModal.razor  # 再接続モーダル
│   │   ├── Pages/                    # ページコンポーネント
│   │   │   ├── Home.razor            # ホームページ
│   │   │   ├── Weather.razor         # 天気予報ページ
│   │   │   ├── Error.razor           # エラーページ
│   │   │   └── NotFound.razor        # 404ページ
│   │   ├── App.razor                 # アプリケーションルート
│   │   └── Routes.razor              # ルーティング設定
│   ├── Program.cs                     # アプリケーションエントリーポイント
│   ├── appsettings.json              # アプリケーション設定
│   └── wwwroot/                      # 静的ファイル
│       └── lib/bootstrap/            # Bootstrap CSSフレームワーク
│
└── Blazor1.Client/                   # クライアント側プロジェクト（WebAssembly）
    ├── Pages/
    │   └── Counter.razor             # カウンターコンポーネント（クライアント側）
    └── Program.cs                    # WebAssemblyホスト設定
```

---

## 主要コンポーネント

### 1. App.razor
アプリケーションのルートコンポーネント。HTMLの基本構造を定義します。

**主な機能:**
- HTMLドキュメントの基本構造
- Bootstrap CSSの読み込み
- Blazorフレームワークスクリプトの読み込み
- リソースプリローダー

### 2. Routes.razor
ルーティングを管理するコンポーネント。

**主な機能:**
- ルートの解決
- レイアウトの適用
- 404エラーハンドリング
- フォーカス管理

### 3. MainLayout.razor
アプリケーション全体のレイアウトを定義します。

**構成:**
- サイドバー（ナビゲーションメニュー）
- メインコンテンツエリア
- エラー表示UI

### 4. NavMenu.razor
ナビゲーションメニューコンポーネント。

**メニュー項目:**
- Home (`/`)
- Counter (`/counter`)
- Weather (`/weather`)

### 5. Home.razor
ホームページコンポーネント。

**ルート:** `/`
**機能:** シンプルなウェルカムメッセージ

### 6. Counter.razor
インタラクティブなカウンターコンポーネント。

**ルート:** `/counter`
**レンダリングモード:** `InteractiveAuto`
**機能:**
- クリックでカウントを増加
- クライアント側でインタラクティブに動作

### 7. Weather.razor
天気予報データを表示するコンポーネント。

**ルート:** `/weather`
**レンダリングモード:** `StreamRendering`（ストリーミングレンダリング）
**機能:**
- 非同期データ読み込みのシミュレーション
- ストリーミングレンダリングによる段階的な表示
- 5日間の天気予報データを表示

---

## レンダリングモード

このプロジェクトでは、複数のレンダリングモードを使用しています。

### 1. Static（静的レンダリング）
デフォルトのレンダリングモード。サーバー側でHTMLを生成し、クライアントに送信します。

**使用例:** `Home.razor`

### 2. InteractiveServer（サーバー側インタラクティブ）
SignalRを使用して、サーバー側でインタラクションを処理します。

**特徴:**
- リアルタイムな双方向通信
- サーバーリソースへの直接アクセス
- ネットワーク遅延の影響あり

### 3. InteractiveWebAssembly（WebAssemblyインタラクティブ）
ブラウザ内で.NETを実行し、クライアント側でインタラクションを処理します。

**特徴:**
- オフライン動作が可能
- サーバー負荷が少ない
- 初期読み込み時間が長い

### 4. InteractiveAuto（自動選択）
初回はサーバー側でレンダリングし、WebAssemblyが読み込まれたら自動的に切り替わります。

**使用例:** `Counter.razor`

### 5. StreamRendering（ストリーミングレンダリング）
データの準備ができ次第、段階的にコンテンツを送信します。

**使用例:** `Weather.razor`
**利点:**
- 初期表示が高速
- ユーザー体験の向上

---

## 実行方法

### 前提条件

- .NET 10.0 SDK がインストールされていること
- 開発環境（Visual Studio、Visual Studio Code、またはコマンドライン）

### 実行手順

1. **プロジェクトディレクトリに移動**
   ```bash
   cd Blazor1
   ```

2. **アプリケーションの実行**
   ```bash
   dotnet run --project Blazor1/Blazor1.csproj
   ```

3. **ブラウザでアクセス**
   - HTTP: `http://localhost:5088`
   - HTTPS: `https://localhost:7172`

### 開発モードでの実行

開発モードでは、以下の機能が有効になります：
- ホットリロード
- 詳細なエラーメッセージ
- WebAssemblyデバッグ

### ビルド

```bash
dotnet build
```

### 発行

```bash
dotnet publish -c Release
```

---

## 技術スタック

### フレームワーク・ライブラリ

- **.NET 10.0**: 実行環境
- **Blazor Web App**: Webアプリケーションフレームワーク
- **ASP.NET Core**: Webサーバー
- **Bootstrap 5**: CSSフレームワーク
- **SignalR**: リアルタイム通信（サーバー側レンダリング時）

### 開発ツール

- **Razor**: コンポーネントマークアップ
- **C#**: プログラミング言語
- **WebAssembly**: クライアント側実行環境

### パッケージ

- `Microsoft.AspNetCore.Components.WebAssembly.Server` (v10.0.2)
- `Microsoft.AspNetCore.Components.WebAssembly` (v10.0.2)

---

## 今後の拡張可能性

このプロジェクトは、以下のような機能を追加して拡張できます：

1. **データベース統合**
   - Entity Framework Coreの追加
   - SQL Server、SQLite、PostgreSQLなどのデータベース接続

2. **認証・認可**
   - ASP.NET Core Identityの統合
   - JWT認証
   - OAuth/OpenID Connect

3. **API統合**
   - RESTful APIの呼び出し
   - gRPCサービスの統合
   - GraphQLクライアント

4. **状態管理**
   - Flux/Reduxパターンの実装
   - グローバル状態管理

5. **テスト**
   - 単体テスト（xUnit、NUnit）
   - 統合テスト
   - E2Eテスト

6. **デプロイ**
   - Azure App Serviceへのデプロイ
   - Dockerコンテナ化
   - CI/CDパイプラインの構築

---

## テンプレートの気になるやつあれこれ
<details>
<summary>全ページに/Layoutが適用される仕組み</summary>

## 1. **Routes.razor** でデフォルトレイアウトを指定

```3:3:Blazor1/Components/Routes.razor
        <RouteView RouteData="routeData" DefaultLayout="typeof(Layout.MainLayout)" />
```

この `DefaultLayout="typeof(Layout.MainLayout)"` により、個別にレイアウトを指定していないページはすべて `MainLayout.razor` が適用されます。

## 2. **App.razor** で ReconnectModal を直接配置

```19:19:Blazor1/Components/App.razor
    <ReconnectModal />
```

`ReconnectModal` は `App.razor` に直接含まれているため、全ページで常に読み込まれます。

## 3. **MainLayout.razor** 内で NavMenu を配置

```5:5:Blazor1/Components/Layout/MainLayout.razor
        <NavMenu />
```

`NavMenu` は `MainLayout.razor` 内に含まれているため、`MainLayout` が適用される全ページで表示されます。

## 階層構造

```
App.razor（最上位）
├── Routes.razor
│   └── RouteView（DefaultLayout="MainLayout"）
│       └── MainLayout.razor
│           ├── NavMenu.razor ← MainLayout内に含まれる
│           └── @Body（各ページのコンテンツがここに表示）
└── ReconnectModal.razor ← App.razorに直接含まれる
```

## まとめ

- `MainLayout` と `NavMenu` は `Routes.razor` の `DefaultLayout` により全ページに適用
- `ReconnectModal` は `App.razor` に直接配置されているため全ページで表示

個別のページで別レイアウトを使う場合は、ページファイルの先頭で `@layout` ディレクティブを指定します（例: `@layout AnotherLayout`）。
</details>

<details>
<summary>Blazorの特殊キーワード</summary>

## 確認方法

### 1. 公式ドキュメントを参照
- **Microsoft公式ドキュメント**: https://learn.microsoft.com/aspnet/core/blazor/components/
- **Razor構文リファレンス**: https://learn.microsoft.com/aspnet/core/mvc/views/razor

### 2. IntelliSenseを活用
Visual StudioやVisual Studio Codeで `.razor` ファイルを編集する際、`@` を入力すると利用可能なディレクティブが候補として表示されます。

### 3. プロジェクト内で検索
```bash
# プロジェクト内で @ で始まるキーワードを検索
grep -r "@\w+" Components/
```

---

## 主要なディレクティブ一覧

### ページ・ルーティング関連

#### `@page`
ページのルートを定義します。

```razor
@page "/"
@page "/weather"
@page "/user/{UserId:int}"
```

**使用例（このプロジェクト内）:**
- `Home.razor`: `@page "/"`
- `Weather.razor`: `@page "/weather"`
- `Counter.razor`: `@page "/counter"`

---

### レイアウト関連

#### `@layout`
特定のページで使用するレイアウトを指定します。

```razor
@layout AnotherLayout
```

**注意:** このプロジェクトでは `Routes.razor` で `DefaultLayout` が設定されているため、個別に指定していない場合は `MainLayout` が使用されます。

#### `@Body`
レイアウトコンポーネント内で、子コンテンツを表示する位置を指定します。

```razor
@inherits LayoutComponentBase

<div class="content">
    @Body  <!-- ここにページコンテンツが表示される -->
</div>
```

**使用例（このプロジェクト内）:**
- `MainLayout.razor`: `<article class="content px-4">@Body</article>`

---

### 名前空間・インポート関連

#### `@using`
名前空間をインポートします。

```razor
@using System.Net.Http
@using Microsoft.AspNetCore.Components.Web
@using Blazor1.Components
```

**使用例（このプロジェクト内）:**
- `_Imports.razor`: 複数の `@using` ディレクティブが定義されています

#### `@namespace`
コンポーネントの名前空間を設定します。

```razor
@namespace Blazor1.Components.Pages
```

---

### 継承・属性関連

#### `@inherits`
コンポーネントの基底クラスを指定します。

```razor
@inherits LayoutComponentBase
```

**使用例（このプロジェクト内）:**
- `MainLayout.razor`: `@inherits LayoutComponentBase`

#### `@attribute`
コンポーネントに属性を追加します。

```razor
@attribute [Authorize]
@attribute [StreamRendering]
@attribute [Route("/custom")]
```

**使用例（このプロジェクト内）:**
- `Weather.razor`: `@attribute [StreamRendering]`

---

### レンダリングモード関連

#### `@rendermode`
コンポーネントのレンダリングモードを指定します。

```razor
@rendermode InteractiveServer
@rendermode InteractiveWebAssembly
@rendermode InteractiveAuto
```

**使用例（このプロジェクト内）:**
- `Counter.razor`: `@rendermode InteractiveAuto`

**利用可能なモード:**
- `InteractiveServer`: サーバー側でインタラクティブに実行
- `InteractiveWebAssembly`: クライアント側（WebAssembly）で実行
- `InteractiveAuto`: 初回はサーバー、以降はWebAssembly
- `StreamRendering`: ストリーミングレンダリング（属性として使用）

---

### コードブロック関連

#### `@code`
C#コードを記述するブロックです。

```razor
@code {
    private int count = 0;
    
    private void Increment()
    {
        count++;
    }
}
```

**使用例（このプロジェクト内）:**
- `Counter.razor`: `@code { private int currentCount = 0; ... }`
- `Weather.razor`: `@code { private WeatherForecast[]? forecasts; ... }`

#### `@functions`
`@code` と同様に、C#コードを記述します（古い構文、現在は `@code` を推奨）。

```razor
@functions {
    // C#コード
}
```

---

### 条件分岐・ループ関連

#### `@if` / `@else if` / `@else`
条件分岐を行います。

```razor
@if (isLoading)
{
    <p>Loading...</p>
}
else if (hasError)
{
    <p>Error occurred</p>
}
else
{
    <p>Content loaded</p>
}
```

**使用例（このプロジェクト内）:**
- `Weather.razor`: `@if (forecasts == null) { ... } else { ... }`

#### `@switch` / `@case` / `@default`
switch文を使用します。

```razor
@switch (status)
{
    case "loading":
        <p>Loading...</p>
        break;
    case "loaded":
        <p>Loaded</p>
        break;
    default:
        <p>Unknown</p>
        break;
}
```

#### `@for`
forループを使用します。

```razor
@for (int i = 0; i < items.Length; i++)
{
    <p>Item @i: @items[i]</p>
}
```

#### `@foreach`
foreachループを使用します。

```razor
@foreach (var item in items)
{
    <p>@item.Name</p>
}
```

**使用例（このプロジェクト内）:**
- `Weather.razor`: `@foreach (var forecast in forecasts) { ... }`

#### `@while`
whileループを使用します。

```razor
@while (condition)
{
    <p>Looping...</p>
}
```

---

### 変数・式の埋め込み

#### `@` (式の埋め込み)
C#の式や変数をHTMLに埋め込みます。

```razor
<p>Count: @currentCount</p>
<p>Total: @(price * quantity)</p>
<p>Name: @user.Name</p>
```

**使用例（このプロジェクト内）:**
- `Counter.razor`: `<p role="status">Current count: @currentCount</p>`
- `Weather.razor`: `<td>@forecast.Date.ToShortDateString()</td>`

#### `@@`
`@` 記号自体を出力する場合に使用します。

```razor
<p>Email: user@@example.com</p>
```

---

### イベントハンドリング関連

#### `@onclick` / `@onchange` / `@oninput` など
DOMイベントをハンドルします。

```razor
<button @onclick="HandleClick">Click me</button>
<input @onchange="HandleChange" />
<input @oninput="HandleInput" />
```

**使用例（このプロジェクト内）:**
- `Counter.razor`: `<button class="btn btn-primary" @onclick="IncrementCount">Click me</button>`

**主要なイベント:**
- `@onclick`: クリックイベント
- `@onchange`: 値変更イベント
- `@oninput`: 入力イベント
- `@onkeydown` / `@onkeyup`: キーボードイベント
- `@onfocus` / `@onblur`: フォーカスイベント
- `@onsubmit`: フォーム送信イベント

#### `@onclick:preventDefault`
イベントのデフォルト動作を防ぎます。

```razor
<a href="/" @onclick="HandleClick" @onclick:preventDefault>Link</a>
```

#### `@onclick:stopPropagation`
イベントの伝播を停止します。

```razor
<div @onclick:stopPropagation>
    <button @onclick="HandleClick">Click</button>
</div>
```

---

### データバインディング関連

#### `@bind`
双方向データバインディングを行います。

```razor
<input @bind="name" />
<input @bind="name" @bind:event="oninput" />
<input @bind="date" @bind:format="yyyy-MM-dd" />
```

#### `@bind:get` / `@bind:set`
双方向バインディングを明示的に制御します。

```razor
<input @bind:get="@value" @bind:set="@((string v) => value = v.ToUpper())" />
```

---

### コンポーネント参照関連

#### `@ref`
コンポーネントや要素への参照を取得します。

```razor
<MyComponent @ref="myComponentRef" />

@code {
    private MyComponent? myComponentRef;
    
    private void CallComponentMethod()
    {
        myComponentRef?.SomeMethod();
    }
}
```

#### `@key`
リストレンダリング時に要素を識別するキーを指定します。

```razor
@foreach (var item in items)
{
    <ItemComponent @key="item.Id" Item="@item" />
}
```

---

### その他の特殊キーワード

#### `<PageTitle>`
ページのタイトルを設定します。

```razor
<PageTitle>Home</PageTitle>
```

**使用例（このプロジェクト内）:**
- `Home.razor`: `<PageTitle>Home</PageTitle>`
- `Weather.razor`: `<PageTitle>Weather</PageTitle>`
- `Counter.razor`: `<PageTitle>Counter</PageTitle>`

#### `<HeadOutlet>`
`<head>` セクションにコンテンツを出力するためのアウトレットです。

```razor
<head>
    <HeadOutlet />
</head>
```

**使用例（このプロジェクト内）:**
- `App.razor`: `<HeadOutlet />`

#### `<ResourcePreloader>`
リソースのプリロードを管理します。

```razor
<head>
    <ResourcePreloader />
</head>
```

**使用例（このプロジェクト内）:**
- `App.razor`: `<ResourcePreloader />`

#### `<ImportMap>`
ESモジュールのインポートマップを定義します。

```razor
<head>
    <ImportMap />
</head>
```

**使用例（このプロジェクト内）:**
- `App.razor`: `<ImportMap />`

---

### コメント

#### `@* ... *@`
Razorコメントです。

```razor
@* これはコメントです *@
```

#### `<!-- ... -->`
HTMLコメントです（サーバー側で処理されます）。

```razor
<!-- これはHTMLコメントです -->
```

---

## このプロジェクトで使用されているディレクティブ

### ディレクティブ
- `@page`: ルーティング定義
- `@attribute`: 属性の追加（`[StreamRendering]`）
- `@rendermode`: レンダリングモード指定
- `@inherits`: 基底クラスの継承
- `@using`: 名前空間のインポート
- `@code`: C#コードブロック

### 特殊キーワード・構文
- `@if` / `@else`: 条件分岐
- `@foreach`: ループ処理
- `@`: 式の埋め込み
- `@onclick`: イベントハンドリング
- `@Body`: レイアウト内でのコンテンツ表示位置

### 特殊コンポーネント
- `<PageTitle>`: ページタイトル
- `<HeadOutlet>`: ヘッドセクションのアウトレット
- `<ResourcePreloader>`: リソースプリローダー
- `<ImportMap>`: インポートマップ

---

## 参考リンク

### 公式ドキュメント
- [Blazor コンポーネントの概要](https://learn.microsoft.com/aspnet/core/blazor/components/)
- [Razor 構文リファレンス](https://learn.microsoft.com/aspnet/core/mvc/views/razor)
- [Blazor イベント処理](https://learn.microsoft.com/aspnet/core/blazor/components/event-handling)
- [Blazor データバインディング](https://learn.microsoft.com/aspnet/core/blazor/components/data-binding)
- [Blazor ルーティング](https://learn.microsoft.com/aspnet/core/blazor/fundamentals/routing)

### その他
- [Blazor University](https://blazor-university.com/) - 非公式だが包括的なチュートリアル
- [Awesome Blazor](https://github.com/AdrienTorris/awesome-blazor) - Blazorリソース集

---

## 補足: ディレクティブの確認方法（コマンドライン）

プロジェクト内で使用されているディレクティブを確認するには：

```bash
# PowerShell
Select-String -Path "*.razor" -Pattern "@\w+" -Recurse

# Bash/Git Bash
grep -r "@\w+" --include="*.razor" .
```


</details>


## 参考リンク

- [Blazor公式ドキュメント](https://learn.microsoft.com/aspnet/core/blazor/)
- [.NET ドキュメント](https://learn.microsoft.com/dotnet/)
- [ASP.NET Core ドキュメント](https://learn.microsoft.com/aspnet/core/)

---

## ライセンス

このプロジェクトはサンプルアプリケーションです。

---

**最終更新日:** 2024年
