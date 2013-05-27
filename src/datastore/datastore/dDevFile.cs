using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace datastore
{
    public class dDevFile : IDisposable
    {
        private String m_path;
        private FileStream m_fs = null;

        internal dDevFile(String path)
        {
            m_path = path;
            open();
        }

        private void makeDir(DirectoryInfo dir)
        {
            if (dir == null || dir.Exists) return;
            DirectoryInfo parentDir = dir.Parent;
            makeDir(parentDir);
            dir.Create();
        }

        private void open()
        {
            close();
            if (File.Exists(m_path)) File.Delete(m_path);
            makeDir(Directory.GetParent(m_path));
            m_fs = File.Create(m_path);
        }

        private void close()
        {
            if (m_fs != null)
            {
                m_fs.Close();
                m_fs.Dispose();
                m_fs = null;
            }
        }

        public void write(byte[] data, int offset, int count)
        {
            if (m_fs != null && m_fs.CanWrite)
            {
                m_fs.Write(data, offset, count);
            }
        }

        void IDisposable.Dispose()
        {
            close();
        }
    }
}
