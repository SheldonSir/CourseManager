根据 https://www.bilibili.com/video/BV1Jy4y1C7hU?p=1 边看边敲代码

**设置无边框窗体**

```xml
<Window 
        ResizeMode="NoResize" 					<!--关闭窗体右下角Resize功能-->
        WindowStartupLocation="CenterScreen"	<!--窗口启动时在屏幕居中-->
        WindowStyle="None"  					<!--关闭窗体边框-->
        AllowsTransparency="True"  				<!--允许窗体透明-->
        Background="Transparent"				<!--窗体透明背景-->
        >
</Window>
```

**Border 的用法**

> Border 只能具有一个子元素。若要显示多个子元素，需要将一个容器元素放置在父元素Border中。
>
> Border 用来描绘容器边框或者背景

用Border描绘一个Close Button

```xml
<Window.Resources>
    <ControlTemplate TargetType="Button" x:Key="CloseBtnStyle">
        <Border Background="Transparent">
            <Path x:Name="closeicon" 
                  Data="M0 0 12 12M0 12 12 0" 
                  Stroke="White" 
                  StrokeThickness="2" 
                  VerticalAlignment="Center" 
                  HorizontalAlignment="Center"/>
        </Border>
        <ControlTemplate.Triggers>
            <Trigger Property="IsMouseOver" Value="True">
                <Setter TargetName="closeicon" Property="Stroke" Value="#D6661E"/>
            </Trigger>
        </ControlTemplate.Triggers>
    </ControlTemplate>
</Window.Resources>

<Border Background="#007DFA" CornerRadius="10, 10, 0, 0">
    <Button Width="30" Height="30" Template="{StaticResource CloseBtnStyle}"/>
</Border>

```



[**ControlTemplate 的用法**](https://www.cnblogs.com/dingli/archive/2011/07/20/2112150.html)

> 在WPF中有三大模板 [*ControlTemplate*](https://docs.microsoft.com/en-us/dotnet/api/system.windows.controls.controltemplate?view=netcore-3.1),  *ItemsPanelTemplate*, *DataTemplate*.
>
> 其中ControlTemplate和ItemsPanelTemplate是控件模板，DataTemplate是数据模板，他们都派生自FrameworkTemplate抽象类。

ControlTemplate:控件模板主要有两个重要属性：VisualTree内容属性和Triggers触发器。所谓VisualTree(视觉树),就是呈现我们所画的控件。Triggers可以对我们的视觉树上的元素进行一些变化。一般用于单内容控件。