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
        private List<Member> listMembers;
        private Helper helper;
       
        public ListView()
        {
            this.memberDAL = new MemberDAL();
            this.listMembers = new List<Member>();
            this.helper = new Helper();
            listMembers = memberDAL.getAllMembers();
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

            foreach(Member member in listMembers){
                Console.WriteLine("{0}: {1}", helper.MemberId, member.MemberID);
                Console.WriteLine("{0}: {1}", helper.Name, member.MemberName);
                Console.WriteLine("{0}: {1}", helper.NumberOfBoats, member.getBoatsByMember(member.MemberID).Count);
                Console.WriteLine();
            }
            
        }

        public void showVerboseList() {
            
            Console.Clear();
            helper.printDivider();
            Console.WriteLine("UTÖKAD MEDLEMSLISTA");
            helper.printDivider();

            Console.WriteLine("\nAnge medlemsId för att redigera en medlem.");
            helper.getBackToStartMessage();

            foreach(Member member in listMembers){
                Console.WriteLine("{0}: {1}", helper.MemberId, member.MemberID);
                Console.WriteLine("{0}: {1}", helper.Name, member.MemberName);
                Console.WriteLine("{0}: {1}", helper.SocialSecNo, member.MemberSocSecNo);

                List<Boat> memberBoats = member.getBoatsByMember(member.MemberID);
                foreach (Boat boat in memberBoats)
                {
                    Console.WriteLine("{0}: {1}", helper.BoatType, boat.BoatType);
                    Console.WriteLine("{0}: {1}", helper.BoatLength, boat.BoatLength);
                }
                Console.WriteLine();
            }
        }

        public string getChoice() {
            //Returns selected member or S for start menu
            string choice = Console.ReadLine();
            return choice;
        }    
    }
}
