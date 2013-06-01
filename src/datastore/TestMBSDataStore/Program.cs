using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using datastore;
using System.IO;

namespace TestMBSDataStore
{
    class Program
    {
        private static void copyDir(DirectoryInfo dir, String rootPath, dDevFileSystem devFS)
        {
            DirectoryInfo[] subDirs = dir.GetDirectories();
            foreach (DirectoryInfo subDir in subDirs)
            {
                copyDir(subDir, rootPath, devFS);
            }
            FileInfo[] files = dir.GetFiles();
            byte[] buffer = new byte[1 << 12];
            foreach (FileInfo file in files)
            {
                using (dDevFile devFile = devFS.createFile(file.FullName.Substring(rootPath.Length)))
                {
                    FileStream fs = file.OpenRead();
                    int readByte = 0;
                    while ((readByte = fs.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        devFile.write(buffer, 0, readByte);
                    }
                }
            }
        }

        private static void loadData()
        {
            using (DataStore ds = new DataStore())
            {
                String guid1 = "01123304408844223";
                dDeviceInfo devInfo = new dDeviceInfo();
                devInfo.Label = "IPHONE 5";
                dDevice dev1 = ds.createDevice(guid1, "phone", devInfo);
                dDevice dev2 = ds.createDevice(guid1, "phone");
                devInfo.Label = "GSM";
                dDevice dev3 = ds.createDevice("394485837722722055055", "SIMCard", devInfo, dev1);
                for (int i = 0; i < 100; ++i)
                {
                    dev1.addCall("123456789", 30, 0,
                        DateTime.Parse("2013/4/3 12:41:22"),
                        DateTime.Parse("2013/4/3 12:41:52"), 0);
                    dContact helen = dev1.createContact("Helen " + i, i);
                    helen.addTextProp((int)datasource.dContact.PropertyType.HOME_NUM, "abc");
                    helen.addDateTimeProp((int)datasource.dContact.PropertyType.DATE, DateTime.Now);
                    byte[] data = System.Text.Encoding.Unicode.GetBytes("data" + i);
                    helen.addBinaryProp((int)datasource.dContact.PropertyType.ITEMPICTURE, data, 0, data.Length);
                    SMSInfo smsInfo = new SMSInfo();
                    smsInfo.FromNumber = "938740182" + i;
                    smsInfo.Text = "hello" + i;
                    dev3.addSMS(0x02008000, smsInfo);
                }

                dDevFileSystem devFS = dev1.createDevFileSystem("D:\\Steven\\temp");
                DirectoryInfo dir = new DirectoryInfo("D:\\HEROSOFT");
                copyDir(dir, dir.FullName, devFS);
                ds.commit();
                // rollback
                dDevice dev = ds.createDevice("never be added", "PHONE");
            }
        }

        private static void readData()
        {
            using (datasource.DataSource ds = new datasource.DataSource())
            {
                datasource.dDevice[] devices = ds.getDevices();
                foreach (datasource.dDevice device in devices)
                {
                    int depth = 0;
                    output("device.UID=" + device.UID, depth);
                    output("device.Label=" + device.Label, depth);
                    datasource.Session[] sessions = device.getSessions();
                    ++depth;
                    foreach (datasource.Session session in sessions)
                    {
                        output("session.Id=" + session.Id, depth);
                        output("session.Timestamp=" + session.Timestamp, depth);
                        // get calls
                        datasource.dCall[] c = device.getCalls(null);
                        if (c.Length != 0) throw new ArgumentNullException();
                        datasource.dCall[] calls = device.getCalls(session);
                        ++depth;
                        foreach (datasource.dCall call in calls)
                        {
                            output("call.Number=" + call.Number, depth);
                            output("call.Duration" + call.Duration, depth);
                            output("call.Status=" + call.Status, depth);
                            output("call.StartTimeStamp=" + call.StartTimeStamp, depth);
                            output("call.StopTimeStamp=" + call.StopTimeStamp, depth);
                        }
                        // get associate sessions
                        datasource.Session[] assSessions = device.getAssociateDeviceSessions(session);
                        foreach (datasource.Session assSes in assSessions)
                        {
                            output("assSes.Id=" + assSes.Id, depth);
                            output("assSes.Device.UID=" + assSes.Device().UID, depth);
                        }
                        // get contacts
                        datasource.dContact[] contacts = device.getContacts(session);
                        foreach (datasource.dContact contact in contacts)
                        {
                            output("contact.Label=" + contact.Label, depth);
                            // get all contact properties
                            datasource.dContact.Property[] props = contact.getProperties();
                            ++depth;
                            foreach (datasource.dContact.Property prop in props)
                            {
                                output("prop.name=" + prop.name, depth);
                                output("prop.value=" + prop.value.ToString(), depth);
                            }
                            --depth;
                        }
                        // get sms
                        datasource.dSMS[] smsList = device.getSMSList(session);
                        foreach (datasource.dSMS sms in smsList)
                        {
                            output("SMS.FromNumber=" + sms.FromNumber, depth);
                            output("SMS.Text=" + sms.Text, depth);
                        }
                        // get filesystem root
                        String[] fsRoots = device.getFileSystemRoots(session);
                        foreach (String fsRoot in fsRoots)
                        {
                            output("FS Root=" + fsRoot, depth);
                        }
                        --depth;
                    }
                }
            }
        }
        static void Main(string[] args)
        {
            // load data
            loadData();

            // read data
            readData();
        }

        private static void output(String message, int depth)
        {
            String indent = "";
            String prefix = (depth > 0) ? "_" : "-";
            while (depth-- > 0)
            {
                indent += " |";
            }
            Console.WriteLine(indent + prefix + message);
        }
    }
}
