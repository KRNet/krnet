using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KR_network
{
    class Msg
    {
        public enum Types {info, manage};
        public enum ManageType
        {
            ACK, NO_ACK, REQUEST_CONNECT, CONNECT,
            REQUEST_DISCONNECT, DISCONNECT
        };
        private Types type;
        private ManageType manageType;
        private string message;
        

        public Msg(string message)
        {
            this.type = Types.info;
            this.message = message;
        }

        public Msg(ManageType manageType)
        {
            this.message = "";
            this.type = Types.manage;
            this.manageType = manageType;
        }

        public string getMessage()
        {
            return this.message;
        }

        public Types getType()
        {
            return this.type;
        }

        public ManageType getManageType()
        {
            return this.manageType;
        }

        public string toString()
        {
            switch (this.type)
            {
                case Types.info:
                    return this.message + "\r\n" + this.type;
                default:
                    return this.manageType + "\r\n" + this.type;
            }            
        }

        public static Msg toMsg(string str)
        {
            string[] array =  str.Split(new string[] { "\r\n" },
                StringSplitOptions.RemoveEmptyEntries);
            Types type = (Types)Enum.Parse(typeof(Types), array[1]);
            if (type == Types.info)
                return new Msg(array[0]);
            else
                return new Msg((ManageType)Enum.Parse(typeof(ManageType),array[0]));
        }  
    }
}
