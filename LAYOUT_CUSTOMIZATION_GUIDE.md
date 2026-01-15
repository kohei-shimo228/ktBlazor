# レイアウトのカスタマイズガイド

レイアウトの配置、デザイン、色を変更するには、以下のファイルを編集します。

## 変更するファイル一覧

### 1. **レイアウトの構造と配置**
- `Blazor1/Components/Layout/MainLayout.razor` - レイアウトのHTML構造

### 2. **レイアウトのスタイル（色・配置）**
- `Blazor1/Components/Layout/MainLayout.razor.css` - レイアウトのスタイル

### 3. **ナビゲーションメニューの構造**
- `Blazor1/Components/Layout/NavMenu.razor` - メニューのHTML構造

### 4. **ナビゲーションメニューのスタイル**
- `Blazor1/Components/Layout/NavMenu.razor.css` - メニューのスタイル

### 5. **グローバルスタイル**
- `Blazor1/wwwroot/app.css` - アプリケーション全体のスタイル

---

## 主要な変更ポイント

### サイドバーの色を変更

**ファイル**: `MainLayout.razor.css`

```css
.sidebar {
    background-image: linear-gradient(180deg, rgb(5, 39, 103) 0%, #3a0647 70%);
}
```

**変更例:**
```css
/* 単色にする場合 */
.sidebar {
    background-color: #2c3e50; /* ダークグレー */
}

/* 別のグラデーションにする場合 */
.sidebar {
    background-image: linear-gradient(180deg, #3498db 0%, #2980b9 100%);
}

/* カスタムグラデーション */
.sidebar {
    background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
}
```

### サイドバーの幅を変更

**ファイル**: `MainLayout.razor.css`

```css
@media (min-width: 641px) {
    .sidebar {
        width: 250px; /* ここを変更 */
    }
}
```

### トップバー（上部バー）の色を変更

**ファイル**: `MainLayout.razor.css`

```css
.top-row {
    background-color: #f7f7f7; /* ここを変更 */
    border-bottom: 1px solid #d6d5d5;
}
```

**変更例:**
```css
.top-row {
    background-color: #34495e; /* ダークグレー */
    color: white;
    border-bottom: 1px solid #2c3e50;
}
```

### ナビゲーションメニューの色を変更

**ファイル**: `NavMenu.razor.css`

```css
/* 通常のリンクの色 */
.nav-item ::deep .nav-link {
    color: #d7d7d7; /* ここを変更 */
}

/* アクティブなリンクの背景色 */
.nav-item ::deep a.active {
    background-color: rgba(255,255,255,0.37); /* ここを変更 */
    color: white;
}

/* ホバー時の背景色 */
.nav-item ::deep .nav-link:hover {
    background-color: rgba(255,255,255,0.1); /* ここを変更 */
    color: white;
}
```

**変更例:**
```css
.nav-item ::deep .nav-link {
    color: #ecf0f1;
}

.nav-item ::deep a.active {
    background-color: #3498db;
    color: white;
}

.nav-item ::deep .nav-link:hover {
    background-color: #2980b9;
    color: white;
}
```

### メインコンテンツエリアの背景色を変更

**ファイル**: `MainLayout.razor.css` または `app.css`

```css
main {
    flex: 1;
    background-color: #ffffff; /* 追加 */
}
```

### 全体のフォントを変更

**ファイル**: `app.css`

```css
html, body {
    font-family: 'Helvetica Neue', Helvetica, Arial, sans-serif; /* ここを変更 */
}
```

**変更例:**
```css
html, body {
    font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
}

/* またはGoogle Fontsを使用 */
@import url('https://fonts.googleapis.com/css2?family=Noto+Sans+JP:wght@400;700&display=swap');

html, body {
    font-family: 'Noto Sans JP', sans-serif;
}
```

### リンクの色を変更

**ファイル**: `app.css`

```css
a, .btn-link {
    color: #006bb7; /* ここを変更 */
}
```

### ボタンの色を変更

**ファイル**: `app.css`

```css
.btn-primary {
    color: #fff;
    background-color: #1b6ec2; /* ここを変更 */
    border-color: #1861ac; /* ここを変更 */
}
```

### レイアウトの配置を変更（サイドバーの位置など）

**ファイル**: `MainLayout.razor.css`

```css
@media (min-width: 641px) {
    .page {
        flex-direction: row; /* row: 横並び（サイドバー左）, column: 縦並び */
    }
}
```

**サイドバーを右側に配置する場合:**
```css
@media (min-width: 641px) {
    .page {
        flex-direction: row-reverse; /* サイドバーを右側に */
    }
}
```

### コンテンツエリアのパディングを変更

**ファイル**: `MainLayout.razor.css`

```css
@media (min-width: 641px) {
    .top-row, article {
        padding-left: 2rem !important; /* ここを変更 */
        padding-right: 1.5rem !important; /* ここを変更 */
    }
}
```

**ファイル**: `app.css`

