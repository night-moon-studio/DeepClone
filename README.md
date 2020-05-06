
<p align="center">
  <span>中文</span> |  
  <a href="https://github.com/night-moon-studio/deepclone/tree/master/lang/english">English</a>
</p>

# DeepClone

[![Member project of Night Moon Studio](https://img.shields.io/badge/member%20project%20of-NMS-9e20c9.svg)](https://github.com/night-moon-studio)
[![NuGet Badge](https://buildstats.info/nuget/DotNetCore.Natasha.deepclone?includePreReleases=true)](https://www.nuget.org/packages/DotNetCore.Natasha.deepclone)
 ![GitHub repo size](https://img.shields.io/github/repo-size/night-moon-studio/deepclone.svg)
 [![Gitter](https://badges.gitter.im/NightMoonStudio/DeepClone.svg)](https://gitter.im/NightMoonStudio/DeepClone?utm_source=badge&utm_medium=badge&utm_campaign=pr-badge)
[![Codecov](https://img.shields.io/codecov/c/github/night-moon-studio/deepclone.svg)](https://codecov.io/gh/night-moon-studio/deepclone)
[![Badge](https://img.shields.io/badge/link-996.icu-red.svg)](https://996.icu/#/zh_CN)
[![GitHub license](https://img.shields.io/github/license/night-moon-studio/deepclone.svg)](https://github.com/night-moon-studio/deepclone/blob/master/LICENSE)


<br/>
  

### 持续构建(CI Build Status)  

| CI Platform | Build Server | Master Build  | Master Test |
|--------- |------------- |---------| --------|
| Travis | Linux/OSX | [![Build status](https://travis-ci.org/night-moon-studio/deepclone.svg?branch=master)](https://travis-ci.org/night-moon-studio/deepclone) | |
| AppVeyor | Windows/Linux |[![Build status](https://ci.appveyor.com/api/projects/status/4qwm7p9dpy7agdoa?svg=true)](https://ci.appveyor.com/project/NMSAzulX/deepclone)|[![Build status](https://img.shields.io/appveyor/tests/NMSAzulX/deepclone.svg)](https://ci.appveyor.com/project/NMSAzulX/deepclone)|
| Azure |  Windows |[![Build Status](https://dev.azure.com/NightMoonStudio/DeepClone/_apis/build/status/night-moon-studio.DeepClone?branchName=master&jobName=Windows)](https://dev.azure.com/NightMoonStudio/DeepClone/_build/latest?definitionId=5&branchName=master)|[![Azure DevOps tests](https://img.shields.io/azure-devops/tests/NightMoonStudio/DeepClone/4.svg)](https://dev.azure.com/NightMoonStudio/DeepClone/_build/latest?definitionId=5&branchName=master) |
| Azure |  Linux |[![Build Status](https://dev.azure.com/NightMoonStudio/DeepClone/_apis/build/status/night-moon-studio.DeepClone?branchName=master&jobName=Linux)](https://dev.azure.com/NightMoonStudio/DeepClone/_build/latest?definitionId=5&branchName=master)|[![Azure DevOps tests](https://img.shields.io/azure-devops/tests/NightMoonStudio/DeepClone/4.svg)](https://dev.azure.com/NightMoonStudio/DeepClone/_build/latest?definitionId=5&branchName=master)  | 
| Azure |  Mac |[![Build Status](https://dev.azure.com/NightMoonStudio/DeepClone/_apis/build/status/night-moon-studio.DeepClone?branchName=master&jobName=macOS)](https://dev.azure.com/NightMoonStudio/DeepClone/_build/latest?definitionId=5&branchName=master)|[![Azure DevOps tests](https://img.shields.io/azure-devops/tests/NightMoonStudio/DeepClone/4.svg)](https://dev.azure.com/NightMoonStudio/DeepClone/_build/latest?definitionId=5&branchName=master) | 

<br/>    

### 项目简介： 

此项目为[Natasha](https://github.com/dotnetcore/Natasha)的衍生项目，为用户提供高性能的深度克隆。  

<br/>    

### 团队成员：


#### 使用方法(User Api)：  

 <br/>  
 
 - 引入 动态构件库： NMS.DeepClone

 - 引入 编译环境库： DotNetCore.Compile.Environment

 - 向引擎中注入定制的域： DomainManagement.RegisterDefault< AssemblyDomain >()

 - 敲代码  
 
 
<br/>  


```C#

//非object类型使用
CloneOperator.Clone(instance);

//object类型使用
ObjectCloneOperator.Clone(obj);

```

```C#
//readonly 字段会根据构造函数中参数名，或者通过注解进行匹配

public class A()
{

   public A(string name,int age){ StuName = name; Age = age; }

   [NeedCtor("name")]
   public readonly StuName;

   [NeedCtor]
   public readonly Age;

}
```

### 发布计划： 
  
 - 2019-08-20 ： 发布v1.0.0.0, 高性能动态深度克隆库。  
 
 <br/>  
 
---------------------  
 <br/>  
 

### 使用方法(User Api)：  
 <br/>  
 
#### 引入动态编译环境：

引用 "DotNetCore.Compile.Environment" 就不用下面的了;  
> "DotNetCore.Compile.Environment" 为动态编译提供了依赖环境.
> 构建文件不具备继承性，所以每一个新的工程都需要引用该库。  

```C#

  <PropertyGroup>
  
    <OutputType>Exe</OutputType>
    <TargetFramework>netcoreapp2.2</TargetFramework>
    
    //控制台/桌面如下
    <PreserveCompilationContext>true</PreserveCompilationContext>
    
    //老版WEB需要
    <MvcRazorExcludeRefAssembliesFromPublish>false</MvcRazorExcludeRefAssembliesFromPublish>
    
    //3.1 新版WEB要加
    <PreserveCompilationReferences>true</PreserveCompilationReferences>
    //3.1 如果不加上面节点也可以引用Razor的编译服务
    Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation
    
    //如果你觉得发布文件夹下关于本地化的文件夹太多，您可以选择如下节点
    //选项：cs / de / es / fr / it / ja / ko / pl / ru / tr / zh-Hans / zh-Hant
    <SatelliteResourceLanguages>en</SatelliteResourceLanguages>
    
  </PropertyGroup>
 
```  

<br/>  
<br/>  


## License
[![FOSSA Status](https://app.fossa.io/api/projects/git%2Bgithub.com%2Fnight-moon-studio%2Fdeepclone.svg?type=large)](https://app.fossa.io/projects/git%2Bgithub.com%2Fnight-moon-studio%2Fdeepclone?ref=badge_large) 
