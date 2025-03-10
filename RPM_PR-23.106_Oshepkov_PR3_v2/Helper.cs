using RPM_PR_23._106_Oshepkov_PR3_v2.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPM_PR_23._106_Oshepkov_PR3_v2
{
    internal class Helper
    {
        private static Shoe_factoryEntities _context;
        public static Shoe_factoryEntities GetContext()
        {
            if (_context == null)
            {
                _context = new Shoe_factoryEntities();
            }
            return _context;
        }
    }
}
