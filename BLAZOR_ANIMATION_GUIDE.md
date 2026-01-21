# Blazor アニメーション適用ガイド

このドキュメントでは、BlazorのRazorページにアニメーションを適用する方法を詳しく説明します。

---

## 目次

1. [アニメーションの基本](#アニメーションの基本)
2. [CSSアニメーションの適用方法](#cssアニメーションの適用方法)
3. [実装例](#実装例)
4. [よく使うアニメーション](#よく使うアニメーション)
5. [パフォーマンスの最適化](#パフォーマンスの最適化)
6. [トラブルシューティング](#トラブルシューティング)

---

## アニメーションの基本

Blazorでアニメーションを適用する主な方法：

1. **CSSアニメーション**（推奨）- 最も一般的でパフォーマンスが良い
2. **CSSトランジション** - シンプルな状態変化に適している
3. **JavaScriptアニメーション** - 複雑なアニメーションが必要な場合

このガイドでは、**CSSアニメーション**を中心に説明します。

---

## CSSアニメーションの適用方法

### 方法1: スコープ付きCSSファイル（推奨）

各Razorコンポーネント専用のCSSファイルを作成します。

**ファイル構造**:
```
Components/Pages/
  ├── YourPage.razor
  └── YourPage.razor.css  ← 同じ名前の.cssファイル
```

**例**: `NotFound.razor` と `NotFound.razor.css`

```razor
<!-- NotFound.razor -->
@page "/not-found"

<div class="animated-container">
    <h1 class="fade-in">Hello</h1>
</div>
```

```css
/* NotFound.razor.css */
.animated-container {
    /* スタイル */
}

.fade-in {
    animation: fadeIn 1s ease-in;
}

@keyframes fadeIn {
    from {
        opacity: 0;
    }
    to {
        opacity: 1;
    }
}
```

**メリット**:
- コンポーネントごとにスタイルが分離される
- 他のコンポーネントに影響しない
- メンテナンスが容易

---

### 方法2: グローバルCSSファイル

プロジェクト全体で使用するアニメーションは、`wwwroot/app.css` に追加します。

**ファイル**: `wwwroot/app.css`

```css
/* グローバルアニメーション */
@keyframes fadeIn {
    from {
        opacity: 0;
    }
    to {
        opacity: 1;
    }
}

.fade-in-global {
    animation: fadeIn 1s ease-in;
}
```

**使用例**:
```razor
<div class="fade-in-global">
    <h1>Hello</h1>
</div>
```

---

### 方法3: インラインスタイル（非推奨）

簡単なアニメーションの場合、インラインスタイルも使用できますが、推奨されません。

```razor
<div style="animation: fadeIn 1s;">
    <h1>Hello</h1>
</div>
```

---

## 実装例

### 例1: 404エラーページのアニメーション

**ファイル**: `Components/Pages/NotFound.razor`

```razor
@page "/not-found"
@layout MainLayout

<PageTitle>404 - Not Found</PageTitle>

<div class="not-found-container">
    <div class="error-code">
        <span class="digit" style="animation-delay: 0s">4</span>
        <span class="digit" style="animation-delay: 0.1s">0</span>
        <span class="digit" style="animation-delay: 0.2s">4</span>
    </div>
    <h1 class="error-title">Page Not Found</h1>
    <p class="error-message">申し訳ございませんが、お探しのページは存在しません。</p>
    <div class="error-actions">
        <a href="/" class="btn btn-primary">ホームに戻る</a>
    </div>
</div>
```

**ファイル**: `Components/Pages/NotFound.razor.css`

```css
.not-found-container {
    display: flex;
    flex-direction: column;
    align-items: center;
    justify-content: center;
    min-height: 60vh;
    text-align: center;
    padding: 2rem;
}

.error-code {
    display: flex;
    gap: 1rem;
    margin-bottom: 2rem;
}

.digit {
    font-size: 8rem;
    font-weight: bold;
    color: var(--link-color, #006bb7);
    display: inline-block;
    animation: bounceIn 0.6s ease-out forwards;
    opacity: 0;
    transform: scale(0) rotate(-180deg);
}

.error-title {
    font-size: 2rem;
    margin-bottom: 1rem;
    animation: fadeInUp 0.8s ease-out 0.3s forwards;
    opacity: 0;
    transform: translateY(20px);
}

.error-message {
    font-size: 1.2rem;
    margin-bottom: 2rem;
    animation: fadeInUp 0.8s ease-out 0.5s forwards;
    opacity: 0;
    transform: translateY(20px);
}

.error-actions {
    animation: fadeInUp 0.8s ease-out 0.7s forwards;
    opacity: 0;
    transform: translateY(20px);
}

@keyframes bounceIn {
    0% {
        opacity: 0;
        transform: scale(0) rotate(-180deg);
    }
    50% {
        transform: scale(1.2) rotate(10deg);
    }
    100% {
        opacity: 1;
        transform: scale(1) rotate(0deg);
    }
}

@keyframes fadeInUp {
    0% {
        opacity: 0;
        transform: translateY(20px);
    }
    100% {
        opacity: 1;
        transform: translateY(0);
    }
}
```

---

### 例2: フェードインアニメーション

**ファイル**: `Components/Pages/YourPage.razor`

```razor
@page "/your-page"

<div class="fade-in-container">
    <h1 class="fade-in">タイトル</h1>
    <p class="fade-in-delay">コンテンツ</p>
</div>
```

**ファイル**: `Components/Pages/YourPage.razor.css`

```css
.fade-in-container {
    padding: 2rem;
}

.fade-in {
    animation: fadeIn 1s ease-in forwards;
    opacity: 0;
}

.fade-in-delay {
    animation: fadeIn 1s ease-in 0.3s forwards;
    opacity: 0;
}

@keyframes fadeIn {
    from {
        opacity: 0;
    }
    to {
        opacity: 1;
    }
}
```

---

### 例3: スライドインアニメーション

**ファイル**: `Components/Pages/YourPage.razor.css`

```css
.slide-in-left {
    animation: slideInLeft 0.5s ease-out forwards;
    opacity: 0;
    transform: translateX(-50px);
}

.slide-in-right {
    animation: slideInRight 0.5s ease-out forwards;
    opacity: 0;
    transform: translateX(50px);
}

.slide-in-up {
    animation: slideInUp 0.5s ease-out forwards;
    opacity: 0;
    transform: translateY(50px);
}

@keyframes slideInLeft {
    to {
        opacity: 1;
        transform: translateX(0);
    }
}

@keyframes slideInRight {
    to {
        opacity: 1;
        transform: translateX(0);
    }
}

@keyframes slideInUp {
    to {
        opacity: 1;
        transform: translateY(0);
    }
}
```

---

### 例4: ホバーアニメーション

**ファイル**: `Components/Pages/YourPage.razor.css`

```css
.animated-button {
    transition: all 0.3s ease;
    transform: scale(1);
}

.animated-button:hover {
    transform: scale(1.1);
    box-shadow: 0 4px 8px rgba(0, 0, 0, 0.2);
}

.animated-card {
    transition: transform 0.3s ease, box-shadow 0.3s ease;
}

.animated-card:hover {
    transform: translateY(-5px);
    box-shadow: 0 8px 16px rgba(0, 0, 0, 0.2);
}
```

---

### 例5: ローディングアニメーション

**ファイル**: `Components/Pages/YourPage.razor`

```razor
@if (isLoading)
{
    <div class="loading-spinner">
        <div class="spinner"></div>
    </div>
}
```

**ファイル**: `Components/Pages/YourPage.razor.css`

```css
.loading-spinner {
    display: flex;
    justify-content: center;
    align-items: center;
    min-height: 200px;
}

.spinner {
    width: 50px;
    height: 50px;
    border: 4px solid #f3f3f3;
    border-top: 4px solid var(--link-color, #006bb7);
    border-radius: 50%;
    animation: spin 1s linear infinite;
}

@keyframes spin {
    0% {
        transform: rotate(0deg);
    }
    100% {
        transform: rotate(360deg);
    }
}
```

---

### 例6: ページ遷移時のアニメーション

**ファイル**: `Components/Pages/YourPage.razor`

```razor
@page "/your-page"

<div class="page-transition">
    <h1>ページコンテンツ</h1>
</div>
```

**ファイル**: `Components/Pages/YourPage.razor.css`

```css
.page-transition {
    animation: pageFadeIn 0.5s ease-in;
}

@keyframes pageFadeIn {
    from {
        opacity: 0;
        transform: translateY(20px);
    }
    to {
        opacity: 1;
        transform: translateY(0);
    }
}
```

---

## よく使うアニメーション

### 1. フェードイン

```css
@keyframes fadeIn {
    from {
        opacity: 0;
    }
    to {
        opacity: 1;
    }
}

.fade-in {
    animation: fadeIn 0.5s ease-in;
}
```

### 2. スライドイン

```css
@keyframes slideIn {
    from {
        transform: translateX(-100%);
        opacity: 0;
    }
    to {
        transform: translateX(0);
        opacity: 1;
    }
}

.slide-in {
    animation: slideIn 0.5s ease-out;
}
```

### 3. バウンス

```css
@keyframes bounce {
    0%, 20%, 50%, 80%, 100% {
        transform: translateY(0);
    }
    40% {
        transform: translateY(-30px);
    }
    60% {
        transform: translateY(-15px);
    }
}

.bounce {
    animation: bounce 1s;
}
```

### 4. パルス

```css
@keyframes pulse {
    0%, 100% {
        transform: scale(1);
    }
    50% {
        transform: scale(1.1);
    }
}

.pulse {
    animation: pulse 2s infinite;
}
```

### 5. ローテーション

```css
@keyframes rotate {
    from {
        transform: rotate(0deg);
    }
    to {
        transform: rotate(360deg);
    }
}

.rotate {
    animation: rotate 2s linear infinite;
}
```

### 6. シェイク（エラー表示など）

```css
@keyframes shake {
    0%, 100% {
        transform: translateX(0);
    }
    10%, 30%, 50%, 70%, 90% {
        transform: translateX(-10px);
    }
    20%, 40%, 60%, 80% {
        transform: translateX(10px);
    }
}

.shake {
    animation: shake 0.5s;
}
```

---

## アニメーションのプロパティ

### animation プロパティの構文

```css
animation: name duration timing-function delay iteration-count direction fill-mode;
```

**例**:
```css
.fade-in {
    animation: fadeIn 1s ease-in 0.5s 1 normal forwards;
}
```

**各プロパティの説明**:
- `name`: アニメーション名（@keyframesで定義）
- `duration`: アニメーションの長さ（例: `1s`, `500ms`）
- `timing-function`: イージング関数（例: `ease`, `ease-in`, `ease-out`, `linear`）
- `delay`: アニメーション開始の遅延（例: `0.5s`）
- `iteration-count`: 繰り返し回数（例: `1`, `infinite`）
- `direction`: アニメーションの方向（例: `normal`, `reverse`, `alternate`）
- `fill-mode`: アニメーション前後の状態（例: `forwards`, `backwards`, `both`）

---

## パフォーマンスの最適化

### 1. transform と opacity を使用

パフォーマンスが良いプロパティ：
- `transform`（translate, scale, rotate）
- `opacity`

避けるべきプロパティ：
- `width`, `height`
- `top`, `left`
- `margin`, `padding`

**良い例**:
```css
@keyframes slideIn {
    from {
        transform: translateX(-100%);
        opacity: 0;
    }
    to {
        transform: translateX(0);
        opacity: 1;
    }
}
```

**悪い例**:
```css
@keyframes slideIn {
    from {
        left: -100px;
        opacity: 0;
    }
    to {
        left: 0;
        opacity: 1;
    }
}
```

### 2. will-change プロパティ

アニメーションする要素に `will-change` を追加：

```css
.animated-element {
    will-change: transform, opacity;
    animation: fadeIn 1s;
}
```

### 3. アニメーションの無効化（ユーザー設定対応）

```css
@media (prefers-reduced-motion: reduce) {
    * {
        animation: none !important;
        transition: none !important;
    }
}
```

---

## 条件付きアニメーション

Blazorの状態に応じてアニメーションを適用する方法：

### 例: データ読み込み時のアニメーション

```razor
@page "/your-page"

@if (isLoading)
{
    <div class="loading-animation">
        <div class="spinner"></div>
    </div>
}
else if (data != null)
{
    <div class="fade-in">
        <!-- データ表示 -->
    </div>
}
```

```css
.loading-animation {
    animation: pulse 1s infinite;
}

.fade-in {
    animation: fadeIn 0.5s ease-in;
}
```

---

## トラブルシューティング

### 問題1: アニメーションが動作しない

**解決策**:
1. CSSファイルが正しくリンクされているか確認
2. クラス名が正しいか確認
3. `@keyframes` が正しく定義されているか確認
4. ブラウザの開発者ツールでエラーを確認

### 問題2: アニメーションが遅い

**解決策**:
1. `transform` と `opacity` を使用しているか確認
2. 不要なアニメーションを削除
3. `will-change` プロパティを追加

### 問題3: アニメーションが途中で止まる

**解決策**:
1. `animation-fill-mode: forwards` を追加
2. `animation-iteration-count` を確認

### 問題4: スコープ付きCSSが適用されない

**解決策**:
1. ファイル名が `ComponentName.razor.css` になっているか確認
2. プロジェクトを再ビルド
3. ブラウザのキャッシュをクリア

---

## 実践的なテンプレート

### テンプレート1: ページ全体のフェードイン

```razor
@page "/your-page"

<div class="page-container">
    <!-- コンテンツ -->
</div>
```

```css
.page-container {
    animation: pageFadeIn 0.5s ease-in;
}

@keyframes pageFadeIn {
    from {
        opacity: 0;
        transform: translateY(20px);
    }
    to {
        opacity: 1;
        transform: translateY(0);
    }
}
```

### テンプレート2: カードのホバーアニメーション

```css
.card-animated {
    transition: all 0.3s ease;
}

.card-animated:hover {
    transform: translateY(-5px) scale(1.02);
    box-shadow: 0 10px 20px rgba(0, 0, 0, 0.2);
}
```

### テンプレート3: リストアイテムの順次アニメーション

```razor
@foreach (var item in items)
{
    <div class="list-item" style="animation-delay: @(items.IndexOf(item) * 0.1)s">
        @item.Name
    </div>
}
```

```css
.list-item {
    animation: fadeInUp 0.5s ease-out forwards;
    opacity: 0;
}

@keyframes fadeInUp {
    to {
        opacity: 1;
        transform: translateY(0);
    }
}
```

---

## まとめ

1. **スコープ付きCSSファイルを使用**（`.razor.css`）
2. **`transform` と `opacity` を優先**（パフォーマンス）
3. **適切なタイミング関数を選択**（`ease`, `ease-in`, `ease-out`）
4. **アニメーション遅延を活用**（順次表示）
5. **ユーザー設定を考慮**（`prefers-reduced-motion`）

これらのテクニックを活用して、魅力的なアニメーションを実装できます！

---

**参考リンク**:
- [CSS Animations - MDN](https://developer.mozilla.org/en-US/docs/Web/CSS/CSS_Animations)
- [CSS Transitions - MDN](https://developer.mozilla.org/en-US/docs/Web/CSS/CSS_Transitions)
- [Blazor CSS Isolation](https://learn.microsoft.com/aspnet/core/blazor/components/css-isolation)
