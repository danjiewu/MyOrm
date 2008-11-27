using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;

namespace MyOrm.Xml
{
    public class TableInfoSection : ConfigurationSection
    {
        private TableNode[] tables;

        [ConfigurationCollection(typeof(TableNode))]
        public TableNode[] Tables
        {
            get { return tables; }
            set { tables = value; }
        }
    }
}
