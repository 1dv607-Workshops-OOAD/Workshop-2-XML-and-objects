using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoatClub.controller;

namespace BoatClub.helper
{
    public class Helper
    {
        //Helper class to avoid DRY

        private string boatType = "Båttyp";
        private string boatLength = "Båtlängd";
        private string socialSecNo = "Personnummer";
        private string memberId = "MEDLEMSID";
        private string name = "Namn";
        private string numberOfBoats = "Antal båtar";
        private string boatId = "BÅT-ID";

        public enum MenuChoice
        {
            Delete,
            Edit,
            Boats,
            Back,
            None
        }

        public string setBoatType(string input)
        {
            string boatType = "";

            if (input == "1")
            {
                boatType = "Segelbåt";
            }
            if (input == "2")
            {
                boatType = "Kajak";
            }
            if (input == "3")
            {
                boatType = "Motorseglare";
            }
            if (input == "4")
            {
                boatType = "Annan";
            }

            return boatType;
        }

        public void printDivider()
        {
            Console.WriteLine("*************************************************");
        }

        public void getBoatTypeMenu()
        {
            Console.WriteLine("Ange båttyp:");
            Console.WriteLine("1 för segelbåt,");
            Console.WriteLine("2 för kayak eller kanot,");
            Console.WriteLine("3 för motorseglare,");
            Console.WriteLine("4 för annan typ.\n");
        }

        public void getBackToStartMessage()
        {
            Console.WriteLine("Ange S för att komma tillbaka till startmenyn.\n");
        }

        public string BoatType { get { return boatType; } }
        public string BoatLength { get { return boatLength; } }
        public string SocialSecNo { get { return socialSecNo; } }
        public string MemberId { get { return memberId; } }
        public string Name { get { return name; } }
        public string NumberOfBoats { get { return numberOfBoats; } }
        public string BoatId { get { return boatId; } }
    }
}
