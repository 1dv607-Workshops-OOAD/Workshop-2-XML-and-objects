﻿using System;
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
        private Member member;

        public EditMemberView(Member member)
        {
            this.helper = new Helper();
            this.member = member;
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

        public Member showSelectedMemberWithBoats()
        {
            List<Boat> boats = member.getBoatsByMember(member.MemberID);
            if (getMemberById() != null) {
                Console.WriteLine("{0}: {1}", helper.MemberId, member.MemberID);
                Console.WriteLine("{0}: {1}", helper.Name, member.MemberName);
                Console.WriteLine("{0}: {1}", helper.SocialSecNo, member.MemberSocSecNo);
                foreach (Boat boat in boats)
                {
                    Console.WriteLine("{0}: {1}", helper.BoatId, boat.BoatId);
                    Console.WriteLine("{0}: {1}", helper.BoatType, boat.BoatType);
                    Console.WriteLine("{0}: {1}", helper.BoatLength, boat.BoatLength);
                }
            }
            else { 
                Console.WriteLine("Medlemmen finns inte! Tryck S för att gå tillbaka till startmenyn.");
            }

            return member;
        }

        //Shows one member (without boat information) for editing member information
        public void showSelectedMemberWithoutBoats()
        {
            Console.Clear();
            helper.printDivider();
            Console.WriteLine("REDIGERA MEDLEM MED MEDLEMSNUMMER " + member.MemberID);
            helper.printDivider();
            Console.WriteLine();
            Console.WriteLine("Lämna tomt för att behålla gammalt värde.\n");

            Console.WriteLine("{0}: {1}", helper.MemberId, member.MemberID);
            Console.WriteLine("{0}: {1}", helper.Name, member.MemberName);
            Console.WriteLine("{0}: {1}", helper.SocialSecNo, member.MemberSocSecNo);
        }

        public Member editMember()
        {
            helper.printDivider();
            Console.Write("Namn: ");
            string newName = Console.ReadLine();
            Console.Write("Personummer: ");
            string newSocialSecNo = Console.ReadLine();

            if (newName == "")
            {
                newName = member.MemberName;
            }
            if (newSocialSecNo == "")
            {
                newSocialSecNo = member.MemberSocSecNo;
            }

            Member editedMember = new Member(member.MemberID, newName, newSocialSecNo);

            return editedMember;
        }

        public Member getMemberById() {
            return member.getMemberById();
        }
    }
}
