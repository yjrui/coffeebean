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
using System.Windows.Shapes;
using MobilEditCom;
using COMlib;
using DataStore = datastore.DataStore;
using DeviceInfo = datastore.dDeviceInfo;
using Device = datastore.dDevice;
using datastore;
using snake.Model;
using System.Threading.Tasks;
using System.IO;


namespace snake
{
    /// <summary>
    /// Interaction logic for Import.xaml
    /// </summary>
    public partial class Import : Window
    {
        public Import()
        {
            InitializeComponent();
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            var sources = FindDevices();
            foreach (var source in sources)
            {
                var item = new ListBoxItem()
                {
                    Content = source.Label,
                    Tag = source
                };
                lbDevice.Items.Add(item);
            }
        }

        private void btnImport_Click(object sender, RoutedEventArgs e)
        {
            if (lbDevice.SelectedIndex < 0)
            {
                MessageBox.Show("请先选择一个设备");
                return;
            }

            var importOption = new ImportOption();
            importOption.Owner = this;
            importOption.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterOwner;
            importOption.ShowDialog();
            if (importOption.IsOKClicked)
            {
                var context = TaskScheduler.FromCurrentSynchronizationContext();
                var source = ((ListBoxItem)lbDevice.SelectedItem).Tag as CSource;
                var importContact = importOption.cbContact.IsChecked.Value;
                var importCall = importOption.cbCall.IsChecked.Value;
                var importSms = importOption.cbSms.IsChecked.Value;
                var importFs = importOption.cbFS.IsChecked.Value;
                Task.Factory.StartNew(() =>
                {
                    DoImport(source, importContact, importCall, importSms, importFs);
                });
            }
        }

        private List<CSource> FindDevices()
        {
            var sources = new List<CSource>();
            try
            {
                //var app = new CMedApplication();

                // init driver
                var driver = new CDriver();

                driver.Ports.Update();              // enable all the ports
                foreach (COMlib.CPort port in driver.Ports)
                {
                    if (port.Type != COMlib.CPort.EType.iTunesBackup)
                        port.Enabled = true;
                    else
                        port.Enabled = false;
                }
                driver.Ports.Push();

                driver.Models.Update();             // enable all the models
                foreach (CModel model in driver.Models)
                    model.Enabled = true;
                driver.Models.Push();

                // find all sources
                driver.Sources.Update();
                foreach (CSource source in driver.Sources)
                    if (source.Online)
                        sources.Add(source);
            }
            catch (Exception e)
            {
                Log("查找设备时发生了错误: " + e.Message + "\n");
            }

            return sources;
        }

        private void Log(string log)
        {
            if (CheckAccess())
            {
                lbResult.Text += log;
            }
            else
            {
                Dispatcher.Invoke(new Action<string>(
                    (l) => { Log(l); }), log);
            }
        }

        private void DoImport(CSource source, bool importContact, bool importCall, bool importSms, bool importFs)
        {
            try
            {
                if (source is CSource.CMobilePhone)
                {
                    CSource.CMobilePhone phone = source as CSource.CMobilePhone;
                    if (phone.IsConnectorRequired)
                    {
                        //Form_Progress installConnectorProgress = new Form_Progress();
                        //installConnectorProgress.Text = "Installing connector";
                        //installConnectorProgress.Show();
                        //phone.InstallConnector(installConnectorProgress.Progress);
                        //installConnectorProgress.Close();
                        phone.InstallConnector();

                        //System.Windows.Forms.MessageBox.Show("Connector was installed, please wait until the device is connected");
                        return;
                    }

                    List<Sms> sms = null;
                    List<Contact> contacts = null;
                    List<Call> calls = null;

                    if (importContact)
                    {
                        Log("读取联系人...\n");
                        contacts = ReadPhoneBook(source);
                        Log("完成\n");
                    }
                    if (importCall)
                    {
                        Log("读取通话记录...\n");
                        calls = ReadCalls(source);
                        Log("完成\n");
                    }
                    if (importSms)
                    {
                        Log("读取短信...\n");
                        sms = ReadSms(source);
                        Log("完成\n");
                    }
                    if (sms != null && sms.Count > 0 ||
                        contacts != null && contacts.Count > 0 ||
                        calls != null && calls.Count > 0)
                    {
                        Log("保存...\n");
                        SaveToDB(phone, contacts, calls, sms);
                        Log("导入完成！\n");
                    }
                    else
                    {
                        Log("没有需要保存的记录\n");
                    }
                }
                else if (source is CSource.CSIMCard)
                {
                    Log("读取文件系统...\n");
                    //string rootFolder = ReadFS(source);
                    string root = @"G:\data\";
                    string rootFolder = System.IO.Path.Combine(root, source.UniqueIdentifier);
                    Log("完成\n");
                    Log("保存...\n");
                    SaveToDB(source, rootFolder);
                    Log("导入完成！\n");
                }
            }
            catch (Exception ex)
            {
                Log("导入时发生了错误: " + ex.Message + "\n");
            }
        }

