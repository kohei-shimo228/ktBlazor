# Blazor ページ作成ガイド

このドキュメントでは、Blazorで新しいページを作成する方法を詳しく説明します。

---

## 目次

1. [基本的な答え](#基本的な答え)
2. [ページ作成の基本手順](#ページ作成の基本手順)
3. [サーバー側とクライアント側の違い](#サーバー側とクライアント側の違い)
4. [最小限のページ作成例](#最小限のページ作成例)
5. [実践的なページ作成例](#実践的なページ作成例)
6. [ルーティングの詳細](#ルーティングの詳細)
7. [よくある質問](#よくある質問)

---

## 基本的な答え

**はい、基本的には `/Pages` に `.razor` ファイルを作成するだけで大丈夫です！**

ただし、以下の点に注意してください：

1. **サーバー側**: `Blazor1/Components/Pages/` に配置
2. **クライアント側**: `Blazor1.Client/Pages/` に配置
3. **`@page` ディレクティブ**を必ず追加する（ルーティングのため）

---

## ページ作成の基本手順

### ステップ1: ファイルを作成

**サーバー側ページの場合**:
```
Blazor1/Components/Pages/YourPage.razor
```

**クライアント側ページの場合**:
```
Blazor1.Client/Pages/YourPage.razor
```

### ステップ2: 最小限のコードを記述

```razor
@page "/your-page"

<PageTitle>Your Page</PageTitle>

<h1>Your Page</h1>
<p>This is your page content.</p>
```

### ステップ3: 完了！

これだけで `/your-page` にアクセスできます。

---

## サーバー側とクライアント側の違い

### サーバー側ページ (`Blazor1/Components/Pages/`)

**特徴**:
- サーバー側でレンダリングされる
- デフォルトで静的レンダリング（Static）
- サーバーリソースに直接アクセス可能
- インタラクティブにするには `@rendermode` を指定

**使用例**:
- データベースからデータを取得するページ
- サーバー側のロジックが必要なページ
- 初期表示が重要なページ

**例**:
```razor
@page "/weather"
@attribute [StreamRendering]

<PageTitle>Weather</PageTitle>

<h1>Weather</h1>
<!-- サーバー側でデータを取得して表示 -->
```

### クライアント側ページ (`Blazor1.Client/Pages/`)

**特徴**:
- ブラウザ内で実行される（WebAssembly）
- デフォルトでインタラクティブ
- オフライン動作が可能
- サーバーへの負荷が少ない

**使用例**:
- インタラクティブなUIが必要なページ
- クライアント側で完結する処理
- リアルタイムな操作が必要なページ

**例**:
```razor
@page "/counter"
@rendermode InteractiveAuto

<PageTitle>Counter</PageTitle>

<h1>Counter</h1>
<button @onclick="Increment">Click me</button>

@code {
    private int count = 0;
    private void Increment() => count++;
}
```

---

## 最小限のページ作成例

### 例1: シンプルなサーバー側ページ

**ファイル**: `Blazor1/Components/Pages/About.razor`

```razor
@page "/about"

<PageTitle>About</PageTitle>

<h1>About Us</h1>
<p>This is the about page.</p>
```

**結果**: `https://localhost:7172/about` でアクセス可能

---

### 例2: シンプルなクライアント側ページ

**ファイル**: `Blazor1.Client/Pages/Todo.razor`

```razor
@page "/todo"
@rendermode InteractiveAuto

<PageTitle>Todo List</PageTitle>

<h1>Todo List</h1>
<p>Your todo items will appear here.</p>
```

**結果**: `https://localhost:7172/todo` でアクセス可能

---

## 実践的なページ作成例

### 例1: フォーム付きページ（サーバー側）

**ファイル**: `Blazor1/Components/Pages/Contact.razor`

```razor
@page "/contact"

<PageTitle>Contact</PageTitle>

<h1>Contact Us</h1>

<form @onsubmit="HandleSubmit">
    <div class="mb-3">
        <label for="name" class="form-label">Name</label>
        <input type="text" class="form-control" id="name" @bind="name" />
    </div>
    
    <div class="mb-3">
        <label for="email" class="form-label">Email</label>
        <input type="email" class="form-control" id="email" @bind="email" />
    </div>
    
    <div class="mb-3">
        <label for="message" class="form-label">Message</label>
        <textarea class="form-control" id="message" @bind="message" rows="5"></textarea>
    </div>
    
    <button type="submit" class="btn btn-primary">Send</button>
</form>

@if (!string.IsNullOrEmpty(submitMessage))
{
    <div class="alert alert-success mt-3">@submitMessage</div>
}

@code {
    private string name = string.Empty;
    private string email = string.Empty;
    private string message = string.Empty;
    private string submitMessage = string.Empty;

    private void HandleSubmit()
    {
        // フォーム送信処理
        submitMessage = $"Thank you, {name}! Your message has been sent.";
        name = string.Empty;
        email = string.Empty;
        message = string.Empty;
    }
}
```

---

### 例2: データ表示ページ（サーバー側 + API連携）

**ファイル**: `Blazor1/Components/Pages/Products.razor`

```razor
@page "/products"
@attribute [StreamRendering]
@inject HttpClient Http

<PageTitle>Products</PageTitle>

<h1>Products</h1>

@if (products == null)
{
    <p>Loading...</p>
}
else
{
    <div class="row">
        @foreach (var product in products)
        {
            <div class="col-md-4 mb-3">
                <div class="card">
                    <div class="card-body">
                        <h5 class="card-title">@product.Name</h5>
                        <p class="card-text">@product.Description</p>
                        <p class="card-text"><strong>Price: $@product.Price</strong></p>
                    </div>
                </div>
            </div>
        }
    </div>
}

@code {
    private List<Product>? products;

    protected override async Task OnInitializedAsync()
    {
        // APIからデータを取得
        products = await Http.GetFromJsonAsync<List<Product>>("api/products");
    }

    private class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
    }
}
```

---

### 例3: インタラクティブなページ（クライアント側）

**ファイル**: `Blazor1.Client/Pages/Calculator.razor`

```razor
@page "/calculator"
@rendermode InteractiveAuto

<PageTitle>Calculator</PageTitle>

<h1>Calculator</h1>

<div class="calculator">
    <div class="display mb-3">
        <input type="text" class="form-control text-end" value="@display" readonly style="font-size: 2rem;" />
    </div>
    
    <div class="buttons">
        <div class="row mb-2">
            <div class="col-3"><button class="btn btn-secondary w-100" @onclick="Clear">C</button></div>
            <div class="col-3"><button class="btn btn-secondary w-100" @onclick="() => AppendOperator('/')">/</button></div>
            <div class="col-3"><button class="btn btn-secondary w-100" @onclick="() => AppendOperator('*')">*</button></div>
            <div class="col-3"><button class="btn btn-danger w-100" @onclick="Delete">⌫</button></div>
        </div>
        
        <div class="row mb-2">
            <div class="col-3"><button class="btn btn-light w-100" @onclick="() => AppendNumber('7')">7</button></div>
            <div class="col-3"><button class="btn btn-light w-100" @onclick="() => AppendNumber('8')">8</button></div>
            <div class="col-3"><button class="btn btn-light w-100" @onclick="() => AppendNumber('9')">9</button></div>
            <div class="col-3"><button class="btn btn-secondary w-100" @onclick="() => AppendOperator('-')">-</button></div>
        </div>
        
        <div class="row mb-2">
            <div class="col-3"><button class="btn btn-light w-100" @onclick="() => AppendNumber('4')">4</button></div>
            <div class="col-3"><button class="btn btn-light w-100" @onclick="() => AppendNumber('5')">5</button></div>
            <div class="col-3"><button class="btn btn-light w-100" @onclick="() => AppendNumber('6')">6</button></div>
            <div class="col-3"><button class="btn btn-secondary w-100" @onclick="() => AppendOperator('+')">+</button></div>
        </div>
        
        <div class="row mb-2">
            <div class="col-3"><button class="btn btn-light w-100" @onclick="() => AppendNumber('1')">1</button></div>
            <div class="col-3"><button class="btn btn-light w-100" @onclick="() => AppendNumber('2')">2</button></div>
            <div class="col-3"><button class="btn btn-light w-100" @onclick="() => AppendNumber('3')">3</button></div>
            <div class="col-3"><button class="btn btn-primary w-100" @onclick="Calculate" style="height: 100px;">=</button></div>
        </div>
        
        <div class="row">
            <div class="col-6"><button class="btn btn-light w-100" @onclick="() => AppendNumber('0')">0</button></div>
            <div class="col-3"><button class="btn btn-light w-100" @onclick="() => AppendNumber('.')">.</button></div>
            <div class="col-3"></div>
        </div>
    </div>
</div>

@code {
    private string display = "0";
    private string currentValue = "0";
    private char? lastOperator = null;

    private void AppendNumber(char number)
    {
        if (currentValue == "0" && number != '.')
        {
            currentValue = number.ToString();
        }
        else
        {
            currentValue += number;
        }
        display = currentValue;
    }

    private void AppendOperator(char op)
    {
        if (lastOperator.HasValue)
        {
            Calculate();
        }
        lastOperator = op;
        currentValue = display;
        display = "0";
    }

    private void Calculate()
    {
        if (!lastOperator.HasValue) return;

        double a = double.Parse(currentValue);
        double b = double.Parse(display);
        double result = 0;

        switch (lastOperator.Value)
        {
            case '+': result = a + b; break;
            case '-': result = a - b; break;
            case '*': result = a * b; break;
            case '/': result = b != 0 ? a / b : 0; break;
        }

        display = result.ToString();
        currentValue = display;
        lastOperator = null;
    }

    private void Clear()
    {
        display = "0";
        currentValue = "0";
        lastOperator = null;
    }

    private void Delete()
    {
        if (display.Length > 1)
        {
            display = display.Substring(0, display.Length - 1);
        }
        else
        {
            display = "0";
        }
        currentValue = display;
    }
}
```

---

## ルーティングの詳細

### 基本的なルーティング

```razor
@page "/path"
```

**例**:
- `@page "/"` → ルート（`/`）
- `@page "/about"` → `/about`
- `@page "/contact"` → `/contact`

### 複数のルートを定義

1つのページに複数のルートを定義できます：

```razor
@page "/users"
@page "/members"
@page "/people"

<PageTitle>Users</PageTitle>
<h1>Users Page</h1>
```

これで `/users`、`/members`、`/people` のすべてで同じページが表示されます。

### ルートパラメータ

```razor
@page "/user/{UserId:int}"

<PageTitle>User @UserId</PageTitle>

<h1>User ID: @UserId</h1>

@code {
    [Parameter]
    public int UserId { get; set; }
}
```

**ルートパラメータの制約**:
- `{id:int}` - 整数のみ
- `{id:guid}` - GUIDのみ
- `{id:bool}` - ブール値のみ
- `{id:datetime}` - 日時のみ
- `{id:long}` - 長整数のみ

### オプショナルパラメータ

```razor
@page "/search"
@page "/search/{SearchTerm}"

<PageTitle>Search</PageTitle>

@if (!string.IsNullOrEmpty(SearchTerm))
{
    <h1>Searching for: @SearchTerm</h1>
}
else
{
    <h1>Search</h1>
}

@code {
    [Parameter]
    public string? SearchTerm { get; set; }
}
```

### クエリパラメータ

```razor
@page "/products"

<PageTitle>Products</PageTitle>

<h1>Products</h1>
@if (!string.IsNullOrEmpty(Category))
{
    <p>Category: @Category</p>
}

@code {
    [SupplyParameterFromQuery]
    public string? Category { get; set; }
}
```

`/products?category=electronics` のようにアクセスできます。

---

## ページの構成要素

### 必須要素

1. **`@page` ディレクティブ**: ルーティングを定義
2. **HTMLコンテンツ**: ページの表示内容

### 推奨要素

1. **`<PageTitle>`**: ブラウザのタブに表示されるタイトル
2. **`@code` ブロック**: C#コードを記述（必要な場合）

### オプション要素

1. **`@attribute [StreamRendering]`**: ストリーミングレンダリング
2. **`@rendermode InteractiveAuto`**: インタラクティブモード
3. **`@inject`**: サービスの注入
4. **`@using`**: 名前空間のインポート

---

## ナビゲーションメニューに追加

新しいページをナビゲーションメニューに追加するには、`NavMenu.razor` を編集します：

**ファイル**: `Blazor1/Components/Layout/NavMenu.razor`

```razor
<div class="nav-item px-3">
    <NavLink class="nav-link" href="/your-page">
        <span class="bi bi-house-door-fill-nav-menu" aria-hidden="true"></span> Your Page
    </NavLink>
</div>
```

---

## よくある質問

### Q1: サーバー側とクライアント側、どちらに作成すべき？

**A**: 
- **サーバー側**: データベースアクセス、サーバー側のロジックが必要な場合
- **クライアント側**: インタラクティブなUI、リアルタイムな操作が必要な場合
- **どちらでも良い場合**: シンプルな表示ページはサーバー側で十分

### Q2: ページが表示されない場合は？

**A**: 以下を確認してください：
1. `@page` ディレクティブが正しく記述されているか
2. ファイルが正しいディレクトリに配置されているか
   - サーバー側: `Blazor1/Components/Pages/`
   - クライアント側: `Blazor1.Client/Pages/`
3. アプリケーションを再起動したか
4. ルートパスが正しいか（大文字小文字は区別されます）

### Q3: レイアウトを変更したい場合は？

**A**: 
- デフォルトのレイアウトを使用しない場合：
  ```razor
  @page "/your-page"
  @layout AnotherLayout
  ```
- レイアウトを使用しない場合：
  ```razor
  @page "/your-page"
  @layout null
  ```

### Q4: ページ間でデータを渡すには？

**A**: 
- **ルートパラメータ**: URLに含める
- **クエリパラメータ**: `?key=value` 形式
- **状態管理サービス**: 複雑なデータの場合
- **NavigationManager**: プログラムでナビゲーション

### Q5: エラーページはどう作る？

**A**: 
- 404エラー: `NotFound.razor` が自動的に使用されます
- 500エラー: `Error.razor` が自動的に使用されます
- カスタムエラーページ: これらのファイルを編集

---

## まとめ

### 最小限のページ作成

1. **ファイルを作成**: `Blazor1/Components/Pages/YourPage.razor`
2. **`@page` を追加**: `@page "/your-page"`
3. **コンテンツを記述**: HTMLと必要に応じて `@code` ブロック

### チェックリスト

- [ ] `@page` ディレクティブが追加されている
- [ ] ファイルが正しいディレクトリに配置されている
- [ ] `<PageTitle>` が設定されている（推奨）
- [ ] ナビゲーションメニューに追加した（必要に応じて）

これで、新しいページを作成できます！

---

**参考リンク**:
- [Blazor ルーティング公式ドキュメント](https://learn.microsoft.com/aspnet/core/blazor/fundamentals/routing)
- [Blazor コンポーネントの概要](https://learn.microsoft.com/aspnet/core/blazor/components/)
