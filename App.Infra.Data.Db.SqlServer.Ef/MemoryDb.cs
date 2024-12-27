
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


    public class MemoryDb
    {
        public static Card? CurrentCard{ get; set; }
       

        public static void CheckUCardLogin()
        {
            if (CurrentCard == null)
            {
                throw new Exception("You do not have access to this operation, please log in.");
            }
        }
        
    }

