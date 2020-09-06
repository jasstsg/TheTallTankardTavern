using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace TTT
{
    public static class GenericExtensions
    {
        public static OutputT GenericTypeCast<InputT, OutputT>(this InputT Input)
        {
            return JsonConvert.DeserializeObject<OutputT>(JsonConvert.SerializeObject(Input));
        }

        /// <summary>
        /// Takes a model to merge with the original.  Non-null properties are overwritten, while null ones are not modified.
        /// </summary>
        /// <typeparam name="T">Type of the models being merged</typeparam>
        /// <param name="OriginalObject">The original object, that will also become the merged model that is returned</param>
        /// <param name="UpdatedObject">The model containing modified properties to be merged with the original</param>
        /// <returns>The merged model</returns>
        public static T Merge<T>(this T OriginalObject, T UpdatedObject)
        {
            Type ObjectType = typeof(T);
            IEnumerable<PropertyInfo> Properties = 
                ObjectType.GetProperties(BindingFlags.Public | BindingFlags.Instance).Where(p => p.CanWrite);

            foreach(PropertyInfo property in Properties)
            {
                var updatedValue = property.GetValue(UpdatedObject);
                if (updatedValue == null)
                {
                    continue;   //Then skip updating this value
                }
                property.SetValue(OriginalObject, updatedValue);
            }

            return OriginalObject;
        }

        public static bool EqualsAny<T>(this T obj, params T[] objs)
        {
            foreach (T o in objs)
            {
                if (obj.Equals(o))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Returns all fields of type T in an IEnumerable object
        /// </summary>
        /// <typeparam name="T">The type of fields to return</typeparam>
        /// <param name="obj">The source object</param>
        /// <returns>IEnumerable<typeparamref name="T"/></returns>
        public static IEnumerable<T> GetAllPublicStaticFields<T>(this object obj)
        {
            return obj.GetType().GetFields(BindingFlags.Public | BindingFlags.Static)
                .Where(f => f.FieldType == typeof(T)).Select(f => (T)f.GetValue(null));
        }

        public static T GetPropertyValue<T>(this object obj, string propertyName)
        {
            return (T)obj.GetType().GetProperty(propertyName).GetValue(obj, null);
        }
    }
}
