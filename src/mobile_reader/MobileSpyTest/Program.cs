using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MobilEditCom;
using System.Collections;
using COMlib;
using MobileSpyTest.Model;
using DataStore = datastore.DataStore;
using DeviceInfo = datastore.dDeviceInfo;
using Device = datastore.dDevice;
using datastore;

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
                driver.Ports.Clear();

                driver.Ports.Update();              // enable all the ports
                foreach (COMlib.CPort port in driver.Ports)
                {
                    if (port.Type != COMlib.CPort.EType.iTunesBackup)
                        port.Enabled = true;
                    else
                        port.Enabled = false;
                }

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
                driver.Models.Clear();
                driver.Models.Update();             // enable all the models
                foreach (CModel model in driver.Models)
                    model.Enabled = true;

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
                        phone.InstallConnector();

                        //System.Windows.Forms.MessageBox.Show("Connector was installed, please wait until the device is connected");
                        return;
                    }

                    

                    try
                    {
                        Console.WriteLine("reading sms...");
                        var sms = ReadSms(source);
                        Console.WriteLine("finished reading sms");
                        Console.WriteLine("reading phone book...");
                        var contacts = ReadPhoneBook(source);
                        Console.WriteLine("finished reading phone book");
                        Console.WriteLine("reading call history...");
                        var calls = ReadCalls(source);
                        Console.WriteLine("finished reading call history");
                        Console.WriteLine("saving to db...");

                        SaveToDB(phone, contacts, calls, sms);
                        Console.WriteLine("finished saving to db");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Oops, some error occured: " + ex.Message);
                    }
                }
            }

            Console.ReadLine();
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
                                if (dateType == "Label" || dateType == "FirstName" || dateType == "LastName")
                                {
                                    if (contact.Name == null)
                                        contact.Name = (item.Data as CEntryItem.CData.CTextData).Text;
                                    else contact.Name += " " + contact.Name;
                                }
                                else
                                    Console.WriteLine("not supported text data type: " + dateType);
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
                                    Console.WriteLine("not supported phone book data: " + item.DataType);
                            }
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

        private static List<Sms> ReadSms(CSource source)
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
                //Console.WriteLine(item.Type.ToString() + "\r\n" + item.State.ToString() + "\r\n" +
                //    item.Number + "\r\n" + item.Text + "\r\nReceived time: " + item.ReceivedTime + "\r\nSent time: " + item.SentTime + "\r\n" + "\r\n\r\n");
            }
            return sms;
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