        private List<Contact> ReadPhoneBook(CSource source)
        {
            var contacts = new List<Contact>();
            foreach (COMlib.CPhonebook phonebook in source.Phonebooks)
            {
                if (phonebook.Type != COMlib.CPhonebook.EType.Phonebook)
                    continue;

                phonebook.Entries.Update();
                foreach (CEntry entry in phonebook.Entries)
                {
                    var contact = new Contact();
                    foreach (CEntryItem item in entry.Items)
                    {
                        if (item.DataType != CEntryItem.EDataType.Empty)
                        {
                            string dateType = item.DataType.ToString();
                            if (item.Data is CEntryItem.CData.CTextData)
                            {
                                if (dateType == "Label" || dateType == "FirstName" || dateType == "LastName")
                                {
                                    if (contact.Name == null)
                                        contact.Name = (item.Data as CEntryItem.CData.CTextData).Text;
                                    else contact.Name += " " + contact.Name;
                                }
                                else
                                    Log("not supported text data type: " + dateType);
                            }
                            else if (item.Data is CEntryItem.CData.CNumberData)
                            {
                                contact.Number = (item.Data as CEntryItem.CData.CNumberData).Number;
                            }
                            else if (item.Data is CEntryItem.CData.CTimeData)
                            {
                                //(item.Data as CEntryItem.CData.CTimeData).Time.ToString();
                            }
                            else if (item.Data is CEntryItem.CData.CAddressData)
                            {
                                //(item.Data as CEntryItem.CData.CAddressData).Street
                                //+ ";" + (item.Data as CEntryItem.CData.CAddressData).City
                                //+ ";" + (item.Data as CEntryItem.CData.CAddressData).Country;
                            }
                            else
                            {
                                //throw new NotImplementedException("phonebook data not supported");
                                if (item.Data != null)
                                    Log("not supported phone book data: " + item.DataType);
                            }
                        }
                    }
                    contacts.Add(contact);
                }
            }
            return contacts;
        }

        private List<Call> ReadCalls(CSource source)
        {
            var calls = new List<Call>();
            foreach (COMlib.CPhonebook phonebook in source.Phonebooks)
            {
                if (phonebook.Type != COMlib.CPhonebook.EType.Dialed &&
                    phonebook.Type != COMlib.CPhonebook.EType.Missed &&
                    phonebook.Type != COMlib.CPhonebook.EType.Received)
                    continue;

                //try
                //{
                phonebook.Entries.Update();
                //    phonebook.Entries.Push();
                //}
                //catch (Exception e)
                //{
                //    Log("phonebook update error: " + e.Message);
                //}
                foreach (CEntry entry in phonebook.Entries)
                {
                    var call = new Call();
                    call.Type = phonebook.Type == COMlib.CPhonebook.EType.Received ? CallType.Received :
                        phonebook.Type == COMlib.CPhonebook.EType.Missed ? CallType.Missed :
                        CallType.Dialed;
                    foreach (CEntryItem item in entry.Items)
                    {
                        if (item.DataType != CEntryItem.EDataType.Empty)
                        {
                            string dateType = item.DataType.ToString();
                            if (item.Data is CEntryItem.CData.CTextData)
                            {
                                //if (dateType == "Label")
                                //    call.Name = (item.Data as CEntryItem.CData.CTextData).Text;
                            }
                            else if (item.Data is CEntryItem.CData.CNumberData)
                            {
                                call.Number = (item.Data as CEntryItem.CData.CNumberData).Number;
                            }
                            else if (item.Data is CEntryItem.CData.CTimeData)
                            {
                                //(item.Data as CEntryItem.CData.CTimeData).Time.ToString();
                            }
                            else if (item.Data is CEntryItem.CData.CAddressData)
                            {
                                //(item.Data as CEntryItem.CData.CAddressData).Street
                                //+ ";" + (item.Data as CEntryItem.CData.CAddressData).City
                                //+ ";" + (item.Data as CEntryItem.CData.CAddressData).Country;
                            }
                            else
                                throw new NotImplementedException("call history data type not supported");
                        }
                    }
                    calls.Add(call);
                }
            }
            return calls;
        }

