# MailKit 模块

## 用法
可按照如下配置方式使用：
1. 通过nuget引用 `LeXun.Hybrid.MailKit` 程序集
> Install-Package LeXun.Hybrid.MailKit
2. 在 `appsettings.json` 中添加如下配置节点
```
{
    "MailKitSender": {
      "Host": "smtp.qq.com",
      "DisplayName": "发件测试", //发送方显示名
      "UserName": "xxx@qq.com",
      "Password": "",
      "Port": 465,
      "EnableSsl": true, //是否启用ssl
      "UseDefaultCredentials": false //是否验证
    }
}
```
3. 在需要的地方使用 IMailKitEmailSender