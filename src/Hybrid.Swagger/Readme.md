﻿# Swagger 模块

## 用法
可按照如下配置方式使用：
1. 通过nuget引用 `Hybrid.Swagger` 程序集
> Install-Package HybridNS.Swagger
2. 在 `appsettings.json` 中添加如下配置节点
```
{
  "Swagger": {
    "Title": "Hybrid API",
    "Version": 1,
    "Url": "/swagger/v1/swagger.json",
    "Enabled": true
  } 
}
```
3. 要禁用Swagger，可以设置`Enabled: false`
