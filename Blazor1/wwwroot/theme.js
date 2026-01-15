// テーマ管理用のJavaScript
(function () {
    'use strict';

    let themeButtonListener = null;

    // テーマを適用する関数
    function applyTheme(theme) {
        document.documentElement.setAttribute('data-theme', theme);
        localStorage.setItem('theme', theme);
        updateThemeIcon(theme);
    }

    // テーマアイコンを更新する関数
    function updateThemeIcon(theme) {
        const iconElement = document.getElementById('theme-icon');
        const buttonElement = document.getElementById('theme-toggle-btn');
        if (iconElement) {
            iconElement.textContent = theme === 'dark' ? '☀️' : '🌙';
        }
        if (buttonElement) {
            buttonElement.title = theme === 'dark' ? 'ライトモードに切り替え' : 'ダークモードに切り替え';
        }
    }

    // 現在のテーマを取得する関数
    function getCurrentTheme() {
        return document.documentElement.getAttribute('data-theme') || 'light';
    }

    // テーマを切り替える関数
    function toggleTheme() {
        const currentTheme = getCurrentTheme();
        const newTheme = currentTheme === 'dark' ? 'light' : 'dark';
        applyTheme(newTheme);
    }

    // テーマ切り替えボタンのイベントリスナーを設定
    function setupThemeToggle() {
        const button = document.getElementById('theme-toggle-btn');
        if (button) {
            // 既存のリスナーを削除
            if (themeButtonListener) {
                button.removeEventListener('click', themeButtonListener);
                themeButtonListener = null;
            }
            
            // 新しいリスナーを追加
            themeButtonListener = function(e) {
                e.preventDefault();
                e.stopPropagation();
                toggleTheme();
            };
            button.addEventListener('click', themeButtonListener);
            
            // 現在のテーマに合わせてアイコンを更新
            const currentTheme = getCurrentTheme();
            updateThemeIcon(currentTheme);
        }
    }

    // テーマを初期化（既にheadで設定されている場合はそれを尊重）
    function initializeTheme() {
        const savedTheme = localStorage.getItem('theme');
        const currentTheme = getCurrentTheme();
        
        // 保存されたテーマがある場合はそれを使用、なければ現在のテーマを維持
        if (savedTheme && savedTheme !== currentTheme) {
            applyTheme(savedTheme);
        } else if (!savedTheme) {
            // 保存されていない場合は現在のテーマを保存
            localStorage.setItem('theme', currentTheme);
            updateThemeIcon(currentTheme);
        } else {
            // 既に正しいテーマが設定されている場合はアイコンのみ更新
            updateThemeIcon(currentTheme);
        }
    }

    // ページ読み込み時に初期化
    function init() {
        initializeTheme();
        setupThemeToggle();
        
        // 定期的にテーマボタンの存在をチェック（ページ遷移対応）- 軽量化
        setInterval(function() {
            const button = document.getElementById('theme-toggle-btn');
            if (button) {
                // ボタンが存在するがリスナーが設定されていない場合
                if (!themeButtonListener) {
                    setupThemeToggle();
                }
                // テーマを再適用（ページ遷移時にリセットされる可能性があるため）
                const savedTheme = localStorage.getItem('theme');
                if (savedTheme) {
                    const currentTheme = getCurrentTheme();
                    if (savedTheme !== currentTheme) {
                        document.documentElement.setAttribute('data-theme', savedTheme);
                        applyTheme(savedTheme);
                    } else {
                        updateThemeIcon(currentTheme);
                    }
                }
            }
        }, 500); // 500ms間隔に変更（軽量化）
        
        // 動的に追加されるボタンにも対応（軽量なMutationObserver）
        let observer = null;
        if (document.body) {
            observer = new MutationObserver(function(mutations) {
                // ボタンが追加された場合のみ設定
                const button = document.getElementById('theme-toggle-btn');
                if (button && !themeButtonListener) {
                    setupThemeToggle();
                }
            });
            
            observer.observe(document.body, {
                childList: true,
                subtree: false // subtreeをfalseに変更（軽量化）
            });
        }
    }

    // 即座に実行（DOMContentLoadedを待たない）
    if (document.readyState === 'loading') {
        document.addEventListener('DOMContentLoaded', init);
    } else {
        init();
    }

    // グローバル関数として公開（デバッグ用）
    window.toggleTheme = toggleTheme;
    window.getCurrentTheme = getCurrentTheme;
})();
