using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaCopy
{
    class Program
    {
        static void Main(string[] args)
        {
            var directories = Scanner.GetDirectories();

            if (Bouncer.CheckCopy(directories))
            {
                var files = Operator.CopyFiles(directories);
                if (Bouncer.CheckDelete(directories))
                {
                    Operator.DeleteFiles(files);
                }
            }
        }
    }
}
