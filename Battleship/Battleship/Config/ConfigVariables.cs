using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship.Config
{
    public static class ConfigVariables
    {
        public static int GridRows
        {
            get
            {
                int rows;
                if (int.TryParse(ConfigurationManager.AppSettings["GridRows"], out rows))
                {
                    return rows;
                }
                else
                {
                    return 8;
                }

            }
        }

        public static int GridCols
        {
            get
            {
                int cols;
                if (int.TryParse(ConfigurationManager.AppSettings["GridCols"], out cols))
                {
                    return cols;
                }
                else
                {
                    return 8;
                }

            }
        }
    }
}
