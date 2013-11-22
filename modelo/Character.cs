using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebMonitor.modelo
{
    public class Character
    {
	    public long id;
        public long lastModified;
        public String name;
        public String realm;
        public String battlegroup;
        public String classe;
        public String race;
        public String gender;
        public int level;
        public int achievementPoints;
        public String thumbnail;
        public String calcClass;
        public long money;

        //public List<ItemUnit> listItensBag;

        public Guild guild;
    }
}
