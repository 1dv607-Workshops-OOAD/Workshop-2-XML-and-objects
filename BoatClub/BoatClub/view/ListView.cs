using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoatClub.helper;
using BoatClub.model;

namespace BoatClub.view
{
    class ListView
    {
        private MemberDAL memberDAL;
        private List<KeyValuePair<string, string>> listMembers;
        private Helper helper;
       
        public ListView()
        {
            this.memberDAL = new MemberDAL();
            this.listMembers = new List<KeyValuePair<string, string>>();
            this.helper = new Helper();
            listMembers = memberDAL.listMembers();
        }

        public Helper.MenuChoice goToStartMenu() { 
            string menuChoice = Console.ReadLine().ToUpper();
            if (menuChoice == "S")
            {
                return Helper.MenuChoice.Back;
            }
            else {
                return Helper.MenuChoice.None;
            }
        }

        public void showCompactList() {
            Console.Clear();
            helper.printDivider();
            Console.WriteLine("FÖRENKLAD MEDLEMSLISTA");
            helper.printDivider();
            helper.getBackToStartMessage();
            foreach (var member in listMembers)
            {
                if (member.Key == memberDAL.getBoatTypeKey() || 
                    member.Key == memberDAL.getBoatLengthKey() ||
                    member.Key == memberDAL.getSocialSecNoKey())
                {
                    continue;
                }
                Console.WriteLine("{0}: {1}", member.Key, member.Value);
            }
        }

        public void showVerboseList() {
            Console.Clear();
            this.helper.printDivider();
            Console.WriteLine("UTÖKAD MEDLEMSLISTA");
            this.helper.printDivider();

            Console.WriteLine("\nAnge medlemsId för att redigera en medlem.");
            helper.getBackToStartMessage();

            foreach (var member in listMembers)
            {
                if (member.Key == memberDAL.getNumberOfBoatsKey()) {
                    continue;
                }
                Console.WriteLine("{0}: {1}", member.Key, member.Value);
            }
        }

        public string getChoice() {
            //Returns selected member or S for start menu
            string choice = Console.ReadLine();
            return choice;
        }    
    }
}
