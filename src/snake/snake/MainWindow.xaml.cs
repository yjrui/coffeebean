﻿using System;
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
using snake.Model;
using System.IO;

namespace snake
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<dDevice> _devices;
        private DataSource _ds = new DataSource();
        private dDevice _selectedDevice;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnRead_Click(object sender, RoutedEventArgs e)
        {
            lbDevice.Items.Clear();
            lbStatus.Content = "正在从数据库读取手机信息";
            var context = TaskScheduler.FromCurrentSynchronizationContext();
            _devices = new List<dDevice>();
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
                        var context = TaskScheduler.FromCurrentSynchronizationContext();
                        dContact[] contacts = null;

                        Task.Factory.StartNew(() =>
                        {
                            contacts = _selectedDevice.getContacts(session);
                        }).ContinueWith(_ =>
                        {
                            if (contacts != null)
                            {
                                foreach (var contact in contacts)
                                {
                                    var c = new Contact() { Name = contact.Label };
                                    var property = contact.getProperties().FirstOrDefault(p => p.name == dContact.PropertyType.NUMBER_NUM.ToString());
                                    if (property != null) c.Number = property.value.ToString();
                                    var contactItem = new ListBoxItem()
                                    {
                                        Content = contact.Label,
                                        Tag = c
                                    };
                                    lbItemPreview.Items.Add(contactItem);
                                }
                            }
                        }, context);
                    }
                    else if (item.Name == "lbiCallHistory")
                    {
                        var context = TaskScheduler.FromCurrentSynchronizationContext();
                        dCall[] calls = null;

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
                        var context = TaskScheduler.FromCurrentSynchronizationContext();
                        dSMS[] sms = null;

                        Task.Factory.StartNew(() =>
                        {
                            sms = _selectedDevice.getSMSList(session);
                        }).ContinueWith(_ =>
                        {
                            if (sms != null)
                            {
                                foreach (var s in sms)
                                {
                                    var smsItem = new ListBoxItem()
                                    {
                                        Content = s.Type == dSMS.SMSType.DELIVER ? s.FromNumber : s.ToNumber,
                                        Tag = s
                                    };
                                    lbItemPreview.Items.Add(smsItem);
                                }
                            }
                        }, context);
                    }
                    else if (item.Name == "lbiFS")
                    {
                        var context = TaskScheduler.FromCurrentSynchronizationContext();
                        string[] roots = null;
                        Task.Factory.StartNew(() =>
                        {
                            roots = _selectedDevice.getFileSystemRoots(session);
                        }).ContinueWith(_ =>
                        {
                            if (roots != null)
                            {
                                foreach (var s in roots)
                                {
                                    int i = s.IndexOf(_selectedDevice.UID);
                                    if (i < 0) continue;
                                    string f = s.Substring(0, i + _selectedDevice.UID.Length);
                                    var fsItem = new ListBoxItem()
                                    {
                                        Content = f,
                                        Tag = s
                                    };
                                    lbItemPreview.Items.Add(fsItem);
                                }
                            }
                        }, context);
                    }
                }
            }
        }

        private void lbDevice_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var lbItem = lbDevice.SelectedItem as ListBoxItem;
            if (lbItem != null)
            {
                _selectedDevice = lbItem.Tag as dDevice;
            }
            else _selectedDevice = null;
        }

        private void lbItemPreview_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            gbDetails.Content = null;
            var lbItem = lbItemPreview.SelectedItem as ListBoxItem;
            if (lbItem != null)
            {
                if (lbItem.Tag is dCall)
                {
                    var callCtrl = new CallDetails(lbItem.Tag as dCall);
                    gbDetails.Content = callCtrl;
                }
                else if (lbItem.Tag is Contact)
                {
                    var contactCtrl = new ContactDetails(lbItem.Tag as Contact);
                    gbDetails.Content = contactCtrl;
                }
                else if (lbItem.Tag is dSMS)
                {
                    var smsCtrl = new SmsDetails(lbItem.Tag as dSMS);
                    gbDetails.Content = smsCtrl;
                }
            }
        }

        private void btnLoad_Click(object sender, RoutedEventArgs e)
        {
            var importScreen = new Import();
            importScreen.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            importScreen.ShowDialog();
        }

        private void miFS_Click(object sender, RoutedEventArgs e)
        {
            if (lbItemPreview.SelectedIndex >= 0)
            {
                string text = ((ListBoxItem)lbItemPreview.SelectedItem).Content.ToString();
                if (Directory.Exists(text))
                {
                    System.Diagnostics.Process.Start(text);
                }
            }
        }
    }
}
