using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoatClub.helper;
using BoatClub.model;

namespace BoatClub.view
{
    class AddMemberView
    {
        private string memberName = "";
        private string memberSocSecNo = "";

        public Member showAddMemberView()
        {
            Helper helper = new Helper();

            Console.Clear();
            helper.printDivider();
            Console.WriteLine("LÄGG TILL MEDLEM");
            helper.printDivider();

            while(memberName == ""){
                Console.Write("\nAnge namn: ");
                memberName = Console.ReadLine();
            }
            
            while(memberSocSecNo == ""){
                Console.Write("Ange personnummer: ");
                memberSocSecNo = Console.ReadLine();
            }
            
            Member newMember = new Member(0, this.memberName, this.memberSocSecNo);

            return newMember;
        }
    }
}
