using System;
using System.Collections.Generic;
using System.Text;

namespace Isag.Log
{
	/// <summary>
	/// Class to model the different types of log events
	/// </summary>
    public class ComponentType
	{
        //Maps ComponentType name to ComponentType id
        public static Dictionary<string, int> ComponentTypes = new Dictionary<string, int>()
        {
             {"LogInitialize"                                               ,  1}
            ,{"Information"                                                 ,  3}
            ,{"ItemBegin"                                                   ,  5}
            ,{"ItemEnd"                                                     ,  6}
            ,{"Warnung"                                                     , 10}
            ,{"Fehler ohne Abbruch"                                         , 20}
            ,{"Fehler mit kontrolliertem Abbruch einer Teilausführung"      , 30}
            ,{"Fehler mit kontrolliertem Abbruch der gesamten Ausführung"   , 40}
        };

        /// <summary>
        /// Returns the ComponentType name for a given id
        /// </summary>
        /// <param name="id">the ComponentType id</param>
        /// <returns>the ComponentType name</returns>
        public static string GetComponentName(int id)
        {
            foreach (KeyValuePair<string,int> pair in ComponentTypes)
            {
                if (pair.Value == id) return pair.Key;
            }
            return null;
        }

        /// <summary>
        /// Returns the default ComponentType id
        /// </summary>
        /// <returns>the default ComponentType id </returns>
        public static int GetDefaultComponentId()
        {
            var valuesEnumerator = ComponentTypes.Values.GetEnumerator();
            valuesEnumerator.MoveNext();
            return valuesEnumerator.Current;
        }

        /// <summary>
        /// Returns the ComponentType id for a given ComponentType name
        /// </summary>
        /// <param name="name">the CompoentType name</param>
        /// <returns>the ComponentType id</returns>
        public static int GetComponentId(string name)
        {
            if (ComponentTypes.ContainsKey(name)) return ComponentTypes[name];
            return 0;
        }
	}
}
