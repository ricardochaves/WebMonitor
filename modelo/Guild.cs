using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebMonitor.modelo
{
    public class Guild
    {
        public long id;
	    public String name;
	    public long gold;
	    public long accounts;
	    public List<Character> characteres;
	    public Realm realm;
        public List<Item> guildTabs;

        public Guild()
        {
        }

        public Guild(string nome, long gold, long accounts)
        {
            this.name = nome;
            this.gold = gold;
            this.accounts = accounts;
        }


    }
}
