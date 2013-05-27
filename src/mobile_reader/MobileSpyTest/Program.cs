using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MobilEditCom;
using System.Collections;
using COMlib;
using MobileSpyTest.Model;
using DataStore = datastore.DataStore;
using DeviceInfo = datastore.DeviceInfo;
using Device = datastore.Device;

namespace MobileSpyTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var app = new CMedApplication();

            // init driver
            var driver = new CDriver(app);

            try
            {
                driver.Ports.Update();              // enable all the ports
                foreach (COMlib.CPort port in driver.Ports)
                {
                    if (port.Type != COMlib.CPort.EType.iTunesBackup)
                        port.Enabled = true;
                    else
                        port.Enabled = false;
                }
                driver.Ports.Push();
            }
            catch (Exception e)
            {
                Console.WriteLine("error updating port: " + e.Message);
            }

            try
            {
                driver.Models.Update();             // enable all the models
                foreach (CModel model in driver.Models)
                    model.Enabled = true;
                driver.Models.Push();
            }
            catch (Exception e)
            {
                Console.WriteLine("error updating model: " + e.Message);
            }

            // find all sources
            driver.Sources.Update();
            var sources = new List<CSource>();
            foreach (CSource source in driver.Sources)
                if (source.Online)
                    sources.Add(source);

            foreach (var source in sources)
            {
                if (source is CSource.CMobilePhone)
                {
                    Console.WriteLine("found one phone");
                    CSource.CMobilePhone phone = source as CSource.CMobilePhone;
                    if (phone.IsConnectorRequired)
                    {
                        //Form_Progress installConnectorProgress = new Form_Progress();
                        //installConnectorProgress.Text = "Installing connector";
                        //installConnectorProgress.Show();
                        //phone.InstallConnector(installConnectorProgress.Progress);
                        //installConnectorProgress.Close();

                        //System.Windows.Forms.MessageBox.Show("Connector was installed, please wait until the device is connected");
                        return;
                    }

                    //source.Sms.Items.Update();
                    //foreach (CSmsItem item in source.Sms.Items)
                    //{
                    //    Console.WriteLine(item.Type.ToString() + "\r\n" + item.State.ToString() + "\r\n" +
                    //        item.Number + "\r\n" + item.Text + "\r\nReceived time: " + item.ReceivedTime + "\r\nSent time: " + item.SentTime + "\r\n" + "\r\n\r\n");
                    //}

                    try
                    {
                        Console.WriteLine("reading phone book...");
                        var contacts = ReadPhoneBook(source);
                        Console.WriteLine("finished reading phone book");
                        Console.WriteLine("reading call history...");
                        var calls = ReadCalls(source);
                        Console.WriteLine("finished reading call history");
                        Console.WriteLine("saving to db...");

                        SaveToDB(phone, contacts, calls);
                        Console.WriteLine("finished saving to db");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Oops, some error occured: " + ex.Message);
                    }
                }
            }
        }

        private static List<Contact> ReadPhoneBook(CSource source)
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
                                if (dateType == "Label")
                                    contact.Name = (item.Data as CEntryItem.CData.CTextData).Text;
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
                                throw new NotImplementedException("phonebook data not supported");
                        }
                    }
                    contacts.Add(contact);
                }
            }
            return contacts;
        }

        private static List<Call> ReadCalls(CSource source)
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
                //    Console.WriteLine("phonebook update error: " + e.Message);
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

        private static void SaveToDB(CSource.CMobilePhone phone_, List<Contact> contacts_, List<Call> calls_)
        {
            using (DataStore ds = new DataStore())
            {
                Guid guid1 = Guid.NewGuid();
                DeviceInfo devInfo = new DeviceInfo();
                devInfo.Label = phone_.Label;
                Device dev1 = ds.createDevice(guid1, "phone", devInfo);
                if (contacts_ != null)
                {
                    foreach (var c in contacts_)
                    {
                        var contact = dev1.createContact(c.Name, 0);
                        contact.addTextProp(0x110, c.Number);
                    }
                }
                if (calls_ != null)
                {
                    foreach (var c in calls_)
                    {
                        dev1.addCall(c.Number, c.Duration, c.Status, c.StartTime, c.StopTime, (int)c.Type);    
                    }
                }
            }
        }
    }
}
