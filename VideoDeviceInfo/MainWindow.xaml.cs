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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using AForge.Video;
using AForge.Video.DirectShow;

namespace VideoDeviceInfo
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private FilterInfoCollection VideoCapTureDevices;
        private VideoCaptureDevice CurrentDevices;

        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
            deviceList.SelectionChanged += DeviceList_SelectionChanged;
        }

        private void DeviceList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                CurrentDevices = new VideoCaptureDevice(VideoCapTureDevices[deviceList.SelectedIndex].MonikerString);
                try
                {
                    deviceInfo.Items.Clear();
                    foreach (var item in CurrentDevices.VideoCapabilities)
                    {
                        deviceInfo.Items.Add(item.FrameSize + " - " + item.AverageFrameRate + "Fps");
                    }

                    if (deviceInfo.Items.Count > 0)
                    {
                        deviceInfo.SelectedIndex = 0;
                    }
                }
                catch (Exception ex)
                {
                    
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            VideoCapTureDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            foreach (FilterInfo VideoCaptureDevice in VideoCapTureDevices)
            {
                deviceList.Items.Add(VideoCaptureDevice.Name);
            }
        }
    }
}
