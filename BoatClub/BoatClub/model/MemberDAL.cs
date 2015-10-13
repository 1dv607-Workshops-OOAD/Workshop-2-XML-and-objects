using BoatClub.helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.Xml.XmlConfiguration;

namespace BoatClub.model
{
    class MemberDAL
    {
        private Helper helper;

        //Strings for generating key value pair lists
        private string boatType;
        private string boatLength;
        private string socialSecNo;
        private string memberId;
        private string name;
        private string numberOfBoats;
        private string boatId;

        //Path to XML file
        private string path = "../../data/Members.xml";

        //Elements and attributes strings for XML files
        private string XMLElementMembers = "Members";
        private string XMLElementMember = "Member";
        private string XMLElementBoat = "Boat";
        private string XMLAttributeMemberId = "memberId";
        private string XMLAttributeName = "name";
        private string XMLAttributeSocialSecNo = "socialSecNo";
        private string XMLAttributeBoatId = "boatId";
        private string XMLAttributeBoatType = "boatType";
        private string XMLAttributeBoatLength = "boatLength";

        public MemberDAL()
        {
            this.helper = new Helper();

            boatType = helper.BoatType;
            boatLength = helper.BoatLength;
            socialSecNo = helper.SocialSecNo;
            memberId = helper.MemberId;
            name = helper.Name;
            numberOfBoats = helper.NumberOfBoats;
            boatId = helper.BoatId;
        }

        public string getBoatTypeKey()
        {
            return boatType;
        }

        public string getBoatLengthKey()
        {
            return boatLength;
        }

        public string getSocialSecNoKey()
        {
            return socialSecNo;
        }

        public string getNameKey()
        {
            return name;
        }

        public string getNumberOfBoatsKey()
        {
            return numberOfBoats;
        }

        //Returns a list of all members
        public List<KeyValuePair<string, string>> listMembers()
        {
            List<KeyValuePair<string, string>> members = new List<KeyValuePair<string, string>>();

            using (XmlTextReader reader = new XmlTextReader(path))
            {
                while (reader.Read())
                {
                    if ((reader.NodeType == XmlNodeType.Element) && (reader.Name == XMLElementMember))
                    {
                        if (reader.HasAttributes)
                        {
                            members.Add(new KeyValuePair<string, string>(this.memberId, reader.GetAttribute(XMLAttributeMemberId)));
                            members.Add(new KeyValuePair<string, string>(name, reader.GetAttribute(XMLAttributeName)));
                            members.Add(new KeyValuePair<string, string>(socialSecNo, reader.GetAttribute(XMLAttributeSocialSecNo)));
                            members.Add(new KeyValuePair<string, string>(numberOfBoats, getNumberOfBoats(reader.GetAttribute(XMLAttributeMemberId)).ToString()));
                        }
                    }
                    if ((reader.NodeType == XmlNodeType.Element) && (reader.Name == XMLElementBoat))
                    {
                        members.Add(new KeyValuePair<string, string>(boatType, reader.GetAttribute(XMLAttributeBoatType)));
                        members.Add(new KeyValuePair<string, string>(boatLength, reader.GetAttribute(XMLAttributeBoatLength)));
                    }
                }
                return members;
            }
        }

        //Returns list containing one member with boats
        public List<KeyValuePair<string, string>> getMemberById(string memberId)
        {
            List<KeyValuePair<string, string>> member = new List<KeyValuePair<string, string>>();

            using (XmlTextReader reader = new XmlTextReader(path))
            {
                XDocument doc = XDocument.Load(path);

                while (reader.Read())
                {
                    if ((reader.NodeType == XmlNodeType.Element) && (reader.Name == XMLElementMember))
                    {
                        if (reader.GetAttribute(XMLAttributeMemberId) == memberId)
                        {
                            member.Add(new KeyValuePair<string, string>(this.memberId, reader.GetAttribute(XMLAttributeMemberId)));
                            member.Add(new KeyValuePair<string, string>(name, reader.GetAttribute(XMLAttributeName)));
                            member.Add(new KeyValuePair<string, string>(socialSecNo, reader.GetAttribute(XMLAttributeSocialSecNo)));

                            // For each element that is a child of member with id = memberId
                            foreach (var boat in doc.Descendants(XMLElementMember)
                                    .Elements(XMLElementBoat).Where(e => e.Parent.Name == XMLElementMember &&
                                    e.Parent.Attribute(XMLAttributeMemberId).Value == memberId))
                            {
                                member.Add(new KeyValuePair<string, string>(boatId, boat.Attribute(XMLAttributeBoatId).Value));
                                member.Add(new KeyValuePair<string, string>(boatType, boat.Attribute(XMLAttributeBoatType).Value));
                                member.Add(new KeyValuePair<string, string>(boatLength, boat.Attribute(XMLAttributeBoatLength).Value));
                            }
                        }
                    }
                }

                if(member.Count == 0){
                    throw new Exception();
                }
               
                return member;
            }
        }

        public void saveMember(Member newMember)
        {
            XDocument doc = XDocument.Load(path);
            XElement memberRoot = new XElement(XMLElementMember);
            memberRoot.Add(new XAttribute(XMLAttributeMemberId, newMember.MemberID.ToString()));
            memberRoot.Add(new XAttribute(XMLAttributeName, newMember.MemberName));
            memberRoot.Add(new XAttribute(XMLAttributeSocialSecNo, newMember.MemberSocSecNo));
            doc.Element(XMLElementMembers).Add(memberRoot);
            doc.Save(path);
        }

