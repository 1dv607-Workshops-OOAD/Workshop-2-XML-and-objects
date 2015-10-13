using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoatClub.helper;

namespace BoatClub.view
{
    public class StartView
    {
        public enum MenuChoice
        {
            AddMember,
            CompactListMembers,
            VerboseListMembers,
            AddBoat,
            None
        }
        public void showStartMenu()
        {
            Helper helper = new Helper();

            Console.Clear();
            helper.printDivider();
            Console.WriteLine("VÄLKOMMEN TILL BÅTKLUBBEN");
            helper.printDivider();
            Console.WriteLine("Välj nedan vad du vill göra.");
            Console.WriteLine("Tryck 1 för att lägga till medlem.");
            Console.WriteLine("Tryck 2 för att visa medlemslista.");
            Console.WriteLine("Tryck 3 för att visa utökad medlemslista och redigera medlemmar.");
            Console.WriteLine("Tryck 4 för att lägga till en båt.");
        }

        public MenuChoice GetMenuChoice()
        {
            char menuChoice = System.Console.ReadKey().KeyChar;
            if (menuChoice == '1')
            {
                return MenuChoice.AddMember;
            }
            if (menuChoice == '2')
            {
                return MenuChoice.CompactListMembers;
            }
            if (menuChoice == '3')
            {
                return MenuChoice.VerboseListMembers;
            }
            if (menuChoice == '4')
            {
                return MenuChoice.AddBoat;
            }

            return MenuChoice.None;
        }
    }
}
