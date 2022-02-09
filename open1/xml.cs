using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace open1
{
    public class xml
    {
        public static void saveData(object obj, string filename)
        {
            XmlSerializer sd = new XmlSerializer(obj.GetType());
            TextWriter writer = new StreamWriter(filename);
            sd.Serialize(writer, obj);
            writer.Close();
        }
    }
}
