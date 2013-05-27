using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace datastore
{
    public class dDevFileSystem
    {
        private String m_root;

        internal dDevFileSystem(String root)
        {
            m_root = root;
        }

        private String makeAbsolutePath(String devPath)
        {
            return m_root + "\\" + devPath;
        }

        public dDevFile createFile(String devFilePath)
        {
            String filePath = makeAbsolutePath(devFilePath);
            return new dDevFile(filePath);
        }
    }
}
