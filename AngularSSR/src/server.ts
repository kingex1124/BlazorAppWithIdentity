import { APP_BASE_HREF } from '@angular/common';
import { CommonEngine, isMainModule } from '@angular/ssr/node';
import express from 'express';
import { dirname, join, resolve } from 'node:path';
import { fileURLToPath } from 'node:url';
import https from 'https'; // 引入 https 模組
import AppServerModule from './main.server';

const serverDistFolder = dirname(fileURLToPath(import.meta.url));
const browserDistFolder = resolve(serverDistFolder, '../browser');
const indexHtml = join(serverDistFolder, 'index.server.html');

const app = express();
const commonEngine = new CommonEngine();

// 設定靜態檔案資料夾，讓 Express 提供 Angular 前端檔案
const browserFolder = join(__dirname, 'browser');
app.use(express.static(browserFolder, { maxAge: '1y' }));

// 設定 Express JSON 解析
app.use(express.json());
/**
 * Example Express Rest API endpoints can be defined here.
 * Uncomment and define endpoints as necessary.
 *
 * Example:
 * ```ts
 * app.get('/api/**', (req, res) => {
 *   // Handle API request
 * });
 * ```
 */

// 新增一個路由來處理從前端發送的請求
app.post('/api/calldotnetapi', express.json(), (req, res) => {
  const postData = JSON.stringify(req.body);
  console.log('前端發送的資料：', postData);
  const options = {
    hostname: 'localhost',
    port: 7166,
    path: '/api/Authenticate/Login',
    method: 'POST',
    headers: {
      'Content-Type': 'application/json',
      'Content-Length': Buffer.byteLength(postData),
      'PLATFORM_CODE': 'Notify',
      'PLATFORM_UUID': '21101644666'
    }
  };

  const apiReq = https.request(options, (apiRes) => {
    let data = '';

    apiRes.on('data', (chunk) => {
      data += chunk;
    });

    apiRes.on('end', () => {
      res.json(JSON.parse(data));
    });
  });

  apiReq.on('error', (error) => {
    console.error('伺服器端呼叫 API 時發生錯誤：', error);
    res.status(500).send('伺服器端呼叫 API 時發生錯誤');
  });

  apiReq.write(postData);
  apiReq.end();
});

/**
 * Serve static files from /browser
 */
app.get(
  '**',
  express.static(browserDistFolder, {
    maxAge: '1y',
    index: 'index.html'
  }),
);

/**
 * Handle all other requests by rendering the Angular application.
 */
app.get('**', (req, res, next) => {
  const { protocol, originalUrl, baseUrl, headers } = req;
  console.log("Tag");
  commonEngine
    .render({
      bootstrap: AppServerModule,
      documentFilePath: indexHtml,
      url: `${protocol}://${headers.host}${originalUrl}`,
      publicPath: browserDistFolder,
      providers: [{ provide: APP_BASE_HREF, useValue: baseUrl }],
    })
    .then((html) => res.send(html))
    .catch((err) => next(err));
});



/**
 * Start the server if this module is the main entry point.
 * The server listens on the port defined by the `PORT` environment variable, or defaults to 4000.
 */
if (isMainModule(import.meta.url)) {
  const port = process.env['PORT'] || 4000;
  console.log(port);
  app.listen(port, () => {
    console.log(`Node Express server listening on http://localhost:${port}`);
  });
}

export default app;
