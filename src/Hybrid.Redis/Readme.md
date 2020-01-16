# Redis 模块

## 单机用法
如果只是单台服务器使用Redis，可按照如下配置方式使用：
1. 通过nuget引用 `Hybrid.Redis` 程序集
> Install-Moduleage Hybrid.Redis
2. 在 `appsettings.json` 中添加如下配置节点
```
{
  "Hybrid": {
    "Redis": {
      "Configuration": "localhost",
      "InstanceName": "HybridDemo",
      "Enabled": "true" 
    } 
  }
}
```
3. 如果要禁用Redis缓存功能，设置上面的配置 `Enabled: false` 即可。当启用Redis时，会自动清除已有的`IDistributedCache`分布式缓存实现

## 集群用法
如果要使用多服务器使用Redis集群，可通过继承基类`RedisModuleBase`重写，给`Redis.Configuration`属性赋值即可。