```css
.content {
    padding-top: 1.1rem; /* ここを変更 */
}
```

---

## 実践例：ダークテーマに変更

### 1. サイドバーをダークグレーに

**`MainLayout.razor.css`**:
```css
.sidebar {
    background-color: #1e1e1e;
}
```

### 2. トップバーをダークに

**`MainLayout.razor.css`**:
```css
.top-row {
    background-color: #2d2d2d;
    border-bottom: 1px solid #3d3d3d;
    color: white;
}
```

### 3. メインコンテンツエリアをダークに

**`MainLayout.razor.css`**:
```css
main {
    flex: 1;
    background-color: #1a1a1a;
    color: #e0e0e0;
}
```

### 4. ナビゲーションメニューを調整

**`NavMenu.razor.css`**:
```css
.nav-item ::deep .nav-link {
    color: #b0b0b0;
}

.nav-item ::deep a.active {
    background-color: #3a3a3a;
    color: white;
}

.nav-item ::deep .nav-link:hover {
    background-color: #2a2a2a;
    color: white;
}
```

---

## 実践例：ライトテーマ（明るい色）に変更

### 1. サイドバーを明るい色に

**`MainLayout.razor.css`**:
```css
.sidebar {
    background-color: #f8f9fa;
    border-right: 1px solid #dee2e6;
}
```

### 2. ナビゲーションメニューを調整

**`NavMenu.razor.css`**:
```css
.nav-item ::deep .nav-link {
    color: #495057;
}

.nav-item ::deep a.active {
    background-color: #e9ecef;
    color: #007bff;
}

.nav-item ::deep .nav-link:hover {
    background-color: #f1f3f5;
    color: #0056b3;
}
```

---

## レイアウト構造の変更

### サイドバーを削除してフルスクリーンレイアウトにする

**`MainLayout.razor`**:
```razor
@inherits LayoutComponentBase

<main>
    <div class="top-row px-4">
        <a href="https://learn.microsoft.com/aspnet/core/" target="_blank">About</a>
    </div>

    <article class="content px-4">
        @Body
    </article>
</main>
```

**`MainLayout.razor.css`**:
```css
.page {
    position: relative;
    display: flex;
    flex-direction: column;
}

main {
    flex: 1;
    width: 100%;
}
```

### ヘッダーを追加する

**`MainLayout.razor`**:
```razor
@inherits LayoutComponentBase

<header class="app-header">
    <h1>My Application</h1>
</header>

<div class="page">
    <div class="sidebar">
        <NavMenu />
    </div>

    <main>
        <article class="content px-4">
            @Body
        </article>
    </main>
</div>
```

**`MainLayout.razor.css`**に追加:
```css
.app-header {
    background-color: #2c3e50;
    color: white;
    padding: 1rem;
    text-align: center;
}
```

---

## カスタムCSS変数を使用する

**`app.css`**に追加:
```css
:root {
    --sidebar-bg: #2c3e50;
    --sidebar-text: #ecf0f1;
    --topbar-bg: #34495e;
    --main-bg: #ffffff;
    --link-color: #3498db;
    --primary-btn: #3498db;
}
```

**`MainLayout.razor.css`**で使用:
```css
.sidebar {
    background-color: var(--sidebar-bg);
    color: var(--sidebar-text);
}

.top-row {
    background-color: var(--topbar-bg);
}
```

---

## レスポンシブデザインの調整

### モバイル表示のブレークポイントを変更

**`MainLayout.razor.css`**:
```css
/* デフォルト: 640.98px */
@media (max-width: 640.98px) {
    /* モバイル用スタイル */
}

/* タブレット以上: 641px */
@media (min-width: 641px) {
    /* デスクトップ用スタイル */
}
```

### カスタムブレークポイント
```css
/* タブレット用 */
@media (min-width: 768px) {
    .sidebar {
        width: 200px;
    }
}

/* デスクトップ用 */
@media (min-width: 1024px) {
    .sidebar {
        width: 300px;
    }
}
```

---

## まとめ

### 変更するファイルの優先順位

1. **`MainLayout.razor.css`** - レイアウト全体の色・配置
2. **`NavMenu.razor.css`** - ナビゲーションメニューの色
3. **`app.css`** - グローバルな色・フォント
4. **`MainLayout.razor`** - レイアウト構造の変更
5. **`NavMenu.razor`** - メニュー構造の変更

### よく変更する項目

- ✅ サイドバーの背景色: `MainLayout.razor.css` の `.sidebar`
- ✅ ナビゲーションメニューの色: `NavMenu.razor.css` の `.nav-link`
- ✅ トップバーの色: `MainLayout.razor.css` の `.top-row`
- ✅ 全体のフォント: `app.css` の `html, body`
- ✅ リンクの色: `app.css` の `a, .btn-link`
- ✅ ボタンの色: `app.css` の `.btn-primary`

変更後は、ブラウザをリロード（またはホットリロード）して確認してください。
