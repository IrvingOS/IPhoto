# IPhoto

## 介绍
在线相册

线上地址：https://photo.isopen.top

Gitee 仓库：[IrvingOS/IPhoto (gitee.com)](https://gitee.com/Irvingsoft/iphoto)

## 软件架构

1. 工具：Visual studio 2019

2. 开发框架：[.Net core 5.0 web mvc](https://docs.microsoft.com/zh-cn/aspnet/core/getting-started/?view=aspnetcore-5.0&tabs=windows)

3. 数据库：Mysql 8.0.21

4. ORM 框架：[Sqlsugar](https://www.donet5.com/Home/Doc)

5. 前端框架：[Bootstrap 4.0](https://v4.bootcss.com/docs/getting-started/introduction/)

6. 前端插件：[Bootstrap-fileinput](http://bootstrap-fileinput.com/)、[masonry（ImageLoaded）](https://imagesloaded.desandro.com/)、[fontawesome](http://www.fontawesome.com.cn/)

7. 项目依赖包（NuGet 包）
    
![输入图片说明](https://images.gitee.com/uploads/images/2021/0424/134236_eb501213_7701512.png "image-20210424131826495.png")

## 包结构

```
D:.
├─Areas // Microsoft .Net core Identity
│  └─Identity
│      ├─Data
│      └─Pages
│          └─Account
│              └─Manage
├─Common // 通用工具类包
│  ├─ApiResult
│  └─MessageSender
├─Controllers // 控制层（Action 与 Api）
├─Migrations // 原先使用 LocalDB 的遗留的数据库迁移数据，没有删除，也不清楚删除后的状况
├─Models // 模型层
├─Properties // 项目启动参数
├─Repositories // 数据库层，主要负责数据库操作。同 dao 层。结构为 基类 + 接口 + 实现类
│  └─Impl
├─Services // 服务层，主要负责业务逻辑处理。
│  └─Impl // 此项目业务逻辑简单，所以服务层直接负责调用数据库层，业务逻辑交由控制层处理
├─Views // 视图层
│  ├─Home // 首页视图
│  ├─Shared // 公共视图
│  └─User // 用户中心视图
└─wwwroot // 静态资源文件夹
    ├─css
    ├─image
    ├─js
    ├─lib
    │  ├─bootstrap
    │  │  └─dist
    │  │      ├─css
    │  │      └─js
    │  ├─bootstrap-fileinput
    │  │  └─5.1.5
    │  │      └─content
    │  │          ├─Content
    │  │          │  └─bootstrap-fileinput
    │  │          │      ├─css
    │  │          │      ├─img
    │  │          │      ├─scss
    │  │          │      │  └─themes
    │  │          │      │      ├─explorer
    │  │          │      │      ├─explorer-fa
    │  │          │      │      └─explorer-fas
    │  │          │      └─themes
    │  │          │          ├─explorer
    │  │          │          ├─explorer-fa
    │  │          │          ├─explorer-fas
    │  │          │          ├─fa
    │  │          │          ├─fas
    │  │          │          └─gly
    │  │          └─Scripts
    │  │              ├─locales
    │  │              └─plugins
    │  ├─jquery
    │  │  └─dist
    │  ├─jquery-validation
    │  │  └─dist
    │  └─jquery-validation-unobtrusive
    ├─Photos // 存储项目中所有图片的文件夹
    └─webfonts // fontawesome 图标库的依赖
```



## 安装教程

### 环境准备

Windows 下工具选择 2019 或 2017 并安装 ASP.NET 相应模块皆可。

Linux 下工具选择 VScode 并安装 dotnet （[macOS](https://docs.microsoft.com/zh-cn/dotnet/core/install/macos)， [Linux](https://docs.microsoft.com/zh-cn/dotnet/core/install/linux)）

### 数据库

安装 Mysql 8.+ 版本，安装一个数据库工具软件（Navicat Premium，建议支持正版）并连接到你已安装的本地 Mysql。通过数据库工具建立一个名为 IPhoto 的数据库，在表中运行项目根目录中的 IPhoto.sql 文件。

此时你的数据库结构应该跟下图一样：

![输入图片说明](https://images.gitee.com/uploads/images/2021/0424/134333_7a7cf5e6_7701512.png "image-20210424124629829.png")

把以下两个数据库连接字符串中的用户名（Uid）和密码（Pwd）配置成本机 Mysql 安装时配置的用户名和密码。

![输入图片说明](https://images.gitee.com/uploads/images/2021/0424/134412_f3771a8a_7701512.png "image-20210424120537273.png")
![输入图片说明](https://images.gitee.com/uploads/images/2021/0424/134450_6a691f63_7701512.png "image-20210424120738253.png")


这时候可以启动项目并成功进入首页。

> **如何启动项目？**
>
> Windows 下在调试控制栏中选择启动类型（IIS、IPhoto）
>
> ![输入图片说明](https://images.gitee.com/uploads/images/2021/0424/134509_8aaf0c27_7701512.png "image-20210424122917969.png")
>
> macOS、Linux 下在终端（终端的目录为 IPhoto）中输入：
>
>  ```shell
> dotnet run
>  ```
>
> **可能出现的报错：**
>
> ![输入图片说明](https://images.gitee.com/uploads/images/2021/0424/134526_454baabf_7701512.png "image-20210424130248962.png")
>
> 1. 数据库连接字符串的用户名或密码有误
> 2. 数据库的密码类型有误（可以从网上搜索到相应的解决办法）

### 配置邮件服务

在邮件发送工具类中配置你的邮箱信息。

​![输入图片说明](https://images.gitee.com/uploads/images/2021/0424/134611_2161596f_7701512.png "image-20210424123317566.png")

其中需要替换的部分为第 21 行、33 行、34 行。分别替换成你的邮箱地址、所属的邮箱服务器和端口号、邮箱的 SMTP 密钥。

### 账号注册

配置好邮箱，在注册中输入你的邮箱地址、密码和确认密码后，如果邮箱配置正确，你会在你的邮箱中收到一封确认邮件，点击其中的连接完成邮箱确认。

### 初次登录

**报错报错报错！**

在 `Views/Shared/_LoginPartial.cshtml` 页面中可以看到调用了一个获取用户头像的方法：`@fileService.GetHeadPhotoByUserId(UserManager.GetUserId(User)).Result.Path`

![输入图片说明](https://images.gitee.com/uploads/images/2021/0424/134631_fb595a6c_7701512.png "image-20210424124030398.png")

转到这个方法，可以看到判断用户的头像文件 ID 是否为空，为空则直接根据性别（Gender）获取数据库中男性和女性对应的文件记录。

![输入图片说明](https://images.gitee.com/uploads/images/2021/0424/134650_4225e5ad_7701512.png "image-20210424124309814.png")

报错的原因是没有在数据库的 File 表中插入两条默认的头像 File 记录（注意非空字段的填充），如下：

![输入图片说明](https://images.gitee.com/uploads/images/2021/0424/134703_5686c545_7701512.png "image-20210424125133585.png")

当然，你也可以改写 `GetHeadPhotoByUserId` 方法，先判断默认的文件 ID 是否存在，存在则返回文件信息，不存在则返回空。

**恭喜你或许可以成功登录了！**

## 插件说明

### Sqlsugar

国人开发的 .Net 生态中的 ORM （对象关系映射）框架。主要负责实体类与数据库表的映射关系并提供通用的查询接口。非常之好用！

好处：简化数据库操作，无需手写 SQL 语句，只需要开发人员专注于业务逻辑。

### Bootstrap

前端框架。主要使用其组件及栅格布局系统。

好处：美化界面、可以做各种屏幕尺寸的 UI 适配。

### Bootstrap-fileinput

Bootstrap 提供的文件上传组件，异步调用后端 Api 接口，并提供很多事件（fileuploaded 等待），简化文件上传的操作。功能丰富，提供文件预览、分片上传等众多功能。

### masonry（ImageLoaded）

首页的图片加载动画由它实现。简单、美观、好用！

### fontawesome

图标库，项目中的性别图标由它提供给。是作为 Bootstrap 自带图标库的补充。

## 待优化

1. 首页通过滚动监听实现分页查询。
2. js 与 css 文件加载优化，当前项目所有的 js 与 css 几乎都在首页加载，正确的方式是根据页面使用加载。作者在实现根据页面使用加载时出现不能正常加载 js 与 css 的 BUG，时间关系没能解决。
3. 用户中心页面 UI 优化，类似于 CSDN 的用户中心。
4. 用户关注功能、图片评论功能的实现。