        private List<Sms> ReadSms(CSource source)
        {
            var sms = new List<Sms>();
            source.Sms.Items.Update();

            foreach (CSmsItem item in source.Sms.Items)
            {
                var s = new Sms();
                var sInfo = new SMSInfo()
                {
                    State = (short)item.State,
                    Text = item.Text,
                    Status = (byte)item.Status,
                    ServiceCenter = item.ServiceCenter
                };
                s.SMSInfo = sInfo;
                if (item.Type == CSmsItem.EType.Deliver)
                {
                    sInfo.FromNumber = item.Number;
                    sInfo.ReceivedTimeStamp = item.ReceivedTime;
                    sInfo.ReceivedTimeZone = item.ReceivedTimezone;
                    sInfo.Type = (short)CSmsItem.EType.Deliver;
                    s.SendReceive = SmsSendReceive.Received;
                }
                else if (item.Type == CSmsItem.EType.Submit)
                {
                    sInfo.ToNumber = item.Number;
                    sInfo.SentTimeStamp = item.SentTime;
                    sInfo.Type = (short)CSmsItem.EType.Submit;
                    s.SendReceive = SmsSendReceive.Sent;
                }
                sms.Add(s);
                //Log(item.Type.ToString() + "\r\n" + item.State.ToString() + "\r\n" +
                //    item.Number + "\r\n" + item.Text + "\r\nReceived time: " + item.ReceivedTime + "\r\nSent time: " + item.SentTime + "\r\n" + "\r\n\r\n");
            }
            return sms;
        }

        private string ReadFS(CSource source)
        {
            if (source.FileSystem != null)
            {
                string root = @"G:\data\";
                COMlib.CFolder folder = source.FileSystem.RootFolder;
                folder.Folders.Update();
                foreach (COMlib.CFolder fld in folder.Folders)
                {
                    if (fld.Name == "mnt")
                    {
                        // for andriod, only interested in the /mnt folder
                        ReadFolder(fld, System.IO.Path.Combine(root, source.UniqueIdentifier, fld.Name));
                    }
                }
                return System.IO.Path.Combine(root, source.UniqueIdentifier);
            }
            return null;
        }

        private void ReadFolder(COMlib.CFolder folder, string outputPath)
        {
            if (folder == null) return;

            if (!Directory.Exists(outputPath))
            {
                Directory.CreateDirectory(outputPath);
            }

            folder.Folders.Update();
            if (folder.Folders != null)
            {
                foreach (COMlib.CFolder fld in folder.Folders)
                {
                    ReadFolder(fld, System.IO.Path.Combine(outputPath, fld.Name));
                }
            }

            folder.Files.Update();
            if (folder.Files != null)
            {
                foreach (COMlib.CFile file in folder.Files)
                {
                    string filepath = System.IO.Path.Combine(outputPath, file.Name);
                    if (File.Exists(filepath))
                    {
                        Log(filepath + " already exists, skip");
                    }
                    else
                    {
                        try
                        {
                            using (FileStream fs = new FileStream(filepath, FileMode.CreateNew))
                            {
                                file.Download(fs);
                            }
                        }
                        catch (UnauthorizedAccessException ex)
                        {
                        }
                    }
                }
            }
        }

        private void SaveToDB(CSource source_, string rootFolder_)
        {
            using (DataStore ds = new DataStore())
            {
                DeviceInfo devInfo = new DeviceInfo();
                devInfo.Label = source_.Label;
                Device dev1 = ds.createDevice(source_.UniqueIdentifier, "simcard", devInfo);
                if (rootFolder_ != null)
                {
                    dDevFileSystem devFS = dev1.createDevFileSystem(rootFolder_);
                    ds.commit();
                }
            }
        }

        private static void SaveToDB(CSource.CMobilePhone phone_,
            List<Contact> contacts_, List<Call> calls_, List<Sms> sms_)
        {
            using (DataStore ds = new DataStore())
            {
                DeviceInfo devInfo = new DeviceInfo();
                devInfo.Label = phone_.Label;
                Device dev1 = ds.createDevice(phone_.UniqueIdentifier, "phone", devInfo);
                if (contacts_ != null)
                {
                    foreach (var c in contacts_)
                    {
                        var contact = dev1.createContact(c.Name, 0);
                        contact.addTextProp(257, c.Number);
                    }
                }
                if (calls_ != null)
                {
                    foreach (var c in calls_)
                    {
                        dev1.addCall(c.Number, c.Duration, c.Status, c.StartTime, c.StopTime, (int)c.Type);
                    }
                }
                if (sms_ != null)
                {
                    foreach (var s in sms_)
                    {
                        dev1.addSMS((int)s.SendReceive, s.SMSInfo);
                    }
                }
                ds.commit();
            }
        }
    }
}
