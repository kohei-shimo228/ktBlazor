// ページ遷移時のテーマ維持（軽量版・イベントベース）
(function() {
    'use strict';
    
    // テーマを即座に適用する関数（トランジション無効化付き）
    function applyThemeImmediately() {
        const savedTheme = localStorage.getItem('theme');
        if (savedTheme) {
            // トランジションを一時的に無効化してフラッシュを防ぐ
            document.documentElement.setAttribute('data-transitioning', 'true');
            document.documentElement.setAttribute('data-theme', savedTheme);
            
            // 次のフレームでトランジションを再有効化
            requestAnimationFrame(function() {
                requestAnimationFrame(function() {
                    document.documentElement.removeAttribute('data-transitioning');
                });
            });
        }
    }
    
    // 初期化時にテーマを適用
    applyThemeImmediately();
    
    // リンククリック時にテーマを確認（遷移前に適用）
    document.addEventListener('click', function(e) {
        const target = e.target.closest('a');
        if (target && target.href && !target.target && !target.hasAttribute('download')) {
            // リンクがクリックされたら、遷移前にテーマを確認・適用
            applyThemeImmediately();
        }
    }, true); // キャプチャフェーズで実行
    
    // ページ遷移時にテーマを再適用
    window.addEventListener('pageshow', function() {
        applyThemeImmediately();
    });
    
    // History APIを監視（軽量）
    window.addEventListener('popstate', function() {
        applyThemeImmediately();
    });
    
    // pushState/replaceStateを監視（Blazorのナビゲーション対応）
    const originalPushState = history.pushState;
    const originalReplaceState = history.replaceState;
    
    history.pushState = function() {
        originalPushState.apply(history, arguments);
        applyThemeImmediately();
    };
    
    history.replaceState = function() {
        originalReplaceState.apply(history, arguments);
        applyThemeImmediately();
    };
    
    // Blazorのナビゲーション完了を監視（軽量）
    if (window.Blazor) {
        // Blazorが読み込まれた後に監視
        document.addEventListener('DOMContentLoaded', function() {
            // Blazorのルーターが更新されたときにテーマを適用
            setTimeout(function() {
                applyThemeImmediately();
            }, 100);
        });
    }
    
})();