        public void deleteMemberById(string memberId)
        {
            XDocument doc = XDocument.Load(path);
            doc.Root.Elements(XMLElementMember)
                .Where(e => e.Attribute(XMLAttributeMemberId).Value.Equals(memberId))
                .Select(e => e).Single()
                .Remove();
            doc.Save(path);
        }

        public void updateMemberById(Member editedMember)
        {
            XDocument doc = XDocument.Load(path);
            var member = doc.Descendants(XMLElementMember)
                .Where(arg => arg.Attribute(XMLAttributeMemberId).Value == editedMember.MemberID.ToString())
                .Single();
            member.Attribute(XMLAttributeName).Value = editedMember.MemberName;
            member.Attribute(XMLAttributeSocialSecNo).Value = editedMember.MemberSocSecNo;
            doc.Save(path);
        }

        //Returns number of boats for one member.
        public int getNumberOfBoats(string memberId)
        {
            XDocument doc = XDocument.Load(path);
            var member = (from elements in doc.Elements(XMLElementMembers).Elements(XMLElementMember)
                          where elements.Attribute(XMLAttributeMemberId).Value == memberId
                          select elements).FirstOrDefault();
            var boatList = member.Elements(XMLElementBoat).ToList();
            return boatList.Count;
        }

        public void saveBoat(Boat newBoat, string memberId)
        {
            XDocument doc = XDocument.Load(path);
            doc.Element(XMLElementMembers).Elements(XMLElementMember)
            .First(c => (string)c.Attribute(XMLAttributeMemberId) == memberId)
            .Add(
                new XElement(
                XMLElementBoat, new XAttribute(XMLAttributeBoatId, newBoat.BoatId),
                                new XAttribute(XMLAttributeBoatType, newBoat.BoatType),
                                new XAttribute(XMLAttributeBoatLength, newBoat.BoatLength)
                )
            );
            doc.Save(path);
        }

        //Returns a list of boats for one member
        public List<KeyValuePair<string, string>> getBoatsByMemberId(string memberId)
        {
            List<KeyValuePair<string, string>> boats = new List<KeyValuePair<string, string>>();
            using (XmlTextReader reader = new XmlTextReader(path))
            {
                XDocument doc = XDocument.Load(path);
                foreach (var item in doc.Descendants(XMLElementMember).Elements(XMLElementBoat)
                        .Where(e => e.Parent.Name == XMLElementMember &&
                        e.Parent.Attribute(XMLAttributeMemberId).Value == memberId))
                {
                    boats.Add(new KeyValuePair<string, string>(boatId, item.Attribute(XMLAttributeBoatId).Value));
                    boats.Add(new KeyValuePair<string, string>(boatType, item.Attribute(XMLAttributeBoatType).Value));
                    boats.Add(new KeyValuePair<string, string>(boatLength, item.Attribute(XMLAttributeBoatLength).Value));
                }
            }

            return boats;
        }

        //Returns one boat
        public List<KeyValuePair<string, string>> getBoatById(string selectedBoatId, string memberId)
        {
            List<KeyValuePair<string, string>> boat = new List<KeyValuePair<string, string>>();
            using (XmlTextReader reader = new XmlTextReader(path))
            {
                XDocument doc = XDocument.Load(path);

                foreach (var item in doc.Descendants(XMLElementMember).Elements(XMLElementBoat)
                        .Where(e => e.Parent.Name == XMLElementMember &&
                        e.Parent.Attribute(XMLAttributeMemberId).Value == memberId))
                {
                    if(item.Attribute(XMLAttributeBoatId).Value == selectedBoatId){
                        boat.Add(new KeyValuePair<string, string>(boatId, item.Attribute(XMLAttributeBoatId).Value));
                        boat.Add(new KeyValuePair<string, string>(boatType, item.Attribute(XMLAttributeBoatType).Value));
                        boat.Add(new KeyValuePair<string, string>(boatLength, item.Attribute(XMLAttributeBoatLength).Value));
                    }
                }
            }

            if (boat.Count == 0)
            {
                throw new Exception();
            }
            return boat;
        }

        public void deleteBoatById(string selectedBoatId, string memberId)
        {
            XDocument doc = XDocument.Load(path);
            doc.Descendants(XMLElementBoat).Where(e => e.Attribute(XMLAttributeBoatId).Value.Equals(selectedBoatId))
                .Where(e => e.Parent.Attribute(XMLAttributeMemberId).Value.Equals(memberId)).Single()
                .Remove();
            doc.Save(path);
        }

        public void updateBoatById(Boat editedBoat, string memberId)
        {
            XDocument doc = XDocument.Load(path);
            var member = doc.Descendants(XMLElementMember)
                .Where(arg => arg.Attribute(XMLAttributeMemberId).Value == memberId)
                .Single();
            member.Element(XMLElementBoat).Attribute(XMLAttributeBoatType).Value = editedBoat.BoatType;
            member.Element(XMLElementBoat).Attribute(XMLAttributeBoatLength).Value = editedBoat.BoatLength;
            doc.Save(path);
        }
    }
}
