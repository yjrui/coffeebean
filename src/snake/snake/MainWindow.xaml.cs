using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using datasource;
using System.Threading.Tasks;

namespace snake
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<Device> _devices;
        private DataSource _ds = new DataSource();
        private Device _selectedDevice;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnRead_Click(object sender, RoutedEventArgs e)
        {
            lbDevice.Items.Clear();
            lbStatus.Content = "正在从数据库读取手机信息";
            var context = TaskScheduler.FromCurrentSynchronizationContext();
            _devices = new List<Device>();
            Task task = Task.Factory.StartNew(() =>
                {
                    //using (var ds = new DataSource())
                    //{
                    _devices.AddRange(_ds.getDevices());
                    //}
                }).ContinueWith(_ =>
                    {
                        foreach (var device in _devices)
                        {
                            var item = new ListBoxItem()
                            {
                                Content = device.Label,
                                Tag = device
                            };
                            lbDevice.Items.Add(item);
                        }
                        lbStatus.Content = "读取完成";
                    }, context);
        }

        private void lbFeature_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_selectedDevice == null)
                return;

            lbItemPreview.Items.Clear();
            Session[] sessions = _selectedDevice.getSessions();
            if (sessions != null && sessions.Length > 0)
            {
                // choose the latest session
                var session = sessions.Last();
                var item = lbFeature.SelectedItem as ListBoxItem;
                if (item != null)
                {
                    if (item.Name == "lbiPhonebook")
                    {
                    }
                    else if (item.Name == "lbiCallHistory")
                    {
                        var context = TaskScheduler.FromCurrentSynchronizationContext();
                        Call[] calls = null;

                        Task.Factory.StartNew(() =>
                            {
                                calls = _selectedDevice.getCalls(session);
                            }).ContinueWith(_ =>
                                {
                                    if (calls != null)
                                    {
                                        foreach (var call in calls)
                                        {
                                            var callItem = new ListBoxItem()
                                            {
                                                Content = call.Number,
                                                Tag = call
                                            };
                                            lbItemPreview.Items.Add(callItem);
                                        }
                                    }
                                }, context);
                    }
                    else if (item.Name == "lbiSMS")
                    {
                    }
                }
            }
        }

        private void lbDevice_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var lbItem = lbDevice.SelectedItem as ListBoxItem;
            if (lbItem != null)
            {
                _selectedDevice = lbItem.Tag as Device;
            }
            else _selectedDevice = null;
        }

        private void lbItemPreview_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            gbDetails.Content = null;
            var lbItem = lbItemPreview.SelectedItem as ListBoxItem;
            if (lbItem != null)
            {
                var callCtrl = new CallDetails(lbItem.Tag as Call);
                gbDetails.Content = callCtrl;
            }
        }
    }
}
