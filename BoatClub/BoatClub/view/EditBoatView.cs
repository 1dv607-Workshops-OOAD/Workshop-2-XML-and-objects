using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoatClub.helper;
using BoatClub.model;

namespace BoatClub.view
{
    class EditBoatView
    {
        private Helper helper;
        private MemberDAL memberDAL;
        private Member member;
        private string boatId;
        private string boatType;
        private string boatLength;
        private string memberId;

        public EditBoatView(Member member) {
            this.helper = new Helper();
            this.memberDAL = new MemberDAL();
            this.member = member;
        }

        //Shows one member´s boats, with menu choices
        public void showMemberBoatsMenu(){

            List<Boat> boats = member.getBoatsByMember(member.MemberID);

            if (boats.Count == 0)
            {
                Console.Clear();
                Console.WriteLine("Medlemmen har inga båtar.");
                helper.getBackToStartMessage();
            }

            else {                
                boats = member.getBoatsByMember(member.MemberID);

                Console.Clear();

                helper.printDivider();
                Console.WriteLine("MEDLEM " + member.MemberID + ":S BÅTAR");
                helper.printDivider();

                Console.WriteLine("\nAnge båtens id för att redigera eller radera.\n");
                
                foreach (Boat boat in boats)
                {
                    Console.WriteLine("{0}: {1}", helper.BoatId, boat.BoatId);
                    Console.WriteLine("{0}: {1}", helper.BoatType, boat.BoatType);
                    Console.WriteLine("{0}: {1}", helper.BoatLength, boat.BoatLength);
                }
            }
        }

        public string getSelectedBoat()
        {
            boatId = Console.ReadLine();
            return boatId;
        }

        //Shows one boat to edit or delete
        public void showEditBoatMenu(string boatId, string memberId) {

            Boat boat = memberDAL.getBoatById(boatId, memberId);
            Console.Clear();
            this.helper.printDivider();
            Console.WriteLine("MEDLEM " + memberId + ":S BÅT " + boatId);
            this.helper.printDivider();

            Console.WriteLine("\nAnge T för att ta bort båt.");
            Console.WriteLine("Ange R för att redigera båt.\n");
            if (boat != null)
            {
                Console.WriteLine("{0}: {1}", helper.BoatId, boat.BoatId);
                Console.WriteLine("{0}: {1}", helper.BoatType, boat.BoatType);
                Console.WriteLine("{0}: {1}", helper.BoatLength, boat.BoatLength);
            }
            else {
                Console.Clear();
                Console.WriteLine("Båten finns inte.");
                helper.getBackToStartMessage();
            }
        }

        public Helper.MenuChoice getEditBoatMenuChoice()
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
            return Helper.MenuChoice.None;
        }

        public Boat editBoat(string selectedBoatId) {
            Console.Clear();
            helper.printDivider();
            Console.WriteLine("REDIGERA BÅT MED ID " + selectedBoatId + "\n");
            helper.printDivider();
            helper.getBoatTypeMenu();
            string newBoatType = helper.setBoatType(Console.ReadLine());
            Console.Write("Båtlängd: \n");
            string newBoatLength = Console.ReadLine();

            if (newBoatLength == "")
            {
                newBoatLength = boatLength;
            }

            Boat editedBoat = new Boat(int.Parse(boatId), newBoatType, newBoatLength, memberId);

            return editedBoat;
        }
    }
}
