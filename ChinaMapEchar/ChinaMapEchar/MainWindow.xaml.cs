using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ChinaMapEchar;
using SimpleMvvmToolkit;

namespace ChinaMapEchar
{
    public partial class MainWindow : Window
    {
        /// <summary>
        /// 运动点动画数据集
        /// </summary>
        private string m_PointData;

        /// <summary>
        /// 点的运动速度 单位距离/秒
        /// </summary>
        private double m_Speed;

        /// <summary>
        /// 运动轨迹弧线的正弦角度
        /// </summary>
        private double m_Angle;

        /// <summary>
        /// 数据源
        /// </summary>
        private List<MapInfomation.MapItem> m_Source;

        /// <summary>
        /// 动画版
        /// </summary>
        private Storyboard m_Sb = new Storyboard();

        List<MapInfomation.MapItem> list = new List<MapInfomation.MapItem>();
        List<MapInfomation.MapToItem> Overtolist = new List<MapInfomation.MapToItem>();

        /// <summary>
        /// 添加到达城市
        /// </summary>
        public DelegateCommand<string> ZZo
        {
            get
            {
                return new DelegateCommand<string>(delegate (string e)
                {
                    if(e == "")
                    {
                        if (checkBox33.IsChecked == false)
                        {
                            for (int i = 0; i < 33; i++)
                            {
                                foreach (var c in Anti.Children)
                                {
                                    if (c is CheckBox)
                                    {
                                        CheckBox tb = (CheckBox)c;
                                        if (tb.Name == _checkBoxList[i])
                                        {
                                            tb.IsChecked = false;
                                        }
                                    }
                                }
                            }
                            list.Clear();
                            Overtolist.Clear();
                        }
                        else
                        {
                            for (int i = 0; i < 33; i++)
                            {
                                foreach (var c in Anti.Children)
                                {
                                    if (c is CheckBox)
                                    {
                                        CheckBox tb = (CheckBox)c;
                                        if (tb.Name == _checkBoxList[i])
                                        {
                                            tb.IsChecked = true;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        Overtolist.Add(MakeData(e));
                    }
                });
            }
        }

        /// <summary>
        /// 去除到达城市
        /// </summary>
        public DelegateCommand<string> ZZB
        {
            get
            {
                return new DelegateCommand<string>(delegate (string e)
                {
                    var item = Overtolist.Find((s) => { return s.To == (Enum.CityEnum.ProvincialCapital)System.Enum.Parse(typeof(Enum.CityEnum.ProvincialCapital), e, true); });
                    Overtolist.Remove(item);
                });
            }
        }

        public MainWindow()
        {
            InitializeComponent();

            this.DataContext = this;

            //参数设置初始化部分
            m_PointData = "M244.5,98.5 L273.25,93.75 C278.03113,96.916667 277.52785,100.08333 273.25,103.25 z";
            m_Speed = 60;
            m_Angle = 15;

            //_checkBoxList初始化
            for (int i = 0; i < 33 ; i++)
            {
                _checkBoxList[i] = "checkBox" + i.ToString() + "";
            }

            //comboBox初始化
            comboBox.ItemsSource = ComboxList;
            comboBox.SelectedIndex = 0;

            //单色模式初始化
            radioButton1.IsChecked = true;

            //CheckBox初始化
            SetCheckBoxContent(comboBox.SelectedItem.ToString());

            //异型窗口拖动事件
            this.MouseMove += new MouseEventHandler(delegate (object sender, MouseEventArgs e)
            {
                Point Pp = Mouse.GetPosition(this);
                if(e.LeftButton == MouseButtonState.Pressed)
                {
                    DragMove();
                }
            });

            //关闭按钮事件
            textBlock.MouseDown += new MouseButtonEventHandler(delegate (object sender, MouseButtonEventArgs e)
            {
                Application.Current.Shutdown();
            });

            //关于按钮
            button1.Click += new RoutedEventHandler(delegate (object sender, RoutedEventArgs e)
            {
                MessageBox.Show("Author : LingMin\nE-Mail : Kid--L@hotmail.com");
            });

            //combobox 选中事件
            comboBox.SelectionChanged += new SelectionChangedEventHandler(delegate (object sender, SelectionChangedEventArgs e)
            {
                SetCheckBoxContent(comboBox.SelectedItem.ToString());
                for(int i = 0;i<33;i++)
                {
                    foreach (var c in Anti.Children)
                    {
                        if (c is CheckBox)
                        {
                            CheckBox tb = (CheckBox)c;
                            if (tb.Name == _checkBoxList[i])
                            {
                                tb.IsChecked = false;
                            }
                        }
                    }
                }
                checkBox33.IsChecked = false;
                list.Clear();
                Overtolist.Clear();
            });

            //生成按钮 Click事件
            button.Click += new RoutedEventHandler(delegate (Object sender, RoutedEventArgs e)
            {
                list.Add(new MapInfomation.MapItem() { From = (Enum.CityEnum.ProvincialCapital)System.Enum.Parse(typeof(Enum.CityEnum.ProvincialCapital), comboBox.SelectedItem.ToString(), true), To = Overtolist });
                m_Source = list;

                AddAnimation(m_Source[0]);
            });
        }

        /// <summary>
        /// 添加到达城市列表，通过枚举获得
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private MapInfomation.MapToItem MakeData(string name)
        {
            Enum.CityEnum.ProvincialCapital Cy = (Enum.CityEnum.ProvincialCapital)System.Enum.Parse(typeof(Enum.CityEnum.ProvincialCapital), name, true);
            Random Rd = new Random();
            MapInfomation.MapToItem sendtolist = new MapInfomation.MapToItem();
            sendtolist=(new MapInfomation.MapToItem() { To = Cy, Diameter = Rd.Next(10,30 ) });
            return sendtolist;
        }

        private List<string> _comboxList = new List<string>();
        /// <summary>
        /// ComboxList 集合列表
        /// </summary>
        public List<string> ComboxList
        {
            get
            {
                if (_comboxList.Count < 1)
                {
                    foreach (string a in System.Enum.GetNames(typeof(Enum.CityEnum.ProvincialCapital)))
                    {
                        _comboxList.Add(a.ToString());
                    }
                }
                return _comboxList;
            }
            set
            {
                _comboxList = value;
            }
        }

        //__checkBoxList 集合
        private string[] _checkBoxList = new string[33];

        //选中迁移源后，名称上移， ChechBox中将不再显示该项
        public void SetCheckBoxContent(string Sname)
        {
            int i = 0;
            foreach (string a in System.Enum.GetNames(typeof(Enum.CityEnum.ProvincialCapital)))
            {
                if (a.ToString() != Sname)
                {
                    foreach (var c in Anti.Children)
                    {
                        if (c is CheckBox)
                        {
                            CheckBox tb = (CheckBox)c;
                            if (tb.Name == _checkBoxList[i])
                            {
                                tb.Content = a.ToString();
                            }
                        }
                    }
                    i++;
                }
            }
        }

        /// <summary>
        /// 添加控件和动画到容器
        /// </summary>
        /// <param name="item">数据项</param>
        private void AddAnimation(MapInfomation.MapItem item)
        {
            grid_Animation.Children.Clear();
            m_Sb.Children.Clear();
            Random rd = new Random();
            foreach (MapInfomation.MapToItem toItem in item.To)
            {
                if (item.From == toItem.To)
                    continue;

                byte[] rgb = new byte[3];
                if (radioButton1.IsChecked == true)
                {
                    rgb = new byte[] { (byte)123, (byte)198, (byte)245 };
                }
                else
                {
                    rgb = new byte[] { (byte)rd.Next(30, 254), (byte)rd.Next(50, 254), (byte)rd.Next(1, 254) };
                }
               
                //运动轨迹
                double l = 0;
                Path particlePath = GetParticlePath(item.From, toItem, rgb, out l);
                grid_Animation.Children.Add(particlePath);
                // 跑动的点
                Grid grid = GetRunPoint(rgb);
                //到达城市的圆
                Ellipse ell = GetToEllipse(toItem, rgb);
                AddPointToStoryboard(grid, ell, m_Sb, particlePath, l, item.From, toItem);
                grid_Animation.Children.Add(grid);
                grid_Animation.Children.Add(ell);

                m_Sb.Begin(this);
            }
        }

        /// <summary>
        /// 获取运动轨迹
        /// </summary>
        /// <param name="from">来自</param>
        /// <param name="toItem">去</param>
        /// <param name="rgb">颜色:r,g,b</param>
        /// <param name="l">两点间的直线距离</param>
        /// <returns>Path</returns>
        private Path GetParticlePath(Enum.CityEnum.ProvincialCapital from, MapInfomation.MapToItem toItem, byte[] rgb, out double l)
        {
            Point startPoint = PointXY.PointXY.GetPoint(from);
            Point endPoint = PointXY.PointXY.GetPoint(toItem.To);

            Path path = new Path();
            Style style = (Style)FindResource("ParticlePathStyle");
            path.Style = style;
            PathGeometry pg = new PathGeometry();
            PathFigure pf = new PathFigure();
            pf.StartPoint = startPoint;
            ArcSegment arc = new ArcSegment();
            //顺时针弧
            arc.SweepDirection = SweepDirection.Clockwise;
            arc.Point = endPoint;
            //半径 正弦定理a/sinA=2r r=a/2sinA 其中a指的是两个城市点之间的距离 角A指a边的对角
            double sinA = Math.Sin(Math.PI * m_Angle / 180.0);
            //计算距离 勾股定理
            double x = startPoint.X - endPoint.X;
            double y = startPoint.Y - endPoint.Y;
            double aa = x * x + y * y;
            l = Math.Sqrt(aa);
            double r = l / (sinA * 3);
            arc.Size = new Size(r, r);
            pf.Segments.Add(arc);
            pg.Figures.Add(pf);
            path.Data = pg;
            path.Stroke = new SolidColorBrush(Color.FromArgb(255, rgb[0], rgb[1], rgb[2]));
            path.StrokeThickness = 1;
            path.Stretch = Stretch.None;
            path.ToolTip = string.Format("{0} --> {1} / 距离：{2} 公里", from.ToString(), toItem.To.ToString(),(int)l*5.8);

            return path;
        }

        /// <summary>
        /// 获取跑动的点
        /// </summary>
        /// <param name="rgb"></param>
        /// <returns>Grid</returns>
        private Grid GetRunPoint(byte[] rgb)
        {
            //一个Grid里包含一个椭圆 一个Path 椭圆做阴影
            //Grid
            Grid grid = new Grid();
            grid.IsHitTestVisible = false;//不参与命中测试
            grid.HorizontalAlignment = HorizontalAlignment.Left;
            grid.VerticalAlignment = VerticalAlignment.Top;
            grid.Width = 40;
            grid.Height = 15;
            grid.RenderTransformOrigin = new Point(0.5, 0.5);
            //Ellipse
            Ellipse ell = new Ellipse();
            ell.Width = 40;
            ell.Height = 15;
            ell.Fill = new SolidColorBrush(Color.FromArgb(255, rgb[0], rgb[1], rgb[2]));
            RadialGradientBrush rgbrush = new RadialGradientBrush();
            rgbrush.GradientOrigin = new Point(0.8, 0.5);
            rgbrush.GradientStops.Add(new GradientStop(Color.FromArgb(255, 0, 0, 0), 0));
            rgbrush.GradientStops.Add(new GradientStop(Color.FromArgb(22, 0, 0, 0), 1));
            ell.OpacityMask = rgbrush;
            grid.Children.Add(ell);
            //Path
            Path path = new Path();
            path.Data = Geometry.Parse(m_PointData);
            path.Width = 30;
            path.Height = 4;
            LinearGradientBrush lgb = new LinearGradientBrush();
            lgb.StartPoint = new Point(0, 0);
            lgb.EndPoint = new Point(1, 0);
            lgb.GradientStops.Add(new GradientStop(Color.FromArgb(88, rgb[0], rgb[1], rgb[2]), 0));
            lgb.GradientStops.Add(new GradientStop(Color.FromArgb(255, 255, 255, 255), 1));
            path.Fill = lgb;
            path.Stretch = Stretch.Fill;
            grid.Children.Add(path);
            return grid;
        }

        /// <summary>
        /// 获取到达城市的圆
        /// </summary>
        /// <param name="toItem">数据项</param>
        /// <param name="rgb">颜色</param>
        /// <returns>Ellipse</returns>
        private Ellipse GetToEllipse(MapInfomation.MapToItem toItem, byte[] rgb)
        {
            Ellipse ell = new Ellipse();
            ell.HorizontalAlignment = HorizontalAlignment.Left;
            ell.VerticalAlignment = VerticalAlignment.Top;
            ell.Width = toItem.Diameter;
            ell.Height = toItem.Diameter;
            ell.Fill = new SolidColorBrush(Color.FromArgb(255, rgb[0], rgb[1], rgb[2]));
            Point toPos = PointXY.PointXY.GetPoint(toItem.To);
            //定位到城市所在的点
            TranslateTransform ttf = new TranslateTransform(toPos.X - ell.Width / 2, toPos.Y - ell.Height / 2);
            ell.RenderTransform = ttf;
            ell.ToolTip = string.Format("{0} {1}", toItem.To.ToString(), toItem.Tip);
            ell.Opacity = 0;
            return ell;
        }

        /// <summary>
        /// 将点加入到动画故事版
        /// </summary>
        /// <param name="runPoint">运动的点</param>
        /// <param name="toEll">达到城市的圆</param>
        /// <param name="sb">故事版</param>
        /// <param name="particlePath">运动轨迹</param>
        /// <param name="l">运动轨迹的直线距离</param>
        /// <param name="from">来自</param>
        /// <param name="toItem">去</param>
        private void AddPointToStoryboard(Grid runPoint, Ellipse toEll, Storyboard sb, Path particlePath, double l, Enum.CityEnum.ProvincialCapital from, MapInfomation.MapToItem toItem)
        {
            //点运动所需的时间
            double pointTime = l / m_Speed;
            //轨迹呈现所需时间(跑的比点快两倍)
            double particleTime = pointTime / 2;
            //生成为控件注册名称的guid
            string name = Guid.NewGuid().ToString().Replace("-", "");

            // 运动的点
            TransformGroup tfg = new TransformGroup();
            MatrixTransform mtf = new MatrixTransform();
            tfg.Children.Add(mtf);
            //纠正最上角沿path运动到中心沿path运动
            TranslateTransform ttf = new TranslateTransform(-runPoint.Width / 2, -runPoint.Height / 2);
            tfg.Children.Add(ttf);
            runPoint.RenderTransform = tfg;
            this.RegisterName("m" + name, mtf);

            MatrixAnimationUsingPath maup = new MatrixAnimationUsingPath();
            maup.PathGeometry = particlePath.Data.GetFlattenedPathGeometry();
            maup.Duration = new Duration(TimeSpan.FromSeconds(pointTime));
            maup.RepeatBehavior = RepeatBehavior.Forever;
            maup.AutoReverse = false;
            maup.IsOffsetCumulative = false;
            //沿切线旋转
            maup.DoesRotateWithTangent = true;
            Storyboard.SetTargetName(maup, "m" + name);
            Storyboard.SetTargetProperty(maup, new PropertyPath(MatrixTransform.MatrixProperty));
            sb.Children.Add(maup);


            // 达到城市的圆
            this.RegisterName("ell" + name, toEll);
            //轨迹到达圆时 圆呈现
            DoubleAnimation ellda = new DoubleAnimation();
            //此处值设置0-1会有不同的呈现效果
            ellda.From = 0.9;
            ellda.To = 1;
            ellda.Duration = new Duration(TimeSpan.FromSeconds(particleTime));
            //推迟动画开始时间 等轨迹连接到圆时 开始播放圆的呈现动画
            ellda.BeginTime = TimeSpan.FromSeconds(particleTime);
            ellda.FillBehavior = FillBehavior.HoldEnd;
            Storyboard.SetTargetName(ellda, "ell" + name);
            Storyboard.SetTargetProperty(ellda, new PropertyPath(Ellipse.OpacityProperty));
            sb.Children.Add(ellda);
            //圆呈放射状
            RadialGradientBrush rgBrush = new RadialGradientBrush();
            GradientStop gStop0 = new GradientStop(Color.FromArgb(255, 0, 0, 0), 0);
            //此为控制点 color的a值设为0 off值走0-1 透明部分向外放射 初始设为255是为了初始化效果 开始不呈放射状 等跑动的点运动到城市的圆后 color的a值才设为0开始呈现放射动画
            GradientStop gStopT = new GradientStop(Color.FromArgb(255, 0, 0, 0), 0);
            GradientStop gStop1 = new GradientStop(Color.FromArgb(255, 0, 0, 0), 1);
            rgBrush.GradientStops.Add(gStop0);
            rgBrush.GradientStops.Add(gStopT);
            rgBrush.GradientStops.Add(gStop1);
            toEll.OpacityMask = rgBrush;
            this.RegisterName("e" + name, gStopT);
            //跑动的点达到城市的圆时 控制点由不透明变为透明 color的a值设为0 动画时间为0
            ColorAnimation ca = new ColorAnimation();
            ca.To = Color.FromArgb(0, 0, 0, 0);
            ca.Duration = new Duration(TimeSpan.FromSeconds(0));
            ca.BeginTime = TimeSpan.FromSeconds(pointTime);
            ca.FillBehavior = FillBehavior.HoldEnd;
            Storyboard.SetTargetName(ca, "e" + name);
            Storyboard.SetTargetProperty(ca, new PropertyPath(GradientStop.ColorProperty));
            sb.Children.Add(ca);
            //点达到城市的圆时 呈现放射状动画 控制点的off值走0-1 透明部分向外放射
            DoubleAnimation eda = new DoubleAnimation();
            eda.To = 1;
            eda.Duration = new Duration(TimeSpan.FromSeconds(2));
            eda.RepeatBehavior = RepeatBehavior.Forever;
            eda.BeginTime = TimeSpan.FromSeconds(particleTime);
            Storyboard.SetTargetName(eda, "e" + name);
            Storyboard.SetTargetProperty(eda, new PropertyPath(GradientStop.OffsetProperty));
            sb.Children.Add(eda);


            // 运动轨迹
            //找到渐变的起点和终点
            Point startPoint = PointXY.PointXY.GetPoint(from);
            Point endPoint = PointXY.PointXY.GetPoint(toItem.To);
            Point start = new Point(0, 0);
            Point end = new Point(1, 1);
            if (startPoint.X > endPoint.X)
            {
                start.X = 1;
                end.X = 0;
            }
            if (startPoint.Y > endPoint.Y)
            {
                start.Y = 1;
                end.Y = 0;
            }
            LinearGradientBrush lgBrush = new LinearGradientBrush();
            lgBrush.StartPoint = start;
            lgBrush.EndPoint = end;
            GradientStop lgStop0 = new GradientStop(Color.FromArgb(255, 0, 0, 0), 0);
            GradientStop lgStop1 = new GradientStop(Color.FromArgb(0, 0, 0, 0), 0);
            lgBrush.GradientStops.Add(lgStop0);
            lgBrush.GradientStops.Add(lgStop1);
            particlePath.OpacityMask = lgBrush;
            this.RegisterName("p0" + name, lgStop0);
            this.RegisterName("p1" + name, lgStop1);
            //运动轨迹呈现
            DoubleAnimation pda0 = new DoubleAnimation();
            pda0.To = 1;
            pda0.Duration = new Duration(TimeSpan.FromSeconds(particleTime));
            pda0.FillBehavior = FillBehavior.HoldEnd;
            Storyboard.SetTargetName(pda0, "p0" + name);
            Storyboard.SetTargetProperty(pda0, new PropertyPath(GradientStop.OffsetProperty));
            sb.Children.Add(pda0);
            DoubleAnimation pda1 = new DoubleAnimation();
            //pda1.From = 0.5; //此处解开注释 值设为0-1 会有不同的轨迹呈现效果
            pda1.To = 1;
            pda1.Duration = new Duration(TimeSpan.FromSeconds(particleTime));
            pda1.FillBehavior = FillBehavior.HoldEnd;
            Storyboard.SetTargetName(pda1, "p1" + name);
            Storyboard.SetTargetProperty(pda1, new PropertyPath(GradientStop.OffsetProperty));
            sb.Children.Add(pda1);
        }
    }
}
