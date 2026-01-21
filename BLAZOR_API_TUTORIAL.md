# Blazor API連携チュートリアル

このドキュメントでは、Blazorで外部APIと連携する方法を詳しく説明します。

---

## 目次

1. [基本的な概念](#基本的な概念)
2. [各要素の説明](#各要素の説明)
3. [実装手順](#実装手順)
4. [完全な実装例](#完全な実装例)
5. [よくある質問](#よくある質問)

---

## 基本的な概念

### 依存性注入（Dependency Injection, DI）

Blazorでは、**依存性注入（DI）**というパターンを使用して、コンポーネントが必要なサービスを取得します。これにより、コードの再利用性とテスト容易性が向上します。

### HTTPクライアント

外部APIと通信するには、**HttpClient**を使用します。Blazorでは、HttpClientを直接使用するのではなく、サービスクラスに注入して使用します。

---

## 各要素の説明

### 1. `builder.Services.AddHttpClient<WeatherService>()`

**場所**: `Program.cs`

**意味**: 
- **HttpClient**をDIコンテナに登録し、`WeatherService`に注入できるようにします
- `AddHttpClient<T>()`は、指定されたサービス型に対してHttpClientを設定する拡張メソッドです
- これにより、`WeatherService`のコンストラクタで`HttpClient`を受け取れるようになります

**なぜ必要か**:
- HttpClientは、接続プールの管理やタイムアウト設定など、適切に管理する必要があります
- DIコンテナが管理することで、リソースの効率的な使用とライフサイクル管理が可能になります

**例**:
```csharp
// Program.cs
builder.Services.AddHttpClient<WeatherService>();
```

これにより、`WeatherService`のコンストラクタで`HttpClient`を受け取れます：

```csharp
public class WeatherService
{
    private readonly HttpClient _httpClient;
    
    public WeatherService(HttpClient httpClient)
    {
        _httpClient = httpClient; // DIコンテナから自動的に注入される
    }
}
```

---

### 2. `@attribute [StreamRendering]`

**場所**: Razorコンポーネント（`.razor`ファイル）の先頭

**意味**:
- **ストリーミングレンダリング**を有効にする属性です
- サーバー側でデータの準備ができ次第、段階的にHTMLをクライアントに送信します
- これにより、データ取得に時間がかかる場合でも、初期表示が高速になります

**動作**:
1. まず、ローディング状態のHTMLが送信される
2. データが取得でき次第、その部分だけが更新される
3. ユーザーは待ち時間を感じにくくなる

**例**:
```razor
@page "/weather"
@attribute [StreamRendering]  // ストリーミングレンダリングを有効化

@if (isLoading)
{
    <p>読み込み中...</p>  // 最初にこれが表示される
}
else
{
    <div>データ表示</div>  // データ取得後にこれが表示される
}
```

**使用する場面**:
- API呼び出しなど、非同期処理に時間がかかる場合
- ユーザー体験を向上させたい場合

---

### 3. `@inject Blazor1.Services.WeatherService WeatherService`

**場所**: Razorコンポーネント（`.razor`ファイル）

**意味**:
- **依存性注入**を使用して、`WeatherService`のインスタンスをコンポーネントに注入します
- `@inject`ディレクティブは、DIコンテナからサービスを取得し、指定した変数名で使用できるようにします

**構文**:
```razor
@inject <型名> <変数名>
```

**例**:
```razor
@inject Blazor1.Services.WeatherService WeatherService

@code {
    private async Task LoadData()
    {
        // WeatherServiceを使用してAPIを呼び出す
        var data = await WeatherService.GetWeatherForecastAsync("130010");
    }
}
```

**なぜ必要か**:
- コンポーネント内で直接HttpClientを使用するのではなく、サービス層を通してAPIを呼び出すことで、コードの再利用性と保守性が向上します
- テスト時にモックオブジェクトを注入しやすくなります

---

### 4. `@using Blazor1.Models`

**場所**: Razorコンポーネント（`.razor`ファイル）または`_Imports.razor`

**意味**:
- **名前空間**をインポートして、その名前空間内の型を短い名前で使用できるようにします
- `@using`ディレクティブは、C#の`using`文と同じ役割を果たします

**例**:
```razor
@using Blazor1.Models

@code {
    // 完全修飾名なしで使用できる
    private WeatherApiResponse? weatherData;
    
    // @usingがない場合、以下のように書く必要がある
    // private Blazor1.Models.WeatherApiResponse? weatherData;
}
```

**推奨される使い方**:
- プロジェクト全体で使用する名前空間は`_Imports.razor`に追加
- 特定のコンポーネントでのみ使用する場合は、そのコンポーネントに追加

---

## 実装手順

### ステップ1: モデルクラスの作成

APIのレスポンスを表すモデルクラスを作成します。

**ファイル**: `Blazor1/Models/WeatherApiModels.cs`

```csharp
using System.Text.Json.Serialization;

namespace Blazor1.Models;

public class WeatherApiResponse
{
    [JsonPropertyName("title")]
    public string? Title { get; set; }

    [JsonPropertyName("forecasts")]
    public List<Forecast>? Forecasts { get; set; }

    [JsonPropertyName("location")]
    public Location? Location { get; set; }
}

public class Forecast
{
    [JsonPropertyName("date")]
    public string? Date { get; set; }

    [JsonPropertyName("telop")]
    public string? Telop { get; set; }

    [JsonPropertyName("temperature")]
    public Temperature? Temperature { get; set; }
}

// 他のモデルクラスも同様に定義...
```

**ポイント**:
- `[JsonPropertyName]`属性で、JSONのプロパティ名とC#のプロパティ名をマッピング
- `?`（null許容型）を使用して、APIからデータが返ってこない場合に対応

---

### ステップ2: サービスクラスの作成

APIを呼び出すサービスクラスを作成します。

**ファイル**: `Blazor1/Services/WeatherService.cs`

```csharp
using System.Net.Http.Json;
using Blazor1.Models;

namespace Blazor1.Services;

public class WeatherService
{
    private readonly HttpClient _httpClient;
    private const string ApiBaseUrl = "https://weather.tsukumijima.net/api/forecast";

    // コンストラクタでHttpClientを受け取る（DI）
    public WeatherService(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri("https://weather.tsukumijima.net/");
    }

    public async Task<WeatherApiResponse?> GetWeatherForecastAsync(string cityId)
    {
        try
        {
            // APIを呼び出してJSONを自動的にデシリアライズ
            var response = await _httpClient.GetFromJsonAsync<WeatherApiResponse>(
                $"{ApiBaseUrl}?city={cityId}"
            );
            return response;
        }
        catch (HttpRequestException)
        {
            // ネットワークエラーなどの場合
            return null;
        }
        catch (Exception)
        {
            // その他のエラー
            return null;
        }
    }
}
```

**ポイント**:
- コンストラクタで`HttpClient`を受け取る（DI）
- `GetFromJsonAsync<T>()`を使用して、JSONを自動的にオブジェクトに変換
- エラーハンドリングを実装

---

### ステップ3: サービスの登録

`Program.cs`でサービスをDIコンテナに登録します。

**ファイル**: `Blazor1/Program.cs`

```csharp
var builder = WebApplication.CreateBuilder(args);

// 他のサービス登録...

// HttpClientをWeatherServiceに注入できるように登録
builder.Services.AddHttpClient<Blazor1.Services.WeatherService>();

var app = builder.Build();
// ...
```

**ポイント**:
- `AddHttpClient<T>()`を使用して、HttpClientをサービスに注入
- これにより、`WeatherService`のコンストラクタで`HttpClient`を受け取れるようになる

---

### ステップ4: 名前空間のインポート

`_Imports.razor`に名前空間を追加します（オプション）。

**ファイル**: `Blazor1/Components/_Imports.razor`

```razor
@using Blazor1.Models
@using Blazor1.Services
```

**ポイント**:
- プロジェクト全体で使用する名前空間を追加
- 各コンポーネントで`@using`を書く必要がなくなる

---

### ステップ5: コンポーネントでの使用

Razorコンポーネントでサービスを使用します。

**ファイル**: `Blazor1/Components/Pages/Weather.razor`

```razor
@page "/weather"
@attribute [StreamRendering]  // ストリーミングレンダリングを有効化
@inject WeatherService WeatherService  // サービスを注入
@using Blazor1.Models  // モデルクラスを使用するため

<PageTitle>天気予報</PageTitle>

<h1>天気予報</h1>

@if (isLoading)
{
    <p>読み込み中...</p>
}
else if (weatherData == null)
{
    <p>データの取得に失敗しました。</p>
}
else
{
    <div>
        <h2>@weatherData.Location?.City</h2>
        @foreach (var forecast in weatherData.Forecasts ?? new List<Forecast>())
        {
            <div>
                <p>@forecast.Telop</p>
                <p>最高: @forecast.Temperature?.Max?.Celsius°C</p>
            </div>
        }
    </div>
}

@code {
    private WeatherApiResponse? weatherData;
    private bool isLoading = true;

    protected override async Task OnInitializedAsync()
    {
        // サービスを使用してAPIを呼び出す
        weatherData = await WeatherService.GetWeatherForecastAsync("130010");
        isLoading = false;
    }
}
```

**ポイント**:
- `@inject`でサービスを注入
- `OnInitializedAsync()`でAPIを呼び出す
- ローディング状態を管理
- エラーハンドリングを実装

---

## 完全な実装例

### 1. モデルクラス（WeatherApiModels.cs）

```csharp
using System.Text.Json.Serialization;

namespace Blazor1.Models;

public class WeatherApiResponse
{
    [JsonPropertyName("title")]
    public string? Title { get; set; }

    [JsonPropertyName("forecasts")]
    public List<Forecast>? Forecasts { get; set; }

    [JsonPropertyName("location")]
    public Location? Location { get; set; }
}

public class Forecast
{
    [JsonPropertyName("date")]
    public string? Date { get; set; }

    [JsonPropertyName("dateLabel")]
    public string? DateLabel { get; set; }

    [JsonPropertyName("telop")]
    public string? Telop { get; set; }

    [JsonPropertyName("temperature")]
    public Temperature? Temperature { get; set; }
}

public class Temperature
{
    [JsonPropertyName("min")]
    public TemperatureValue? Min { get; set; }

    [JsonPropertyName("max")]
    public TemperatureValue? Max { get; set; }
}

public class TemperatureValue
{
    [JsonPropertyName("celsius")]
    public string? Celsius { get; set; }

    [JsonPropertyName("fahrenheit")]
    public string? Fahrenheit { get; set; }
}

public class Location
{
    [JsonPropertyName("prefecture")]
    public string? Prefecture { get; set; }

    [JsonPropertyName("city")]
    public string? City { get; set; }
}
```

---

### 2. サービスクラス（WeatherService.cs）

```csharp
using System.Net.Http.Json;
using Blazor1.Models;

namespace Blazor1.Services;

public class WeatherService
{
    private readonly HttpClient _httpClient;
    private const string ApiBaseUrl = "https://weather.tsukumijima.net/api/forecast";

    public WeatherService(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri("https://weather.tsukumijima.net/");
    }

    public async Task<WeatherApiResponse?> GetWeatherForecastAsync(string cityId)
    {
        try
        {
            var response = await _httpClient.GetFromJsonAsync<WeatherApiResponse>(
                $"{ApiBaseUrl}?city={cityId}"
            );
            return response;
        }
        catch (HttpRequestException ex)
        {
            // ログに記録するなど
            Console.WriteLine($"HTTPエラー: {ex.Message}");
            return null;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"エラー: {ex.Message}");
            return null;
        }
    }
}
```

---

### 3. Program.cs

```csharp
using Blazor1.Client.Pages;
using Blazor1.Components;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

// HttpClientをWeatherServiceに注入できるように登録
builder.Services.AddHttpClient<Blazor1.Services.WeatherService>();

var app = builder.Build();

// ミドルウェアの設定...
app.Run();
```

---

### 4. Razorコンポーネント（Weather.razor）

```razor
@page "/weather"
@attribute [StreamRendering]
@inject WeatherService WeatherService
@using Blazor1.Models

<PageTitle>天気予報</PageTitle>

<h1>天気予報</h1>

@if (isLoading)
{
    <p><em>読み込み中...</em></p>
}
else if (weatherData == null)
{
    <div class="alert alert-warning">
        データの取得に失敗しました。
    </div>
}
else
{
    <div>
        <h2>@weatherData.Location?.Prefecture @weatherData.Location?.City</h2>
        
        <div class="row">
            @foreach (var forecast in weatherData.Forecasts ?? new List<Forecast>())
            {
                <div class="col-md-4">
                    <div class="card">
                        <div class="card-body">
                            <h5>@forecast.DateLabel</h5>
                            <p>@forecast.Telop</p>
                            @if (forecast.Temperature?.Max?.Celsius != null)
                            {
                                <p>最高: @forecast.Temperature.Max.Celsius°C</p>
                            }
                            @if (forecast.Temperature?.Min?.Celsius != null)
                            {
                                <p>最低: @forecast.Temperature.Min.Celsius°C</p>
                            }
                        </div>
                    </div>
                </div>
            }
        </div>
    </div>
}

@code {
    private WeatherApiResponse? weatherData;
    private bool isLoading = true;

    protected override async Task OnInitializedAsync()
    {
        weatherData = await WeatherService.GetWeatherForecastAsync("130010");
        isLoading = false;
    }
}
```

---

## データフローの理解

```
1. ユーザーがページにアクセス
   ↓
2. OnInitializedAsync()が呼ばれる
   ↓
3. WeatherService.GetWeatherForecastAsync()が呼ばれる
   ↓
4. HttpClientがAPIにリクエストを送信
   ↓
5. APIからJSONレスポンスが返ってくる
   ↓
6. GetFromJsonAsync<T>()がJSONをオブジェクトに変換
   ↓
7. コンポーネントの状態が更新される
   ↓
8. UIが再レンダリングされる
```

---

## よくある質問

### Q1: なぜHttpClientを直接使わないのか？

**A**: 
- HttpClientは、接続プールの管理やタイムアウト設定など、適切に管理する必要があります
- DIコンテナが管理することで、リソースの効率的な使用とライフサイクル管理が可能になります
- テスト時にモックオブジェクトを注入しやすくなります

### Q2: `@attribute [StreamRendering]`は必須か？

**A**: 
- 必須ではありませんが、推奨されます
- API呼び出しに時間がかかる場合、ユーザー体験が向上します
- データ取得中でも、ローディング状態を表示できるため、ページが「固まっている」ように見えません

### Q3: エラーハンドリングはどうする？

**A**: 
- サービスクラスで`try-catch`を使用してエラーを処理
- コンポーネントで`null`チェックを行い、エラー状態を表示
- 必要に応じて、ログ記録やエラーページへのリダイレクトを実装

### Q4: 複数のAPIを呼び出す場合は？

**A**: 
- 各API用に別々のサービスクラスを作成
- `Program.cs`でそれぞれを登録：
  ```csharp
  builder.Services.AddHttpClient<WeatherService>();
  builder.Services.AddHttpClient<NewsService>();
  ```

### Q5: APIキーが必要な場合は？

**A**: 
- `appsettings.json`にAPIキーを保存
- サービスクラスで`IConfiguration`を注入して取得：
  ```csharp
  public WeatherService(HttpClient httpClient, IConfiguration configuration)
  {
      _httpClient = httpClient;
      var apiKey = configuration["WeatherApi:ApiKey"];
      _httpClient.DefaultRequestHeaders.Add("X-API-Key", apiKey);
  }
  ```

---

## まとめ

1. **モデルクラスを作成**: APIのレスポンス構造を表す
2. **サービスクラスを作成**: API呼び出しロジックを実装
3. **サービスを登録**: `Program.cs`で`AddHttpClient<T>()`を使用
4. **コンポーネントで使用**: `@inject`でサービスを注入して使用
5. **ストリーミングレンダリング**: `@attribute [StreamRendering]`でUXを向上

このパターンに従うことで、保守性が高く、テストしやすいコードを書くことができます。

---

**参考リンク**:
- [Blazor公式ドキュメント - HTTP リクエスト](https://learn.microsoft.com/aspnet/core/blazor/call-web-api)
- [依存性注入のベストプラクティス](https://learn.microsoft.com/aspnet/core/fundamentals/dependency-injection)
