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



**ControlTemplate 的用法**

[LINK](https://www.cnblogs.com/dingli/archive/2011/07/20/2112150.html)

> 在WPF中有三大模板 [*ControlTemplate*](https://docs.microsoft.com/en-us/dotnet/api/system.windows.controls.controltemplate?view=netcore-3.1),  *ItemsPanelTemplate*, *DataTemplate*.
>
> 其中ControlTemplate和ItemsPanelTemplate是控件模板，DataTemplate是数据模板，他们都派生自FrameworkTemplate抽象类。

ControlTemplate:控件模板主要有两个重要属性：VisualTree内容属性和Triggers触发器。所谓VisualTree(视觉树),就是呈现我们所画的控件。Triggers可以对我们的视觉树上的元素进行一些变化。一般用于单内容控件。



**WPF中的数据绑定**
[LINK](https://www.bilibili.com/video/BV1mJ411F7zG?p=7)

1. 直接通过事件的方式将数据绑定到控件上(传统的绑定方式，只是单向的)
   ```xaml
   <Grid>
        <StackPanel>
            <Slider x:Name="slider" Width="200" ValueChanged="Slider_ValueChanged"></Slider>
            <TextBox x:Name="txt" Width="200"></TextBox>
        </StackPanel>        
    </Grid>
   ```

   ```c#
   public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            txt.Text = e.NewValue.ToString();
        }
    }
   ```
2. 通过Binding 的方式绑定
   ```xaml
   <Grid>
        <StackPanel>
            <Slider x:Name="slider" Width="200" ></Slider>
            <TextBox x:Name="txt" Width="200" Text="{Binding ElementName=slider, Path=Value}"></TextBox>
        </StackPanel>
    </Grid>
   ```
> Binding 通过查找的方式，在上下文中查找元素名称为slider的元素， 找到该元素后，将该元素的value绑定到TextBox的Text中， 其后面还可以设置绑定的Mode, 是一次性的， 还是单向的， 还是双向的等等
> Text="{Binding ElementName=slider, Path=Value, Mode=OneWay}"


3. 通过绑定到资源的方式
   ```xaml
    <Window>
        <Window.Resources>
            <TextBox x:Key="tb">Hello Sheldon</TextBox>
        </Window.Resources>
        <Grid>
            <StackPanel>
                <TextBox x:Name="txt" Width="200" Text="{Binding Source={StaticResource tb}, Path=Text, Mode=OneWay}"></TextBox>
            </StackPanel>
        </Grid>
    </Window>

   ```
	> 通过Binding 的方式先通过key找到静态资源tb, 然后通过Path将tb上的Text解析到txt中的Text中

4. 通过数据上下文的方式进行绑定到属性
    ```xaml
    <StackPanel>
        <TextBox x:Name="txt" Width="200" Text="{Binding Name}"></TextBox>
    </StackPanel>
    ```

    ```c#
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            txt.DataContext = new Person() { Name = "Sheldon" };
        }
    }

    public class Person
    {
        public string Name { get; set; }
    }
    ```
	> 需要注意的是属性名必须与绑定名称一致， 且是public访问权限

5. 通过实现[INotifyPropertyChange](https://docs.microsoft.com/zh-cn/dotnet/api/system.componentmodel.inotifypropertychanged?view=net-5.0)接口来实现**双向绑定**
    > INotifyPropertyChanged 接口用于通知客户端（通常是绑定客户端）属性值已更改。
    <br>确切的说通过Binding的方式将TextBox 的 Text 绑定到了MainWindowViewModel::Name 属性上, 
    <br>当Text发生变化时, 会通过set操作修改 MainWindowViewModel::Name
    <br>但反过来, 如果没有实现 INotifyPropertyChanged 接口的话, 在后端修改 MainWindowViewModel::Name 值后, 是不能直接反映到UI中去的
    <br>所以 INotifyPropertyChanged 的作用就是用于通知 UI 更新数据
    <br>这一正一反的操作就大致实现了双向绑定    

    ```xaml
    <Grid>
        <TextBox x:Name="tx" Width="100" Height="30" Text="{Binding Name}"/>
    </Grid>
    ```
    ```c#
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new MainWindowViewModel();
        }
    }
    
    class MainWindowViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private String name;

        public String Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged();
            }
        }

        // [CallerMemberName]
        // 如果使用 CallerMemberName 特性，则调用 NotifyPropertyChanged 方法不必将属性名称指定为字符串参数
        protected void OnPropertyChanged([CallerMemberName]String properName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(properName));
        }

        public MainWindowViewModel()
        {
            Name = "Sheldon";
            Task.Run(async()=> {
                await Task.Delay(3000);
                Name = "Hello Sheldon";
            });
        }
    }
    ```

6. 通过安装 mvvmlight 包来实现双向绑定
    > 项目->引用->管理 NuGet 程序包-> 搜索 mvvmlight 安装
    > 在ViewModel中直接继承 ViewModelBase 类, 然后在set 中调用 RaisePropertyChanged() 即可实现5一样的效果
    ```xaml
        <Button x:Name="save" Command="{Binding SaveCommand}" Width="100" Height="30" Content="Save"/>
    ```
    ```c#
        class MainWindowViewModel : ViewModelBase
        {
            private String name;

            public String Name
            {
                get { return name; }
                set
                {
                    name = value;
                    Console.WriteLine(name);
                    RaisePropertyChanged();
                }
            }
            public MainWindowViewModel()
            {
                Name = "Sheldon";

                SaveCommand = new RelayCommand(()=> {
                    Name = "Hello Sheldon";
                });
            }

            public RelayCommand SaveCommand { get; set; } // 关于按钮的绑定,RelayCommand 实现了ICommand接口
        }
    ```

