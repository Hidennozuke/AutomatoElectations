using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomatoElectations.Connector
{
    class Connect
    {
        public static ElectationsEntities Context { get; } = new ElectationsEntities();

        public static int idMember;
        public static int idCand;
        public static int idActualRole;
        public static int idNewPasport;
        public static int idActualUser;
        public static int idParty;
    }
}
