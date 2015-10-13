using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoatClub.helper;
using BoatClub.model;

namespace BoatClub.view
{
    class AddBoatView
    {
        private Helper helper;
        private MemberDAL memberDAL;
        private bool memberExists = true;

        public AddBoatView()
        {
            this.memberDAL = new MemberDAL();
            this.helper = new Helper();
        }

        public void showAddBoatMenu()
        {
            Console.Clear();
            this.helper.printDivider();
            Console.WriteLine("LÄGG TILL EN BÅT");
            this.helper.printDivider();
            Console.WriteLine("\nVälj medlemsnummer.\n");
        }

        public void showMemberList()
        {
            List<Member> listMembers = new List<Member>();
            listMembers = memberDAL.getAllMembers();

            foreach (Member member in listMembers)
            {
                Console.WriteLine("{0}: {1}", helper.MemberId, member.MemberID);
                Console.WriteLine("{0}: {1}", helper.Name, member.MemberName);
                Console.WriteLine("{0}: {1}\n", helper.SocialSecNo, member.MemberSocSecNo);
            }
        }

        public Member getSelectedMember()
        {
            Member member = memberDAL.getMemberById(Console.ReadLine());
            return member;
        }

        public bool doesMemberExist() {
            return memberExists;
        }

        public void addBoat(Member member)
        {
            string boatType = "";
            string boatLength = ""; 
            
            Console.Clear();
            helper.printDivider();
            Console.WriteLine("LÄGG TILL EN BÅT");
            helper.printDivider();
            
            while (boatType == "")
            {
                helper.getBoatTypeMenu();
                boatType = helper.setBoatType(Console.ReadLine());
            }

            while(boatLength == ""){
                Console.Write("Ange båtens längd: \n"); 
                boatLength = Console.ReadLine();
            }
            
            member.addBoatToMember("0", boatType, boatLength);
        }
    }
}
