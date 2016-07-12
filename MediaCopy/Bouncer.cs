using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaCopy
{
    class Bouncer
    {
        public static bool CheckDelete(List<DatedDirectory> dirs)
        {
            Messenger.Display(ConsoleColor.Cyan, "Sollen die Dateien aus folgenden Verzeichnissen gelöscht werden? (j/n)");
            var directoryList = dirs.Select(dir => dir.Directory.FullName);
            directoryList.ToList().ForEach(dir => Messenger.Display(ConsoleColor.White, dir));
            var answer = Console.ReadLine();
            switch (answer)
            {
                case "j":
                    Messenger.Display(ConsoleColor.Cyan, "Haben sie überprüft ob die Dateien am Ziel angekommen sind? Die Quelldateien sind danach weg (j/n)");
                    var securityAnswer = Console.ReadLine();
                    switch (securityAnswer)
                    {
                        case "j":
                            return true; ;
                        case "n":
                            return false;
                        default:
                            return CheckDelete(dirs);
                    }
                case "n":
                    return false;
                default:
                    return CheckDelete(dirs);
            }
        }
        public static bool CheckCopy(List<DatedDirectory> dirs)
        {
            if (dirs.Any())
            {
                Messenger.Display(ConsoleColor.Cyan, "Sollen die Dateien aus folgenden Verzeichnissen kopiert werden? (j/n)");
                var directoryList = dirs.Select(dir => dir.Directory.FullName);
                directoryList.ToList().ForEach(dir => Messenger.Display(ConsoleColor.White, dir));
                var answer = Console.ReadLine();
                switch (answer)
                {
                    case "j":
                        return true;
                    case "n":
                        return false;
                    default:
                        return CheckCopy(dirs);
                }
            }
            return false;
        }
    }
}
