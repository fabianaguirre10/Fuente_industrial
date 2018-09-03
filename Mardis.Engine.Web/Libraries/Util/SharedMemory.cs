using System;
using System.Collections.Generic;

namespace Mardis.Engine.Web.Libraries.Util
{
    /// <summary>
    /// Memoria Compartida
    /// </summary>
    public static class SharedMemory
    {
        public static Dictionary<string, object> items = new Dictionary<string, object>();

        /// <summary>
        /// Set Value al diccionario
        /// </summary>
        /// <param name="key"></param>
        /// <param name="valueObject"></param>
        /// <returns></returns>
        public static bool Set(string key, object valueObject)
        {
            bool isValid = false;

            try
            {
                items.Add(key, valueObject);
                isValid = true;
            }
            catch (Exception)
            {

                isValid = false;
            }

            return isValid;
        }

        /// <summary>
        /// Devuelveme el valor
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static object Get(string key)
        {
            object returnValue = null;

            try
            {
                returnValue = items[key];

            }
            catch
            {
                returnValue = null;
            }

            return returnValue;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        public static void Remove(string key)
        {
            try
            {
                items.Remove(key);

            }
            catch
            {

            }
        }
    }
}
