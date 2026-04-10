# MyQuizApp

Blazor Web App (.NET 8) と PostgreSQL を使用したクイズ管理システムです。
Dockerコンテナ上で、各種アプリケーションが稼働するほか、開発環境もDocker上で行うことができます。

---

## 動作環境

- OS: Windows / macOS / Linux
- Docker: Docker Desktop または Docker Engine
- .NET: .NET 8 SDK (ローカルでの開発・デバッグ時)

---

## クイックスタート

以下の手順に従って、アプリケーションを起動してください。

### 1. リポジトリをクローン
git clone https://github.com/YOUR_USERNAME/MyQuizApp.git
cd MyQuizApp

### 2. 環境変数の準備
.env.example をコピーして .env ファイルを作成します。

cp .env.example .env

※ DB_CONNECTION_STRING 内のパスワードなどは、必要に応じて .env 内で調整してください。

### 3. Docker コンテナの起動
プロジェクトのルートディレクトリで以下のコマンドを実行します。

docker-compose up -d --build


### 4. アプリケーションへアクセス
コンテナ起動後、約 15〜30 秒（DBの初期化およびマイグレーション完了後）でブラウザからアクセス可能になります。
 http://localhost:8080

