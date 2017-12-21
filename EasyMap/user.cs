using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using PhotoSettings;

namespace EasyMap
{
    public class user
    {
        public static string _userName;
        public static string _password;
        public static string _quanxian;

        public string userName
        {
            get { return _userName; }
            set { _userName = value; }
        }

        public string password
        {
            get { return _password; }
            set { _password = value; }
        }
        public string quanxian
        {
            get { return _quanxian; }
            set { _quanxian = value; }
        }
    }
}
