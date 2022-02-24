using Grasshopper.Kernel;
using System;
using System.Drawing;


namespace FabSerial
{
    public class FabSerialInfo : GH_AssemblyInfo
    {
        public override string Name
        {
            get
            {
                return "FabSerial";
            }
        }
        public override Bitmap Icon
        {
            get
            {
                //Return a 24x24 pixel bitmap to represent this GHA library.
                return Properties.Resources.serialIcon;
            }
        }
        public override string Description
        {
            get
            {
                //Return a short string describing the purpose of this GHA library.
                return "";
            }
        }
        public override Guid Id
        {
            get
            {
                return new Guid("3a2b3663-c55f-45cf-868a-4762e0d35162");
            }
        }

        public override string AuthorName
        {
            get
            {
                //Return a string identifying you or your company.
                return "";
            }
        }
        public override string AuthorContact
        {
            get
            {
                //Return a string representing your preferred contact details.
                return "";
            }
        }
    }
}
