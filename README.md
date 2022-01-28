![logo](https://gimg2.baidu.com/image_search/src=http%3A%2F%2Fc-ssl.duitang.com%2Fuploads%2Fblog%2F202103%2F31%2F20210331142011_48a88.thumb.1000_0.jpg&refer=http%3A%2F%2Fc-ssl.duitang.com&app=2002&size=f9999,10000&q=a80&n=0&g=0n&fmt=jpeg?sec=1645793311&t=9832ae8b7c1923f71bdfc3503162d916)
# Desktop Elves(桌面精灵程序)
## Version 1.1
## 作者:code-520(祈愿)
## 新增
* 语音指令“清理C盘”，调用内置C盘清理脚本进行清理，清理临时文件等无用垃圾文件（本人亲自在自己电脑上运行测试，清理垃圾几个G）
* 语音指令“打开XXX”，可根据语法自己设置打开电脑上特定应用（语法参数为程序所在路径）
## 修改
* 修改了关键字语音类型的判断方式
* 删除了部分冗余代码（持续需改）
<<<<<<< HEAD
* 删除了原定数据类型Today和Welcome
=======
* 删除录了原定数据类型Today和Welcome
>>>>>>> main
* 移除3D射线，提高性能
## 内容
* 加入角色**可莉**
    * 动画
    * 语音
    * 互动
## 描述
* 角色具有一定的情绪值，初始为100(可自定义Emotions.json)
* 和角色进行不同的互动会改变情绪值，不同的情绪值会有不同的反馈效果
## 使用方法
* 点击可点击部位进行互动
* 根据设定语法进行互动
    * [当前可用关键字](https://github.com/code-520/Desktop-Elves/blob/main/Assets/StreamingAssets/Grammars.json)
* 可以根据需求自己设定语音关键字
    * 在**Desktop Elves\Desktop Elves\Desktop Elves_Data\StreamingAssets\Grammars.json** 里设计自己需要的效果
    * 例如**打开B站-msedge.exe|https://www.bilibili.com/** 即发出语音指令，程序会调用Edge浏览器打开后面那个URL
    * **key_words-audio_name** 说出关键字key_words,程序会调用Audio\\Keli文件夹下的audio_name语音 *(在源工程内修改然后重新编译即可)*
## 各脚本作用
* WindowManager.cs:透明窗体、窗口置顶、鼠标穿透、角色范围判断
* Head.cs:头部互动控制
* body.cs:胸口处互动控制
* Legs.cs:腿部互动控制
* JsonHelper.cs:Json数据的处理
* Datas.cs:定义用到的数据类型
* Speech.cs:语音识别控制
* RoleManger.cs:角色整体控制，包括旋转、初始效果、程序退出等
## 现存不足
* 模型穿模...(问题很严重，暂未修复，3D模型处理方面萌新)
* 旋转到90度后可能会导致无法操控 *(修改全屏鼠标滚轮都可导致旋转后带来的新问题)*
## 免责声明
* 本项目仅用于个人娱乐，不会进行任何形式的商业行为
* 模型等资源均来源于网络
* [模型下载](https://www.cnblogs.com/yaoling1997/p/13983109.html)
