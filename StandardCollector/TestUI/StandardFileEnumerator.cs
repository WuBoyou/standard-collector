using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Standard.Test.UI.Winforms
{
    public class StandardFileEnumerator : IEnumerable<string>
    {
        private string path;
        IEnumerable<string> fileEnumerator;

        public StandardFileEnumerator(string path)
        {
            this.path = path;
            this.fileEnumerator = Directory.EnumerateFiles(this.path, "*.*", SearchOption.AllDirectories);
        }

        public void SetPath(string path)
        {
            this.path = path;
        }

        public IEnumerator<string> GetEnumerator()
        {
            var files = from file in Directory.EnumerateFiles(this.path, "*.*", SearchOption.AllDirectories)
                        where IsSupportedFile(file)
                        select file;
            return files.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        private bool IsSupportedFile(string path)
        {
            string extension = Path.GetExtension(path);
            if (extension == ".pdf" || extension == ".doc" || extension == ".docx")
                return true;

            return false;
        }
    }
}
