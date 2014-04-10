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
        private Types type;
        private string message;
        
        public Msg(string message, Types type )
        {
            this.type = type;
            this.message = message;
        }

        public string getMessage()
        {
            return this.message;
        }

        public string toString()
        {
            return this.message + "\r\n" + this.type;
        }

        public static Msg toMsg(string str)
        {
            string[] array =  str.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            return new Msg(array[0], (Types)Enum.Parse(typeof(Types), array[1]));
        }  

    }
}
