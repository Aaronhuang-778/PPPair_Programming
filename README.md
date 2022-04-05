# 2022软工结对编程

[TOC]

## 代码位置

```shell
PPPair_Programming/
├── bin/ # 助教测试用文件夹
│   ├── Wordlist.exe # 可按Wordlist.exe args[]格式命令行测试
│   ├── Core.dll # 核心模块 DLL，适用于.NET FrameWork 4.7.2项目引用
|   ├── HYCore.dll # 提供给另一组调用的核心模块类库，适用于.NET Core 3.1项目引用
|   ├── input.txt # 可以作为测试.exe输入数据
|   ├── solution.txt # 测试.exe输出结果
|   ├── data.py #自动生成test%d.txt测试文件、result%d.txt答案文件
|   ├── c_circle # -c -r 测试文件
|   ├── c_no_circle # -c 测试文件
|   ├── w_circle # -w -r 测试文件
|   ├── w_no_circle # -w 测试文件
|   ├── m_no_circle # -m 测试文件
|   └── n_no_circle # -n 测试文件
├── README.md
├── PPPair_Programming.sln # 解决方案，包括核心模块Core，界面GUICode，测试TestProject，交换类库HYCore
├── Core/ # 核心模块Core项目
│   └── Core.sln # .NET FrameWork 4.7.2
├── GUICode/ # 界面GUICode项目
│   └── GUICode.sln # .NET FrameWork 4.7.2 WPF
├── TestProject/ # 【test_data分支】100%完全覆盖Core单元测试TestProject项目
│   └── TestProject.sln # .NET FrameWork 4.7.2
├── HYCore/ # 【test_data分支】交换给另一组类库HYCore项目
│   └── HYCore.sln # .NET Core 3.1
└── img #博客中用到的图片
```



## GUI

**位置**：`./GUICode/GUICode.csproj`

<img src="https://s2.loli.net/2022/04/05/QK8NuSrHzc1sV4p.png" alt="image-20220405180856551" style="zoom: 50%;" />

**使用主题**：

Google创建的[Material design](https://material.io/design/introduction)

使用的GitHub上面的**[ MaterialDesignInXamlToolkit](https://github.com/MaterialDesignInXAML/MaterialDesignInXamlToolkit)**

[快速入门教程](https://github.com/MaterialDesignInXAML/MaterialDesignInXamlToolkit/wiki/Super-Quick-Start)

[学习如何正确使用组件和样式]()



## 依赖

**.Net Framework >= 4.7**

NuGet：

```
MaterialDesignThemes
ShowMeTheXAML.MSBuild
```

