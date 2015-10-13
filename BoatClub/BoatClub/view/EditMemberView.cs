using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoatClub.helper;
using BoatClub.model;

namespace BoatClub.view
{
    class EditMemberView
    {
        Helper helper;
        private MemberDAL memberDAL;
        private string name;
        private string socialSecNo;

        public EditMemberView()
        {
            this.helper = new Helper();
            this.memberDAL = new MemberDAL();
        }

        public Helper.MenuChoice getMenuChoice()
        {
            string menuChoice = Console.ReadLine().ToUpper();
            if (menuChoice == "T")
            {
                return Helper.MenuChoice.Delete;
            }
            if (menuChoice == "R")
            {
                return Helper.MenuChoice.Edit;
            }
            if (menuChoice == "B")
            {
                return Helper.MenuChoice.Boats;
            }
            if (menuChoice == "S")
            {
                return Helper.MenuChoice.Back;
            }

            return Helper.MenuChoice.None;
        }

        public void showEditMemberMenu()
        {
            Console.Clear();
            helper.printDivider();
            Console.WriteLine("VALD MEDLEM");
            helper.printDivider();

            Console.WriteLine("\nAnge T för att ta bort medlem.");
            Console.WriteLine("Ange R för att redigera medlem.");
            Console.WriteLine("Ange B för att redigera medlemmens båtar.");
            helper.getBackToStartMessage();
        }

        public void showSelectedMember(string memberId)
        {

            try { 
                List<KeyValuePair<string, string>> member = memberDAL.getMemberById(memberId);
                foreach (var element in member)
                {
                    if (element.Key == memberDAL.getNumberOfBoatsKey())
                    {
                        continue;
                    }
                    Console.WriteLine("{0}: {1}", element.Key, element.Value);
                }
            }

            catch(Exception){
                Console.WriteLine("Medlemmen finns inte! Tryck S för att gå tillbaka till startmenyn.");
            }
        }

        //Shows one member (without boat information) for editing member information
        public void showSelectedMemberWithoutBoats(string memberId)
        {
            List<KeyValuePair<string, string>> member = memberDAL.getMemberById(memberId);
            Console.Clear();
            helper.printDivider();
            Console.WriteLine("REDIGERA MEDLEM MED MEDLEMSNUMMER " + memberId);
            helper.printDivider();
            Console.WriteLine();
            Console.WriteLine("Lämna tomt för att behålla gammalt värde.\n");

            foreach (var element in member)
            {
                if (element.Key == memberDAL.getNameKey() || element.Key == memberDAL.getSocialSecNoKey())
                {
                    if (element.Key == memberDAL.getNameKey())
                    {
                        name = element.Value;
                    }
                    if (element.Key == memberDAL.getSocialSecNoKey())
                    {
                        socialSecNo = element.Value;
                    }
                    Console.WriteLine("{0}: {1}", element.Key, element.Value);
                }

                else
                {
                    continue;
                }
            }
        }

        public Member editMember(string memberId)
        {
            Console.Write("Namn: ");
            string newName = Console.ReadLine();
            Console.Write("Personummer: ");
            string newSocialSecNo = Console.ReadLine();

            if (newName == "")
            {
                newName = name;
            }
            if (newSocialSecNo == "")
            {
                newSocialSecNo = socialSecNo;
            }

            Member editedMember = new Member(int.Parse(memberId), newName, newSocialSecNo);

            return editedMember;
        }
    }
}
